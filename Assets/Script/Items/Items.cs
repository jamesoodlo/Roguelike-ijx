using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    private GameObject findPlayer;
    public GameObject interactText;
    public Interaction player;
    public bool isInteracted = false;
    public float distanceFromTarget;

    void Start()
    {
        interactText.SetActive(false);
    }

    void Update()
    {
        findPlayer = GameObject.Find("Player");

        distanceFromTarget = Vector3.Distance(findPlayer.transform.position, transform.position);
        Interacted();
    }

    public void Interacted()
    {
        if(distanceFromTarget <= 2.5) 
        {
            interactText.SetActive(true);
            
            player = findPlayer.GetComponent<Interaction>();
        }
        else
        {
            player = null;

            interactText.SetActive(false);
        }

        if(distanceFromTarget <= 2.5 && player.isInteract && player.canInteract)
        {
            isInteracted = true;
            interactText.SetActive(false);
        }
        else
        {
            isInteracted = false;
        }
    }
}
