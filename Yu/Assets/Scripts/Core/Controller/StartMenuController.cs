using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour
{
    public Button NewGameButton;
    public Button ContinueButton;
    public Button ConfigButton;
    public Button ExitButton;
    public Button FF36Button;
    public Button ChangeLanguageButton;

    public GameObject WaterEffect;

    public GameConfig gameConfig;
    private bool isEnglish = false;

    public void ChangeLanguage()
    {
        isEnglish = !isEnglish;
        //Services.Get<ActionManager>().ChangeGameData(isEnglish ? gameConfig.gameDataFilePaths_English : gameConfig.gameDataFilePaths);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
