public class UIMapWindow : Window {
    private UIMapComponent MyComp;
    public override void Init(Game game, GameObj gameObj) {
        base.Init(game, gameObj);
        MyComp = gameObj.GetComp<UIMapComponent>();
        MyComp.UIMapMaxClose.onClick.AddListener(() => {
            MyComp.UIMapMax.SetActive(false);
            MyComp.UIMapMin.SetActive(true);
        });
        MyComp.UIMapMinButton.onClick.AddListener(() => {
            MyComp.UIMapMin.SetActive(false);
            MyComp.UIMapMax.SetActive(true);
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