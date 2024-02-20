using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMediator : IMediator
{
    private ItemView itemView;
    [Regist]
    public DaimondProxy daimondProxy;
    [Regist]
    private PlayerProxy playerProxy;
    public override void Register(IView view)
    {
        itemView = (ItemView)view;
    }
    public void GetAllDaimonds()
    {
        daimondProxy.EatAllDaimonds();
    }
    public void Bomb()
    {
        Broadcast(SpecialItemEvent.ON_BOMB);
    }
    public void SetPlayerHp(int hp)
    {
        playerProxy.SetPlayerHp(hp);
    }
    [Listener(SkillEvent.ON_RANDOM_SKILL_COMPLETE)]
    private void StopTween()
    {
        itemView.StopTween(true);
    }
    [Listener(SkillEvent.ON_SKILL_LEVELUP)]
    private void ContinueTween()
    {
        itemView.StopTween(false);
    }
}
