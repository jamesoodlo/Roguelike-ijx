using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashProjectile : MonoBehaviour
{
    [Header("Gun System")]
    public GameObject bulletPrefab;

    public void Fire()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }

}
