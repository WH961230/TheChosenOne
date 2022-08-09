using UnityEngine;

public class GameObj : IGameObj {
    public Data MyData;
    protected GameComp MyComponent; // 组件
    protected Game MyGame;
    protected GameObject MyObj;

    public virtual void Init(Game game, Data data) {
        this.MyGame = game;
        this.MyData = data;

        // 设置物体
        MyObj = data.MyObj;
        MyObj.name = this.MyData.MyName = string.Concat(this.MyData.MyName, this.MyData.InstanceID);
        MyObj.transform.SetParent(MyData.MyRootTran);

        // 获取组件
        MyComponent = MyObj.transform.GetComponent<GameComp>();

        // 注册动画状态机
        if (MyComponent && null != MyComponent.RegisterAnimator) {
            MyGame.MyGameSystem.MyAnimatorSystem.GetEntity().RegisterAnimator(MyData.InstanceID, MyComponent.RegisterAnimator);
        }

        // 是否初始化物体位置
        if (data.IfInitMyObj) {
            MyObj.transform.localPosition = MyData.MyTranInfo.MyPos;
            MyObj.transform.localRotation = MyData.MyTranInfo.MyRot;
        }

        // 是否默认关闭
        if (data.IsDefaultClose) {
            Hide();
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

    public T GetComponent<T>() where T : GameComp {
        return (T) MyComponent;
    }

    public virtual void Update() {
    }

    public virtual void FixedUpdate() {
    }

    public virtual void LateUpdate() {
    }
}