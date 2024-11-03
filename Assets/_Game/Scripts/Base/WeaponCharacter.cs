
using UnityEngine;

public class WeaponCharacter : MonoBehaviour
{
    public WeaponType weaponType;
    public GameObject bullet;
    public Transform pointShoot;
    private Weapon weapon;
    private CapsuleCollider capsule;
         
    public void Shoot(Vector3 target,CharacterBase character)
    {
        if (bullet == null|| weapon.type !=weaponType)
        {   if (bullet != null) Destroy(bullet);
            bullet = Instantiate(DataManager.instance.GetWeaponDataOS().GetWeapon(weaponType));
            weapon = bullet.AddComponent<Weapon>();
            capsule = bullet.AddComponent<CapsuleCollider>();
            capsule.isTrigger = true;
            weapon.SetUsingPeopel(character);
        }
        else
        {
            bullet.SetActive(true);
        }
        bullet.transform.position = pointShoot.position;
        weapon.SetTarget(target);
        weapon.euler = 0;
    }
}
