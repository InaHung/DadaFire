using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Weapon
{
    public Vector3 rotate;
    public float moveSpeed;
    public ShieldCircle circle;
    private Transform playerTransform;
    public float radius;
    public List<ShieldCircle> curCircle = new List<ShieldCircle>();
    private float originSpeed;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
      
    }
    private void Update()
    {
        transform.Rotate(rotate*moveSpeed*Time.deltaTime);
        transform.position = playerTransform.position;
    }
    public void SetPlayerTransform(Transform transform)
    {
        playerTransform = transform;
    }
   
    public void SheildCount(int count)
    {
       
        foreach(ShieldCircle circleObject in curCircle)
        {
            Destroy(circleObject.gameObject);
        }
        curCircle.Clear();
        for(int i=0;i<count;i++)
        {
            float angle = i*(360 / count);
            float radians = angle * Mathf.Deg2Rad;
            Vector3 spawnPosition = new Vector3(transform.position.x+radius * Mathf.Cos(radians),transform.position.y+radius * Mathf.Sin(radians), 0);
            ShieldCircle shieldCircle =Instantiate(circle, spawnPosition, Quaternion.identity);
            shieldCircle.transform.SetParent(transform);
            curCircle.Add(shieldCircle);
            shieldCircle.onAttackEnemy =(enemy)=>
            {
                enemy.Injured(damage);
            }; 
        }
    }
    
}
