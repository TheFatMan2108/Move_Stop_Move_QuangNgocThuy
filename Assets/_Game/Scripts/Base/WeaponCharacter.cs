
using System.Collections.Generic;
using UnityEngine;

public class WeaponCharacter : MonoBehaviour
{
    public WeaponType weaponType;
    public Queue<GameObject> bullets = new Queue<GameObject>();
    public Transform pointShoot;
    private Weapon weaponCurent;
    private CapsuleCollider capsule;
         
    public void Shoot(Vector3 target,CharacterBase character)
    {
        GameObject bullet = null;
        if (bullets.Count<=0)
        {   
            bullet = Instantiate(DataManager.instance.GetWeaponDataOS().GetWeapon(weaponType));
            weaponCurent = bullet.AddComponent<Weapon>();
            capsule = bullet.AddComponent<CapsuleCollider>();
            capsule.isTrigger = true;
            weaponCurent.SetUsingPeopel(character);
            weaponCurent.SetWeaponCharacter(this);
        }
        else
        {
            bullet = bullets.Dequeue();
            weaponCurent = bullet.GetComponent<Weapon>();
            bullet.SetActive(true);
        }
        bullet.transform.position = pointShoot.position;
        weaponCurent.SetTarget(target); 
        weaponCurent.euler = 0;
    }

    public void AddToPool(GameObject bullet)
    {
        bullet.SetActive(false);
        bullets.Enqueue(bullet);
    }

    public void SetUpWeapon(WeaponType weaponType, Transform pointShoot)
    {
        this.weaponType = weaponType;
        this.pointShoot = pointShoot;
    }
}
