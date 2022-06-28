public class WindowFeature : IFeature {
    private WindowManager windowManager = new WindowManager();
    private Game game;
    public void Init(Game game) {
        this.game = game;
    }

    public void Update() {
        windowManager.Update();
    }

    public void Clear() {
        windowManager.RemoveAll();
    }

    // 窗口注册 自动注入 物体注册
    public void Register<T>(Data data) where T : IWindow, new() {
        windowManager.Register<T>(game, data);
    }

    public void Remove<T>() where T : IWindow {
        windowManager.Remove<T>();
    }
}