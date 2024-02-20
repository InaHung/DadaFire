using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMediator : IMediator
{
    [Regist]
    private ScoreProxy scoreProxy;
    private ScoreView scoreView;


    public override void Register(IView view)
    {
        scoreView = (ScoreView)view;
    }

   public int SetInitLevel()
    {
        return scoreProxy.curLevel;
        
    }
    public int SetInitScore()
    {
        return scoreProxy.maxScore;

    }


    [Listener(ScoreEvent.ON_SCORE_SETTING_COMPLETE)]
    private void SetScore()
    {
        
        scoreView.SetScore(scoreProxy.currentScore);
    }
    [Listener(ScoreEvent.ON_LEVEL_SETTING_COMPLETE)]
    private void SetCurrentLevel()
    {
        scoreView.SetLevel(scoreProxy.curLevel);
    }
    [Listener(ScoreEvent.ON_MAXSCORE_SETTING_COMPLETE)]
    private void SetMaxSlider()
    {
        scoreView.SetMaxSlider(scoreProxy.maxScore);
    }

}
