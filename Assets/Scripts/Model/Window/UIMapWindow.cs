public class UIMapWindow : Window {
    private UIMapComponent MapComp;
    public override void Init(Game game, GameObj gameObj) {
        base.Init(game, gameObj);
        var tempGO = (UIMapGameObj)gameObj;
        MapComp = tempGO.GetComp();

        RegisterEvent(MapComp.UIMapMinButton.onClick, OpenMaxMap);
        RegisterEvent(MapComp.UIMapMaxClose.onClick, CloseMaxMap);
    }

    public override void CloseAll() {
        UnRegisterEvent(MapComp.UIMapMinButton.onClick, OpenMaxMap);
        UnRegisterEvent(MapComp.UIMapMaxClose.onClick, CloseMaxMap);
        base.CloseAll();
    }

    private void OpenMaxMap() {
        MapComp.UIMapMin.SetActive(false);
        MapComp.UIMapMax.SetActive(true);
    }

    private void CloseMaxMap() {
        MapComp.UIMapMax.SetActive(false);
        MapComp.UIMapMin.SetActive(true);
    }
}