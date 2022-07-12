public class UIMapWindow : Window {
    private UIMapComponent uimapComponent;
    public override void Init(Game game, Data data) {
        var obj = game.MyGameObjFeature.Get<UIMapGameObj>(data.InstanceID).MyData.MyObj;
        uimapComponent = obj.transform.GetComponent<UIMapComponent>();
        uimapComponent.UIMapMaxClose.onClick.AddListener(() => {
            uimapComponent.UIMapMax.SetActive(false);
            uimapComponent.UIMapMin.SetActive(true);
        });
        uimapComponent.UIMapMinButton.onClick.AddListener(() => {
            uimapComponent.UIMapMin.SetActive(false);
            uimapComponent.UIMapMax.SetActive(true);
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