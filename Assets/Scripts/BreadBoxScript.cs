using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadBoxScript : MonoBehaviour
{
    [SerializeField]
    private GameObject burgerBunPrefab;
    [SerializeField]
    private GameObject playerHand;

    private void OnEnable()
    {
        BreadBoxTriggerScript.OnPlayerActivatedTriggerEvent += InteractWithBreadBox;
    }

    private void OnDisable()
    {
        BreadBoxTriggerScript.OnPlayerActivatedTriggerEvent -= InteractWithBreadBox;
    }

    public void InteractWithBreadBox()
    {
        if(playerHand.transform.childCount == 0)
        {
            GameObject burgerBun = Instantiate(burgerBunPrefab);
            burgerBun.transform.SetParent(playerHand.transform);
            burgerBun.transform.localPosition = new Vector3(1.28f, 0.15f, 0.001f);
            burgerBun.transform.localRotation = Quaternion.identity;
        }
    }
}
