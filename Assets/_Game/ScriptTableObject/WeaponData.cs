using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Data Weapons",menuName ="Data/DataOS/Weapons")]
public class WeaponData : ScriptableObject
{
   public List<WeaponItemData> data;

    public GameObject GetWeapon(WeaponType type)
    {
        foreach (WeaponItemData item in data)
        {
            if (type == item.weaponType) return item.baseWeapon; 
        }
        return null;
    }
    private void OnValidate()
    {
        
    }
}
