using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerScript : MonoBehaviour
{
    public delegate void OnOrderCompletedDelegate(int price);
    public static event OnOrderCompletedDelegate OnOrderCompletedEvent; 
    public delegate void OnCivilianKilledDelegate();
    public static event OnCivilianKilledDelegate OnCivilianKilledEvent;

    [SerializeField]
    private AudioClip deathSound;
    [SerializeField]
    private NavMeshAgent agent;
    private Animator anim;
    private AudioSource audioSource;
    [SerializeField]
    private GameObject[] CustomerTargets;
    private Transform availableTargetsParent;
    private Transform occupiedTargetsParent;
    public Transform currentCustomersTarget;
    [SerializeField]
    private Transform[] EndPositions = new Transform[2];
    private readonly int speedHash = Animator.StringToHash("Speed");
    private bool hasCollided = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        EndPositions[0] = GameObject.Find("EndPosition1").transform;
        EndPositions[1] = GameObject.Find("EndPosition2").transform;
        availableTargetsParent = GameObject.Find("CustomerTargets").transform;
        occupiedTargetsParent = GameObject.Find("OccupiedCustomerTargets").transform;
        CustomerTargets = new GameObject[availableTargetsParent.childCount];
        for (int i = 0; i < availableTargetsParent.childCount; i++)
        {
            CustomerTargets[i] = availableTargetsParent.GetChild(i).gameObject;
        }
        if(CustomerTargets.Length > 0)
        {
            currentCustomersTarget = CustomerTargets[Random.Range(0, CustomerTargets.Length)].transform;
            currentCustomersTarget.SetParent(occupiedTargetsParent);
            agent.SetDestination(currentCustomersTarget.position);
        }
        else
        {
            Destroy(gameObject);
        }
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
        if(collision.gameObject.tag == "Bullet")
        {
            agent.isStopped = true;
            audioSource.pitch = Random.Range(0.75f, 1.5f);
            audioSource.PlayOneShot(deathSound);
            if (OnCivilianKilledEvent != null)
                OnCivilianKilledEvent();
            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            GetComponent<Animator>().SetTrigger("Death");
            Destroy(gameObject, 10f);
            hasCollided = true;
            currentCustomersTarget.SetParent(availableTargetsParent);
        }
        else if(collision.gameObject.tag == "Coffee")
        {
            agent.SetDestination(EndPositions[Random.Range(0,2)].position);
            if (OnOrderCompletedEvent != null)
                OnOrderCompletedEvent(20);
            Destroy(collision.gameObject);
            Destroy(gameObject, 15f);
            hasCollided = true;
            currentCustomersTarget.SetParent(availableTargetsParent);
        }
        else if (collision.gameObject.tag == "burger")
        {
            agent.SetDestination(EndPositions[Random.Range(0, 2)].position);
            if (OnOrderCompletedEvent != null)
                OnOrderCompletedEvent(60);
            Destroy(collision.gameObject);
            Destroy(gameObject, 15f);
            hasCollided = true;
            currentCustomersTarget.SetParent(availableTargetsParent);
        }
    }
}
