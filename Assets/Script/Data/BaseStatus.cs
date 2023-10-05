using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BaseStatus", menuName = "RoguelikeProject/BaseStatus", order = 0)]
public class BaseStatus : ScriptableObject 
{
    public int currentPoint;
    public int Point;

    [Header("Level")]
    public bool canLvUp = true;
    public int level;
    public float exp;
    public float maxExp;
    
    [Header("Status")]
    public float maxHealth;
    public int maxStamina;
    public float maxShield;
    public float attackDamage; 
    public float speed;

    [Header("Skill")]
    public bool hasSlasher;
    public bool hasBarrier;
    public int slasherLv = 1;
    public int barrierLv = 1;
}

