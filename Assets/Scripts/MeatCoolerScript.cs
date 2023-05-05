using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeatCoolerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject burgerPattyPrefab;
    [SerializeField]
    private GameObject playerHand;

    private void OnEnable()
    {
        MeatCoolerTrigger.OnPlayerActivatedTriggerEvent += InteractWithMeatCooler;
    }

    private void OnDisable()
    {
        MeatCoolerTrigger.OnPlayerActivatedTriggerEvent -= InteractWithMeatCooler;
    }

    public void InteractWithMeatCooler()
    {
        if(playerHand.transform.childCount == 0)
        {
            GameObject burgerPatty = Instantiate(burgerPattyPrefab);
            burgerPatty.transform.SetParent(playerHand.transform);
            burgerPatty.transform.localPosition = new Vector3(1.146f, -0.007f, -0.26f);
            burgerPatty.transform.localRotation = Quaternion.identity;
        }
    }
}
