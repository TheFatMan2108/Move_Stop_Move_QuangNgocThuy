using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<Transform> startPoints = new List<Transform>();
    public Transform mainMenu;
    public GameObject npcs;
    public List<ISpawnable> spawnables = new List<ISpawnable>();
    [Tooltip("Tổng số NPC sẽ xuất hiện")]
    public int npcCount = 10;
    [Tooltip("Số lượng NPC trên bản đồ")]
    public int totalNPC = 10;
    public TextMeshProUGUI txtScore;
    private Player player;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        ShowScoreUI();
        
    }
    private void Start()
    {
        player = Player.player;
        player.SetPosition(mainMenu.position, false, new Vector3(0, 0, -1));

    }

    public void BackToMain()
    {
        // code nay hoi dan can sua lai
        player.ResetPlayer();
        player.SetPosition(mainMenu.position, false, new Vector3(0, 0, -1));
        npcCount = 0;
        for (int i = 0; i < npcs.transform.childCount; i++)
        {
            if (!npcs.transform.GetChild(i).gameObject.activeInHierarchy) continue;
            npcs.transform.GetChild(i).GetComponent<NPC>().Dead();
        }
        npcs.SetActive(false);
        UI_Manager.instance.UIMainMenu.BackToMainMenu();
    }

    public void StartGame()
    {
        // code nay hoi dan can sua lai
        player.SetPosition(startPoints[Random.Range(0,startPoints.Count)].position,true,new Vector3(0,0,1));
        npcs.SetActive(true);
        player.isDead = false;
        npcCount = 20;
        for(int i = 0; i < npcs.transform.childCount; i++)
        {
            if(npcs.transform.GetChild(i).gameObject.activeInHierarchy) continue;
            npcs.transform.GetChild(i).GetComponent<NPC>().Spawn();
        }
    }

    public void AddSpawn(ISpawnable npc)
    {
        if (npcCount < 1)
        {
            Debug.Log("Win");
            return;
        }
        spawnables.Add(npc);
        SpawnNPC(npc);
    }

    public void SetNPCCount()
    {
        npcCount--;
    }

    public void ShowScoreUI()
    {
        UI_Manager.instance.UpdateAlivePeopel(GetScore());
    }


    public void SpawnNPC(ISpawnable npc)
    {
        if (npcCount < totalNPC||player.isDead) return;
        StartCoroutine(SpawnNPCCorotine(3, npc));
    }

    IEnumerator SpawnNPCCorotine(float time, ISpawnable npc)
    {
        yield return new WaitForSeconds(time);
        npc.Spawn();
        RemoveSpawn(npc);
    }
    public float GetScore()
    {
        return npcCount + 1;
    }
    internal void RemoveSpawn(ISpawnable npc) => spawnables.Remove(npc);
}

