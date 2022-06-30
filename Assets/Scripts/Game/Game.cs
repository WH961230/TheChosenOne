using UnityEngine;

public class Game : MonoBehaviour {
    private GameManager gameManager = new GameManager();
    private GameSystem gameSystem = new GameSystem();
    private GameComponent gameComponent = new GameComponent();
    public GameSystem MyGameSystem {
        get {
            return gameSystem;
        }
    }

    public GameComponent MyGameComponent {
        get {
            return gameComponent;
        }
    }

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