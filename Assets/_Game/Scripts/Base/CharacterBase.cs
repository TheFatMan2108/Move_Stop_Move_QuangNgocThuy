
using System;
using UnityEngine;
using RandomNameGeneratorLibrary;
using System.Collections;
using System.Collections.Generic;

public class CharacterBase : MonoBehaviour
{
    [Header("Info")]
    public string nameCharacter = string.Empty;
    public float size = 1;
    public int score = 0;
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
    [SerializeField] protected WeaponType curentTypeWeapon;
    [SerializeField] protected HatType curentTypeHat;
    [SerializeField] protected PantType curentTypePant;
    [SerializeField] protected UICharacter uICharacter;
    protected List<WeaponCharacter> weaponObjs = new List<WeaponCharacter>();
    protected WeaponCharacter weaponCurrent;
    protected List<GameObject> hatObjs = new List<GameObject>();
    protected GameObject hatCurrent;
    protected ColorItemData myColor;
    #endregion
    protected Animator animator;
    protected virtual void Awake()
    {
        OnInit();
    }

    private void OnInit()
    {
        controllerState = new ControllerState();
        animator = GetComponentInChildren<Animator>();
        animator.transform.localScale = new Vector3(size, size, size);
    }

    protected virtual void Start()
    {
        dataManager = DataManager.instance;
        ChangeWeapon(curentTypeWeapon);
        ChangeHat(curentTypeHat);
        ChangePant(curentTypePant);
        ChangeSkin(curentSkin);
        uICharacter.SetLevel(score.ToString());
        uICharacter.SetName(nameCharacter);
        SetColorUI();
        StartCoroutine(CheckDataManager());
        //yield return new WaitUntil(()=> dataManager);

    }

    
    IEnumerator CheckDataManager()
    {
        yield return new WaitUntil(() => dataManager);
        Debug.Log("Để khi nào hết null thì chạy");  

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
    public virtual void ChangeWeapon(WeaponType weaponType)
    {
        WeaponCharacter weaponObj = CheckWeaponNull(weaponType);
        if (weaponCurrent != null) weaponCurrent.gameObject.SetActive(false);
        if (weaponObjs.Count <= 0 || weaponObj==null)
        {
            weaponObj = Instantiate(dataManager.GetWeaponDataOS().GetWeapon(weaponType), rightHand).AddComponent<WeaponCharacter>();
            weaponObj.SetUpWeapon(weaponType,rightHand);
            weaponObjs.Add(weaponObj);
        }
        else
        {
            weaponObj.gameObject.SetActive(true);
        }
        this.curentTypeWeapon = weaponType;
        weaponCurrent = weaponObj;
    }
    public WeaponCharacter CheckWeaponNull(WeaponType weaponType)
    {
        foreach (var item in weaponObjs)
        {
            if (item.weaponType == weaponType)
            {
                return item;
            }
        }
        return null;
    }
    public virtual void ChangeHat(HatType hatType)
    {
        // do lười nên không thực hiện pool ở đây =)))
        if (hatCurrent != null)
            Destroy(hatCurrent);
        hatCurrent = Instantiate(dataManager.GetHatDataOS().GeHat(hatType), hat);
        curentTypeHat = hatType;
    }
    public virtual void ChangePant(PantType pantType)
    {
        Material[] materials = pant.materials;
        materials[0] = dataManager.GetPantDataOS().GetPant(pantType);
        pant.materials = materials;
        curentTypePant = pantType;
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
        weaponCurrent.Shoot(target.transform.position, this);
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
