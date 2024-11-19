using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data Pant", menuName = "Data/Pant")]
public class PantItemData : ItemDataBase
{
    public Material basePant;
    public PantType pantType;

    private void OnValidate()
    {
        nameItem = pantType.ToString();
    }
}
