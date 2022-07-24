using UnityEngine;

public class GameObj : IGameObj {
    public Data MyData;
    protected MonoBehaviour MyComponent; // 组件
    protected Game MyGame;
    protected GameObject MyObj;

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
        LogSystem.Print("拾取隐藏 id: " + MyData.InstanceID);
        MyObj.SetActive(false);
    }

    public virtual void Drop(Vector3 point) {
        LogSystem.Print("丢弃到玩家脚下 id: " + MyData.InstanceID);
        Display();
        MyObj.transform.position = point;
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