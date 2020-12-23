using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponVisualEffects : MonoBehaviour
{
    public ParticleSystem muzzleFlash;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Flash()
    {
        muzzleFlash.Play();
        
    }

}
