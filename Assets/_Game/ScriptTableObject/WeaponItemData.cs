using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Weapon Item", menuName ="Data/Weapon")]
public class WeaponItemData : ItemDataBase
{
    public GameObject baseWeapon;
    public WeaponType weaponType;

    private void OnValidate()
    {
        nameItem = weaponType.ToString();
    }
}
