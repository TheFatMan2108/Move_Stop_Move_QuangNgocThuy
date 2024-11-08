
using System;
using UnityEngine;
using RandomNameGeneratorLibrary;

[RequireComponent(typeof(WeaponCharacter))]
public class CharacterBase : MonoBehaviour
{
    [Header("Info")]
    public string nameCharacter = string.Empty;
    public float size = 1;
    public int score = 0;
    public WeaponCharacter weapon;
    public ControllerState controllerState;
    public float speed = 10f;
    public float radiusAttack = 4f;
    public LayerMask lmTarget;
    public GameObject target;
    public bool isDead;
    protected Collider[] collidersBuffer = new Collider[10];
    protected DataManager dataManager;
    #region Skin
    [SerializeField] protected SkinnedMeshRenderer skin;
    [SerializeField] protected Transform hat;
    [SerializeField] protected Transform rightHand;
    [SerializeField] protected SkinnedMeshRenderer pant;
    //
    [SerializeField] protected ColorType curentSkin;
    [SerializeField] protected WeaponType curentWeapon;
    protected GameObject weaponObj;
    protected GameObject hatObj;
    protected ColorItemData myColor;
    [SerializeField] protected HatType curentHat;
    [SerializeField] protected PantType curentPant;
    [SerializeField] protected UICharacter uICharacter;
    #endregion
    protected Animator animator;
    protected virtual void Awake()
    {
        OnInit();
    }

    private void OnInit()
    {
        weapon = GetComponent<WeaponCharacter>();
        controllerState = new ControllerState();
        animator = GetComponentInChildren<Animator>();
        curentWeapon = weapon.weaponType;
        animator.transform.localScale = new Vector3(size, size, size);
    }

    protected virtual void Start()
    {
        dataManager = DataManager.instance;
        ChangeWeapon(curentWeapon);
        ChangeHat(curentHat);
        ChangePant(curentPant);
        ChangeSkin(curentSkin);
        uICharacter.SetLevel(score.ToString());
        uICharacter.SetName(nameCharacter);
        SetColorUI();
    }

    protected void RandomName()
    {
        var generator = new PersonNameGenerator();
        nameCharacter = generator.GenerateRandomFirstName();
        uICharacter.SetName(nameCharacter);
    }
    protected virtual void Update()
    {

    }
    protected void ChangeSkin(ColorType curentSkin)
    {
        myColor = dataManager.GetColorDataOS().GetColor(curentSkin);
        Material[] materials = skin.materials;
        materials[0] = myColor.material;
        skin.materials = materials;
    }
    protected virtual void ChangeWeapon(WeaponType weaponType)
    {
        if (weaponObj != null)
            weaponObj.SetActive(false);
        weaponObj = Instantiate(dataManager.GetWeaponDataOS().GetWeapon(weaponType), rightHand);
        this.curentWeapon = weaponType;
        weapon.weaponType = weaponType;
    }
    protected virtual void ChangeHat(HatType hatType)
    {
        if (hatObj != null)
            hatObj.SetActive(false);
        hatObj = Instantiate(dataManager.GetHatDataOS().GeHat(hatType), hat);
    }
    protected virtual void ChangePant(PantType pantType)
    {
        Material[] materials = pant.materials;
        materials[0] = dataManager.GetPantDataOS().GetPant(pantType);
        pant.materials = materials;
    }

    public virtual void UpSize(int score)
    {
        this.score += score;
        size+=0.1f;
        animator.transform.localScale = new Vector3(size, size, size);
        uICharacter.SetLevel(this.score.ToString());
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = UnityEngine.Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radiusAttack);
    }
    public virtual void Attack()
    {
        if (target == null) return;
        weapon.Shoot(target.transform.position, this);
    }
    public virtual void Dead()
    {
        isDead = true;
    }
    public virtual void TriggerCalled() { }
    public void HideCharacter() => gameObject.SetActive(false);
    public virtual void SetColorUI()
    {
        uICharacter.nameCharacter.color = myColor.color;
        uICharacter.image.color = myColor.color;
    }
    public virtual void DeadTrigger()
    {
        // do something

    }
    public virtual void UpdateStar(float star) { }
    public virtual void UpdateCoin(float coin) { }
    public ColorItemData GetColorItemData() => myColor;
}
