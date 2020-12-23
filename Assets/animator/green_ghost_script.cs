using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class green_ghost_script : MonoBehaviour
{
    Animator rabbitAnimator;
    bool isReviving = false;
    float reviveTime = 1.5f;
    public int health = 3, maxHealth = 3;
    Transform player;
    float minDist = 1f;
    void Start()
    {
        rabbitAnimator = GameObject.Find("green_ghost").GetComponent<Animator>();
        player = GameObject.Find("GameObject").transform;
    }
    void Update()
    {
        // Do nothing when reviving
        if (isReviving) return;
        // Start reviving
        if (health < 0) {
            rabbitAnimator.SetFloat("ghost_is_die", 2f);
            StartCoroutine(Revive());
        }

        transform.LookAt(player);
        ChasePlayer();
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
        rabbitAnimator.SetFloat("ghost_is_die", 0f);
        isReviving = false;
    }

    void ChasePlayer(){
        if(Vector3.Distance(transform.position,player.position) >= minDist){ 
            transform.position += transform.forward * 2.5f *Time.deltaTime;
        }
    }
}
