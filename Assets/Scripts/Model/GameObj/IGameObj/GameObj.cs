﻿using UnityEngine;

public class GameObj : IGameObj {
    protected GameComp MyComp; // 组件
    protected GameObject MyObj;
    protected Game MyGame;

    public virtual void Init(Game game, Data data) {
        MyGame = game;

        // 物体
        MyObj = data.MyObj;
        MyObj.transform.SetParent(data.MyRootTran);
        MyObj.transform.localPosition = data.MyTranInfo.MyPos; // 物体位置
        MyObj.transform.localRotation = data.MyTranInfo.MyRot;

        MyObj.name = data.MyName = string.Concat(data.MyName, data.InstanceID);

        // 组件
        var comp = MyObj.transform.GetComponent<GameComp>();
        MyComp = comp;

        // 动画状态机
        if (comp && null != comp.RegisterAnimator) {
            var entity = MyGame.MyGameSystem.MyAnimatorSystem.GetEntity();
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

    // 卸载
    public virtual void UnInstall(Transform root, Vector3 point, Quaternion rot, bool isLocal) {
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

    public virtual void Update() {
    }

    public virtual void FixedUpdate() {
    }

    public virtual void LateUpdate() {
    }

    protected GameComp GetComp() {
        return MyComp;
    }
}