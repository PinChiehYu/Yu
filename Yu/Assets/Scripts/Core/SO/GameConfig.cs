using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "CustomPlugin/GameConfig", order = 1)]
public class GameConfig : ScriptableObject
{
    public string[] gameDataFilePaths = new string[] {  };
    public string[] gameDataFilePaths_English = new string[] {  };
}
