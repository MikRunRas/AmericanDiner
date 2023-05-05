using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PedestrianScript : MonoBehaviour
{
    public delegate void OnCivilianKilledDelegate();
    public static event OnCivilianKilledDelegate OnCivilianKilledEvent;
    public AudioClip deathSound;
    private AudioSource audioSource;
    private bool isWest;
    private float speed;
    private Rigidbody rb;
    private bool hasBeenShot = false;

    void Start()
    {
        if(transform.position.z > 0)
        {
            isWest = true;
        }
        else
        {
            isWest = false;
        }
        speed = Random.Range(2f, 3f);
        Destroy(gameObject, 60f);
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!hasBeenShot)
        {
            if (isWest)
            {
                transform.position += Vector3.back * speed * Time.deltaTime;
            }
            else
            {
                transform.position += Vector3.forward * speed * Time.deltaTime;

            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Bullet" && !hasBeenShot && collision.collider.GetComponent<Rigidbody>().velocity.magnitude > 3f)
        {
            audioSource.pitch = Random.Range(0.75f, 1.5f);
            audioSource.PlayOneShot(deathSound);
            rb.constraints = RigidbodyConstraints.FreezeAll;
            if (OnCivilianKilledEvent != null)
                OnCivilianKilledEvent();
            GetComponent<Animator>().SetTrigger("Death");
            hasBeenShot = true;
            Destroy(GetComponent<CapsuleCollider>());
            Destroy(gameObject, 10f);
        }
    }

}
