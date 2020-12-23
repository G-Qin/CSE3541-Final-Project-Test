using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47 : MonoBehaviour
{
    public Rigidbody bullet;
    public float bulletSpeed;
    public AudioClip ak47Shot, noAmmo, reload;
    public AudioSource source;
    public float shootRate = 10f, clickRate = 2f;
    public float nextFire = 0f, nextClick = 0f;
    public int maxAmmo = 30, currentAmmo;
    public bool isReloading = false;
    public float reloadTime = 3f;
    public RecoilController recoilObject;
    public WeaponVisualEffects visualEffects;
    public Transform shootPosition;
    
    void Start() 
    {
        source = GetComponent<AudioSource>();
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        // Don't do anything during reload
        if (isReloading) return;

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo){
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetMouseButton(0)){
            HandleShot();            
        }
        
    }

    void HandleShot(){
        if (Time.time > nextFire && currentAmmo > 0){
            currentAmmo--;
            visualEffects.Flash();
            // Calculate next fire time
            nextFire = Time.time + 1f / shootRate;
            // Play shot sound
            source.PlayOneShot(ak47Shot);
            // Update recoil effect after each shot
            recoilObject.recoil += 0.1f;
            // Generate bullet and shoot
            ShootBullet();
        } else if (currentAmmo <= 0 && Time.time > nextClick) {
            nextClick = Time.time + 1f / clickRate;
            source.PlayOneShot(noAmmo);
        }
    }

    IEnumerator Reload(){
        isReloading = true;
        source.PlayOneShot(reload);
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void ShootBullet(){
        Rigidbody bulletInstance;
        bulletInstance = Instantiate(bullet, shootPosition.position, shootPosition.rotation) as Rigidbody;
        bulletInstance.AddForce(shootPosition.forward * bulletSpeed);
    }
}
