using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderingBookTriggerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject orderingMagasine;
    [SerializeField]
    private GameObject playerStats;
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
            playerStats.SetActive(false);
            orderingMagasine.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
