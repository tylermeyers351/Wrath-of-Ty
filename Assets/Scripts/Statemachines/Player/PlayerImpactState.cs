using UnityEngine;

public class PlayerImpactState : PlayerBaseState
{
    private readonly int PlayerAnimatorHash = Animator.StringToHash("Impact1");

    private const float CrossFadeDuration = 0.1f;

    private float duration = 1f;

    public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(PlayerAnimatorHash, CrossFadeDuration);
    }


    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        duration -= deltaTime;
        if (duration <= 0)
        {
            ReturnToLocomotion();
        }
    }

    public override void Exit()
    {

    }
}
