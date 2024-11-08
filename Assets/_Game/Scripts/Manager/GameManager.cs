using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour, IEndGamesable,IStateGame
{
    public static GameManager Instance { get; private set; }

    public List<Transform> startPoints = new List<Transform>();
    public Transform mainMenu;
    public GameObject npcs;
    public HashSet<ISpawnable> spawnables = new HashSet<ISpawnable>();
    public List<IEndGamesable> endGamesables = new List<IEndGamesable>();
    public List<IStateGame> states = new List<IStateGame>();
    public List<IChangeName> changeNames = new List<IChangeName>();
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
        AddEndGamesable(this);
        AddStateGame(this);
        
    }
    private void Start()
    {
        player = Player.player;
        player.SetPosition(mainMenu.position, false);

    }


    public void AddSpawn(ISpawnable npc)
    {
        if (npcCount < 1&&!player.isDead)
        {
            Debug.Log("Win");
            return;
        }
        spawnables.Add(npc);
        SpawnNPC(npc);
    }
    public void OnSpawn()
    {
        foreach (var item in spawnables)
        {
            item.Spawn();
        }
    }
    public void AddEndGamesable(IEndGamesable endGamesable)=>endGamesables.Add(endGamesable);
    public void RemoveEndGamesable(IEndGamesable endGamesable)=>endGamesables.Remove(endGamesable);
    public void OnEndGame()
    {
        foreach (var item in endGamesables)
        {
            item.EndGame();
        }
    }
    public void AddStateGame(IStateGame stateGame)=>states.Add(stateGame);
    public void RemoveStateGame(IStateGame stateGame)=>states.Remove(stateGame);
    public void OnStartGame()
    {
        foreach (var item in states)
        {
            item.StartGame();
        }
    }
    public void OnStopGame()
    {
        foreach (var item in states)
        {
            item.StopGame();
        }
    }
    public void AddChangeName(IChangeName changeName)=>changeNames.Add(changeName);
    public void RemoveChangeName(IChangeName changeName)=>changeNames.Remove(changeName);
    public void OnChangeName(string changeName)
    {
        foreach (var item in changeNames)
        {
            item.ChangeName(changeName);
        }
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

    public void EndGame()
    {
        npcCount = 0;
    }
    private void OnDestroy()
    {
        RemoveEndGamesable(this);
        RemoveStateGame(this);
    }
    public void StartGame()
    {
        // code nay hoi dan can sua lai
        player.SetPosition(startPoints[Random.Range(0, startPoints.Count)].position, true);
        player.isDead = false;
        npcCount = 20;
        ShowScoreUI();
        OnSpawn();
    }
    public void StopGame()
    {
        player.SetPosition(mainMenu.position, true);

    }
}

