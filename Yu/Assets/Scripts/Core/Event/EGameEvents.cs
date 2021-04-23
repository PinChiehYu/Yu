public enum EGameEvents
{
    Invalid = 200,

    GameInitStart,
    GameInitFinished,
    StartupInitFinished,

    GameStart,
    GameOver,
    
    MainGame_Update,
    MainGame_FixedUpdate,
    MainGame_LateUpdate,
    ToNpcSniper,

    SetSniperGame,
    SetSniperGameResult,
    EnterSniperGameState,
    LeaveSniperGameState,

    EnterLustSniperGameState,

    EnterHintwallState,
    LeaveHintwallState,
    SetLeaveHintwallStateMethod,

    OpenBulletTableUI,
    CloseBulletTableUI,

    StartWaveGame,
    StopWaveGame,
    SwitchTargetMode,
    SniperGameEnd,
    SetSniperGamePause,
    SniperGameEndFinished,
    SetLeaveSniperGameStateMethod,

    GotTheClue,
    GotTheClueAfterNpcTalked,
    TalkToNpc,
    TransferMap,
    ChangedState,
    OpenUI,
    Action,

    SetPlayerControl,
    SetPlayerCrorch,

    SetFishRunAway,

    SetSniperGameAccessPermission,
    SetBulletTableAccessPermission,
    SetHintwallStateAccessPermission,
    SetDoorAccessPermission,
    SetNpcAccessPermission,
    SetClueAccessPermission,
    SetBigMapBtnAccessPermission,
    SetUIButtonAccessPermission,
    SetUIButtonHighLight,
    SetMapCMCamera,
    SetCurrentPlace,
    SetState,
    SelectPlaceTo,

    UpdateKarmaValue,
    SetKarmaValue,
    SetKarmaValueRange,
    SetKarmaValuePause,
    ShowLust,
    StartLustFight,
    OpenSurroundingAnimator,

    MapGameEnter,
    MapGameEnd,

    TalkToRedboxNPC,
    RedboxSwitchArea,

    SetFishWaveGameUI,
    SetFishWarningState,
    SetFishDangerState,
    FishGameResult,
    WaveEndBoundy,

    ShowMapItemUI,
}
