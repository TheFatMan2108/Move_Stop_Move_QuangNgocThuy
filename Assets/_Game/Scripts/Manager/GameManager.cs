using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<ISpawnable> spawnables = new List<ISpawnable>();
    [Tooltip("Tổng số NPC sẽ xuất hiện")]
    public int npcCount = 10;
    [Tooltip("Số lượng NPC trên bản đồ")]
    public int totalNPC = 10;
    public TextMeshProUGUI txtScore;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
       ShowScoreUI();
    }
    public void AddSpawn(ISpawnable npc)
    {
        if (npcCount <1)
        {
            Debug.Log(  "Win");
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
        txtScore.text = string.Format("Alive: {0}", GetScore());
    }


    public void SpawnNPC(ISpawnable npc)
    {
        if(npcCount<totalNPC)return;
        StartCoroutine(SpawnNPCCorotine(3, npc));
        
    }

    IEnumerator SpawnNPCCorotine(float time,ISpawnable npc)
    {
        yield return new WaitForSeconds(time);
        npc.Spawn();
        RemoveSpawn(npc);
    }
    public float GetScore()
    {
        return npcCount+1;
    }
    internal void RemoveSpawn(ISpawnable npc) => spawnables.Remove(npc);
}
   
