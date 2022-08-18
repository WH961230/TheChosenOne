public class UIMapGameObj : GameObj {
    private UIMapData uimapData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        uimapData = (UIMapData)data;
    }
}