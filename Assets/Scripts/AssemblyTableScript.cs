using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AssemblyTableScript : MonoBehaviour
{
    [SerializeField]
    private GameObject playerHand;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject burgerPrefab;
    [SerializeField]
    private GameObject[] plates;
    private GameObject closestPlate;

    private void OnEnable()
    {
        AssemblyTableTriggerScript.OnPlayerActivatedTriggerEvent += InteractWithAssemblyTable;
    }

    private void OnDisable()
    {
        AssemblyTableTriggerScript.OnPlayerActivatedTriggerEvent -= InteractWithAssemblyTable;
    }

    private void InteractWithAssemblyTable()
    {
        if(player.transform.position.z < 10.5f)
        {
            closestPlate = plates[0];
        }
        else if (player.transform.position.z > 10.5f && player.transform.position.z < 11.7f)
        {
            closestPlate = plates[1];
        }
        else
        {
            closestPlate = plates[2];
        }

        if (playerHand.transform.childCount != 0 && closestPlate.transform.childCount == 0 && (playerHand.transform.GetChild(0).gameObject.tag == "cookedBurgerPatty" || playerHand.transform.GetChild(0).gameObject.tag == "burgerbun"))
        {
            GameObject objectToPlace = playerHand.transform.GetChild(0).gameObject;
            objectToPlace.transform.parent = closestPlate.transform;
            objectToPlace.transform.position = closestPlate.transform.position + new Vector3(0f, 0.062032f, 0f);
        }
        else if ((playerHand.transform.childCount != 0 && closestPlate.transform.childCount != 0) && ((playerHand.transform.GetChild(0).gameObject.tag == "burgerbun" && closestPlate.transform.GetChild(0).gameObject.tag == "cookedBurgerPatty") || (playerHand.transform.GetChild(0).gameObject.tag == "cookedBurgerPatty" && closestPlate.transform.GetChild(0).gameObject.tag == "burgerbun")))
        {
            GameObject objectToPlace = playerHand.transform.GetChild(0).gameObject;
            Destroy(objectToPlace);
            Destroy(closestPlate.transform.GetChild(0).gameObject);
            GameObject finishedBurger = Instantiate(burgerPrefab);
            finishedBurger.transform.position = closestPlate.transform.position + new Vector3(0f, 0.062032f, 0f);
            finishedBurger.transform.parent = closestPlate.transform;
        }
        else if(playerHand.transform.childCount == 0 && closestPlate.transform.childCount != 0)
        {
            GameObject objectToTake = closestPlate.transform.GetChild(0).gameObject;
            objectToTake.transform.SetParent(playerHand.transform);
            objectToTake.transform.localPosition = new Vector3(1.28f, 0.15f, 0.001f);
        }
    }
}
