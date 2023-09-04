using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float Speed = 0.1f;
    public float SecondsUntilDestroy = 3f;
    float startTime;
    
    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        this.gameObject.transform.position += Speed * this.gameObject.transform.forward;

        if (Time.time - startTime >= SecondsUntilDestroy)
        {
            Destroy(this.gameObject);
        }
    }

    
    IEnumerator DestroyBullet(float destroyTime = 0.0f)
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(DestroyBullet());
        }
    }
}
