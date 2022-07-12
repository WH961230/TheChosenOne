public class UIBackpackWindow : Window {
    private UIBackpackComponent uibackpackComponent;
    public override void Init(Game game, Data data) {
        var obj = game.MyGameObjFeature.Get<UIBackpackGameObj>(data.InstanceID).MyData.MyObj;
        uibackpackComponent = obj.transform.GetComponent<UIBackpackComponent>();
        uibackpackComponent.MyUIBackpackBtn.gameObject.SetActive(true);
        uibackpackComponent.MyUIBackpackWindow.gameObject.SetActive(false);
        uibackpackComponent.MyUIBackpackBtn.onClick.AddListener(() => {
            uibackpackComponent.MyUIBackpackWindow.gameObject.SetActive(true);
            uibackpackComponent.MyUIBackpackBtn.gameObject.SetActive(false);
        });
        uibackpackComponent.MyUIBackpackCloseBtn.onClick.AddListener(() => {
            uibackpackComponent.MyUIBackpackBtn.gameObject.SetActive(true);
            uibackpackComponent.MyUIBackpackWindow.gameObject.SetActive(false);
        });
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