using System.Collections.Generic;
using UnityEngine;

public class MainGameState : GameState
{
    private StartMenuController _startMenuController;
    private bool isFirstPlay = true;
    public MainGameState(StateManager stateManager, MainSystem mainSystem) : base(stateManager, mainSystem)
    {
        Debug.Log("MainGameState Constructor");
        ListenForEvent(EGameEvents.SetState, OnSetState);
    }

    private EventResult OnSetState(object eventData)
    {
        EventResult eventresult = new EventResult(false);

        return eventresult;
    }

    public override void Init()
    {
        Debug.Log("MainGameState Init");
    }

    public override void Enter()
    {
        _startMenuController = Services.Get<UIManager>().OpenWorldUI<StartMenuController>("StartMenu");
        if (isFirstPlay)
        {
            _startMenuController.NewGameButton.onClick.AddListener(FirstPlay);
            //_startMenuController.NewGameButton.onClick.AddListener(InterView);
            isFirstPlay = false;
        }
        else
        {
            _startMenuController.NewGameButton.onClick.AddListener(NormalPlay);
        }

        //FF36
        //_startMenuController.FF36Button.onClick.AddListener(()=> {
        //    InterView();
        //});
    }

    private void FirstPlay()
    {
        //Demo
        /*Services.Get<ActionManager>().ChangeGameData(
            new string[] {
                    "GameData/NewDemo/chapterData",
                    "GameData/NewDemo/cluesData",
                    "GameData/NewDemo/npcData",
                    "GameData/NewDemo/characterNameListData" });
        Services.Get<StoryManager>().ChangeStoryController("Story/StoryController_NewDemo");*/
        Services.Get<DataManager>().InitPlayerData("New Player");

        //測試 直接接一個任務 任務名字: testMission, 目標名字: Castro
        List<int> talkChapterNums = new List<int>();
        talkChapterNums.Add(2);
        string missionName = "testMission";
        Services.Get<DataManager>().AddMissionData(missionName, talkChapterNums);

        Services.Get<UIManager>().OpenUI<TransitionWindowController>("TransitionWindow").StartTransition(
            FirstTransitionFadeInCallback,
            FirstTransitionFadeOutCallback
        );
    }

    private void FirstTransitionFadeInCallback()
    {
        Services.Get<UIManager>().CloseUI(_startMenuController.gameObject);
        //Services.Get<ActionManager>().SetBg("CG_BGtest_seasideST01");
    }

    private void FirstTransitionFadeOutCallback()
    {
        /*
        Services.Get<ActionManager>().StartChapterAction(MainSystem.gameConfig.newDemoFirstStoryIndex, () =>
        {
            Debug.Log(MainSystem.gameConfig.newDemoFirstStoryIndex + " 對話結束");
           
            //TODO 要改成用Action 切換State
            StateManager.ChangeState(new MapGameState(StateManager, MainSystem));
        });
        */
    }

    private void NormalPlay()
    {
        Services.Get<StoryManager>().ChangeStoryController("StoryController_NewGame");
        Services.Get<UIManager>().OpenUI<TransitionWindowController>("TransitionWindow").StartTransition(
            TransitionFadeInCallback,
            TransitionFadeOutCallback
            );
    }

    private void TransitionFadeInCallback()
    {
        Services.Get<UIManager>().CloseUI(_startMenuController.gameObject);
    }

    private void TransitionFadeOutCallback()
    {
    }

    public override void Destroy()
    {
        Debug.Log("MainGameState Destroy");
        _startMenuController?.NewGameButton.onClick.RemoveAllListeners();
        base.Destroy();
    }
}
