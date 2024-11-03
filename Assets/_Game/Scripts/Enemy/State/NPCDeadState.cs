
using UnityEngine;
using UnityEngine.AI;

public class NPCDeadState : NPCStateParent
{
    public NPCDeadState(string name, Animator animator, CharacterBase character, ControllerState controllerState, NavMeshAgent agent) : base(name, animator, character, controllerState, agent)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}
