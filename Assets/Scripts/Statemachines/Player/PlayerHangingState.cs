using UnityEngine;

public class PlayerHangingState : PlayerBaseState
{
    private Vector3 ledgeForward;
    private Vector3 closestPoint;

    private readonly int HangingIdle = Animator.StringToHash("HangingIdle");

    private const float CrossFadeDuration = 0.1f;

    public PlayerHangingState(PlayerStateMachine stateMachine, Vector3 ledgeForward, Vector3 closestPoint) : base(stateMachine)
    {
        this.ledgeForward = ledgeForward;
        this.closestPoint = closestPoint;
    }

    public override void Enter()
    {
        stateMachine.transform.rotation = Quaternion.LookRotation(ledgeForward, Vector3.up);
        
        stateMachine.Animator.CrossFadeInFixedTime(HangingIdle, CrossFadeDuration);
    }


    public override void Tick(float deltaTime)
    {
        // This might not work.. see lecture.
        if (Input.GetKeyDown(KeyCode.S))
        {
            stateMachine.Controller.Move(Vector3.zero);
            stateMachine.ForceReceiver.Reset();
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            stateMachine.Controller.Move(Vector3.zero);
            stateMachine.ForceReceiver.Reset();
            stateMachine.SwitchState(new PlayerPullUpState(stateMachine));
        }   
    }
    
    public override void Exit()
    {

    }
}