using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data Pant", menuName = "Data/DataOS/Pants")]
public class PantData : ScriptableObject
{
    public List<PantItemData> data;

    public Material GetPant(PantType type)
    {
        foreach (PantItemData item in data)
        {
            if (type == item.pantType) return item.basePant;
        }
        return null;
    }
    private void OnValidate()
    {

    }
}
