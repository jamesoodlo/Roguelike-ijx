using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    EnemyRange enemy;
    EnemyStats enemyStats;

    [Header("Gun System")]
    public float fireRate = 0.4f;
    private float nextFire = 0.0f;
    public int maxAmmo = 8;
    public int ammo;
    public GameObject bulletPrefab;

    void Start()
    {
        enemy = GetComponentInParent<EnemyRange>();
        enemyStats = GetComponentInParent<EnemyStats>();
        ammo = maxAmmo;
    }

    void Update()
    {
        if(enemyStats.currentHealth <= 0)
        {
            
        }
        else
        {
            if(enemy.target != null && Time.time > nextFire)
            {
                if(ammo > 0)
                {
                    nextFire = Time.time + fireRate;
                    Fire();
                }
                else
                {
                    Reload();
                }
                
            }
        }
    }

    public void Fire()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
        ammo -= 1;
    }

    private void Reload()
    {
        if(ammo <= 0)
        {
            StartCoroutine(Reloading());
        }
    }

    IEnumerator Reloading(float reloadTime = 3f)
    {
        yield return new WaitForSeconds(reloadTime);
        ammo = maxAmmo;
    }
}
