public abstract class Entity : IEntity {
    public Data MyData;
    protected Game MyGame;
    protected GameSystem MyGS;
    public virtual void Init(Game game, Data data) {
        MyGame = game;
        MyGS = game.MyGameSystem;
        MyData = data;
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