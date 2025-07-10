using System.Numerics;
using UnityEngine;

public class PlayerDodgingState : PlayerBaseState
{
    private readonly int DodgingBlendTreeHash = Animator.StringToHash("DodgeBlendTree");
    private readonly int DodgingForwardHash = Animator.StringToHash("DodgeForward");
    private readonly int DodgingRightHash = Animator.StringToHash("DodgeRight");

    private const float CrossFadeDuration = 0.1f;

    private float remainingDodgeTime;

    private UnityEngine.Vector3 dodgingDirectionInput;

    public PlayerDodgingState(PlayerStateMachine stateMachine, UnityEngine.Vector3 dodgingDirectionInput) : base(stateMachine)
    {
        this.dodgingDirectionInput = dodgingDirectionInput;
    }

    public override void Enter()
    {
        remainingDodgeTime = stateMachine.DodgeDuration;
        stateMachine.Animator.SetFloat(DodgingForwardHash, dodgingDirectionInput.y);
        stateMachine.Animator.SetFloat(DodgingRightHash, dodgingDirectionInput.x);
        stateMachine.Animator.CrossFadeInFixedTime(DodgingBlendTreeHash, CrossFadeDuration);
        stateMachine.Health.SetDamageable(true);

    }


    public override void Tick(float deltaTime)
    {
        UnityEngine.Vector3 movement = new UnityEngine.Vector3();
        movement += 2f * stateMachine.transform.right * dodgingDirectionInput.x * stateMachine.DodgeLength / stateMachine.DodgeDuration;
        movement += 2f * stateMachine.transform.forward * dodgingDirectionInput.y * stateMachine.DodgeLength / stateMachine.DodgeDuration;
        Move(movement, deltaTime);
        FaceTarget();
        remainingDodgeTime -= deltaTime;
        if (remainingDodgeTime <= 0)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
    }
    
    public override void Exit()
    {
        stateMachine.Health.SetDamageable(false);
    }
}
