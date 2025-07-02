using UnityEngine;

public class PlayerPullUpState : PlayerBaseState
{
    private readonly int HeroPullup = Animator.StringToHash("HeroPullUp");
    private readonly Vector3 Offset = new Vector3(0f, 2.325f, 0.65f);

    private const float CrossFadeDuration = 0.1f;

    public PlayerPullUpState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(HeroPullup, CrossFadeDuration);
    }


    public override void Tick(float deltaTime)
    {
        AnimatorStateInfo stateInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorTransitionInfo transitionInfo = stateMachine.Animator.GetAnimatorTransitionInfo(0);

        if (transitionInfo.anyState) return; // still transitioning
        if (stateInfo.IsName("HeroPullUp") && stateInfo.normalizedTime < 1f) return;

        stateMachine.Controller.enabled = false;
        stateMachine.transform.Translate(Offset, Space.Self);
        stateMachine.Controller.enabled = true;

        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine, false));
        stateMachine.ForceReceiver.Reset();
    }

    public override void Exit()
    {
        stateMachine.ForceReceiver.Reset();
        stateMachine.Controller.Move(Vector3.zero);
    }
}
