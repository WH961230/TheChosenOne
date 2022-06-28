using UnityEngine;

public class UIMainWindow : Window {
    private UIMainComponent uimainComponent;
    public override void Init(Game game, Data data) {
        var obj = game.Get<GameObjFeature>().Get(data.InstanceID).MyData.MyObj;
        uimainComponent = obj.transform.GetComponent<UIMainComponent>();
        uimainComponent.MyButton.onClick.AddListener(() => {
            Debug.Log("按下 button");
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