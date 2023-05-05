using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.TextCore.Text;

public class CoffeeMachineScript : MonoBehaviour
{
    [SerializeField]
    private AudioClip doneSound;
    [SerializeField]
    private AudioClip coffeeSound;
    private AudioSource audioSource;
    [SerializeField]
    private GameObject timerPrefab;
    [SerializeField]
    private GameObject tickPrefab;
    [SerializeField]
    private GameObject coffeeCupPrefab; 
    [SerializeField]
    private GameObject playerHand;
    private bool isBrewingCoffee = false;
    private bool isCoffeeDone = false;
    private GameObject timerObject;
    private GameObject coffeeCup;
    private GameObject tickObject;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        CoffeeMachineTrigger.OnPlayerActivatedTriggerEvent += InteractWithCoffeeMachine;
    }

    private void OnDisable()
    {
        CoffeeMachineTrigger.OnPlayerActivatedTriggerEvent -= InteractWithCoffeeMachine;
    }

    public void InteractWithCoffeeMachine()
    {
        if (!isBrewingCoffee && !isCoffeeDone && playerHand.transform.childCount == 0)
        {
            audioSource.pitch = Random.Range(0.75f, 1.5f);
            audioSource.volume = 0.5f;
            audioSource.PlayOneShot(coffeeSound);
            isBrewingCoffee = true;
            coffeeCup = Instantiate(coffeeCupPrefab);
            coffeeCup.transform.position = new Vector3(-1.373f, 1.508f, -4.79f);
            timerObject = Instantiate(timerPrefab);
            timerObject.GetComponent<TimerScript>().progressTime = 0.2f;
            timerObject.transform.position = new Vector3(-1.646f, -0.026f, -3.6f);
        } 
        else if(!isBrewingCoffee && isCoffeeDone && playerHand.transform.childCount == 0)
        {
            Destroy(tickObject);
            coffeeCup.transform.SetParent(playerHand.transform);
            coffeeCup.transform.position = playerHand.transform.position + new Vector3(.1f,.2f,-.2f);
            coffeeCup.transform.localRotation = Quaternion.identity;
            isCoffeeDone = false;
        }
    }

    void Update()
    {
        if (isBrewingCoffee && !isCoffeeDone)
        {
            if(timerObject.IsDestroyed())
            {
                audioSource.pitch = Random.Range(0.75f, 1.5f);
                audioSource.PlayOneShot(doneSound);
                tickObject = Instantiate(tickPrefab);
                tickObject.transform.position = new Vector3(-3.58f, -6.17f, -39.75f);
                isCoffeeDone = true;
                isBrewingCoffee = false;
            }
        }
    }
}
