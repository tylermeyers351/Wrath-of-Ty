using UnityEngine;

public class EnemyImpactState : EnemyBaseState
{
    private readonly int EnemyAnimatorHash = Animator.StringToHash("Impact1");

    private const float CrossFadeDuration = 0.1f;

    private float duration = 1.0f;

    public EnemyImpactState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(EnemyAnimatorHash, CrossFadeDuration);
    }


    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        duration -= deltaTime;

        if (duration <= 0)
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
        }
    }

    public override void Exit()
    {

    }
}
