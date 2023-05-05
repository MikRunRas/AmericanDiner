using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlockScript : MonoBehaviour
{
    [SerializeField]
    private AudioClip glockSound;
    private AudioSource audioSource;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject muzzleFlashPrefab;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject glockTip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (gameObject.transform.parent != null && gameObject.transform.parent.tag == "PlayerHand")
        {
            if (Input.GetButtonDown("Shoot"))
            {
                audioSource.pitch = Random.Range(0.75f, 1.5f);
                audioSource.PlayOneShot(glockSound);
                GameObject bullet = Instantiate(bulletPrefab, glockTip.transform.position + new Vector3(0f,0f,0.1f), glockTip.transform.rotation);
                GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, glockTip.transform.position, glockTip.transform.rotation);
                bullet.GetComponent<Rigidbody>().AddForce(glockTip.transform.forward * 1500f, ForceMode.Force);
                Destroy(bullet, 10f);
                Destroy(muzzleFlash, 0.05f);
            }
        }
    }
}
