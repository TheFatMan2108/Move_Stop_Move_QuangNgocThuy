
using UnityEngine;
using UnityEngine.AI;

public class NPCStateParent : StateParentBase
{
    protected NavMeshAgent agent;
    protected NPC npc;
    protected bool triggerCalled;
    public NPCStateParent(string name, Animator animator, CharacterBase character, ControllerState controllerState, NavMeshAgent agent) : base(name, animator, character, controllerState)
    {
        this.agent = agent;
        npc = character as NPC;
    }

    public override void Enter()
    {
        base.Enter();
        triggerCalled = false;
        
    }
    public override void Update()
    {
        base.Update();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public void TriggerCalled() => triggerCalled = true;
}
