using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data Pant", menuName = "Data/Pant")]
public class PantItemData : ScriptableObject
{
    public Material basePant;
    public string namePant;
    public PantType pantType;
    public int price = 1;

    private void OnValidate()
    {
        namePant = pantType.ToString();
    }
}
