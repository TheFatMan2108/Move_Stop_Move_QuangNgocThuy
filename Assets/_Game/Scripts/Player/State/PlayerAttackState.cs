
using UnityEngine;

public class PlayerAttackState : PlayerStateParent
{
    Vector2 newDir = Vector2.zero;
    Vector3 target = Vector3.zero;
    public PlayerAttackState(string name, Animator animator, CharacterBase character, ControllerState controllerState, Player player) : base(name, animator, character, controllerState, player)
    {
    }
    public override void Enter()
    {
        base.Enter();
        stateCouter = 1;
        player.controller.enabled = false;
    }
    public override void Update()
    {
        if (player.target == null) target = Vector2.zero;
        else target = player.target.transform.position;
        Vector3 direction = target - player.transform.position;
        newDir = new Vector3(direction.x, 0, direction.z);
        SetDirecion(newDir);
        base.Update();
       
        if (triggerCalled) controllerState.ChangeState(player.idle); 
    }
    public override void Exit()
    {
        base.Exit();
        player.controller.enabled = true;
    }
}
