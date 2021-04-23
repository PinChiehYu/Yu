using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class ActionManager
{
    /*
    private QsScript<QsData> _chapterData;
    private QsData[] _chapterDataDatas;
    private QsScript<QsData> _cluesData;
    private QsData[] _cluesDataDatas;
    private QsScript<QsData> _npcData;
    private QsData[] _npcDataDatas;

    private QsScript<QsData_NameList> _characterNameListData;
    private QsData_NameList[] _characterNameListDataDatas;

    private GameObject talkGameObject;
    private TalkController talkController;

    public ActionManager(string[] gameDataFilePaths)
    {
        ChangeGameData(gameDataFilePaths);

        talkController = Services.Get<UIManager>().OpenUI<TalkController>("TalkController");
        foreach(var go in _characterNameListDataDatas)
        {
            talkController.SetCharacterDataList(go.Code, go.Chinesename);
        }
    }

    public void ChangeGameData(string[] gameDataFilePaths)
    {
        _chapterData = Services.Get<ResourcesManager>().GetAsset<QsScript<QsData>>(gameDataFilePaths[0]);
        _chapterDataDatas = _chapterData.dataArray;
        _cluesData = Services.Get<ResourcesManager>().GetAsset<QsScript<QsData>>(gameDataFilePaths[1]);
        _cluesDataDatas = _cluesData.dataArray;
        _npcData = Services.Get<ResourcesManager>().GetAsset<QsScript<QsData>>(gameDataFilePaths[2]);
        _npcDataDatas = _npcData.dataArray;
        _characterNameListData = Services.Get<ResourcesManager>().GetAsset<QsScript<QsData_NameList>>(gameDataFilePaths[3]);
        _characterNameListDataDatas = _characterNameListData.dataArray;
    }

    public void StartChapterAction(int chapterIndex, Action callback = null)
    {
        talkController.SetActionData(CreateActionDatas(chapterIndex, _chapterDataDatas), callback);
        talkController.StartTalk();

        talkController.insertAction = InsertChapterData;
    }

    private void InsertChapterData(int chapterIndex)
    {
        talkController.SetActionData(CreateActionDatas(chapterIndex, _chapterDataDatas));
    }

    public void StartClueAction(int chapterIndex, Action callback = null)
    {
        talkController.SetActionData(CreateActionDatas(chapterIndex, _cluesDataDatas), callback);
        talkController.StartTalk();

        talkController.insertAction = InsertClueData;
    }

    private void InsertClueData(int chapterIndex)
    {
        talkController.SetActionData(CreateActionDatas(chapterIndex, _cluesDataDatas));
    }

    public void StartNPCAction(int chapterIndex, Action callback = null)
    {
        talkController.SetActionData(CreateActionDatas(chapterIndex, _npcDataDatas), callback);
        talkController.StartTalk();

        talkController.insertAction = InsertNpcData;
    }

    private void InsertNpcData(int chapterIndex)
    {
        talkController.SetActionData(CreateActionDatas(chapterIndex,_npcDataDatas));
    }

    public void SetBg(string bgName)
    {
        talkController.SetBg(bgName);
    }

    public void SetBg(int chapterIndex)
    {
        //TO DO
        //用chapterIndex開bg
        //talkController.SetBg(bgName);
    }

    public void Close()
    {

    }

    private ActionData[] CreateActionDatas(int chapterIndex, QsData[] datas)
    {
        QsData[] data = Array.FindAll(datas, d => d.Index == chapterIndex);
        ActionData[] _actionDatas = new ActionData[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            _actionDatas[i] = CreateActionData(
                data[i].Action,
                data[i].Arg1,
                data[i].Arg2,
                data[i].Arg3,
                data[i].Arg4,
                data[i].Arg5,
                data[i].Arg6,
                data[i].Arg7
                );
        }
        return _actionDatas;
    }

    private ActionData CreateActionData(
    string Action,
    string Arg1,
    string Arg2,
    string Arg3,
    string Arg4,
    string Arg5,
    string Arg6,
    string Arg7
    )
    {
        ActionData actionData = new ActionData();
        actionData.Action = Action;
        actionData.Args.Add(Arg1);
        actionData.Args.Add(Arg2);
        actionData.Args.Add(Arg3);
        actionData.Args.Add(Arg4);
        actionData.Args.Add(Arg5);
        actionData.Args.Add(Arg6);
        actionData.Args.Add(Arg7);
        return actionData;
    }
    */
}