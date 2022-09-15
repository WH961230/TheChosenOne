using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameSystem {
    #region 全局系统

    private Game game;

    public GameObjFeature GOFeature {
        get { return game.MyGameObjFeature; }
    }

    public WindowFeature WinFeature {
        get { return game.MyWindowFeature; }
    }

    public EntityFeature EntityFeature {
        get { return game.MyEntityFeature; }
    }

    #endregion

    #region 子系统

    private List<GameSys> systems = new List<GameSys>();

    #endregion

    public T Get<T>() where T : GameSys {
        foreach (var system in systems) {
            if (system.GetType() == typeof(T)) {
                return system as T;
            }
        }

        return null;
    }

    public void Init(Game game) {
        this.game = game;
        foreach (var systemSetting in SoData.MySOGameSetting.systemSetting) {
            var instance = systemSetting.OnInit(this);
            systems.Add(instance);
        }
        
        // 全局 UI
        Get<UISystem>().InstanceUIMainWindow();
    }

    public void Update() {
        foreach (var system in systems) {
            system.Update();
        }
    }

    public void FixedUpdate() {
        foreach (var system in systems) {
            system.FixedUpdate();
        }
    }

    public void LateUpdate() {
        foreach (var system in systems) {
            system.LateUpdate();
        }
    }

    public void Clear() {
        foreach (var system in systems) {
            system.Clear();
        }
    }

    public int InstanceWindow<T1, T2, T3>(Data data) where T1 : IWindow, new() where T2 : GameObj, new() where T3 : Entity, new() {
        data.InstanceID = data.MyObj.GetInstanceID();
        game.MyEntityFeature.Register<T3>(data);
        var go = game.MyGameObjFeature.Register<T2>(data);
        game.MyWindowFeature.Register<T1>(go);
        return data.InstanceID;
    }

    public int InstanceGameObj<T1, T2>(Data data) where T1 : GameObj, new() where T2 : Entity, new() {
        data.InstanceID = data.MyObj.GetInstanceID();
        game.MyEntityFeature.Register<T2>(data);
        game.MyGameObjFeature.Register<T1>(data);
        return data.InstanceID;
    }

    public void InstanceEntity<T>(Data data) where T : Entity, new() {
        data.InstanceID = data.MyObj.GetInstanceID();
        game.MyEntityFeature.Register<T>(data);
    }
}