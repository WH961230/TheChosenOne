using UnityEngine;

public class GameObj : IGameObj {
    protected GameComp Comp; // 组件
    protected GameObject MyObj;
    protected GameSystem GS;
    protected GameMessageCenter GMC;

    public virtual void Init(Game game, Data data) {
        GS = game.MyGameSystem;
        GMC = game.MyGameMessageCenter;

        // 物体
        MyObj = data.MyObj;
        MyObj.transform.SetParent(data.MyRootTran);
        MyObj.transform.localPosition = data.MyTranInfo.MyPos; // 物体位置
        MyObj.transform.localRotation = data.MyTranInfo.MyRot;

        MyObj.name = data.MyName = data.MyName + data.Sign + '_' + '[' + data.InstanceID + ']';

        // 组件
        var comp = MyObj.transform.GetComponent<GameComp>();
        Comp = comp;

        // 动画状态机
        if (comp && null != comp.RegisterAnimator) {
            var entity = GS.Get<AnimatorSystem>().GetEntity();
            entity.RegisterAnimator(data.InstanceID, comp.RegisterAnimator);
        }

        // 是否默认关闭
        if (data.IsActive) {
            Display();
        } else {
            Hide();
        }
    }

    public virtual void Display() {
        MyObj.SetActive(true);
    }

    public virtual void Hide() {
        MyObj.SetActive(false);
    }

    public virtual void SetDir(Quaternion dir, bool local = false) {
        if (local) {
            MyObj.transform.localRotation = dir;
        } else {
            MyObj.transform.rotation = dir;
        }
    }

    public virtual void SetPos(Vector3 position, bool local = false) {
        if (local) {
            MyObj.transform.localPosition = position;
        } else {
            MyObj.transform.position = position;
        }
    }

    public virtual void SetChild(Transform root, Vector3 point, Vector3 rot, bool isLocal, bool isShow) {
        // 设置父节点
        MyObj.transform.SetParent(root);

        // 设置位置
        if (isLocal) {
            MyObj.transform.localPosition = point;
            MyObj.transform.localRotation = Quaternion.Euler(rot);
        } else {
            MyObj.transform.position = point;
            MyObj.transform.rotation = Quaternion.Euler(rot);
        }

        if (isShow) {
            Display();
        } else {
            Hide();
        }
    }

    public virtual void Clear() {
    }

    public virtual void Update() {
    }

    public virtual void FixedUpdate() {
    }

    public virtual void LateUpdate() {
    }

    public GameObject GetObj() {
        return MyObj;
    }

    protected GameComp GetComp() {
        return Comp;
    }
}