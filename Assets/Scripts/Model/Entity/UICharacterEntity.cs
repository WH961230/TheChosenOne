public class UICharacterEntity : Entity {
    private UICharacterData uicharacterData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.uicharacterData = (UICharacterData)data;
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }
}