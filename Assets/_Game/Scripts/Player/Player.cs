using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Player : CharacterBase, IMaker
{
    public static Player player {  get; private set; }
    public CharacterController controller;
    public Transform body;
    public GameObject nameTagUI,makerUI;
    public Vector3 directionMove { get; private set; } = Vector3.zero;
    public Vector3 oldDirectionMove { get; private set; } = Vector3.zero;
    #region State
    public PlayerIdleState idle { get; private set; }
    public PlayerRunState run { get; private set; }
    public PlayerAttackState attack { get; private set; }
    public PlayerDanceState dance { get; private set; }
    public PlayerDeadState dead { get; private set; }
    #endregion
    #region Input
    private PlayerController playerController;
    public InputAction move { get; private set; }
    #endregion
    string nameKilled = "";
    Color killedColor = Color.white;
    float coin = 0;
    float star = 0;
    protected override void Awake()
    {
        base.Awake();
        player = this;
        OnInitState();
        OnInitInput();
        EnableMove();
        controller = GetComponent<CharacterController>();
    }



    protected override void Start()
    {
        base.Start();
        controllerState.InstallState(idle);

    }

    protected override void Update()
    {
        base.Update();
        controllerState.curentState.Update();
    }
    public override void Attack()
    {
        base.Attack();
    }

    public override void UpSize(int score)
    {
        if (this.score < score)
        {
            // thuc hien show pop up khi dat duoc do cao moi
        }
        base.UpSize(score);
    }
    protected override void ChangeHat(HatType hatType)
    {
        base.ChangeHat(hatType);

    }

    protected override void ChangePant(PantType pantType)
    {
        base.ChangePant(pantType);
    }

    protected override void ChangeWeapon(WeaponType weaponType)
    {
        base.ChangeWeapon(weaponType);

    }


    public virtual GameObject FindTarget()
    {
        GameObject lastPoint = null;
        float minDistance = Mathf.Infinity;
        GameObject currentPos = gameObject;
        int collidersCount = Physics.OverlapSphereNonAlloc(currentPos.transform.position, radiusAttack, collidersBuffer, lmTarget);
        for (int i = 0; i < collidersCount; i++)
        {
            if (collidersBuffer[i].gameObject == currentPos) continue;
            float distance = Vector3.Distance(currentPos.transform.position, collidersBuffer[i].transform.position);
            collidersBuffer[i].TryGetComponent(out IMaker maker1);
            collidersBuffer[i].TryGetComponent(out CharacterBase character);
            if (maker1 == null||character==null||character.isDead) continue;
            maker1.HideMaker();
            if (distance < minDistance)
            {
                minDistance = distance;
                lastPoint = collidersBuffer[i].gameObject;


            }
        }
        if (lastPoint != null && lastPoint.TryGetComponent(out IMaker maker)) maker.ShowMaker();
        return lastPoint;
    }


    #region Input
    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 newVector = context.ReadValue<Vector2>();
        directionMove = new Vector3(newVector.x, 0, newVector.y);
        oldDirectionMove = new Vector3(newVector.x, 0, newVector.y);
    }
    private void OffMove(InputAction.CallbackContext context)
    {
        directionMove = Vector3.zero;
    }
    public void DisableMove()
    {
        move.performed -= OnMove;
        move.canceled -= OffMove;
    }

    public void EnableMove()
    {
        move.performed += OnMove;
        move.canceled += OffMove;
    }
    #endregion
    #region OnInit
    private void OnInitInput()
    {
        playerController = new PlayerController();
        move = playerController.Player.Move;
        move.Enable();
    }

    private void OnInitState()
    {
        idle = new PlayerIdleState(CacheString.StateIlde, animator, this, controllerState, this);
        run = new PlayerRunState(CacheString.StateRun, animator, this, controllerState, this);
        attack = new PlayerAttackState(CacheString.StateAttack, animator, this, controllerState, this);
        dance = new PlayerDanceState(CacheString.StateDance, animator, this, controllerState, this);
        dead = new PlayerDeadState(CacheString.StateDead, animator, this, controllerState, this);
    }
    #endregion
    private void OnDestroy()
    {
        DisableMove();
    }

    public void ShowMaker()
    {
        makerUI.SetActive(true);
    }

    public void HideMaker()
    {
        makerUI.SetActive(false);
    }

    public override void TriggerCalled()
    {
        base.TriggerCalled();
        (controllerState.curentState as PlayerStateParent).TriggerCalled();
    }
    public void SetOldDirection(Vector2 old)
    {
        oldDirectionMove = old;
    }
    public void SetNameKilled(string name)=>nameKilled = name;
    public void SetKillerColor(Color color)=>killedColor = color;
    public override void Dead()
    {
        base.Dead();
        controllerState.ChangeState(dead);
        controller.enabled = false;
        DisableMove();
    }

    public override void DeadTrigger()
    {
        base.DeadTrigger();
        UI_Manager.instance.UpdateInfoEndGame(GameManager.Instance.GetScore(), nameKilled, killedColor, star, coin);
    }

    public override void UpdateStar(float star)
    {
       base.UpdateStar(star);
       this.star += star;
    }

    public override void UpdateCoin(float coin)
    {
      coin = Mathf.Floor(coin);
      this.coin += coin;
    }
    public void SetPosition(Vector3 position,bool isActiveCharacterController,Vector3 direction)
    {
        transform.position = position;
        controller.enabled = isActiveCharacterController;
        nameTagUI.SetActive(isActiveCharacterController);
        player.body.localRotation = Quaternion.LookRotation(direction);
        if (isActiveCharacterController)
        {
            EnableMove();

        }
        else
        {
            DisableMove();
        }
    }
    public void ResetPlayer()
    {
        gameObject.SetActive(true);
        controller.enabled = true;
        controllerState.ChangeState(idle);
        
    }
}
