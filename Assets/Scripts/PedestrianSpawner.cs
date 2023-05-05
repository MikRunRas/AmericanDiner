using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] personPrefabs;
    private float xPosition;
    private float zPosition;
    private Quaternion rotation;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnPerson", 4f, 7f);
    }

    void SpawnPerson()
    {
        xPosition = Random.Range(6.45f, 9.4f);
        if(xPosition < 8f)
        {
            zPosition = -35f;
            rotation = new Quaternion(0f, 0f, 0f, 0f);
        }
        else
        {
            zPosition = 35f;
            rotation = new Quaternion(0f, -180f, 0f, 0f);
        }
        GameObject newPerson = Instantiate(personPrefabs[UnityEngine.Random.Range(0, 4)]);
        newPerson.transform.position = new Vector3(xPosition, 0f, zPosition);
        newPerson.transform.rotation = rotation;
    }
}
