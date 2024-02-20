using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMediator : IMediator
{
    [Regist]
    private PlayerProxy playerProxy;
    CameraView cameraView;
    public override void Register(IView view)
    {
        cameraView = (CameraView)view;
    }
    public Transform GetPlayerTransform()
    {
        return playerProxy.playerTransform;
    }
    [Listener(SpecialItemEvent.ON_BOMB)]
    private void Bomb()
    {
        cameraView.Bomb();
    }
}
