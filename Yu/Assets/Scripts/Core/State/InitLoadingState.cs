using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitLoadingState : GameState
{
    private long STATE_INIT_LOADING;

    public InitLoadingState(StateManager stateManager, MainSystem mainSystem) : base(stateManager, mainSystem)
    {
        //if (Tools.Network.CheckNetwork())
        //{
        //    Setup();
        //}
        Setup();

    }
    private void Setup()
    {
#if DEBUG_UI
#endif
        
        STATE_INIT_LOADING = TaskManager.CreateState();

        //MainUiManager.Instance.ResetUISystem();

        TaskManager.AddTask(new InitLoadingTask(), TaskManager.AllStates());
        ListenForEvent(EGameEvents.StartupInitFinished, OnInitFinished);
    }

    private EventResult OnInitFinished(object eventData)
    {
        StateManager.ChangeState(new MainGameState(StateManager, MainSystem));
        return null;
    }

    public override void Init()
    {
        TaskManager.ChangeState(STATE_INIT_LOADING);
    }

    public override void Enter()
    {
    }
}