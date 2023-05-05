using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShotgunTriggerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject playerHand;
    [SerializeField]
    private GameObject shotGun;

    private Vector3 shotgunSpot;
    private Quaternion shotgunRotation;

    private bool playerInRange = false;
    private bool playerHasShotgun = false;

    private void Start()
    {
        shotgunSpot = shotGun.transform.position;
        shotgunRotation = shotGun.transform.rotation;
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
            if (playerHasShotgun)
            {
                shotGun.transform.position = shotgunSpot;
                shotGun.transform.rotation = shotgunRotation;
                shotGun.transform.parent = null;
                playerHasShotgun = false;
            }
            else if(!playerHasShotgun && playerHand.transform.childCount == 0)
            {
                shotGun.transform.parent = playerHand.transform;
                shotGun.transform.position = playerHand.transform.position;
                shotGun.transform.rotation = playerHand.transform.rotation;
                playerHasShotgun = true;
            }
        }
    }
}
