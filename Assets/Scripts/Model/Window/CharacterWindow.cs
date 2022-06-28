public class CharacterWindow : Window {
    private CharacterComponent characterComponent;
    public override void Init(Game game, Data data) {
        var obj = game.Get<GameObjFeature>().Get(data.InstanceID).MyData.MyObj;
        characterComponent = obj.transform.GetComponent<CharacterComponent>();
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