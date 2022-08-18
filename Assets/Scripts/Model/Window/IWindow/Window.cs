public class Window : IWindow {
    public Game MyGame;
    public GameObj MyGameObj;
    public GameComp MyComp;

    public virtual void Init(Game game, GameObj gameObj) {
        MyGame = game;
        MyGameObj = gameObj;
        MyComp = gameObj.GetComp();
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