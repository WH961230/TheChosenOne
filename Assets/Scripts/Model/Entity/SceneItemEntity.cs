using UnityEngine;

public class SceneItemEntity : Entity {
    private SceneItemData sceneitemData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.sceneitemData = (SceneItemData)data;
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }
}