using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CarSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] carPrefabs;
    private float xPosition;
    private float zPosition;
    private Quaternion rotation;
    void Start()
    {
        InvokeRepeating("SpawnCar", 0f, 10f);
    }

    void SpawnCar()
    {
        if(Random.Range(0f, 10f) < 5f)
        {
            xPosition = 86f;
            zPosition = 55f;
            rotation = new Quaternion(0f, -180f, 0f, 0f);
        }
        else
        {
            xPosition = -55.7f;
            zPosition = -51f;
            rotation = new Quaternion(0f, 0f, 0f, 0f);

        }
        GameObject newCar = Instantiate(carPrefabs[UnityEngine.Random.Range(0, 3)]);
        newCar.transform.position = new Vector3(xPosition, -42.36f, zPosition);
        newCar.transform.rotation = rotation;
    }
}
