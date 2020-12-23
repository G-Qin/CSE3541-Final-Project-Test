using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rabbit_script : MonoBehaviour
{
    Animator rabbitAnimator;
    bool isReviving = false;
    float reviveTime = 1.5f;
    public int health = 3, maxHealth = 3;
    Transform player;
    void Start()
    {
        rabbitAnimator = GameObject.Find("rabbit").GetComponent<Animator>();
        player = GameObject.Find("GameObject").transform;
    }
    void Update()
    {
        // Do nothing when reviving
        if (isReviving) return;
        // Start reviving
        if (health < 0) {
            rabbitAnimator.SetFloat("rabbit_is_die", 2f);
            StartCoroutine(Revive());
        }

        transform.LookAt(player);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")){
            health--;            
        }        
    }

    IEnumerator Revive(){
        isReviving = true;
        yield return new WaitForSeconds(reviveTime);
        transform.position = new Vector3(Random.Range(-3, 17f), 0, Random.Range(6, -9f));
        health = maxHealth;
        rabbitAnimator.SetFloat("rabbit_is_die", 0f);
        isReviving = false;
    }
}
