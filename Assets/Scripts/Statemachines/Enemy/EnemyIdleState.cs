using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private readonly int EnemyAnimatorHash = Animator.StringToHash("EnemyBlendTree");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private readonly int BreakTriggerHash = Animator.StringToHash("BreakTrigger");

    private float breakTimer = 0f;
    private float nextBreakTime;
    private float minBreak = 6f;
    private float maxBreak = 15f;

    private const float AnimatorDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;

    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(EnemyAnimatorHash, CrossFadeDuration);

        breakTimer = 0f;
        nextBreakTime = Random.Range(minBreak, maxBreak);
    }

    public override void Tick(float deltaTime)
    {
        breakTimer += deltaTime;

        if (breakTimer >= nextBreakTime)
        {
            stateMachine.Animator.SetTrigger(BreakTriggerHash);
            breakTimer = 0f;
            nextBreakTime = Random.Range(minBreak, maxBreak);
        }

        Move(deltaTime);

        if (IsInChaseRange())
        {
            Debug.Log("In range");
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            // Transition to chaseing state
            return;
        }
        
        FacePlayer();
        stateMachine.Animator.SetFloat(SpeedHash, 0f, AnimatorDampTime, deltaTime);
    }
    
    public override void Exit() { }

}
