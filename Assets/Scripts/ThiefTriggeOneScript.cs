using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefTriggeOneScript : MonoBehaviour
{
    public delegate void OnThiefReachedTriggerDelegate();
    public static event OnThiefReachedTriggerDelegate OnThiefReachedTriggerEvent;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Thief")
        {
            if (OnThiefReachedTriggerEvent != null)
                OnThiefReachedTriggerEvent();
        }
    }
}
