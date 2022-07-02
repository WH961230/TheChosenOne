public class UIDebugToolWindow : Window {
    private UIDebugToolComponent uidebugtoolComponent;
    public override void Init(Game game, Data data) {
        var obj = game.Get<GameObjFeature>().Get(data.InstanceID).MyData.MyObj;
        uidebugtoolComponent = obj.transform.GetComponent<UIDebugToolComponent>();
        
        uidebugtoolComponent.MyDebugToolWin.SetActive(false);
        uidebugtoolComponent.MyDebugToolBtn.onClick.AddListener(() => {
            uidebugtoolComponent.MyDebugToolWin.SetActive(true);
        });

        uidebugtoolComponent.MyDebugToolCloseBtn.onClick.AddListener(() => {
            uidebugtoolComponent.MyDebugToolWin.SetActive(false);
        });

        uidebugtoolComponent.MyDebugToolCreateCharacterBtn.onClick.AddListener(() => {
            game.MyGameSystem.MyCharacterSystem.InstanceCharacter(false);
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