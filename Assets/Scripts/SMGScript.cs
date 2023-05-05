using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMGScript : MonoBehaviour
{
    [SerializeField]
    private AudioClip SMGSound;
    private AudioSource audioSource;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject SMGTip;
    private bool isShooting = false;
    [SerializeField]
    private GameObject muzzleFlashPrefab;
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
                isShooting = true;
                StartCoroutine(SpawnBulletsCoroutine());
            }
            else if (Input.GetButtonUp("Shoot"))
            {
                isShooting = false;
            }
        }
    }

    IEnumerator SpawnBulletsCoroutine()
    {
        while (isShooting)
        {
            audioSource.pitch = Random.Range(0.75f, 1.5f);
            audioSource.PlayOneShot(SMGSound);
            GameObject bullet = Instantiate(bulletPrefab, SMGTip.transform.position + new Vector3(0f, 0f, 0.1f), SMGTip.transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(SMGTip.transform.forward * 1500f, ForceMode.Force);
            GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, SMGTip.transform.position, SMGTip.transform.rotation);
            Destroy(muzzleFlash, 0.05f);
            Destroy(bullet, 10f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
