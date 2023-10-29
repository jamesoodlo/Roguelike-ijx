using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashProjectile : MonoBehaviour
{
    AudioSource audio;
    
    public BaseStatus playerDataStat;
    public float damage;
    public float Speed = 0.1f;
    public float SecondsUntilDestroy = 3f;
    float startTime;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        startTime = Time.time;
        damage = playerDataStat.attackDamage;

        audio.Play();
    }

    void Update()
    {
        SkillLv();

        this.gameObject.transform.position += Speed * this.gameObject.transform.forward;

        if (Time.time - startTime >= SecondsUntilDestroy)
        {
            Destroy(this.gameObject);
        }
    }

    public void SkillLv()
    {
        if(playerDataStat.slasherLv == 1)
        {
            transform.localScale = new Vector3(1.0f, 0, 1.0f);
        }
        else if(playerDataStat.slasherLv == 2)
        {
            transform.localScale = new Vector3(1.25f, 0, 1.25f);
        }
        else if(playerDataStat.slasherLv == 3)
        {
            transform.localScale = new Vector3(1.5f, 0, 1.5f);
        }
        else if(playerDataStat.slasherLv == 4)
        {
            transform.localScale = new Vector3(1.75f, 0, 1.75f);
        }
        else if(playerDataStat.slasherLv == 5)
        {
            transform.localScale = new Vector3(2.0f, 0, 2.0f);
        }
        else if(playerDataStat.slasherLv == 6)
        {
            transform.localScale = new Vector3(2.25f, 0, 2.25f);
        }
        else if(playerDataStat.slasherLv == 7)
        {
            transform.localScale = new Vector3(2.5f, 0, 2.5f);
        }
        else if(playerDataStat.slasherLv == 8)
        {
            transform.localScale = new Vector3(3.25f, 0, 3.25f);
        }
        else if(playerDataStat.slasherLv == 9)
        {
            transform.localScale = new Vector3(3.5f, 0, 3.5f);
        }
        else if(playerDataStat.slasherLv == 10)
        {
            transform.localScale = new Vector3(3.75f, 0, 3.75f);
        }
    }
}
