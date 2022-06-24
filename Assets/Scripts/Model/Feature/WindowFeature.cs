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
    public void Register<T>() where T : IWindow, new() {
        windowManager.Register<T>(game);
    }

    public void Remove<T>() where T : IWindow {
        windowManager.Remove<T>();
    }
}