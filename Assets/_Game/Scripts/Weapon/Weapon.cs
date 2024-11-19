
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float speed = 10f;
    public float euler = 10;
    public WeaponType type;
    public float distance = 0f;
    private Vector3 target;
    private CharacterBase chara;
    private Rigidbody rb;
    private WeaponCharacter weaponCharacter;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        rb.velocity = target.normalized*speed;
        transform.eulerAngles = new Vector3(90, 0, euler);euler += 1000 * Time.deltaTime;
        if(Vector3.Distance(transform.position,chara.transform.position)>=chara.radiusAttack+5) weaponCharacter.AddToPool(gameObject);  
    }

    public void SetTarget(Vector3 target)
    {
        this.target = target-transform.position;
        this.target.y = 0;
        distance = Vector3.Distance(target,chara.transform.position);
        distance = Mathf.FloorToInt(distance);
        

    }
    public void SetType(WeaponType type) => this.type = type; 
    public void SetUsingPeopel(CharacterBase character) => this.chara = character;
    public void SetWeaponCharacter(WeaponCharacter weaponCharacter)=>this.weaponCharacter = weaponCharacter;
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.TryGetComponent(out CharacterBase character))
        {
            if (character == null||chara==character) return;
            chara.UpdateStar(1);
            chara.UpdateCoin(distance);
            if (character.TryGetComponent(out Player player) && player != null)
            {
                player.SetNameKilled(chara.nameCharacter);
                player.SetKillerColor(chara.GetColorItemData().color);
            }
            character.Dead();
            weaponCharacter.AddToPool(gameObject);
            chara.UpSize((int)distance);
           
        };
    }
}
