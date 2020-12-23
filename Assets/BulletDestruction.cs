using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestruction : MonoBehaviour
{
    public ParticleSystem hitEffect;
    void OnCollisionEnter(Collision collision)
    {
        ParticleSystem hitEffectInstance;
        hitEffectInstance = Instantiate(hitEffect, transform.position, transform.rotation);
        hitEffectInstance.Play();
        Destroy(hitEffectInstance, 1f);
        Destroy(gameObject);
    }
}
