using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : Weapon
{
    public SpriteRenderer sprite;
    public Vector3 MoveDirection
    {
        get; set;
    }
    public float moveSpeed;

    private void Update()
    {

        transform.Translate(MoveDirection * moveSpeed * Time.deltaTime);

    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.tag == "cameraColliderUp")
        {
            MoveDirection = Vector2.Reflect(MoveDirection, Vector2.up);
            sprite.transform.right = MoveDirection;
        }
        if (collision.tag == "cameraColliderDown")
        {
            MoveDirection = Vector2.Reflect(MoveDirection, Vector2.down);
            sprite.transform.right = MoveDirection;
        }
        if (collision.tag == "cameraColliderRight")
        {
            MoveDirection = Vector2.Reflect(MoveDirection, Vector2.right);
            sprite.transform.right = MoveDirection;
        }
        if (collision.tag == "cameraColliderLeft")
        {
            MoveDirection = Vector2.Reflect(MoveDirection, Vector2.left);
            sprite.transform.right = MoveDirection;
        }
    }
   
}


