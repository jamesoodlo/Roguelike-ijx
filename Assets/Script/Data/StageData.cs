using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "RoguelikeProject/StageData", order = 0)]
public class StageData : ScriptableObject
{
    public int currentStage;
    public int maxStage;
    public int HighestStage;
}
