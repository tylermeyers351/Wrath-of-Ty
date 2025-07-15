using System.Collections;
using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    private float timeToQuit = 5f;
    private float timer = 0f;

    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.ragdoll.ToggleRagdoll(true);
        stateMachine.Weapon.gameObject.SetActive(false);
    }


    public override void Tick(float deltaTime)
    {
        timer += deltaTime;
        if (timer > timeToQuit)
        {
            QuitGame();
        }
    }

    public override void Exit() { }

    private void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
