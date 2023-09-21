using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BaseStatus", menuName = "RoguelikeProject/BaseStatus", order = 0)]
public class BaseStatus : ScriptableObject 
{
    public float maxHealth;
    public int maxStamina;
    public float maxBlocked;
    public float attackDamage; 
}

