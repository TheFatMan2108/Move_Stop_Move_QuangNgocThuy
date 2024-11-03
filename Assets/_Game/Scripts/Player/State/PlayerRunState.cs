
using UnityEngine;

public class PlayerRunState : PlayerStateParent
{
    public PlayerRunState(string name, Animator animator, CharacterBase character, ControllerState controllerState, Player player) : base(name, animator, character, controllerState, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();
        player.target = player.FindTarget();
        if (directionMove.sqrMagnitude == 0)
        {
            if (player.target != null)
            {
                controllerState.ChangeState(player.attack);
                return;
            }
            controllerState.ChangeState(player.idle);
        }
    }
    public override void Exit()
    {
        base.Exit();
        
    }
}
