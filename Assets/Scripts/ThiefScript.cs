using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ThiefScript : MonoBehaviour
{
    public delegate void OnThiefStoleMoneyDelegate(int amountStolen);
    public static event OnThiefStoleMoneyDelegate OnThiefStoleMoneyEvent;
    public delegate void OnThiefKilledDelegate();
    public static event OnThiefKilledDelegate OnThiefKilledEvent;

    [SerializeField]
    private AudioClip deathSound;
    [SerializeField]
    private NavMeshAgent agent;
    private Animator anim;
    private AudioSource audioSource;
    [SerializeField]
    private Transform[] EndPositions = new Transform[2];
    private Transform[] ThiefDestination = new Transform[3]; 
    private readonly int speedHash = Animator.StringToHash("Speed");
    private bool hasCollided = false;
    private int destinationCounter = 0;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        EndPositions[0] = GameObject.Find("EndPosition1").transform;
        EndPositions[1] = GameObject.Find("EndPosition2").transform;
        ThiefDestination[0] = GameObject.Find("ThiefTargetOne").transform;
        ThiefDestination[1] = GameObject.Find("ThiefTargetTwo").transform;
        ThiefDestination[2] = GameObject.Find("ThiefTargetThree").transform;
        agent.SetDestination(ThiefDestination[0].position);
    }
    private void FixedUpdate()
    {
        float speed = agent.velocity.magnitude;
        anim.SetFloat(speedHash, speed);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (hasCollided)
            return;
        if (collision.gameObject.tag == "Bullet")
        {
            agent.isStopped = true;
            audioSource.pitch = Random.Range(0.75f, 1.5f);
            audioSource.PlayOneShot(deathSound);
            if (OnThiefKilledEvent != null)
                OnThiefKilledEvent();
            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            GetComponent<Animator>().SetTrigger("Death");
            CancelInvoke("StealMoney");
            CancelInvoke("ChangeDestination");
            Destroy(gameObject, 10f);
            hasCollided = true;
        }
    }

    private void OnEnable() => ThiefTriggeOneScript.OnThiefReachedTriggerEvent += StartStealing;

    private void OnDisable() => ThiefTriggeOneScript.OnThiefReachedTriggerEvent -= StartStealing;

    private void StartStealing()
    {
        InvokeRepeating("ChangeDestination", 10f, 10f);
        InvokeRepeating("StealMoney", 0f, 9f);
    }
    private void ChangeDestination()
    {
        destinationCounter++; 
        if (destinationCounter > 2)
        {
            agent.SetDestination(EndPositions[Random.Range(0, 1)].position);
            CancelInvoke("StealMoney");
            CancelInvoke("ChangeDestination");
            Destroy(gameObject, 15f);
        }

        else
        {
            Transform newDestination = ThiefDestination[destinationCounter];

            agent.SetDestination(newDestination.position);

        }
    }
    private void StealMoney()
    {

        if (OnThiefStoleMoneyEvent != null)
        {
            OnThiefStoleMoneyEvent(-5);
        }
    }
}
