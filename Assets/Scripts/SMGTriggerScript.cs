using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMGTriggerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject playerHand;
    [SerializeField]
    private GameObject SMG;

    private Vector3 SMGLocation;
    private Quaternion SMGRotation;

    private bool playerInRange = false;
    private bool playerHasSMG = false;

    private void Start()
    {
        SMGLocation = SMG.transform.position;
        SMGRotation = SMG.transform.rotation;
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
            if (playerHasSMG)
            {
                SMG.transform.position = SMGLocation;
                SMG.transform.rotation = SMGRotation;
                SMG.transform.parent = null;
                playerHasSMG = false;
            }
            else if (!playerHasSMG && playerHand.transform.childCount == 0)
            {
                SMG.transform.SetParent(playerHand.transform);
                SMG.transform.localPosition = new Vector3(1.5f, 0.7f, 0f);
                SMG.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
                playerHasSMG = true;
            }
        }
    }
}
