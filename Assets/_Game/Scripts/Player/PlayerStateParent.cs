
using UnityEngine;

public class PlayerStateParent : StateParentBase
{
    protected CharacterController controller;
    protected Player player;
    protected Vector3 directionMove, oldDirection;
    Vector3 velocity;
    protected bool triggerCalled;
    public PlayerStateParent(string name, Animator animator, CharacterBase character, ControllerState controllerState, Player player) : base(name, animator, character, controllerState)
    {
        controller = player.controller;
        this.player = player;
    }

    public override void Enter()
    {
        base.Enter();
        velocity = Vector3.zero;
        triggerCalled = false;
    }
    public override void Update()
    {
        base.Update();
        directionMove = player.directionMove;
        oldDirection = player.oldDirectionMove;
        if (oldDirection.sqrMagnitude != 0)
        {
            player.body.localRotation = Quaternion.LookRotation(oldDirection);
        }
        velocity = directionMove.normalized * player.speed;
        velocity = velocity + new Vector3(0, -20f, 0);
        if (player.controller.enabled)
            player.controller.Move(velocity * Time.deltaTime);
    }
    public override void Exit()
    {
        base.Exit();
    }
    public void TriggerCalled()=>triggerCalled=true;
    public void SetDirecion(Vector2 direcion)=>player.SetOldDirection(direcion);
}
