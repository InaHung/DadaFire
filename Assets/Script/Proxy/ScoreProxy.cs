using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreProxy : IProxy
{
    public EXPSetting expSetting;
    public int currentScore;
    public int curLevel;
    public int maxScore;
    [Regist]
    private SkillProxy skillProxy;
    public void SetInitLevel()
    {
        curLevel = expSetting.singleEXPSettings[0].level;
        maxScore = expSetting.singleEXPSettings[0].maxScore;
        currentScore = 0;
    }
    public void AddScore(int score)
    {
        currentScore += score;
        
        if (currentScore >= maxScore)
        {
            currentScore -= maxScore;
            curLevel++;
            skillProxy.GiveSkills();
            Broadcast(ScoreEvent.ON_LEVEL_SETTING_COMPLETE);
            maxScore = expSetting.singleEXPSettings[curLevel - 1].maxScore;
            Broadcast(ScoreEvent.ON_MAXSCORE_SETTING_COMPLETE);
        }
        Broadcast(ScoreEvent.ON_SCORE_SETTING_COMPLETE);
    }
    
}
