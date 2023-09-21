using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBaseStatus", menuName = "RoguelikeProject/EnemyBaseStatus", order = 0)]
public class EnemyBaseStatus : ScriptableObject 
{
    public float maxHealth;
    public float maxStamina;
    public float attackDamage;
}
