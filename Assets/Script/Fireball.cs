using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fireball : MonoBehaviour
{
    public Animator animator;
    public int damage;
    public Action onDestroy;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="player")
        {
            PlayerView player = collision.GetComponent<PlayerView>();
            player.PlayerDamage(damage);
        }
       
    }
    public void Destroy()
    {
        Destroy(transform.gameObject);
        onDestroy();
    }

    public void StopAmimator(bool stop)
    {
        if(stop)
        {
            animator.speed = 0;
        }
        else
        {
            animator.speed = 1;
        }
       
    }


}
