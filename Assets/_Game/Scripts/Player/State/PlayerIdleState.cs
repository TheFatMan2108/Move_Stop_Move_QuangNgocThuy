
using UnityEngine;

public class PlayerIdleState : PlayerStateParent
{
    public PlayerIdleState(string name, Animator animator, CharacterBase character, ControllerState controllerState, Player player) : base(name, animator, character, controllerState, player)
    {
    }
    public override void Enter()
    {
        base.Enter();
        
    }
    public override void Update()
    {
        base.Update();
        if (directionMove.sqrMagnitude > 0) controllerState.ChangeState(player.run);
    }
    public override void Exit()
    {
        base.Exit();
    }
}
