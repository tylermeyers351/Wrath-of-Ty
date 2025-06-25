using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    private readonly int EnemyAnimatorHash = Animator.StringToHash("EnemyMelee");

    private const float CrossFadeDuration = 0.1f;

    public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Weapon.SetAttack(stateMachine.AttackDamage, stateMachine.AttackKnockback);
        stateMachine.Animator.CrossFadeInFixedTime(EnemyAnimatorHash, CrossFadeDuration);
    }


    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(stateMachine.Animator) >= 1)
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        }
    }

    public override void Exit() {}
}
