
using UnityEngine;
using UnityEngine.AI;

public class NPCRunState : NPCStateParent
{
    public NPCRunState(string name, Animator animator, CharacterBase character, ControllerState controllerState, NavMeshAgent agent) : base(name, animator, character, controllerState, agent)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
    }
    public override void Update()
    {
        base.Update();
       
        if (agent.velocity.sqrMagnitude <= 0)
        {
            npc.target = npc.FindTarget();
            if (npc.target != null)
            {
                controllerState.ChangeState(npc.attack);
                return;
            }
            controllerState.ChangeState(npc.idle);
        }


    }
    public override void Exit()
    {
        base.Exit();
    }
}
