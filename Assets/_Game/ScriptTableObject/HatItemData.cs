using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Item", menuName = "Data/Hat")]
public class HatItemData : ScriptableObject
{
    public GameObject baseHat;
    public string nameHat;
    public HatType hatType;
    public int price = 1;

    private void OnValidate()
    {
        nameHat = hatType.ToString();
    }
}
