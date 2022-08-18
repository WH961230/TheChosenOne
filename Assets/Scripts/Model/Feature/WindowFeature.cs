public class WindowFeature : IFeature {
    private WindowManager windowManager = new WindowManager();
    private Game game;
    public void Init(Game game) {
        this.game = game;
    }

    public void Update() {
        windowManager.Update();
    }

    public void FixedUpdate() {
        windowManager.FixedUpdate();
    }
    
    public void LateUpdate() {
        windowManager.LateUpdate();
    }

    public void Clear() {
        windowManager.RemoveAll();
    }

    // 窗口注册 自动注入 物体注册
    public void Register<T>(GameObj go) where T : IWindow, new() {
        // LogSystem.Print($"注册 Window => data.Name: {data.MyName}");
        windowManager.Register<T>(game, go);
    }

    public void Remove<T>() where T : IWindow {
        windowManager.Remove<T>();
    }

    public T Get<T>() where T : IWindow, new() {
        return windowManager.Get<T>();
    }
}