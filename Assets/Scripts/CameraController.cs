using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform thirdPersonCameraPosition;
    [SerializeField]
    Transform firstPersonCameraPosition;
    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    GameObject playerHead;

    private bool _isFirstPerson = false;
    private Transform _targetPos;

    private void Start()
    {
        _targetPos = thirdPersonCameraPosition;
    }

    void Update()
    {
        if (Input.GetButtonDown("ChangeCamera"))
        {
            if (_isFirstPerson)
            {
                _targetPos = thirdPersonCameraPosition.transform;
                mainCamera.transform.SetParent(null);
            }
            else
            {
                _targetPos = firstPersonCameraPosition.transform;
                mainCamera.transform.SetParent(playerHead.transform);
            }

            _isFirstPerson = !_isFirstPerson;
        }
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(mainCamera.transform.position, _targetPos.position) < 0.01f)
            return;

        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, _targetPos.position, Time.deltaTime * 10);
        mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, _targetPos.rotation, Time.deltaTime * 10);
    }
}
