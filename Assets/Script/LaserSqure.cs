using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LaserSqure : MonoBehaviour
{
    public Action<Enemy> onAttackEnemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="enemy")
        {
            Enemy enemy=collision.transform.GetComponent<Enemy>();
            onAttackEnemy(enemy);
        }
    }
}
