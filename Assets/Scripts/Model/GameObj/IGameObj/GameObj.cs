﻿using UnityEngine;

public class GameObj : IGameObj {
    public Data MyData;
    protected GameObjFeature gameObjFeature;
    protected GameObject MyObj;
    public virtual void Init(Game game, Data data) {
        this.gameObjFeature = game.Get<GameObjFeature>();
        this.MyData = data;
        this.MyData.InstanceID = MyData.MyObj.GetInstanceID();
        MyObj = data.MyObj;
        BundleBaseComponent();
        BundleCustomComponent();
    }

    public virtual void BundleBaseComponent() {
        MyObj.transform.position = MyData.MyTranInfo.MyPos;
        MyObj.transform.rotation = MyData.MyTranInfo.MyRot;
    }

    public virtual void BundleCustomComponent() {
        
    }

    public virtual void Clear() {
    }

    public virtual void Update() {
    }
}