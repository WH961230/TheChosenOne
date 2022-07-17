public abstract class Entity : IEntity {
    public Data Data;
    protected GameObjFeature gameObjFeature;
    protected EntityFeature entityFeature;
    protected Game MyGame;
    public virtual void Init(Game game, Data data) {
        this.MyGame = game;
        this.entityFeature = game.MyEntityFeature;
        this.gameObjFeature = game.MyGameObjFeature;
        this.Data = data;
    }

    public virtual void Update() {
    }

    public virtual void FixedUpdate() {
    }

    public virtual void LateUpdate() {
    }

    public virtual void Clear() {
    }

    public T GetData<T>() where T : Data {
        return (T)Data;
    }
}