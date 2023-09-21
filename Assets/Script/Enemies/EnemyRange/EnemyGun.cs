using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    EnemyRange enemyRange;
    EnemyRangeStats enemyRangeStat;

    public Animator anim;

    [Header("Gun System")]
    public Slider ammoBar;
    public GameObject ammoText;
    public bool isFiring = false;
    public bool isReloading = false;
    public float fireRate = 0.4f;
    private float nextFire = 0.0f;
    public int maxAmmo = 8;
    public int ammo;
    public GameObject bulletPrefab;

    void Start()
    {
        enemyRange = GetComponentInParent<EnemyRange>();
        enemyRangeStat = GetComponentInParent<EnemyRangeStats>();
        ammoText.SetActive(false);
        ammo = maxAmmo;
    }

    void Update()
    {
        SetAmmoBar();
        
        anim.SetBool("Firing", isFiring);
    }

    public void SetAmmoBar()
    {
        ammoBar.maxValue = maxAmmo;
        ammoBar.value = ammo;
    }

    public void Fire()
    {
        ammoText.SetActive(false);

        if(enemyRange.target != null && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            isFiring = true;

            Instantiate(bulletPrefab, transform.position, transform.rotation);

            ammo -= 1;
        }        
    }

    public void Reload()
    {
        ammoText.SetActive(true);
        anim.Play("Reload");
        isReloading = true;
        isFiring = false;
        StartCoroutine(Reloading());
    }

    IEnumerator Reloading(float reloadTime = 3.08f)
    {
        yield return new WaitForSeconds(reloadTime);
        ammo = maxAmmo;
        isReloading = false;
    }
}
