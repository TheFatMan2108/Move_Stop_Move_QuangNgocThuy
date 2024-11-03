
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;

public class NPCIdleState : NPCStateParent
{
    public NPCIdleState(string name, Animator animator, CharacterBase character, ControllerState controllerState, NavMeshAgent agent) : base(name, animator, character, controllerState, agent)
    {
    }
    public override void Enter()
    {
        base.Enter();
        stateCouter = 0.5f;
    }
    public override void Update()
    {
        base.Update();
        if (stateCouter > 0) return;
        if (agent.remainingDistance <= agent.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(character.transform.position, character.radiusAttack, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, UnityEngine.Color.blue, 1.0f); //so you can see with gizmos
                agent.SetDestination(point);
            }
        }
        if (agent.velocity.sqrMagnitude > 0) controllerState.ChangeState(npc.run);
    }
    public override void Exit()
    {
        base.Exit();
    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
