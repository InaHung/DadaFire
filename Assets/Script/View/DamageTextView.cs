using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageTextView : MonoBehaviour,IView
{
    [Regist]
    private DamagTextMediator mediator;
    public ObjectPool myPool;
    private void Awake()
    {
        Communicator.Connect(this);
        mediator.Register(this);
    }
    public void ShowText(Vector3 position,float damage)
    {
        EnemyDamageText damageText = myPool.GetObject() as EnemyDamageText;
        damageText.textMesh.text = damage.ToString();
        damageText.transform.position = position;
    }

   
}
