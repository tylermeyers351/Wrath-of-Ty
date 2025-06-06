using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    private State currentState;

    public void SwitchState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    private void Update()
    {
        // Null conditional operator (doesn't work with monobehaviors and scriptable objects)
        currentState?.Tick(Time.deltaTime);
    }

}
