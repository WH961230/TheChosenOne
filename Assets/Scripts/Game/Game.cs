using UnityEngine;

public class Game : MonoBehaviour {
    private GameManager gameManager = new GameManager();
    private GameSystem gameSystem = new GameSystem();
    void Start() {
        // 数据模块模块注册
        Register<WindowFeature>();
        Register<GameObjFeature>();
        Register<EntityFeature>();
        
        // 逻辑初始化
        gameSystem.Init(this);
    }

    void Clear() {
        Remove<WindowFeature>();
        Remove<GameObjFeature>();
        Remove<EntityFeature>();
    }

    void Update() {
        gameManager.Update();
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