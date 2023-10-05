using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashSkill : MonoBehaviour
{
    [Header("Gun System")]
    public bool onSlash = false;
    public float fireRate = 0.4f;
    private float nextFire = 0.0f;
    public GameObject slashPrefab;

    void Start()
    {
        
    }

    void Update()
    {
        Fire();
    }

    public void Fire()
    {

        if(onSlash && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            onSlash = false;

            Instantiate(slashPrefab, transform.position, transform.rotation);
        }        
    }
}
