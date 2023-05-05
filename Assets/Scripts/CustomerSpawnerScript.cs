using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawnerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject[] customerPrefabs;
    private float xPosition;
    private float zPosition;
    private Quaternion rotation;

    void Start()
    {
        InvokeRepeating("SpawnCustomer", 0f, 15f);
    }

    void SpawnCustomer()
    {
        xPosition = Random.Range(6.45f, 9.4f);
        if (xPosition < 8f)
        {
            zPosition = -35f;
            rotation = new Quaternion(0f, 0f, 0f, 0f);
        }
        else
        {
            zPosition = 35f;
            rotation = new Quaternion(0f, -180f, 0f, 0f);
        }
        GameObject newCustomer = Instantiate(customerPrefabs[UnityEngine.Random.Range(0, 4)]);
        newCustomer.transform.position = new Vector3(xPosition, 0f, zPosition);
        newCustomer.transform.rotation = rotation;
    }
}
