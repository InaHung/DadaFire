using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMediator : IMediator
{
    [Regist]
    private ScoreProxy scoreProxy;
    [Regist]
    private PlayerProxy playerProxy;
    private PlayerView playerView;

    public override void Register(IView view)
    {
        this.playerView = (PlayerView)view;
    }

    public void SetPlayerTransform(Transform transform)
    {
        playerProxy.SetPlayerTransform(transform);
    }

    [Listener(PlayerEvent.ON_SET_PLAYER_MOVESPEED)]
    private void SetPlayerMovespeed()
    {
        playerView.IncreaseSpeed(playerProxy.playerSpeedPercentage);
    }
    [Listener(SkillEvent.ON_RANDOM_SKILL_COMPLETE)]
    private void StopMoving()
    {
        playerView.CanMove(false);
    }
    [Listener(SkillEvent.ON_SKILL_LEVELUP)]
    private void StartMoving()
    {
        playerView.CanMove(true);
    }
    [Listener(PlayerEvent.ON_SET_PLAYER_HP)]
    private void SetHp()
    {
        playerView.IncreaseHp(playerProxy.increseHp);
    }
    public void PlayerDead()
    {
        Broadcast(PlayerEvent.ON_PLAYER_DEAD);
    }


    
}
