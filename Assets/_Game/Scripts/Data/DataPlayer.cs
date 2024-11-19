using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataPlayer 
{
    public int coin;
    public string name;
    public int indexSkin;
    public int indexWeapon;
    public int indexHat;
    public int indexPant;
    public int highScore;
    public int level;
    public List<int> weaponsStatus;
    public List<int> hatStatus;
    public List<int> pantStatus;

    public DataPlayer()
    {
        coin = 0;
        indexSkin = 0; 
        indexWeapon = 0;
        indexHat = 0;
        indexPant = 0;
        highScore = 100;
        level = 1;
        name = string.Empty;
        weaponsStatus = new List<int>();
        hatStatus = new List<int>();
        pantStatus = new List<int>();
    }
}
