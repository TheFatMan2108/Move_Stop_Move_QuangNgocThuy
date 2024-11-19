using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Player : CharacterBase, IMaker, IEndGamesable, IStateGame, IChangeName, IDataSave
{
    public static Player instance { get; private set; }
    public CharacterController controller;
    public Transform body;
    public GameObject nameTagUI, makerUI;
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
    public InputAction move { get; private set; }
    private PlayerController playerController;
    #endregion
    string nameKilled = "";
    Color killedColor = Color.white;
    float coin = 0;
    float star = 0;
    int indexskin = 0;
     public bool isWin;
    protected override void Awake()
    {
        base.Awake();
        instance = this;
        OnInitState();
        OnInitInput();
        EnableMove();
        controller = GetComponent<CharacterController>();
        indexskin = (int)curentSkin;
    }



    protected override void Start()
    {
        base.Start();
        controllerState.InstallState(idle);
        GameManager.Instance.AddEndGamesable(this);
        GameManager.Instance.AddStateGame(this);
        GameManager.Instance.AddChangeName(this);

    }

    protected override void Update()
    {
        base.Update();
        controllerState.curentState.Update();
    }
    public override void UpSize(int score)
    {
        if (this.score < score)
        {
            // thuc hien show pop up khi dat duoc do cao moi
        }
        base.UpSize(score);
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
            if (maker1 == null || character == null || character.isDead) continue;
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
        GameManager.Instance.RemoveEndGamesable(this);
        GameManager.Instance.RemoveStateGame(this);
        GameManager.Instance.RemoveChangeName(this);
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
    public void SetNameKilled(string name) => nameKilled = name;
    public void SetKillerColor(Color color) => killedColor = color;
    public override void Dead()
    {
        if (isDead||isWin) return;
        base.Dead();
        controllerState.ChangeState(dead);
        controller.Move(Vector3.zero);
        controller.enabled = false;
        DisableMove();
        directionMove = Vector3.zero;
    }
    public void Win()
    {
        isWin = true;
        controllerState.ChangeState(dance);
        controller.Move(Vector3.zero);
        controller.enabled = false;
        DisableMove();
        directionMove = Vector3.zero;
    }
    public override void DeadTrigger()
    {
        base.DeadTrigger();
        int score =(int) GameManager.Instance.GetScore();
        UI_Manager.instance.UpdateInfoEndGame(score, nameKilled, killedColor, star, coin);
        if(score < DataManager.instance.GetMaxScore())
        {
            UI_Manager.instance.UIMainMenu.UpdateHighScore(score);
        }
        GameManager.Instance.UpdateCoin((int)coin);
        GameManager.Instance.OnEndGame();
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
    public void SetPosition(Vector3 position, bool isActiveCharacterController)
    {
        transform.position = position;
        controller.enabled = isActiveCharacterController;
        uICharacter.gameObject.SetActive(isActiveCharacterController);
        oldDirectionMove = Vector3.back;
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

    public void EndGame()
    {
        // reset lại kích cỡ
        size = 1;
        score = 0;
        coin = 0;
        star = 0;
        uICharacter.SetLevel(score.ToString());
        animator.transform.localScale = new Vector3(size, size, size);
        DisableMove();
    }

    public void StartGame()
    {
        EnableMove();
        isDead = false;
        isWin = false;
        uICharacter.gameObject.SetActive(true);
    }

    public void StopGame()
    {
        DisableMove();
        uICharacter.gameObject.SetActive(false);
        gameObject.SetActive(true);
        controller.enabled = false;
        oldDirectionMove = Vector3.back;
        controllerState.ChangeState(idle);
    }

    public void ChangeName(string newName)
    {
        nameCharacter = newName;
        uICharacter.SetName(newName);
    }
    public void UpdateChoiceSkinLeft()
    {
        // logic skin left
        indexskin--;
        if (indexskin <= 0)
        {
            indexskin = 0;
        }
        curentSkin = (ColorType)indexskin;

        ChangeSkin(curentSkin);
        DataManager.instance.SaveIDataSave();
    }
    public void UpdateChoiceSkinRight()
    {
        // logic skin right
        int colorCount = Enum.GetValues(typeof(ColorType)).Length;
        indexskin++;
        if (indexskin >= colorCount)
        {
            indexskin = colorCount - 1;
        }
        curentSkin = (ColorType)indexskin;
        ChangeSkin(curentSkin);
        DataManager.instance.SaveIDataSave();
    }

    public void Save(ref DataPlayer data)
    {
        data.indexSkin = indexskin;
        data.name = nameCharacter;
        data.indexHat = (int)curentTypeHat;
        data.indexPant = (int)curentTypePant;
        data.indexWeapon = (int)curentTypeWeapon;
    }

    public void Load(DataPlayer data)
    {
        indexskin = data.indexSkin;
        curentTypeWeapon = (WeaponType) data.indexWeapon;
        curentTypeHat = (HatType) data.indexHat;
        curentTypePant = (PantType)data.indexPant;
        curentSkin = (ColorType)indexskin;
        StartCoroutine(CheckNull());
        nameCharacter = data.name;
    }
    IEnumerator CheckNull()
    {
        yield return new WaitUntil(()=>DataManager.instance);
        ChangeSkin(curentSkin);
        ChangeWeapon(curentTypeWeapon);
        ChangeHat(curentTypeHat);
        ChangePant(curentTypePant);
        yield return new WaitUntil(() => UI_Manager.instance);
        UI_Manager.instance.UIMainMenu.SetNamePlayer(nameCharacter);
    }
}
