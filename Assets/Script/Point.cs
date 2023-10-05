using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public bool canMove = false;
    public GameObject target;
    public float speed;
    private int[] pointValue = {5, 10, 20, 30, 40, 50, 60};
    public int getPointValue;

    void Start()
    {
        StartCoroutine(DelayMoveTowards());
        getPointValue = pointValue[Random.Range(0, pointValue.Length)];
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
