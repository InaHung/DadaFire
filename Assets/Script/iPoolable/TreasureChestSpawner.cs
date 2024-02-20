using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class TreasureChestSpawner : MonoBehaviour
{
    public ObjectPool myPool;
    public Action<Vector3> onGetTreasure;
    public int initSpawnCount;
    public Tween spawnTween;
    public float reSpawnTime;
    private void Start()
    {
        for(int i=0;i<initSpawnCount;i++)
        {
            for(int j=0;j<initSpawnCount;j++)
            {
                float x = (i - (initSpawnCount - 1) / 2)*20f;
                float y = (j - (initSpawnCount - 1) / 2)*20f;
                Vector3 spawnPosition = new Vector3(x, y, 0);
                TreasureSpawn(spawnPosition);
            }
        }

    }
    public void TreasureSpawn(Vector3 spawnPosition)
    {
        TreasureChest treasureChest = myPool.GetObject() as TreasureChest;
        treasureChest.transform.position = spawnPosition;
        treasureChest.onGetTreasure = GetTreasure;
        
    }
    public void GetTreasure(Vector3 position)
    {
        spawnTween = DOVirtual.DelayedCall(reSpawnTime, () =>
           {
               TreasureSpawn(position);
           });
        onGetTreasure(position);
    }
    private void OnDestroy()
    {
        spawnTween.Kill();
    }


}
