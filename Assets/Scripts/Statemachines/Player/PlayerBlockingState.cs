using UnityEngine;

public class PlayerBlockingState : PlayerBaseState
{
    private readonly int BlockingHash = Animator.StringToHash("Blocking");

    private const float CrossFadeDuration = 0.1f;

    public PlayerBlockingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Health.SetDamageable(true);
        stateMachine.Animator.CrossFadeInFixedTime(BlockingHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        if (!stateMachine.InputReader.IsBlocking)
        {
            if (stateMachine.Targeter.CurrentTarget == null)
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
                return;
            }
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            return;
        }
    }

    public override void Exit()
    {
        stateMachine.Health.SetDamageable(false);
    }
}
