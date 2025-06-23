using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private readonly int EnemyAnimatorHash = Animator.StringToHash("EnemyBlendTree");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private const float AnimatorDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;

    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(EnemyAnimatorHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        stateMachine.Animator.SetFloat(SpeedHash, 0f, AnimatorDampTime, deltaTime);
        IsInChaseRange();
    }
    
    public override void Exit() { }

}
