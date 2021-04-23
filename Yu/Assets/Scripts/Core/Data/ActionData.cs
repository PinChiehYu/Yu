using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionData
{
    public List<string> Args = new List<string>();
    public string Action;
    public string Arg1;
    public string Arg2;
    public string Arg3;
    public string Arg4;
    public string Arg5;
    public string Arg6;
    public string Arg7;
}

public class ActionKey
{
    public static readonly string Say = "say";
    public static readonly string Choice = "choice";
    public static readonly string SetProperty = "setProperty";
    public static readonly string Operation = "operation";
    public static readonly string ThoughtControl = "thoughtControl";
    public static readonly string SetButton = "setButton";
    public static readonly string SetPlace = "setPlace";
    public static readonly string SetKarmaRange = "setKarmaRange";
    public static readonly string SetCharacterPos = "setCharacterPos";
    public static readonly string shake = "shake";
    public static readonly string zoom = "zoom";
    public static readonly string SetFavorability = "setFavorability";
    public static readonly string SetBoard = "setBoard";
    public static readonly string GetItem = "getItem";
    public static readonly string SetHighLight = "setHighLight";
    public static readonly string SetFade = "setFade";
    public static readonly string SetPPT = "setPPT";
    public static readonly string SetSniperGame = "setSniperGame";
    public static readonly string SetVideo = "setVideo";
}

public class OperationKey
{
    public const string ShowInterViewResult = "showInterViewResult";
    public const string Open = "open";
}

public enum SayArgument
{
    leftRoleName = 0,
    leftRoleMood,
    rightRoleName,
    rightRoleMood,
    characterName,
    content,
    bgImageName,
}

public enum ButtonArgument
{
    buttonText = 0,
    nextIndex,
    waitForClick,
}

public enum PropertyArgument
{
    propertyKey = 0,
    propertyValue,
}

public enum OperationArgument
{
    operationKey = 0,
}

public enum OpenArgument
{
    openMapName = 1,
}

public enum UIButtonArgument
{
    buttonName = 0,
    buttonValue
}

public enum PlaceArgument
{
    placeName = 0,
}

public enum KarmaRangeArgument
{
    minValue = 0,
    maxValue
}

public enum CharacterPosArgument
{
    duringTime = 0,
    delayWhileDone,
    character,
    pos,
    behind
}

public enum ShakeArgument
{
    duringTime = 0,
    delayWhileDone,
    character,
    amplitude,
    zRotationAmplitude,
}

public enum ZoomArgument
{
    duringTime = 0,
    delayWhileDone,
    target,
    scaleDelta,
}

public enum FavorabilityArgument
{
    characterName = 0,
    value
}

public enum BoardArgument
{
    duringTime = 0,
    delayWhileDone,
    target,
    position,
    alpha
}

public enum GetItemArgument
{
    itemName = 0
}

public enum HighLightArgument
{
    buttonName = 0
}

public enum FadeArgument
{
    fadeInTime = 0,
    fadeOutTime,
    stayTime,
    bgImageName = 6
}

public enum PPTArgument
{
    PPTName = 0
}

public enum VideoArgument
{
    VideoName = 0
}

public class CharacterData
{
    public string code;
    public string Charactername;
}
