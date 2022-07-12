public class SceneItemGameObj : GameObj {
    private SceneItemComponent sceneItemComponent;
    private SceneItemData sceneitemData;

    public override void Init(Game game, Data data) {
        base.Init(game, data);
        sceneitemData = (SceneItemData) data;
        MyComponent = MyObj.transform.GetComponent<SceneItemComponent>();
        sceneItemComponent = (SceneItemComponent) MyComponent;
        sceneitemData.MySceneItemSign = MyObj.GetComponent<SceneItemComponent>().SceneItemSign;
    }

    public void HideObj() {
        MyObj.SetActive(false);
    }

    public void ShowObj() {
        MyObj.SetActive(true);
    }

    public override void Clear() {
        base.Clear();
    }

    public override void Update() {
        base.Update();
    }
}