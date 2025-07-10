using UnityEngine;

public class WalkState : PlayerState
{
    public WalkState(PlayerController controller) : base(controller) {}

    public override void Enter() { }

    public override void HandleInput()
    {
        if (controller.InputMove.magnitude < 0.1f)
            controller.SwitchState(new IdleState(controller));
    }

    public override void Update()
    {
        Vector3 move = controller.transform.right * controller.InputMove.x +
                       controller.transform.forward * controller.InputMove.y;

        controller.Move(move.normalized * controller.walkSpeed);
        controller.Animation.SetSpeed(controller.InputMove.magnitude);
    }

    public override void Exit() { }
}
