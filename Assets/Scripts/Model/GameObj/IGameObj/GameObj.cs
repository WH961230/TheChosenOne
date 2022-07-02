using UnityEngine;

public class GameObj : IGameObj {
    public Data MyData;
    protected GameObject MyObj;
    public virtual void Init(Game game, Data data) {
        this.MyData = data;
        this.MyData.InstanceID = MyData.MyObj.GetInstanceID();
        MyObj = data.MyObj;
        MyObj.name = this.MyData.MyName = string.Concat(this.MyData.MyName, this.MyData.InstanceID);
        BundleBaseComponent();
    }

    protected virtual void BundleBaseComponent() {
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