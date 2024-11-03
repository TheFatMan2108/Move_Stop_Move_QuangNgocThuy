
using UnityEngine;
using UnityEngine.AI;


public class NPCAttackState : NPCStateParent
{
    Vector3 oldDirection= Vector3.zero;
    Vector3 target= Vector3.zero;
    public NPCAttackState(string name, Animator animator, CharacterBase character, ControllerState controllerState, NavMeshAgent agent) : base(name, animator, character, controllerState, agent)
    {
    }
    public override void Enter()
    {
        base.Enter();
        agent.isStopped = true;
    }
    public override void Update()
    {
        base.Update();
        if (npc.target == null) target = Vector2.zero;
        else target = npc.target.transform.position;
        Vector3 direction = target - npc.transform.position;
        oldDirection = new Vector3(direction.x, 0, direction.z);
        if (oldDirection.sqrMagnitude != 0)
        {
            npc.transform.localRotation = Quaternion.LookRotation(oldDirection);
        }
        if (triggerCalled) controllerState.ChangeState(npc.idle);
    }
    public override void Exit()
    {
        base.Exit();
        agent.isStopped = false;
    }
}
