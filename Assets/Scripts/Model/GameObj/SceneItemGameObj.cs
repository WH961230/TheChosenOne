public class SceneItemGameObj : GameObj {
    private SceneItemData sceneitemData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        sceneitemData = (SceneItemData)data;
        sceneitemData.MySceneItemSign = MyObj.GetComponent<SceneItemComponent>().SceneItemSign;
    }

    public override void Clear() {
        base.Clear();
    }

    public override void Update() {
        base.Update();
    }
}