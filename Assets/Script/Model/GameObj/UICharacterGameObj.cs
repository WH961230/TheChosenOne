public class UICharacterGameObj : GameObj {
    private UICharacterData uicharacterData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        uicharacterData = (UICharacterData)data;
    }

    public UICharacterComponent GetComp() {
        return base.GetComp() as UICharacterComponent;
    }
}