using UnityEngine;

public class GameObj : IGameObj {
    public Data MyData;
    private GameObject myObj;
    protected MonoBehaviour MyComponent; // 组件

    protected Game MyGame;

    protected GameObject MyObj {
        get { return myObj; }
        set { myObj = value; }
    }

    public virtual void Init(Game game, Data data) {
        this.MyGame = game;
        this.MyData = data;

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

    public virtual void Drop() {
    }

    public virtual void Clear() {
    }

    public T GetData<T>() where T : Data {
        return (T)MyData;
    }

    public T GetComponent<T>() where T : MonoBehaviour {
        return (T) MyComponent;
    }

    public virtual void Update() {
    }

    public virtual void FixedUpdate() {
    }

    public virtual void LateUpdate() {
    }
}