using UnityEngine;

public class PlayerTestState : PlayerBaseState
{

    private float countdown = 4f;
    private int lastPrintedSecond;

    public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Enter");
    }

    public override void Tick(float deltaTime)
    {
        countdown -= Time.deltaTime;

        int currentSecond = Mathf.FloorToInt(countdown);
        if (currentSecond != lastPrintedSecond && countdown > 0)
        {
            Debug.Log("Tick Remaining: " + currentSecond);
            lastPrintedSecond = currentSecond;
        }

        if (countdown <= 0)
        {
            stateMachine.SwitchState(new PlayerTestState(stateMachine));
        }
        
    
    }

    public override void Exit()
    {
        Debug.Log("Exit");
    }
}
