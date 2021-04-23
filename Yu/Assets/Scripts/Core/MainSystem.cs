using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MainSystem : Singleton<MainSystem>
{
    
    private const int CHECK_NETWORK_EVERY_X_FRAME = 30;
    private static bool m_servicesInited = false;
    private EventManager m_eventManager;
    private StateManager m_stateManager;
    public StateManager StateManager
    {
        get { return m_stateManager; }
    }
    private StoryManager m_storyManager;
    public StoryManager StoryManager
    {
        get { return m_storyManager; }
    }
    protected GameEventListener m_eventListener;

    private int m_frame = -1;
    private float m_deltaTime;

    public Canvas worldCanvas;
    public Canvas canvas;
    public Cinemachine.CinemachineBrain cinemachineBrain;
    public GameObject crosshairController;
    public GameObject audioController;
    public GameConfig gameConfig;

    private bool switchSettingUIController = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!m_servicesInited)
        {
            m_eventManager = new EventManager();
            m_stateManager = new StateManager();

            m_eventListener = new GameEventListener();

            Services.Set<EventManager>(m_eventManager);
            Services.Set<UIManager>(new UIManager(worldCanvas, canvas));
            Services.Set<ResourcesManager>(new ResourcesManager());
            Services.Set<DataManager>(new DataManager());
            //Services.Set<ActionManager>(new ActionManager(gameConfig.gameDataFilePaths));

            Services.Set<StoryManager>(m_storyManager);
        }

        m_eventListener.ListenForEvent(EGameEvents.GotTheClue, OnGotTheClue);
        m_eventListener.ListenForEvent(EGameEvents.TalkToNpc, OnTalkToNpc);
        m_eventListener.ListenForEvent(EGameEvents.TransferMap, OnTransferMap);
        m_eventListener.ListenForEvent(EGameEvents.ChangedState, OnChangedState);
        m_eventListener.ListenForEvent(EGameEvents.OpenUI, OnOpenUI);
        m_eventListener.ListenForEvent(EGameEvents.Action, OnAction);

        m_eventListener.ListenForEvent(EGameEvents.SniperGameEndFinished, OnSniperGameEndFinished);

        m_stateManager.ChangeState(new InitLoadingState(m_stateManager, this));
    }

    private EventResult OnGotTheClue(object eventData)
    {
        EventResult eventresult = new EventResult(false);
        m_storyManager.ClueProcess(eventData);

        //TODO 在DataManager Set
        Services.Get<DataManager>().SetClueHaveGotten((string)eventData);

        return eventresult;
    }

    private EventResult OnTalkToNpc(object eventData)
    {
        EventResult eventresult = new EventResult(false);
        m_storyManager.NPCProcess(eventData);
        return eventresult;
    }

    private EventResult OnTransferMap(object eventData)
    {
        EventResult eventresult = new EventResult(false);
        m_storyManager.DoorProcess(eventData);
        return eventresult;
    }

    private EventResult OnChangedState(object eventData)
    {
        EventResult eventresult = new EventResult(false);
        m_storyManager.StateProcess(eventData);
        return eventresult;
    }

    private EventResult OnOpenUI(object eventData)
    {
        EventResult eventresult = new EventResult(false);
        m_storyManager.OpenUIProcess(eventData);
        return eventresult;
    }

    private EventResult OnAction(object eventData)
    {
        EventResult eventresult = new EventResult(false);
        m_storyManager.ActionProcess(eventData);
        return eventresult;
    }

    private EventResult OnSniperGameEndFinished(object eventData)
    {
        StateManager.ChangeState(new MainGameState(m_stateManager, this));
        m_storyManager.Reset();
        Services.Get<DataManager>().Reset();
        return null;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F12))
        {
            switchSettingUIController = !switchSettingUIController;
            if (switchSettingUIController)
            {
                Services.Get<UIManager>().OpenUI<SettingUIController>("SettingUIController").gameObject.SetActive(true);
            }
            else
            {
                Services.Get<UIManager>().OpenUI<SettingUIController>("SettingUIController").gameObject.SetActive(false);
            }
        }

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    Services.Get<DataManager>().Save();
        //}

        // //test 作弊按鈕
        // if (Input.GetKeyDown(KeyCode.P))
        // {//西門
        //     m_storyManager.SetFlowVarBool("XimendingMissionFinished", true);
        // }

        // if (Input.GetKeyDown(KeyCode.O))
        // {//關渡
        //     m_storyManager.SetFlowVarBool("GuanduMissionFinished", true);
        // }

        // if (Input.GetKeyDown(KeyCode.I))
        // {//第一次合子彈
        //     m_storyManager.SetFlowVarBool("firstSuccessfullyMadeBullet", true);
        // }

        //TO DO
        //multiple language
        //LocalizationManager.Update();

        m_frame++;
        if (m_frame % CHECK_NETWORK_EVERY_X_FRAME == 0)
        {
            //TO DO
            //CkeckNetwork();
        }

        m_deltaTime = Time.deltaTime;
#if UNITY_EDITOR
#if UNITY_5_5_OR_NEWER
        UnityEngine.Profiling.Profiler.BeginSample("State Manager  Update");
#else
			Profiler.BeginSample("State Manager  Update");
#endif
#endif

        if (m_stateManager != null)
        {
            m_stateManager.Update(m_deltaTime);
        }

#if UNITY_EDITOR
#if UNITY_5_5_OR_NEWER
        UnityEngine.Profiling.Profiler.EndSample();
        UnityEngine.Profiling.Profiler.BeginSample("Input Manager  Update");
#else
			Profiler.EndSample();
			Profiler.BeginSample("Input Manager  Update");
#endif
#endif

        //TO DO
        //other input?
        //if (m_inputMgr != null)
        //{
        //    m_inputMgr.Update(m_deltaTime);
        //}

#if UNITY_EDITOR
#if UNITY_5_5_OR_NEWER
        UnityEngine.Profiling.Profiler.EndSample();
#else
			Profiler.EndSample();
#endif
#endif

        if (m_eventManager != null)
        {
            m_eventManager.SendEvent((int)EGameEvents.MainGame_Update, Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        if (m_eventManager != null)
        {
            m_eventManager.SendEvent((int)EGameEvents.MainGame_FixedUpdate, Time.deltaTime);
        }
    }

    void LateUpdate()
    {
        if (m_eventManager != null)
        {
            m_eventManager.SendEvent((int)EGameEvents.MainGame_LateUpdate, Time.deltaTime);
        }
    }
}
