using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour, IView
{
    Transform playerTransform;
    public float moveSpeed;
    public Vector3 offset;
    public bool isBomb;
    public BoxCollider2D boxCollider2D;
    [Regist]
    private CameraMediator mediator;
    private void Awake()
    {
        Communicator.Connect(this);
        mediator.Register(this);
    }
    private void Start()
    {
        SetPlayerTransform();
    }
    private void LateUpdate()
    {
        transform.position = playerTransform.position + offset;
    }
    public void SetPlayerTransform()
    {
        playerTransform = mediator.GetPlayerTransform();
    }
    public void Bomb()
    {

        StartCoroutine(BombProgress());
    }
    IEnumerator BombProgress()
    {
        isBomb = true;
        boxCollider2D.enabled = true;
        yield return new WaitForSeconds(0.1f);
        isBomb = false;
        boxCollider2D.enabled = false;
    }
}
