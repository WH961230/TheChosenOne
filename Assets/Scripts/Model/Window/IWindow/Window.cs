public class Window : IWindow {
    public Game MyGame;
    public GameSystem MyGS;
    public GameObj MyGameObj;
    public GameComp MyComp;

    public virtual void Init(Game game, GameObj gameObj) {
        MyGame = game;
        MyGS = game.MyGameSystem;
        MyGameObj = gameObj;
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