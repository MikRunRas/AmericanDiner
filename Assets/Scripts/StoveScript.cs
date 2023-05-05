using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StoveScript : MonoBehaviour
{
    [SerializeField]
    private AudioClip doneSound;
    [SerializeField]
    private AudioClip meatSound;
    private AudioSource audioSource;
    [SerializeField]
    private GameObject playerHand;
    [SerializeField]
    private GameObject tickPrefab;
    [SerializeField]
    private GameObject timerPrefab;
    [SerializeField]
    private GameObject steamPrefab;
    private GameObject timerObject;
    private GameObject tickObject;
    private GameObject meatToCook;
    private GameObject steam;
    public bool isCookingMeat = false;
    public bool isMeatDone = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        StoveTriggerScript.OnPlayerActivatedTriggerEvent += InteractWithStove;
    }

    private void OnDisable()
    {
        StoveTriggerScript.OnPlayerActivatedTriggerEvent -= InteractWithStove;
    }

    public void InteractWithStove()
    {
        if(playerHand.transform.childCount != 0)
        {
            if (playerHand.transform.GetChild(0).gameObject.tag == "sausage" || playerHand.transform.GetChild(0).gameObject.tag == "burgerpatty"
            && !isCookingMeat && !isMeatDone)
            {
                audioSource.pitch = Random.Range(0.75f, 1.5f);
                audioSource.PlayOneShot(meatSound);
                meatToCook = playerHand.transform.GetChild(0).gameObject;
                meatToCook.transform.parent = null;
                isCookingMeat = true;
                meatToCook.transform.position = new Vector3(Random.Range(-4.4f, -4.86f), 1.16f, Random.Range(6.5f, 8.356f));
                steam = Instantiate(steamPrefab);
                steam.transform.position = meatToCook.transform.position;
                timerObject = Instantiate(timerPrefab);
                timerObject.GetComponent<TimerScript>().progressTime = 0.2f;
                timerObject.transform.position = new Vector3(-3.34f, 0.4f, 7.49f);
                timerObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }
        if (playerHand.transform.childCount == 0 && isMeatDone && !isCookingMeat)
        {
            Destroy(tickObject);
            meatToCook.transform.SetParent(playerHand.transform);
            meatToCook.transform.localPosition = new Vector3(1.18f, -0.03f, -0.37f);
            meatToCook.transform.localRotation = Quaternion.identity;
            isMeatDone = false;
        }
    }

    private void Update()
    {
        if(isCookingMeat && !isMeatDone)
        {
            if(timerObject.IsDestroyed())
            {
                audioSource.pitch = Random.Range(0.75f, 1.5f);
                audioSource.PlayOneShot(doneSound);
                tickObject = Instantiate(tickPrefab);
                tickObject.transform.position = new Vector3(-39.74f, -6.17f, 7.27f);
                tickObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                isMeatDone = true;
                isCookingMeat = false;
                meatToCook.GetComponent<Renderer>().material.color = new Color(0.6f, 0.3f, 0.0f, 1.0f);
                meatToCook.gameObject.tag = "cookedBurgerPatty";
                BurgerPattyScript meatToCookScript = meatToCook.GetComponent<BurgerPattyScript>();
                meatToCookScript.isCooked = true;
                Destroy(steam);
            }
        }
    }
} 
