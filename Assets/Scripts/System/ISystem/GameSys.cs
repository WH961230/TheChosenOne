public class GameSys : ISystem {
    protected GameSystem MyGameSystem;
    protected GameObjFeature MyGameObjFeature;
    protected EntityFeature MyEntityFeature;
    protected WindowFeature MyWindowFeature;
    public virtual void Init(GameSystem gameSystem) {
        MyGameSystem = gameSystem;
        MyGameObjFeature = gameSystem.MyGameObjFeature;
        MyEntityFeature = gameSystem.MyEntityFeature;
        MyWindowFeature = gameSystem.MyWindowFeature;
    }

    public virtual void Update() {
    }

    public virtual void FixedUpdate() {
    }

    public virtual void Clear() {
    }

    public virtual void LateUpdate() {
    }

    // 获取物体
    protected virtual T GetGameObj<T>(int id) where T : GameObj, new () {
        return MyGameObjFeature.Get<T>(id);
    }

    // 获取实体
    protected virtual T GetEntity<T>(int id) where T : Entity, new() {
        return MyEntityFeature.Get<T>(id);
    }
}