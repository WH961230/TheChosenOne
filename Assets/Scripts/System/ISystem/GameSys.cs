public class GameSys : ISystem {
    protected GameSystem MyGameSystem;
    public virtual void Init(GameSystem gameSystem) {
        this.MyGameSystem = gameSystem;
    }

    public virtual void Update() {
    }

    public virtual void FixedUpdate() {
    }

    public virtual void Clear() {
    }

    public virtual void LateUpdate() {
    }
}