public class UITipWindow : Window {
    private UITipComponent uitipComponent;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        var obj = game.MyGameObjFeature.Get<UITipGameObj>(data.InstanceID).MyData.MyObj;
        uitipComponent = obj.transform.GetComponent<UITipComponent>();
        uitipComponent.MyTip.SetActive(false);

        MyGame.MyGameMessageCenter.Register<UITipType, string>(GameMessageConstants.UITIPWINDOW_SETTIPDESCRIPTION, MsgSetTipDescription);
    }

    public override void Open() {
        uitipComponent.MyTip.SetActive(true);
    }

    public override void Update() {
        if (string.IsNullOrEmpty(uitipComponent.MyTipName.text) || string.IsNullOrEmpty(uitipComponent.MyTipKeycode.text)) {
            Close();
        } else {
            Open();
        }
    }

    public override void Close() {
        uitipComponent.MyTip.SetActive(false);
    }

    public override void Clear() {
        MyGame.MyGameMessageCenter.UnRegister(GameMessageConstants.UITIPWINDOW_SETTIPDESCRIPTION);
    }

    private void MsgSetTipDescription(UITipType tipType, string description) {
        SetTipDescription(tipType, description);
    }

    public void SetTipDescription(UITipType tipType, string description) {
        switch (tipType) {
            case UITipType.ItemNameTip:
                uitipComponent.MyTipName.text = description;
                break;
            case UITipType.ItemKeycode:
                uitipComponent.MyTipKeycode.text = description;
                break;
        }
    }
}

public enum UITipType {
    ItemNameTip,
    ItemKeycode,
}