public class GameSys : ISystem {
    protected GameSystem MyGS;
    protected GameObjFeature MyGameObjFeature;
    protected EntityFeature MyEntityFeature;
    protected WindowFeature MyWindowFeature;
    public virtual void Init(GameSystem gameSystem) {
        MyGS = gameSystem;
        MyGameObjFeature = gameSystem.GOFeature;
        MyEntityFeature = gameSystem.EntityFeature;
        MyWindowFeature = gameSystem.WinFeature;
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