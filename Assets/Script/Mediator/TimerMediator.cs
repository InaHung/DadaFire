using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerMediator : IMediator
{
    [Regist]
    private TimerProxy timerProxy;
    public TimerView timerView;

    public override void Register(IView view)
    {
        timerView = (TimerView)view;

    }
    public void SetCurrentTime(float time)
    {
        timerProxy.curTime = time;
    }
    [Listener(BossEvent.ON_BOSS_APPEAR)]
    [Listener(SkillEvent.ON_RANDOM_SKILL_COMPLETE)]
    private void TimePulse()
    {
        timerView.pause = true;
    }
    [Listener(SkillEvent.ON_SKILL_LEVELUP)]
    [Listener(BossEvent.ON_BOSS_DEAD)]
    private void TimeContinue()
    {
        timerView.pause = false;
    }



}
