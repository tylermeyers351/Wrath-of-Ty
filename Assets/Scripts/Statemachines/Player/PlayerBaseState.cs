using Mono.Cecil.Cil;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        Vector3 vertMovement = stateMachine.ForceReceiver.Movement;
        stateMachine.Controller.Move((motion + vertMovement) * deltaTime);
    }
    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void FaceTarget()
    {
        if (stateMachine.Targeter.CurrentTarget == null) { return; }

        Vector3 targetVect = stateMachine.Targeter.CurrentTarget.transform.position;
        Vector3 playerVect = stateMachine.transform.position;

        Vector3 lookPos = targetVect - playerVect;

        lookPos.y = 0;

        stateMachine.transform.rotation = Quaternion.LookRotation(lookPos);

    }
}
