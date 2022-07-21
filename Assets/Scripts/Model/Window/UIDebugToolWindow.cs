using UnityEngine;

public class UIDebugToolWindow : Window {
    private UIDebugToolComponent uidebugtoolComponent;

    public override void Init(Game game, Data data) {
        var obj = game.MyGameObjFeature.Get<UIDebugToolGameObj>(data.InstanceID).MyData.MyObj;
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

        uidebugtoolComponent.MyDebugToolCreateCubeAtGround.onClick.AddListener(() => {
            var characterPos =  MyGame.MyGameSystem.MyCharacterSystem.GetMainCharacterComponent().transform.position;
            var tempObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            tempObj.transform.position = GameData.GetGround(characterPos);
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