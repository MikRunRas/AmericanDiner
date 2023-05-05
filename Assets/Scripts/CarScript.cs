using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    private bool isWest;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        if (transform.position.z > 0)
        {
            isWest = true;
        }
        else
        {
            isWest = false;
        }
        speed = Random.Range(5f, 10f);
        Destroy(gameObject, 30);
    }

    // Update is called once per frame
    void Update()
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
