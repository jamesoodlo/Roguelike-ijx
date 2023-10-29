using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BaseStatus", menuName = "RoguelikeProject/BaseStatus", order = 0)]
public class BaseStatus : ScriptableObject 
{
    public int currentEggs;
    public int Eggs;

    [Header("Level")]
    public bool canLvUp = true;
    public int level;
    public float exp;
    public float maxExp;
    
    [Header("Base Status")]
    public float maxHealth;
    public float maxStamina;
    public float maxStaminaRegen;
    public float maxGuard;
    public float attackDamage; 
    public float speed;

    [Header("Current Status")]
    public float currentMaxHealth;
    public float currentAttackDamage; 
    public float currentSpeed;

    [Header("Buff Status")]
    public float maxHealthBuff;
    public float attackDamageBuff; 
    public float speedBuff;

    [Header("Starter Status")]
    public float maxHealthStarter;
    public float maxStaminaStarter;
    public float maxStaminaRegenStarter;
    public float maxGuardStarter;
    public float attackDamageStarter; 
    public float speedStarter;

    [Header("Items")]
    public int item1Num;
    public int item2Num;
    public int item1Pocket;
    public int item2Pocket;

    [Header("Skill")]
    public bool hasSlasher;
    public bool hasSuperDuck;
    public int slasherLv = 1;
    public int superDuckLv = 1;
}

