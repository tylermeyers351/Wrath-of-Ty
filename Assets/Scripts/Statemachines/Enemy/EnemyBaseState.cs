using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected bool IsInChaseRange()
    {
        Vector3 playerPos = stateMachine.Player.transform.position;
        Vector3 enemyPos = stateMachine.transform.position;

        float distanceBetween = Vector3.Distance(playerPos, enemyPos);
        float playerChasingRange = stateMachine.PlayerChasingRange;

        if (distanceBetween <= playerChasingRange)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
