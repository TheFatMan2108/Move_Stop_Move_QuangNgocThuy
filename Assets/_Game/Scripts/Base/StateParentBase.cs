
using UnityEngine;

public class StateParentBase : IState
{
    protected string name;
    protected Animator animator;
    protected CharacterBase character;
    protected ControllerState controllerState;
    protected float stateCouter;
    protected bool isTrigger;

    public StateParentBase(string name, Animator animator, CharacterBase character, ControllerState controllerState)
    {
        this.name = name;
        this.animator = animator;
        this.character = character;
        this.controllerState = controllerState;
    }

    public virtual void Enter()
    {
        animator.SetBool(name, true);
        isTrigger = false;
    }
    public virtual void Update()
    {
        stateCouter -= Time.deltaTime;
    }
    public virtual void Exit()
    {
        animator.SetBool(name, false);
    }
    public void IsTrigger()=>isTrigger = true;
}
