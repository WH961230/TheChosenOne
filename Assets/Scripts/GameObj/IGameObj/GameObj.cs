using UnityEngine;

public class GameObj : IGameObj {
    public Data Data;
    protected GameObjFeature gameObjFeature;
    protected GameObject obj;
    public virtual void Init(Game game, Data data) {
        this.gameObjFeature = game.Get<GameObjFeature>();
        this.Data = data;
        this.Data.InstanceID = CreateGameObj(Data).GetInstanceID();
    }

    public virtual void Clear() {
    }

    public virtual void Update() {
    }

    private GameObject CreateGameObj(Data data) {
        switch (data.ComponentType.Length) {
            case 0:
                return new GameObject(data.Name);
            case 1:
                return new GameObject(data.Name, data.ComponentType[0]);
            case 2:
                return new GameObject(data.Name, data.ComponentType[0], data.ComponentType[1]);
        }

        return null;
    }
}