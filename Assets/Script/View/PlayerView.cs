using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour, IView
{
    public float moveSpeed;
    public Rigidbody2D myRigidbody;
    Vector2 movement;
    public int MaxHp;
    int currentHP;
    bool isAttacked = true;
    public float coolDownTime;
    public Slider slider;
    private float originSpeed;
    private float addictionSpeed = 1;
    private Tween coolDownTween;
   [Regist]
    private PlayerMediator mediator;



    private void Awake()
    {
        Communicator.Connect(this);
        mediator.Register(this);
        SetPlayerTransform();
        currentHP = MaxHp;
        slider.maxValue = MaxHp;
        slider.value = MaxHp;
        originSpeed = moveSpeed;
    }

    private void Update()
    {

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


    }
    private void FixedUpdate()
    {
        myRigidbody.MovePosition(myRigidbody.position + movement * moveSpeed * Time.fixedDeltaTime * addictionSpeed);
    }
    
    public void IncreaseSpeed(float speed)
    {
        addictionSpeed = speed;
        Debug.Log(addictionSpeed);
    }
    public void CanMove(bool canMove)
    {
        if (canMove)
        {
            moveSpeed = originSpeed;
        }
        else
            moveSpeed = 0;
    }


    public void SetPlayerTransform()
    {
        mediator.SetPlayerTransform(transform);
    }


    public void PlayerDamage(int HP)
    {
        if (isAttacked)
        {
            currentHP -= HP;

            if (currentHP <= 0)
            {
                currentHP = 0;
                mediator.PlayerDead();
            }
            isAttacked = false;
            coolDownTween = DOVirtual.DelayedCall(coolDownTime, () =>
            {
                isAttacked = true;

            });
            slider.value = currentHP;
        }
    }
    public void IncreaseHp(int hp)
    {
        currentHP += hp;
        if (currentHP >= MaxHp)
        {
            currentHP = MaxHp;
        }
        slider.value = currentHP;
    }
    private void OnDestroy()
    {
        coolDownTween.Kill();
    }
}
