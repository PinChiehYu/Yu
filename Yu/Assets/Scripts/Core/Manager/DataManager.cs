using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager
{
    private PlayerData playerData;

    public PlayerData PlayerData
    {
        get => playerData;
        set => playerData = value;
    }

    public string CurrentMissionName { get => currentMissionName;}

    private string currentMissionName;

    //Init player data
    public void InitPlayerData(string playerName)
    {
        playerData = new PlayerData(playerName);
    }

    public void SetPlayerData(PlayerData oldPlayerData)
    {
        if (oldPlayerData != null)
        {
            playerData = oldPlayerData;
        }
    }

    public void AddMissionData(string missionName, List<int> talkChapterNums)
    {
        currentMissionName = missionName;
        MissionData missionData = new MissionData(missionName, talkChapterNums);
        if (!playerData.missionDatas.Contains(missionData)){
            playerData.missionDatas.Add(missionData);
        }
    }

    public void AddTargetDataToMission(TargetData targetData, string refMissionName)
    {
        MissionData missionData = playerData.missionDatas.Find(name => name.missionName == refMissionName);
        if(missionData != null)
        {
            missionData.targetDatas.Add(targetData);
        }
    }

    public TargetData GetCurrentTargetData(string refMissionName)
    {
        MissionData missionData = playerData.missionDatas.Find(name => name.missionName == refMissionName);
        TargetData targetData = missionData.targetDatas[0];
        return targetData;
    }

    public void SetCurrentTargetData(string refMissionName, string characteristic, string thought)
    {
        MissionData missionData = playerData.missionDatas.Find(name => name.missionName == refMissionName);
        TargetData targetData = missionData.targetDatas[0];
        targetData.Characteristic = characteristic;
        targetData.Thought = thought;
    }

    public void Save()
    {
        TargetData targetData = GetCurrentTargetData("testMission");
        string targetDatajson = JsonUtility.ToJson(targetData, true);
        Debug.Log("targetData = " + targetDatajson);
    }

    public bool HasTalked(int value)
    {
        return PlayerData.completedTalkChapterNums.Contains(value) ? true : false;
    }

    public void SetTalked(int value)
    {
        if (!PlayerData.completedTalkChapterNums.Contains(value))
        {
            PlayerData.completedTalkChapterNums.Add(value);
        }
    }

    public void SetClueHaveGotten(string clueName)
    {
        if (string.IsNullOrEmpty(currentMissionName))
            return;
        MissionData missionData = playerData.missionDatas.Find(name => name.missionName == currentMissionName);
        foreach(var td in missionData.targetDatas)
        {
            ClueState cs = td.ClueStates.Find(state => state.Name == clueName);
            if (cs != null)
            {
                cs.HasGotten = true;
                return;
            }
        }
    }

    public void SetProperty(string key, int value)
    {
        if (!PlayerData.PropertyDic.ContainsKey(key))
        {
            PlayerData.PropertyDic[key] = value;
        }
        else
        {
            PlayerData.PropertyDic[key] += value;
        }
    }

    public string GetInterViewResult()
    {
        string result = "";
        if (PlayerData.PropertyDic.ContainsKey("interViewPoint_A") && PlayerData.PropertyDic.ContainsKey("interViewPoint_B"))
        {
            string _result = PlayerData.PropertyDic["interViewPoint_A"] > PlayerData.PropertyDic["interViewPoint_B"] ? "A" : "B";
            result += _result;
        }
        if (PlayerData.PropertyDic.ContainsKey("interViewPoint_C") && PlayerData.PropertyDic.ContainsKey("interViewPoint_D"))
        {
            string _result = PlayerData.PropertyDic["interViewPoint_C"] > PlayerData.PropertyDic["interViewPoint_D"] ? "C" : "D";
            result += _result;
        }
        return result;
    }

    public void SetFavorability(string key, int value)
    {
        if (!PlayerData.Favorability.ContainsKey(key))
        {
            PlayerData.Favorability[key] = value;
        }
        else
        {
            PlayerData.Favorability[key] += value;
        }
    }

    public int GetFavorability(string key)
    {
        return PlayerData.Favorability.ContainsKey(key) ? PlayerData.Favorability[key] : 0;
    }

    public void Reset()
    {
        playerData = null;
    }
}
