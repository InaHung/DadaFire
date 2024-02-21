using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMediator : IMediator
{
    private GameOverView gameOverView;
    public override void Register(IView view)
    {
        gameOverView = (GameOverView)view;
    }
    [Listener(PlayerEvent.ON_PLAYER_DEAD)]
    [Listener(BossEvent.ON_FINAL_BOSS_DEAD)]
    private void GameOver()
    {
        gameOverView.GameOver();
    }

}
