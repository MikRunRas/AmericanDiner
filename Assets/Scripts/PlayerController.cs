using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject playerHand;
    private float moveSpeed = 5f;
    private float rotateSpeed = 200f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.Rotate(Vector3.up * horizontal * rotateSpeed * Time.fixedDeltaTime);

        Vector3 moveDirection = transform.right * vertical * moveSpeed;
        rb.velocity = moveDirection;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Throw") && playerHand.transform.childCount > 0)
        {
            GameObject objectToThrow = playerHand.transform.GetChild(0).gameObject;
            Rigidbody objectRigidbody = objectToThrow.GetComponent<Rigidbody>();
            if(objectRigidbody != null)
            {
                objectRigidbody.constraints = RigidbodyConstraints.None;
                objectToThrow.transform.parent = null;
                Vector3 throwDirection = transform.right;
                float throwForce = 10f;
                objectRigidbody.AddForce(throwDirection * throwForce, ForceMode.Impulse);
            }
        }
    }

}
