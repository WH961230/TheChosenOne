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

    /// <summary>
    /// 装载
    /// </summary>
    /// <param name="root"></param>
    /// <param name="point"></param>
    /// <param name="rot"></param>
    /// <param name="isLocal"></param>
    public virtual void Install(Transform root, Vector3 point, Quaternion rot, bool isLocal) {
        LogSystem.Print("装载 id: " + MyData.InstanceID);
        // 显示
        Display();
        // 设置父节点
        MyObj.transform.SetParent(root);
        // 设置位置
        if (isLocal) {
            MyObj.transform.localPosition = point;
            MyObj.transform.localRotation = rot;
        } else {
            MyObj.transform.position = point;
            MyObj.transform.rotation = rot;
        }
    }

    /// <summary>
    /// 卸载
    /// </summary>
    /// <param name="root"></param>
    /// <param name="point"></param>
    /// <param name="rot"></param>
    /// <param name="isLocal"></param>
    public virtual void UnInstall(Transform root, Vector3 point, Quaternion rot, bool isLocal) {
        LogSystem.Print("卸载 id: " + MyData.InstanceID);
        // 设置父节点
        MyObj.transform.SetParent(root);
        // 设置位置
        if (isLocal) {
            MyObj.transform.localPosition = point;
            MyObj.transform.localRotation = rot;
        } else {
            MyObj.transform.position = point;
            MyObj.transform.rotation = rot;
        }

        // 隐藏
        Hide();
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