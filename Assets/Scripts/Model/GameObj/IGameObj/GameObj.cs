using UnityEngine;

public class GameObj : IGameObj {
    public Data Data;
    protected GameObjFeature gameObjFeature;
    protected GameObject obj;
    public virtual void Init(Game game, Data data) {
        this.gameObjFeature = game.Get<GameObjFeature>();
        this.Data = data;
        this.Data.InstanceID = Data.MyObj.GetInstanceID();
    }

    public virtual void Clear() {
    }

    public virtual void Update() {
    }
}