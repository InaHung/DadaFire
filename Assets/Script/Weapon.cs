using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : IPoolable
{
    public int damage;
    public bool pause;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy"&&pause==false)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.Injured(damage);
            

        }
    }
  


}
