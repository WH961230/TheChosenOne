using System;
using System.Collections.Generic;
using UnityEngine;

public class GameObj : IGameObj {
    public Data MyData;
    protected GameObjFeature gameObjFeature;
    protected GameObject MyObj;
    protected List<Action> MyAction;
    public virtual void Init(Game game, Data data) {
        this.gameObjFeature = game.Get<GameObjFeature>();
        this.MyData = data;
        this.MyData.InstanceID = MyData.MyObj.GetInstanceID();
        MyObj = data.MyObj;
        BundleBaseComponent();
    }

    public virtual void BundleBaseComponent() {
        MyObj.transform.SetParent(MyData.MyTranInfo.MyRootTran);
        MyObj.transform.localPosition = MyData.MyTranInfo.MyPos;
        MyObj.transform.localRotation = MyData.MyTranInfo.MyRot;
    }

    public virtual void Display() {
        MyObj.SetActive(true);
    }

    public virtual void Hide() {
        MyObj.SetActive(false);
    }

    public virtual void Clear() {
    }

    public virtual void Update() {
    }
}