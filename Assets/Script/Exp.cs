using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    public bool canMove = false;
    public GameObject target;
    public float speed;

    void Start()
    {
        StartCoroutine(DelayMoveTowards());
    }

    void Update()
    {
        followPlayer();
    }

    public void followPlayer()
    {
        target = GameObject.Find("Player");
        
        if(canMove) transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    IEnumerator DelayMoveTowards(float delay = 1.0f)
    {
        yield return new WaitForSeconds(delay);
        canMove = true;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            Destroy(this.gameObject);
        }  
    }
}
