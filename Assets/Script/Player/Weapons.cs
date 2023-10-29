using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    SoundFx soundFx;
    public BaseStatus playerDataStat;
    public bool onAttack = false;
    public float damage;
    private float timeSinceHit;
    public int hitCount;

    void Start()
    {
        soundFx = GetComponentInParent<SoundFx>();

        damage = playerDataStat.attackDamage;
    }

    void Update()
    {

        if(hitCount > 0)
        {
            timeSinceHit += Time.deltaTime;
        }
        else
        {
            timeSinceHit = 0;
        }

        if(timeSinceHit > 1.0f || hitCount > 3)
        {
            hitCount = 0;
        }
        
        if(hitCount == 2)
        {
            damage = playerDataStat.attackDamage + 5;
        }
        else if(hitCount == 3)
        {
            damage = playerDataStat.attackDamage + 10;
        }
        else
        {
            damage = playerDataStat.attackDamage;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Enemy")
        {
            if(onAttack)
            {
                hitCount += 1;
            }    
        } 

        if(other.gameObject.tag == "ItemBox")
        {
            if(onAttack)
            {
                soundFx.woodBoxSfx.Play();
            }    
        } 
    }
}
