using UnityEngine;

public class GameObj : IGameObj {
    public Data MyData;
    private GameObject myObj;
    private Transform myTransform;

    protected GameObject MyObj {
        get { return myObj; }
        set { myObj = value; }
    }

    protected Transform MyTransform {
        get {
            if (null != MyObj) {
                return MyObj.transform;
            }

            return null;
        }
        set {
            if (null != MyObj) {
                LogSystem.PrintE($"SetTransform name:{MyData.MyName} pos:{value.position} rot:{value.rotation}");
                MyTransform = value;
            }
        }
    }
    
    public virtual void Init(Game game, Data data) {
        this.MyData = data;
        this.MyData.InstanceID = MyData.MyObj.GetInstanceID();
        MyObj = data.MyObj;
        MyObj.name = this.MyData.MyName = string.Concat(this.MyData.MyName, this.MyData.InstanceID);
        MyObj.transform.SetParent(MyData.MyRootTran);
        if (data.IfInitMyObj) {
            MyObj.transform.localPosition = MyData.MyTranInfo.MyPos;
            MyObj.transform.localRotation = MyData.MyTranInfo.MyRot;
        }
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