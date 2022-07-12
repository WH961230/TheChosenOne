public class UITipWindow : Window {
    private UITipComponent uitipComponent;
    public override void Init(Game game, Data data) {
        var obj = game.MyGameObjFeature.Get<UITipGameObj>(data.InstanceID).MyData.MyObj;
        uitipComponent = obj.transform.GetComponent<UITipComponent>();
        uitipComponent.MyTip.SetActive(false);
    }

    public override void Open() {
        uitipComponent.MyTip.SetActive(true);
    }

    public override void Update() {
    }

    public override void Close() {
        uitipComponent.MyTip.SetActive(false);
    }

    public override void Clear() {
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