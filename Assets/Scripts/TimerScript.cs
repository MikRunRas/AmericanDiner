using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject progressBar; 
    [SerializeField]
    private GameObject barSize;
    public float progressTime;
    private Vector3 scale;

    private void Start()
    {
        scale = progressBar.transform.localScale;
    }
    void Update()
    {
        scale.z += progressTime * Time.deltaTime;
        progressBar.transform.localScale = scale;
        progressBar.transform.localPosition += new Vector3(0f, 0f, -1f) * (progressTime * Time.deltaTime) / 2;

        if(progressBar.transform.localScale.z > barSize.transform.localScale.z)
        {
            Destroy(gameObject);
        }
    }
}
