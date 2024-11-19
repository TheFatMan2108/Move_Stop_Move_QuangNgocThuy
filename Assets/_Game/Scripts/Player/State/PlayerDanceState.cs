
using UnityEngine;

public class PlayerDanceState : PlayerStateParent
{
    public PlayerDanceState(string name, Animator animator, CharacterBase character, ControllerState controllerState, Player player) : base(name, animator, character, controllerState, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.isWin = true;
        player.DeadTrigger();
    }
    public override void Update()
    {
        base.Update();
    }
    public override void Exit()
    {
        base.Exit();
        player.isWin = false;
    }
}
