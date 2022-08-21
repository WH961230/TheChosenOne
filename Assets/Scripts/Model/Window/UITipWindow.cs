public class UITipWindow : Window {
    private UITipComponent TipComp;
    public override void Init(Game game, GameObj gameObj) {
        base.Init(game, gameObj);

        var tempGO = (UITipGameObj)gameObj;

        TipComp = tempGO.GetComp();

        // 注册物体
        TempGO.Add(TipComp.MyTip);

        CloseAll();

        MyGame.MyGameMessageCenter.Register<UITipType, string>(GameMessageConstants.UITIPWINDOW_SETTIPDESCRIPTION, MsgSetTipDescription);
    }

    public override void Clear() {
        MyGame.MyGameMessageCenter.UnRegister<UITipType, string>(GameMessageConstants.UITIPWINDOW_SETTIPDESCRIPTION, MsgSetTipDescription);
        base.Clear();
    }

    public override void Update() {
        if (string.IsNullOrEmpty(TipComp.MyTipName.text) || string.IsNullOrEmpty(TipComp.MyTipKeycode.text)) {
            CloseAll();
        } else {
            OpenAll();
        }
    }

    private void MsgSetTipDescription(UITipType tipType, string description) {
        SetTipDescription(tipType, description);
    }

    private void SetTipDescription(UITipType tipType, string description) {
        switch (tipType) {
            case UITipType.ItemNameTip:
                TipComp.MyTipName.text = description;
                break;
            case UITipType.ItemKeycode:
                TipComp.MyTipKeycode.text = description;
                break;
        }
    }
}

public enum UITipType {
    ItemNameTip,
    ItemKeycode,
}