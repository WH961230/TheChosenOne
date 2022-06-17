public abstract class Entity : IEntity {
    public Data Data;
    protected GameObjFeature gameObjFeature;
    protected EntityFeature entityFeature;
    public virtual void Init(Game game, Data data) {
        this.entityFeature = game.Get<EntityFeature>();
        this.gameObjFeature = game.Get<GameObjFeature>();
        this.Data = data;
    }

    public virtual void Update() {
    }

    public virtual void Clear() {
    }
}