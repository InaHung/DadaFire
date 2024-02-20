using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class MaxDrill : Weapon
{
    private Enemy neareatEnemy;
    public float moveSpeed;
    private bool isSpin;
    public float radius = 2;
    public int resolution = 40;
    public float spinDurationTime = 0.5f;
    private Tween spinTween;
    public Func<Enemy> onGetNearEnemy;
    
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.Injured(damage);


        }
    }
    public void GetNearEnemy(Enemy enemy)
    {
        neareatEnemy = enemy;
        
    }
    private void Update()
    {
        if(neareatEnemy==null||neareatEnemy.isDead==true)
        {
            spinTween.Kill();
            isSpin = false;
            neareatEnemy = onGetNearEnemy();
        }
        if (!isSpin&& neareatEnemy != null)
        {
            if (Vector2.Distance(transform.position, neareatEnemy.transform.position) < 0.05f)
            {
                Spin();
                return;
            }
            transform.position=Vector2.MoveTowards(transform.position, neareatEnemy.transform.position, moveSpeed*Time.deltaTime);

        }
    }

    private void Spin()
    {
        isSpin = true;
        Vector2 centerPosition = (Vector2)neareatEnemy.transform.position - new Vector2(radius, 0);
        Vector3[] pathPositions = GenerateCirclePoints(radius, resolution, centerPosition);
        spinTween = transform.DOPath(pathPositions, spinDurationTime).OnComplete(() =>
        {
            isSpin = false;
        });
    }

    private Vector3[] GenerateCirclePoints(float radius, int resolution, Vector2 centerPosition)
    {
        Vector3[] points = new Vector3[resolution];
        float angleIncrement = 360f / resolution;

        for (int i = 0; i < resolution; i++)
        {
            float angle = i * angleIncrement;
            float x = centerPosition.x + Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            float y = centerPosition.y + Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            points[i] = new Vector3(x, y, 0);
        }

        return points;
    }
    public void Pause(bool pause)
    {
        if(pause)
        {
            spinTween.Pause();
        }
        else
        {
            spinTween.Play();
        }
    }
    private void OnDestroy()
    {
        spinTween.Kill();
    }

}
