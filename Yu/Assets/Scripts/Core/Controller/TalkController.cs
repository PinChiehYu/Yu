using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;
using System;

public class TalkController : MonoBehaviour
{
    public Flowchart flowchart;
    public Image topBoard;
    public Image bottomBoard;
    public Image bgImage;
    public Image fadeImage;
    public Animator animBG;
    public Image leftImage;
    public Transform BehindSayDialog;
    public Transform InFrontOfSayDialog;
    //public Vibrator leftVibrator;
    public Image rightImage;
    //public Vibrator rightVibrator;
    public Text characterNameText;
    public GameObject blockInput;

    private ActionData[] _actionDatas = null;
    private Action _callback;
    private int _actionIndex = 0;

    private List<CharacterData> CharacterDataList = new List<CharacterData>();

    private List<int> BtnIndexList = new List<int>();

    public delegate void InsertAction(int index);
    public InsertAction insertAction;

    private const string animBgString = "anim_";

    void Awake()
    {
        flowchart = GetComponentInChildren<Flowchart>();
    }

    void Start()
    {
        leftImage.gameObject.SetActive(false);
        rightImage.gameObject.SetActive(false);
        blockInput.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            BtnClicked(0);
        }
    }

    public void SetCharacterDataList(string code,string name)
    {
        CharacterData chData = new CharacterData();
        chData.code = code;
        chData.Charactername = name;
        CharacterDataList.Add(chData);
    }

    public void StartTalk(Action onComplete = null)
    {
        flowchart.SetBooleanVariable("Finished", false);
        Services.Get<StoryManager>().SetFlowVarBool("inDialogue", true);
        Services.Get<EventManager>().SendEvent((int)EGameEvents.SetPlayerControl, false);
        Services.Get<EventManager>().SendEvent((int)EGameEvents.SetKarmaValuePause, true);
        blockInput.SetActive(true);
        StartCoroutine(Execute(onComplete));
    }

    public IEnumerator Execute(Action onComplete = null)
    {
        if (onComplete != null)
        {
            _callback = onComplete;
        }
        yield return new WaitForEndOfFrame();
        flowchart.StopBlock("Action");
        yield return new WaitForEndOfFrame();
        flowchart.ExecuteBlock("Action");
    }

    public void SetBg(string bgName)
    {
        if (string.IsNullOrEmpty(bgName) || bgName == "none")
        {
            bgImage.gameObject.SetActive(false);
            return;
        }

        if (bgName.Contains(animBgString))
        {
            animBG.SetTrigger(bgName.Substring(animBgString.Length));
            return;
        }
        else
            animBG.SetTrigger("None");
            
        bgImage.gameObject.SetActive(true);
        if(bgImage.sprite == null || bgName != bgImage.sprite.name)
        {
            Sprite _sp = Services.Get<ResourcesManager>().GetAsset<Sprite>("Tex/" + bgName);
            if (_sp)
            {
                bgImage.sprite = _sp;
            }
            else
            {
                Debug.LogError("cant find the image by this bg name! name is " + bgName);
                bgImage.gameObject.SetActive(false);
            }
        }
    }

    public void SetActionData(ActionData[] data, Action callback = null)
    {
        if (_actionDatas != null)
        {
            List<ActionData> tempActionDatas = new List<ActionData>();
            for (int i = 0; i < data.Length; i++)
            {
                tempActionDatas.Add(data[i]);
            }
            if (_actionIndex != _actionDatas.Length - 1)
            {
                for (int i = _actionIndex + 1; i < _actionDatas.Length; i++)
                {
                    tempActionDatas.Add(_actionDatas[i]);
                }
            }
            _actionDatas = tempActionDatas.ToArray();
            _actionIndex = 0;
        }
        else
        {
            _actionDatas = data;
        }
 
        if(callback != null)
        {
            _callback = callback;
        }
    }

    public void SetNextAction()
    {
        //if the next action is say
        if (_actionDatas[_actionIndex].Action == ActionKey.Say)
        {
            setTalk();
            flowchart.ExecuteBlock(ActionKey.Say);
        }
        else if (_actionDatas[_actionIndex].Action == ActionKey.Choice)
        {
            setBtn();
            flowchart.ExecuteBlock(ActionKey.Choice);
        }
        else if (_actionDatas[_actionIndex].Action == ActionKey.SetProperty)
        {
            setProperty();
            flowchart.ExecuteBlock(ActionKey.SetProperty);
        }
        else if (_actionDatas[_actionIndex].Action == ActionKey.Operation)
        {
            setOperation();
            flowchart.ExecuteBlock(ActionKey.Operation);
        }
        else if (_actionDatas[_actionIndex].Action == ActionKey.ThoughtControl)
        {
            setThoughtControl();
            flowchart.ExecuteBlock(ActionKey.ThoughtControl);
        }
        else if (_actionDatas[_actionIndex].Action == ActionKey.SetButton)
        {
            setButton();
            flowchart.ExecuteBlock(ActionKey.SetButton);
        }
        else if (_actionDatas[_actionIndex].Action == ActionKey.SetPlace)
        {
            setPlace();
            flowchart.ExecuteBlock(ActionKey.SetPlace);
        }
        else if (_actionDatas[_actionIndex].Action == ActionKey.SetKarmaRange)
        {
            setKarmaRange();
            flowchart.ExecuteBlock(ActionKey.SetKarmaRange);
        }
        else if (_actionDatas[_actionIndex].Action == ActionKey.SetCharacterPos)
        {
            setCharacterPos();
            flowchart.ExecuteBlock(ActionKey.SetCharacterPos);
        }
        else if (_actionDatas[_actionIndex].Action == ActionKey.shake)
        {
            shake();
            flowchart.ExecuteBlock(ActionKey.shake);
        }
        else if (_actionDatas[_actionIndex].Action == ActionKey.zoom)
        {
            zoom();
            flowchart.ExecuteBlock(ActionKey.zoom);
        }
        else if (_actionDatas[_actionIndex].Action == ActionKey.SetFavorability)
        {
            setFavorability();
            flowchart.ExecuteBlock(ActionKey.SetFavorability);
        }
        else if (_actionDatas[_actionIndex].Action == ActionKey.SetBoard)
        {
            setBoard();
            flowchart.ExecuteBlock(ActionKey.SetBoard);
        }
        else if (_actionDatas[_actionIndex].Action == ActionKey.GetItem)
        {
            getItem();
            flowchart.ExecuteBlock(ActionKey.GetItem);
        }
        else if (_actionDatas[_actionIndex].Action == ActionKey.SetHighLight)
        {
            setHighLight();
            flowchart.ExecuteBlock(ActionKey.SetHighLight);
        }
        else if (_actionDatas[_actionIndex].Action == ActionKey.SetFade)
        {
            setFade();
            flowchart.ExecuteBlock(ActionKey.SetFade);
        }
        else if (_actionDatas[_actionIndex].Action == ActionKey.SetPPT)
        {
            flowchart.ExecuteBlock(ActionKey.SetPPT);
        }
        else if (_actionDatas[_actionIndex].Action == ActionKey.SetSniperGame)
        {
            setSniperGame();
            flowchart.ExecuteBlock(ActionKey.SetSniperGame);
        }
        else if (_actionDatas[_actionIndex].Action == ActionKey.SetVideo)
        {
            setVideo();
            flowchart.ExecuteBlock(ActionKey.SetVideo);
        }
        else
        {
            Debug.LogError(_actionDatas[_actionIndex].Action + " is invalid value!");
            flowchart.ExecuteBlock("InvalidAction");
        }
    }

    private void setTalk()
    {
        SetBg(_actionDatas[_actionIndex].Args[(int)SayArgument.bgImageName]);

        flowchart.SetStringVariable("Dialogue", _actionDatas[_actionIndex].Args[(int)SayArgument.content]);

        setRoleImage(_actionDatas[_actionIndex]);
        setCharactername(_actionDatas[_actionIndex]);
    }

    private void setBtn()
    {
        Debug.Log("setBtn");
        BtnIndexList.Add(Int32.Parse(_actionDatas[_actionIndex].Args[(int)ButtonArgument.nextIndex]));

        //get and set btn name by _actionDatas[_actionIndex].Args[(int)ActionArgument.buttonText]
        //btnView.SetBtn(_actionDatas[_actionIndex].Args[(int)ButtonArgument.buttonText]);

        if (_actionDatas[_actionIndex].Args[(int)ButtonArgument.waitForClick] == "yes" ||
            _actionIndex == _actionDatas.Length - 1
            )
        {
            return;
        }

        if (_actionDatas[_actionIndex + 1].Action != "choice")
        {
            return;
        }
        else if (_actionDatas[_actionIndex + 1].Action == "choice")
        {
            //setBtn again if the action is choice after increase _actionIndex;
            _actionIndex++;
            //Recursive
            setBtn();
        }
    }

    private void setProperty()
    {
        Services.Get<DataManager>().SetProperty(_actionDatas[_actionIndex].Args[(int)PropertyArgument.propertyKey], Int32.Parse(_actionDatas[_actionIndex].Args[(int)PropertyArgument.propertyValue]));
    }

    private void setOperation()
    {
        switch (_actionDatas[_actionIndex].Args[(int)OperationArgument.operationKey])
        {
            case OperationKey.ShowInterViewResult:
                switch (Services.Get<DataManager>().GetInterViewResult())
                {
                    case "AC":
                        insertAction?.Invoke(7001);
                        break;
                    case "AD":
                        insertAction?.Invoke(7002);
                        break;
                    case "BC":
                        insertAction?.Invoke(7003);
                        break;
                    case "BD":
                        insertAction?.Invoke(7004);
                        break;
                }
                break;
            case OperationKey.Open:
                if(_actionDatas[_actionIndex].Args[(int)OpenArgument.openMapName]== "Big Map")
                {
                    Services.Get<EventManager>().SendEvent((int)EGameEvents.SetState, "MapGameState");
                    Services.Get<EventManager>().SendEvent((int)EGameEvents.SelectPlaceTo, "Big Map");
                }
                break;
            default:
                break;
        }
    }

    private void setThoughtControl()
    {
        Services.Get<EventManager>().SendEvent((int)EGameEvents.ToNpcSniper, "NpcSniperGameState");
    }

    private void setButton()
    {
        object[] package = new object[2] { _actionDatas[_actionIndex].Args[(int)UIButtonArgument.buttonName], _actionDatas[_actionIndex].Args[(int)UIButtonArgument.buttonValue] };
        Services.Get<EventManager>().SendEvent((int)EGameEvents.SetUIButtonAccessPermission, package);
    }

    private void setPlace()
    {
        float.TryParse(_actionDatas[_actionIndex].Args[(int)ZoomArgument.duringTime], out float duringTime);
        bool.TryParse(_actionDatas[_actionIndex].Args[(int)ZoomArgument.delayWhileDone], out bool delayWhileDone);

        Services.Get<UIManager>().OpenUI<TransitionWindowController>("TransitionWindow").StartTransition(null,
            () => {
                //Services.Get<MapManager>().SelectPlaceTo(_actionDatas[_actionIndex].Args[(int)PlaceArgument.placeName]);
                flowchart.SetBooleanVariable("Delay", false);
            });
    }

    private void setKarmaRange()
    {
        object[] package = new object[2] { _actionDatas[_actionIndex].Args[(int)KarmaRangeArgument.minValue], _actionDatas[_actionIndex].Args[(int)KarmaRangeArgument.maxValue] };
        Services.Get<EventManager>().SendEvent((int)EGameEvents.SetKarmaValueRange, package);
    }

    private void setCharacterPos()
    {
        Image imageCache = _actionDatas[_actionIndex].Args[(int)CharacterPosArgument.character] == "right" ? rightImage : leftImage;
        int.TryParse(_actionDatas[_actionIndex].Args[(int)CharacterPosArgument.pos], out int targetValue);
        float.TryParse(_actionDatas[_actionIndex].Args[(int)CharacterPosArgument.duringTime], out float duringTime);
        bool.TryParse(_actionDatas[_actionIndex].Args[(int)CharacterPosArgument.delayWhileDone], out bool delayWhileDone);
        bool.TryParse(_actionDatas[_actionIndex].Args[(int)CharacterPosArgument.behind], out bool behind);
        flowchart.SetBooleanVariable("Delay", delayWhileDone);

        float startPosCache = imageCache.rectTransform.localPosition.x;
        float targetPosCache = 0;

        imageCache.transform.SetParent(behind ? BehindSayDialog : InFrontOfSayDialog);

        switch (targetValue)
        {
            case 1:
                targetPosCache = -480;
                break;
            case 2:
                targetPosCache = -360;
                break;
            case 3:
                targetPosCache = -260;
                break;
            case 4:
                targetPosCache = 0;
                break;
            case 5:
                targetPosCache = 260;
                break;
            case 6:
                targetPosCache = 360;
                break;
            case 7:
                targetPosCache = 480;
                break;
        }

        StartCoroutine(dialogueShow(duringTime, 
            (float percent) =>
            {
                imageCache.rectTransform.localPosition = Vector3.right * Mathf.Lerp(startPosCache, targetPosCache, percent);
            },
            () =>
            {
                flowchart.SetBooleanVariable("Delay", false);
            }));
    }

    private void shake()
    {
        //Vibrator vibratorCache = _actionDatas[_actionIndex].Args[(int)ShakeArgument.character] == "right" ? rightVibrator : leftVibrator;
        float.TryParse(_actionDatas[_actionIndex].Args[(int)ShakeArgument.duringTime], out float duringTime);
        float.TryParse(_actionDatas[_actionIndex].Args[(int)ShakeArgument.amplitude], out float amplitude);
        float.TryParse(_actionDatas[_actionIndex].Args[(int)ShakeArgument.zRotationAmplitude], out float zRotationAmplitude);
        bool.TryParse(_actionDatas[_actionIndex].Args[(int)ShakeArgument.delayWhileDone], out bool delayWhileDone);
        flowchart.SetBooleanVariable("Delay", delayWhileDone);

        //vibratorCache.Shake(duringTime, amplitude, zRotationAmplitude, () => { flowchart.SetBooleanVariable("Delay", false); });
    }

    private void zoom()
    {
        float.TryParse(_actionDatas[_actionIndex].Args[(int)ZoomArgument.duringTime], out float duringTime);
        float.TryParse(_actionDatas[_actionIndex].Args[(int)ZoomArgument.scaleDelta], out float scaleDelta);
        bool.TryParse(_actionDatas[_actionIndex].Args[(int)ZoomArgument.delayWhileDone], out bool delayWhileDone);
        flowchart.SetBooleanVariable("Delay", delayWhileDone);
        Image targetImage = null;
        switch (_actionDatas[_actionIndex].Args[(int)ZoomArgument.target])
        {
            case "left":
                targetImage = leftImage;
                break;
            case "right":
                targetImage = rightImage;
                break;
            case "backGround":
                targetImage = bgImage;
                break;
        }

        Vector3 currentScaleCache = targetImage.rectTransform.localScale;
        Vector3 targetScaleCache = Vector3.one * scaleDelta;
        StartCoroutine(dialogueShow(duringTime, 
            (float percent) =>
            {
                targetImage.rectTransform.localScale = Vector3.Lerp(currentScaleCache, targetScaleCache, percent);
            },
            () =>
            {
                flowchart.SetBooleanVariable("Delay", false);
            }));
    }

    private void setFavorability()
    {
        int.TryParse(_actionDatas[_actionIndex].Args[(int)FavorabilityArgument.value], out int value);
        string characterName = _actionDatas[_actionIndex].Args[(int)FavorabilityArgument.characterName];
        int valueCache = Services.Get<DataManager>().GetFavorability(characterName) + value;
        Services.Get<DataManager>().SetFavorability(characterName, valueCache);
    }

    private void setBoard()
    {
        float.TryParse(_actionDatas[_actionIndex].Args[(int)BoardArgument.duringTime], out float duringTime);
        bool.TryParse(_actionDatas[_actionIndex].Args[(int)BoardArgument.delayWhileDone], out bool delayWhileDone);
        float.TryParse(_actionDatas[_actionIndex].Args[(int)BoardArgument.alpha], out float alpha);
        bool isTop = _actionDatas[_actionIndex].Args[(int)BoardArgument.target] == "top";
        bool isIn = _actionDatas[_actionIndex].Args[(int)BoardArgument.position] == "in";
        flowchart.SetBooleanVariable("Delay", delayWhileDone);
        duringTime = Mathf.Max(duringTime, Time.deltaTime);

        Image targetImage = isTop ? topBoard : bottomBoard;
        Vector2 startPos = targetImage.rectTransform.anchoredPosition;
        Vector2 targetPos = Vector2.up * targetImage.rectTransform.sizeDelta.y * (isTop ? 1 : -1);
        Vector2 p1 = isIn ? targetPos : startPos;
        Vector2 p2 = isIn ? startPos : targetPos;
        Color colorCache = targetImage.color;
        float startAlpha = targetImage.color.a;
        float targetAlpha = alpha;

        StartCoroutine(dialogueShow(duringTime, 
            (float percent) =>
            {
                targetImage.rectTransform.anchoredPosition = Vector2.Lerp(p1, p2, percent);
                colorCache.a = Mathf.Lerp(startAlpha, targetAlpha, percent);
                targetImage.color = colorCache;
            },
            () =>
            {
                flowchart.SetBooleanVariable("Delay", false);
            }));
    }

    private void getItem()
    {
        flowchart.SetBooleanVariable("Delay", true);
        //GetItemTip getItemTip = Services.Get<UIManager>().OpenUI<GetItemTip>("GetItemTip");
        //getItemTip.ShowTip(_actionDatas[_actionIndex].Args[(int)GetItemArgument.itemName], flowchart);
    }

    private void setHighLight()
    {
        Services.Get<EventManager>().SendEvent((int)EGameEvents.SetUIButtonHighLight, _actionDatas[_actionIndex].Args[(int)HighLightArgument.buttonName]);
    }

    private void setFade()
    {
        float.TryParse(_actionDatas[_actionIndex].Args[(int)FadeArgument.fadeInTime], out float fadeInTime);
        float.TryParse(_actionDatas[_actionIndex].Args[(int)FadeArgument.fadeOutTime], out float fadeOutTime);
        float.TryParse(_actionDatas[_actionIndex].Args[(int)FadeArgument.stayTime], out float stayTime);
        string backGroundName = _actionDatas[_actionIndex].Args[(int)FadeArgument.bgImageName];

        fadeInTime = Mathf.Max(Time.deltaTime, fadeInTime);
        fadeOutTime = Mathf.Max(Time.deltaTime, fadeOutTime);
        stayTime = Mathf.Max(Time.deltaTime, stayTime);

        flowchart.SetBooleanVariable("Delay", true);
        StartCoroutine(IFade(fadeInTime, fadeOutTime, stayTime, backGroundName));
    }
    private IEnumerator IFade(float fadeInTime, float fadeOutTime, float stayTime, string backGroundName)
    {
        float timeCache = Time.time;
        Color colorCache = fadeImage.color;

        if (!Input.GetKey(KeyCode.Q))
        while (Time.time - timeCache < fadeInTime)
        {
            yield return new WaitForEndOfFrame();
            colorCache.a = Mathf.Clamp01((Time.time - timeCache) / fadeInTime);
            fadeImage.color = colorCache;
        }

        SetBg(backGroundName);
        if (!Input.GetKey(KeyCode.Q))
        yield return new WaitForSeconds(stayTime);

        timeCache = Time.time;
        if (!Input.GetKey(KeyCode.Q))
        while (Time.time - timeCache < fadeOutTime)
            {
            yield return new WaitForEndOfFrame();
            colorCache.a = 1 - Mathf.Clamp01((Time.time - timeCache) / fadeOutTime);
            fadeImage.color = colorCache;
        }

        flowchart.SetBooleanVariable("Delay", false);
    }

    public IEnumerator dialogueShow(float time, Action<float> action, Action callBack = null)
    {
        float timeCache = Time.time + time;

        while (Time.time < timeCache)
        {
            yield return new WaitForEndOfFrame();

            float percent = 1 - (timeCache - Time.time) / time;

            if (action != null)
                action.Invoke(percent);
        }

        if (callBack != null)
            callBack.Invoke();
    }

    private void setSniperGame()
    {
        Services.Get<EventManager>().SendEvent((int)EGameEvents.EnterSniperGameState);
    }

    private void setVideo()
    {
        Services.Get<UIManager>().OpenUI<VideoController>("VideoController").Play(null);
    }

    private void setRoleImage(ActionData data)
    {
        if(data.Args[(int)SayArgument.leftRoleName] != "none")
        {
            Sprite sp = Services.Get<ResourcesManager>().GetAsset<Sprite>(
                "Portraits/" +
                data.Args[(int)SayArgument.leftRoleName] +
                "/" +
                data.Args[(int)SayArgument.leftRoleName] +
                "_" +
                data.Args[(int)SayArgument.leftRoleMood]
                );
            if (sp == null)
            {
                leftImage.gameObject.SetActive(false);
                Debug.LogError("cant find the image by this role name! name is " + data.Args[(int)SayArgument.leftRoleName] +
                "/" +
                data.Args[(int)SayArgument.leftRoleName] +
                "_" +
                data.Args[(int)SayArgument.leftRoleMood]);
            }
            else
            {
                leftImage.sprite = sp;
                leftImage.gameObject.SetActive(true);
            }
        }
        else
        {
            leftImage.gameObject.SetActive(false);
        }

        if (data.Args[(int)SayArgument.rightRoleName] != "none")
        {
            Sprite sp = Services.Get<ResourcesManager>().GetAsset<Sprite>(
                "Portraits/" +
                data.Args[(int)SayArgument.rightRoleName] +
                "/" +
                data.Args[(int)SayArgument.rightRoleName] +
                "_" +
                data.Args[(int)SayArgument.rightRoleMood]
                );
            
            if (sp == null)
            {
                rightImage.gameObject.SetActive(false);
                Debug.LogError("cant find the image by this role name! name is " + data.Args[(int)SayArgument.rightRoleName] +
                "/" +
                data.Args[(int)SayArgument.rightRoleName] +
                "_" +
                data.Args[(int)SayArgument.rightRoleMood]);
            }
            else
            {
                rightImage.sprite = sp;
                rightImage.gameObject.SetActive(true);
            }
        }
        else
        {
            rightImage.gameObject.SetActive(false);
        }
    }

    private void setCharactername(ActionData data)
    {
        if(data.Args[(int)SayArgument.characterName] != "none" && !string.IsNullOrEmpty(data.Args[(int)SayArgument.characterName]))
        {
            CharacterData _cd = CharacterDataList.Find(x => x.code.Contains(data.Args[(int)SayArgument.characterName]));
            if (_cd != null)
            {
                characterNameText.text = _cd.Charactername;
            }
            else
            {
                characterNameText.text = "***";
                Debug.LogError("cant find the image by this character name! name is " + data.Args[(int)SayArgument.characterName]);
            }
          
        }
        else
        {
            characterNameText.text = "";
        }
        
    }

    public void ActionFinished()
    {
        //關掉顯示
        bgImage.gameObject.SetActive(false);
        leftImage.gameObject.SetActive(false);
        rightImage.gameObject.SetActive(false);
        blockInput.SetActive(false);
        _actionIndex = 0;
        _actionDatas = null;
        //DoCallBack
        if (_callback != null)
        {
            _callback();
        }
        
        Services.Get<EventManager>().SendEvent((int)EGameEvents.SetPlayerControl, true);
    }

    public void BtnClicked(int index)
    {
        //get and play next action index
        insertAction?.Invoke(BtnIndexList[index]);
        BtnIndexList.Clear();
        //reset btns view

        flowchart.ExecuteBlock("Action");
    }

    public void Repeat()
    {
        if (_actionIndex == _actionDatas.Length - 1)
        {
            animBG.SetTrigger("None");
            flowchart.SetBooleanVariable("Finished", true);
            Services.Get<StoryManager>().SetFlowVarBool("inDialogue", false);
            Services.Get<EventManager>().SendEvent((int)EGameEvents.SetKarmaValuePause, false);
        }
        else
        {
            _actionIndex++;
        }
    }
}
