using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatCoolerTrigger : MonoBehaviour
{

    public delegate void OnPlayerActivatedTriggerDelegate();
    public static event OnPlayerActivatedTriggerDelegate OnPlayerActivatedTriggerEvent;

    private bool playerInRange = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
    void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange)
        {
            if (OnPlayerActivatedTriggerEvent != null)
                OnPlayerActivatedTriggerEvent();
        }
    }
}
