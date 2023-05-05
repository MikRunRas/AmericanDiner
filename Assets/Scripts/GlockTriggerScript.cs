using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlockTriggerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject playerHand;
    [SerializeField]
    private GameObject glock;

    private Vector3 glockSpot;
    private Quaternion glockRotation;

    private bool playerInRange = false;
    private bool playerHasGlock = false;

    private void Start()
    {
        glockSpot = glock.transform.position;
        glockRotation = glock.transform.rotation;
    }


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
            if (playerHasGlock)
            {
                glock.transform.position = glockSpot;
                glock.transform.rotation = glockRotation;
                glock.transform.parent = null;
                playerHasGlock = false;
            }
            else if (!playerHasGlock && playerHand.transform.childCount == 0)
            {
                glock.transform.SetParent(playerHand.transform);
                glock.transform.localPosition = new Vector3(0.7f, 0.7f, 0f);
                glock.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
                playerHasGlock = true;
            }
        }
    }
}
