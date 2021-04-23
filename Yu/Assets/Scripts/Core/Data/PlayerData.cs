using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string playerName = string.Empty;
    public int money = 2000;
    public List<TipData> tipDatas;
    public BulletData RealBulletData;
    public List<MissionData> missionDatas;
    public List<int> completedTalkChapterNums = new List<int>();
    public Dictionary<string, int> PropertyDic = new Dictionary<string, int>();
    public Dictionary<string, int> Favorability = new Dictionary<string, int>();

    public PlayerData(string playerName)
    {
        this.playerName = playerName;
        tipDatas = new List<TipData>();
        RealBulletData = new BulletData();
        missionDatas = new List<MissionData>();
    }
}

[System.Serializable]
public class TipData
{
    public string tipName = string.Empty;
    public string tipType = string.Empty;
}

public enum BulletType
{
    Real,
    Correction,
}

[System.Serializable]
public class BulletData
{
    public string bulletName = string.Empty;
    public BulletType bulletType = BulletType.Real;
    public int bulletCount = -1;
}

[System.Serializable]
public class MissionData
{
    public string missionName = string.Empty;
    public List<TargetData> targetDatas;
    public List<int> talkChapterNums = new List<int>();
    public int talkIndex = 0;
    public bool isFinished = false;

    public MissionData(string missionName, List<int> talkChapterNums)
    {
        this.missionName = missionName;
        this.targetDatas = new List<TargetData>();
        this.talkChapterNums = talkChapterNums;
    }
}

[System.Serializable]
public class TargetData
{
    public string targetName = string.Empty;
    public string correctBulletType = string.Empty;
    public List<ClueState> ClueStates = new List<ClueState>();
    public List<GroupClueState> GroupClueStates = new List<GroupClueState>();
    public string Characteristic;
    public string Thought;
}

[System.Serializable]
public class ClueState
{
    public string Name;
    public bool HasGotten;
    public bool HasSelected;

    public ClueState(string name, bool hasGotten, bool hasSelected = false)
    {
        Name = name;
        HasGotten = hasGotten;
        HasSelected = hasSelected;
    }
}

[System.Serializable]
public class GroupClueState
{
    public bool HasSelected;

    public List<ClueState> ClueStates;

    public GroupClueState(bool hasSelected = false)
    {
        HasSelected = hasSelected;
        ClueStates = new List<ClueState>();
    }
}