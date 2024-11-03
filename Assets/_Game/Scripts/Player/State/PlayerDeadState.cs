
using UnityEngine;

public class PlayerDeadState : PlayerStateParent
{
    public PlayerDeadState(string name, Animator animator, CharacterBase character, ControllerState controllerState, Player player) : base(name, animator, character, controllerState, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
       player.controller.Move(Vector3.zero);
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
