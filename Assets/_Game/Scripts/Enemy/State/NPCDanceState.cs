
using UnityEngine;
using UnityEngine.AI;

public class NPCDanceState : NPCStateParent
{
    public NPCDanceState(string name, Animator animator, CharacterBase character, ControllerState controllerState, NavMeshAgent agent) : base(name, animator, character, controllerState, agent)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();
    }
    public override void Exit()
    {
        base.Exit();
    }
}
