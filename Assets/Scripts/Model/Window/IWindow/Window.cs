public class Window : IWindow {
    public Data MyData;

    public virtual void Init(Game game, Data data) {
        this.MyData = data;
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