using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event
{



}

public class ScoreEvent
{
    public const string ON_SCORE_SETTING_COMPLETE = "ScoreEvent.ON_SCORE_SETTING_COMPLETE";
    public const string ON_LEVEL_SETTING_COMPLETE = "ScoreEvent.ON_LEVEL_SETTING_COMPLETE";
    public const string ON_MAXSCORE_SETTING_COMPLETE = "ScoreEvent.ON_MAXSCORE_SETTING_COMPLETE";
}

public class PlayerEvent
{
    public const string ON_SET_PLAYER_TRANSFORM_COMPLETE = "PlayerEvent.ON_SET_PLAYER_TRANSFORM_COMPLETE";
    public const string ON_SET_PLAYER_MOVESPEED = "PlayerEvent.ON_SET_PLAYER_MOVESPEED";
    public const string ON_SET_PLAYER_HP = "PlayerEvent.ON_SET_PLAYER_HP";
    public const string ON_PLAYER_DEAD = "PlayerEvent.ON_PLAYER_DEAD";
}
public class EnemyEvent
{
    public const string ON_GET_ENEMY_INFO = "EnemyEvent.ON_GET_ENEMY_POSITION";
}
public class BossEvent
{
    public const string ON_BOSS_DEAD = "BossEvent.ON_BOSS_DEAD";
    public const string ON_BOSS_APPEAR = "BossEvent.ON_BOSS_APPEAR";
}

public class SkillEvent
{
    public const string ON_RANDOM_SKILL_COMPLETE = "SkillEvent.ON_RANDOM_SKILL_COMPLETE";
    public const string ON_SKILL_LEVELUP = "SkillEvent.ON_SKILL_LEVELUP";
}

public class SpecialItemEvent
{
    public const string ON_BOMB = "SpecialItemEvent.ON_BOMB";
}
public class EnemyTextEvent
{
    public const string ON_SHOW_ENEMY_DAMAGE = "EnemyTextEvent.ON_SHOW_ENEMY_DAMAGE";
}