public abstract class Entity : IEntity {
    public Data Data;
    protected GameObjFeature gameObjFeature;
    protected EntityFeature entityFeature;
    public virtual void Init(Game game, Data data) {
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
}