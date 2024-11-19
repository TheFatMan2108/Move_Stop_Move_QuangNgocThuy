using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "Weapon Item", menuName = "Data/Hat")]
public class HatItemData : ItemDataBase
{
    public GameObject baseHat;
    public HatType hatType;

    protected void OnValidate()
    {
        nameItem = hatType.ToString();
    }
}
