public class UICharacterWindow : Window {
    private UICharacterComponent uicharacterComponent;
    public override void Init(Game game, Data data) {
        var obj = game.Get<GameObjFeature>().Get<UICharacterGameObj>(data.InstanceID).MyData.MyObj;
        uicharacterComponent = obj.transform.GetComponent<UICharacterComponent>();
    }

    public override void Open() {
    }

    public override void Update() {
    }

    public override void Close() {
    }

    public override void Clear() {
    }
}