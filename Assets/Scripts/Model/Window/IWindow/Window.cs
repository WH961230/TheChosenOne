public class Window : IWindow {
    public Data MyData;
    public Game MyGame;

    public virtual void Init(Game game, Data data) {
        this.MyData = data;
        this.MyGame = game;
    }

    public virtual void Open() {
    }

    public virtual void Update() {
    }

    public virtual void FixedUpdate() {
    }

    public virtual void LateUpdate() {
    }

    public virtual void Close() {
    }

    public virtual void Clear() {
    }
}