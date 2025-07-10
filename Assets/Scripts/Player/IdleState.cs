using UnityEngine;

public class IdleState : PlayerState
{
 public IdleState(PlayerController controller) : base(controller) {}

    public override void Enter() { }

    public override void HandleInput()
    {
        if (controller.InputMove.magnitude > 0.1f)
            controller.SwitchState(new WalkState(controller));
    }

    public override void Update()
    {
        controller.Move(Vector3.zero);
    }

    public override void Exit() { }
}