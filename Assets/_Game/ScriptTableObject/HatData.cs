using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data hats", menuName = "Data/DataOS/Hats")]
public class HatData : ScriptableObject
{
    public List<HatItemData> data;

    public GameObject GeHat(HatType type)
    {
        foreach (HatItemData item in data)
        {
            if (type == item.hatType) return item.baseHat;
        }
        return null;
    }
    private void OnValidate()
    {

    }
}
