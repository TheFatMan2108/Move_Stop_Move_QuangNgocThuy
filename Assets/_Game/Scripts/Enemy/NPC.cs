using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class NPC : CharacterBase,IMaker,ISpawnable,IEndGamesable,IStateGame
{
    public GameObject maker;
    public Vector3 spawnPoint = Vector3.zero;
    #region State
    public NPCIdleState idle { get;private set; }
    public NPCRunState run { get; private set; }
    public NPCAttackState attack { get; private set; }
    public NPCDanceState dance { get; private set; }
    public NPCDeadState dead { get; private set; }
    #endregion
    protected NavMeshAgent agent;
    OffscreenIndicator indicators;
    OffscreenIndicator.Indicator indicator;
    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        OnInitState();
        spawnPoint = transform.position;
        RandomName();
    }

    private void OnInitState()
    {
        idle = new NPCIdleState(CacheString.StateIlde, animator, this, controllerState, agent);
        run = new NPCRunState(CacheString.StateRun, animator, this, controllerState, agent);
        attack = new NPCAttackState(CacheString.StateAttack, animator, this, controllerState, agent);
        dance = new NPCDanceState(CacheString.StateDance, animator, this, controllerState, agent);
        dead = new NPCDeadState(CacheString.StateDead, animator, this, controllerState, agent);
    }

    protected override void Start()
    {
       AddIndicator();
        base.Start();
        controllerState.InstallState(idle);
        RamdomWeapon();
        RandomHat();
        RandomPant();
        RandomSkin();
        GameManager.Instance.AddEndGamesable(this);
        GameManager.Instance.AddStateGame(this);
        Dead();
    }
    private void AddIndicator()
    {
        indicators = OffscreenIndicator.Instance;
        indicators.targetIndicators.Add(new OffscreenIndicator.Indicator()
        {
            target = transform.transform
        });
        indicators.InstanIndicator();
    }

    protected override void Update()
    {
        base.Update();
        controllerState.curentState.Update();
        if (Vector3.Distance(Player.player.transform.position, transform.position) > Player.player.radiusAttack)
        {
            HideMaker();
        }
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
            collidersBuffer[i].TryGetComponent(out CharacterBase character);
            if ( character == null || character.isDead) continue;
            if (distance < minDistance)
            {
                minDistance = distance;
                lastPoint = collidersBuffer[i].gameObject;
            }
        }
        return lastPoint;
    }
    public override void TriggerCalled()
    {
        base.TriggerCalled();
        (controllerState.curentState as NPCStateParent).TriggerCalled();
    }

    public override void Attack()
    {
        
        if (target == null)
        {
            controllerState.ChangeState(idle);
            return;
        };
        base.Attack();
    }

    public void ShowMaker()
    {
       maker.SetActive(true);
    }

    public void HideMaker()
    {
       maker.SetActive(false);
    }

    public override void Dead()
    {
        base.Dead();
        controllerState.ChangeState(dead);
        agent.isStopped = true;
        isDead = true;
    }

    public override void DeadTrigger()
    {
        base.DeadTrigger();
        GameManager.Instance.SetNPCCount();
        GameManager.Instance.ShowScoreUI();
        GameManager.Instance.AddSpawn(this);
        indicator.indicatorUI.gameObject.SetActive(false);
    }

    public void Spawn()
    {
        gameObject.SetActive(true);
        controllerState.ChangeState(idle);
        agent.isStopped = false;
        isDead = false;
        RandomHat();
        RandomPant();
        RandomSkin();
        transform.position = spawnPoint;
        RandomName();
        RandomScore();
        indicator.indicatorUI.gameObject.SetActive(true);
    }
    private void OnDestroy()
    {
        GameManager.Instance.RemoveSpawn(this);
        GameManager.Instance.RemoveEndGamesable(this);
        GameManager.Instance.RemoveStateGame(this);
    }
    public void RandomHat()
    {
        int size = dataManager.GetHatDataOS().data.Count;
        HatType hat = (HatType)Random.Range(0, size);
        
        ChangeHat(hat);
    }
    public void RamdomWeapon()
    {
        int size = dataManager.GetWeaponDataOS().data.Count;
        WeaponType weapon = (WeaponType)Random.Range(0, size);
        ChangeWeapon(weapon);
    }
    public void RandomPant()
    {
        int size = dataManager.GetPantDataOS().data.Count;
        PantType pant = (PantType)Random.Range(0, size);
        ChangePant(pant);
    }
    public void RandomSkin()
    {
        int size = dataManager.GetColorDataOS().data.Count;
        ColorType color = (ColorType)Random.Range(0, size);
        ChangeSkin(color);
        SetColorUI();
    }
    public void RandomScore()
    {
        int min =(int) score - 3;
        int max =(int) score + 11;
        int nScore = Random.Range(min,max);
        UpSize(nScore);
    }

    public override void SetColorUI()
    {
        base.SetColorUI();
        indicators.SetColor(transform,myColor.color);
    }

    public override void UpSize(int score)
    {
        score = Mathf.Clamp(score, 0, int.MaxValue);
        base.UpSize(score);
        indicators.SetScore(transform);
    }
    public void SetIndicator(OffscreenIndicator.Indicator indicator)=>this.indicator = indicator;

    public void EndGame()
    {
        // giảm lại kích thước và điểm
        score = 0;
        size = 1;
        animator.transform.localScale = new Vector3(size, size, size);
        controllerState.ChangeState(dead);
    }

    public void StartGame()
    {
        Spawn();
    }

    public void StopGame()
    {
        
    }
}
