using System;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private float previousFrameTime;
    private bool alreadAppliedForce;

    private Attack currentAttack;

    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        currentAttack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(currentAttack.AnimationName, currentAttack.TransitionDuration);
        stateMachine.Weapon.SetAttack(currentAttack.Damage, currentAttack.Knockback);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        FaceTarget();

        float normalizedTime = GetNormalizedTime(stateMachine.Animator);


        if (normalizedTime >= previousFrameTime && normalizedTime < 1f)
        {
            if (normalizedTime > currentAttack.ForceTime)
            {
                TryApplyForce();
            }

            if (stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
        {
            if (stateMachine.Targeter.CurrentTarget != null)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
            
        }
        previousFrameTime = normalizedTime;
    }


    public override void Exit()
    {

    }

    private void TryComboAttack(float normalizedTime)
    {
        if (currentAttack.ComboStateIndex == -1) { return; }

        if (normalizedTime < currentAttack.ComboAttackTime) { return; }

        stateMachine.SwitchState(new PlayerAttackingState(stateMachine, currentAttack.ComboStateIndex));
    }

    private void TryApplyForce()
    {
        if (alreadAppliedForce) { return; }
        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * currentAttack.Force);
        alreadAppliedForce = true;
    }
}
