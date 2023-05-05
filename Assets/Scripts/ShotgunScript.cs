using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShotgunScript : MonoBehaviour
{
    [SerializeField]
    private AudioClip shotgunSound;
    private AudioSource audioSource;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject player; 
    [SerializeField]
    private GameObject shotgunTip;
    [SerializeField]
    private GameObject muzzleFlashPrefab;

    private void Start()
    {
        audioSource= GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (gameObject.transform.parent != null && gameObject.transform.parent.tag == "PlayerHand")
        {
            if (Input.GetButtonDown("Shoot"))
            {
                audioSource.pitch = Random.Range(0.75f, 1.5f);
                audioSource.PlayOneShot(shotgunSound);
                player.GetComponent<Rigidbody>().AddForce(-player.transform.right * 1000f, ForceMode.Force);
                GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, shotgunTip.transform.position, shotgunTip.transform.rotation);
                Destroy(muzzleFlash, 0.05f);
                for (int i = 0; i < 10; i++)
                {
                    float spreadAngle = Random.Range(-15f, 15f);
                    Vector3 bulletDirection = Quaternion.Euler(spreadAngle, spreadAngle, spreadAngle) * shotgunTip.transform.right;
                    GameObject bullet = Instantiate(bulletPrefab, shotgunTip.transform.position + new Vector3(0.1f, 0.1f, 0.1f), Quaternion.identity);
                    bullet.GetComponent<Rigidbody>().AddForce(bulletDirection * 1000f, ForceMode.Force);
                    Destroy(bullet, 10f);
                }
            }
        }
    }
}
