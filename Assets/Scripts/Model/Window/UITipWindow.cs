public class UITipWindow : Window {
    private UITipComponent MyComp;
    public override void Init(Game game, GameObj gameObj) {
        base.Init(game, gameObj);
        MyComp = gameObj.GetComp<UITipComponent>();
        MyComp.MyTip.SetActive(false);
        MyGame.MyGameMessageCenter.Register<UITipType, string>(GameMessageConstants.UITIPWINDOW_SETTIPDESCRIPTION, MsgSetTipDescription);
    }

    public override void Clear() {
        MyGame.MyGameMessageCenter.UnRegister<UITipType, string>(GameMessageConstants.UITIPWINDOW_SETTIPDESCRIPTION, MsgSetTipDescription);
        base.Clear();
    }

    public override void Open() {
        MyComp.MyTip.SetActive(true);
    }

    public override void Update() {
        if (string.IsNullOrEmpty(MyComp.MyTipName.text) || string.IsNullOrEmpty(MyComp.MyTipKeycode.text)) {
            Close();
        } else {
            Open();
        }
    }

    public override void Close() {
        MyComp.MyTip.SetActive(false);
    }

    private void MsgSetTipDescription(UITipType tipType, string description) {
        SetTipDescription(tipType, description);
    }

    public void SetTipDescription(UITipType tipType, string description) {
        switch (tipType) {
            case UITipType.ItemNameTip:
                MyComp.MyTipName.text = description;
                break;
            case UITipType.ItemKeycode:
                MyComp.MyTipKeycode.text = description;
                break;
        }
    }
}

public enum UITipType {
    ItemNameTip,
    ItemKeycode,
}