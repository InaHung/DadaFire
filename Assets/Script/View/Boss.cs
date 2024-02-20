using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Boss : Enemy
{
    public Rigidbody2D myRigidbody;
    public float impulse;
    private bool isSprint;
    private Tween sprintTween;
    private Tween sprintIntervalTween;
    private Tween firstSprintTween;
    private Tween waitToSprintTween;
    public float sprintTime;
    public float firstSprintTime;
    public float sprintIntervalTime;
    protected GameObject bossBorder;
    public GameObject arrow;
    private Vector2 originVelocity;
    public Slider slider;
    protected override void Awake()
    {
        base.Awake();
        slider.maxValue = maxHP;
        slider.value = currentHP;
    }
    protected override void EnemyMove()
    {
        if (playerTransform != null && isSprint == false)
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
    }
    public override void Injured(int damage)
    {
        currentHP -= damage;
        slider.value = currentHP;
        onInjured(transform.position, damage);
        EnemyDead();
        

    }
    public virtual void StartAction()
    {
        firstSprintTween = DOVirtual.DelayedCall(firstSprintTime, () =>
        {
            Sprint();
        });
    }
    public void Sprint()
    {
        isSprint = true;
        Vector2 moveDirection = (playerTransform.position - transform.position).normalized;
        arrow.SetActive(true);
        arrow.transform.right = moveDirection;
        waitToSprintTween=DOVirtual.DelayedCall(1f, () =>
        {
            arrow.SetActive(false);
            myRigidbody.AddForce(moveDirection * impulse, ForceMode2D.Impulse);
           
        });
        sprintTween = DOVirtual.DelayedCall(sprintTime, () =>
        {
            isSprint = false;
        });
        sprintIntervalTween = DOVirtual.DelayedCall(sprintIntervalTime, () =>
        {
            Sprint();
        });
    }
    public override void Pause(bool pause)
    {
        if (pause)
        {
            firstSprintTween.Pause();
            sprintIntervalTween.Pause();
            sprintTween.Pause();
            waitToSprintTween.Pause();
            moveSpeed = 0;
            originVelocity = myRigidbody.velocity;
            myRigidbody.velocity = new Vector2(0, 0);
        }
        else
        {
            firstSprintTween.Play();
            sprintIntervalTween.Play();
            sprintTween.Play();
            waitToSprintTween.Play();
            moveSpeed = originSpeed;
            myRigidbody.velocity = originVelocity;
        }
    }
    public void SetBorder(GameObject border)
    {
        bossBorder = border;
    }
    private void OnDestroy()
    {
        firstSprintTween.Kill();
        sprintIntervalTween.Kill();
        sprintTween.Kill();
        waitToSprintTween.Kill();
    }
}
    
   


