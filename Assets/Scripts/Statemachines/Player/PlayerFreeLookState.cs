using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private bool shouldFade;

    private readonly int FreeLookAnimatorHash = Animator.StringToHash("FreeLookBlendTree");
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");

    private const float AnimatorDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;

    public PlayerFreeLookState(PlayerStateMachine stateMachine, bool shouldFade = true) : base(stateMachine)
    {
        this.shouldFade = shouldFade;
    }

    public override void Enter()
    {
        stateMachine.InputReader.TargetEvent += OnTarget;
        stateMachine.InputReader.JumpEvent += OnJump;

        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0f);

        if (shouldFade)
        {
            stateMachine.Animator.CrossFadeInFixedTime(FreeLookAnimatorHash, CrossFadeDuration);
        }
        else
        {
            stateMachine.Animator.Play(FreeLookAnimatorHash);
        }
    }


    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }
        
        Vector3 movement = CalculateMovement();
        
        Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);

        // stateMachine.Controller.Move(movement * stateMachine.FreeLookMovementSpeed * deltaTime);

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0f, AnimatorDampTime, deltaTime);
            return;
        }

        if (stateMachine.InputReader.IsSprinting)
        {
            stateMachine.FreeLookMovementSpeed = 10;
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 2f, AnimatorDampTime, deltaTime);
            // Debug.Log("Sprinting");
        }
        else
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1f, AnimatorDampTime, deltaTime);
            stateMachine.FreeLookMovementSpeed = 6;
        }


        FaceMovementDirection(movement, deltaTime);

    }


    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnTarget;
        stateMachine.InputReader.JumpEvent -= OnJump;
    }

    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
    }

    private void OnTarget()
    {
        if (!stateMachine.Targeter.SelectTarget()) { return; }

        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }
    
    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation,
            Quaternion.LookRotation(movement),
            deltaTime * stateMachine.RotationDamping);
    }

    private Vector3 CalculateMovement()
    {
        // Debug.Log("Input: " + stateMachine.InputReader.MovementValue);

        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        // Debug.Log("Forward Vector: " + forward);
        // Debug.Log("Right Vector: " + right);

        forward.y = 0f;
        right.y = 0f;


        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.InputReader.MovementValue.y + right * stateMachine.InputReader.MovementValue.x;
    }
}
