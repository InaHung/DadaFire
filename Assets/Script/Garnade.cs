using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Garnade : MonoBehaviour
{
    public float moveSpeed;
    private float originSpeed;
    public int damage;
    private Tween destroyTween;
    public float destroyTime;
    private Vector2 moveDirection;
    private void Awake()
    {
        originSpeed = moveSpeed;
    }
    void Update()
    {
        transform.Translate(moveDirection* moveSpeed*Time.deltaTime);
    }
    public void GarnadeMove(Vector2 direction)
    {
        moveDirection = direction;
        destroyTween = DOVirtual.DelayedCall(destroyTime, () =>
           {
               Destroy(transform.gameObject);
           });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "player")
        {
            PlayerView player = collision.transform.GetComponent<PlayerView>();
            player.PlayerDamage(damage);
            Destroy(transform.gameObject);
            destroyTween.Kill();
        }
        if (collision.tag == "borderUp")
        {
            moveDirection = Vector2.Reflect(moveDirection, Vector2.up);

        }
        if (collision.tag == "borderDown")
        {
            moveDirection = Vector2.Reflect(moveDirection, Vector2.down);

        }
        if (collision.tag == "borderRight")
        {
            moveDirection = Vector2.Reflect(moveDirection, Vector2.right);

        }
        if (collision.tag == "borderLeft")
        {
            moveDirection = Vector2.Reflect(moveDirection, Vector2.left);

        }
    }
    public void StopAction(bool pause)
    {
        if(pause)
        {
            destroyTween.Pause();
            moveSpeed = 0;
        }   
        else
        {
            destroyTween.Play();
            moveSpeed = originSpeed;
        }
    }
    private void OnDestroy()
    {
        destroyTween.Kill();
    }

}
