using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingData", menuName = "RoguelikeProject/SettingData", order = 0)]
public class SettingData : ScriptableObject
{
    [Header("All Setting")]
    public float musicSound;
    public float ambientSound;
    public float effectSound;
    public float uiSound;
    public int qualityIndex;
    public bool isFullScreen;

    [Header("Menu Setting")]
    public bool isTutorial;
    public bool notNewGame;
}
