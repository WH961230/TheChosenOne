public abstract class Entity : IEntity {
    public Data MyData;
    protected Game MyGame;
    public virtual void Init(Game game, Data data) {
        this.MyGame = game;
        this.MyData = data;
    }

    public virtual void Update() {
    }

    public virtual void FixedUpdate() {
    }

    public virtual void LateUpdate() {
    }

    public virtual void Clear() {
    }

    protected Data GetData() {
        return MyData;
    }
}