﻿using UnityEngine;

public class Game : MonoBehaviour {
    #region 数据

    private GameManager gameManager = new GameManager();

    #endregion

    #region 系统

    private GameSystem gameSystem = new GameSystem();

    public GameSystem MyGameSystem {
        get { return gameSystem; }
    }

    #endregion

    #region 组件

    private GameComponent gameComponent = new GameComponent();

    public GameComponent MyGameComponent {
        get { return gameComponent; }
    }

    #endregion

    #region 模块

    public GameObjFeature MyGameObjFeature {
        get { return Get<GameObjFeature>(); }
    }

    public WindowFeature MyWindowFeature {
        get { return Get<WindowFeature>(); }
    }

    public EntityFeature MyEntityFeature {
        get { return Get<EntityFeature>(); }
    }

    #endregion

    #region 消息

    private GameMessageCenter gameMessageCenter = new GameMessageCenter();
    public GameMessageCenter MyGameMessageCenter {
        get {
            return gameMessageCenter;
        }
    }

    #endregion
    void Start() {
        // 数据模块模块注册
        Register<WindowFeature>();
        Register<GameObjFeature>();
        Register<EntityFeature>();

        // 逻辑初始化
        gameSystem.Init(this);
        gameComponent.Init(this);
    }

    void Clear() {
        Remove<WindowFeature>();
        Remove<GameObjFeature>();
        Remove<EntityFeature>();
        gameSystem.Clear();
        gameComponent.Clear();
    }

    void Update() {
        gameSystem.Update();
        gameManager.Update();
        gameComponent.Update();
    }

    private void Register<T>() where T : IFeature, new() {
        gameManager.Register<T>(this);
    }

    private void Remove<T>() where T : IFeature, new() {
        gameManager.Remove<T>();
    }

    public T Get<T>() where T : IFeature {
        return gameManager.Get<T>();
    }
}