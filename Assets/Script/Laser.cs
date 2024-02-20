using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Weapon
{
    public Animator lazerAnimator;
    public List<LaserSqure> laserSqures = new List<LaserSqure>();

    private void Awake()
    {
        foreach(var square in laserSqures)
        {
            square.onAttackEnemy = AttackEnemy;
        }
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
    }
    public void StartLaser(string name)
    {
        lazerAnimator.SetTrigger(name);
    }
    public void AttackEnemy(Enemy enemy)
    {
        enemy.Injured(damage);
    }
   
}
