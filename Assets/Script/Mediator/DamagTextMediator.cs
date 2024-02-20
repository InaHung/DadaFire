using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagTextMediator : IMediator
{
    [Regist]
    private EnemyProxy enemyProxy;
    private DamageTextView damageTextView;
    public override void Register(IView view)
    {
        damageTextView = (DamageTextView)view;
    }
    [Listener(EnemyTextEvent.ON_SHOW_ENEMY_DAMAGE)]
    private void ShowEnemyDamage()
    {
        damageTextView.ShowText(enemyProxy.damagePosition,enemyProxy.damageText);
    }
}
