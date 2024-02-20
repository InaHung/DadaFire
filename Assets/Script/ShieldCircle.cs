using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ShieldCircle : MonoBehaviour
{
    public Action<Enemy> onAttackEnemy;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "enemy")
        {
            Enemy enemy = collision.transform.GetComponent<Enemy>();
            onAttackEnemy(enemy);
        }
    }
}
