using UnityEngine;

public class ItemEntity : Entity {
    private ItemData sceneitemData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.sceneitemData = (ItemData)data;
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }
}