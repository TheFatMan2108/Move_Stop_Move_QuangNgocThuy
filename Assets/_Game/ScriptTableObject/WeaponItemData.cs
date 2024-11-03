using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Weapon Item", menuName ="Data/Weapon")]
public class WeaponItemData : ScriptableObject
{
    public GameObject baseWeapon;
    public string nameWeapon;
    public WeaponType weaponType;
    public int price =1;

    private void OnValidate()
    {
        nameWeapon = weaponType.ToString();
    }
}
