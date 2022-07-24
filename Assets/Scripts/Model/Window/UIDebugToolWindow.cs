using UnityEngine;

public class UIDebugToolWindow : Window {
    private UIDebugToolComponent uidebugtoolComponent;

    public override void Init(Game game, Data data) {
        base.Init(game, data);
        var obj = MyGame.MyGameObjFeature.Get<UIDebugToolGameObj>(data.InstanceID).MyData.MyObj;
        uidebugtoolComponent = obj.transform.GetComponent<UIDebugToolComponent>();

        uidebugtoolComponent.MyDebugToolFunctionWin.SetActive(false);
        uidebugtoolComponent.MyDebugToolCloseBtn.gameObject.SetActive(false);
        uidebugtoolComponent.MyConsoleWin.SetActive(false);
        uidebugtoolComponent.MyDebugToolSideBtnWin.SetActive(false);

        uidebugtoolComponent.MyDebugToolBtn.onClick.AddListener(() => {
            uidebugtoolComponent.MyDebugToolFunctionWin.SetActive(true);
            uidebugtoolComponent.MyConsoleWin.SetActive(false);
            uidebugtoolComponent.MyDebugToolSideBtnWin.SetActive(true);
            uidebugtoolComponent.MyDebugToolCloseBtn.gameObject.SetActive(true);
        });

        uidebugtoolComponent.MyDebugToolCloseBtn.onClick.AddListener(() => {
            uidebugtoolComponent.MyDebugToolFunctionWin.SetActive(false);
            uidebugtoolComponent.MyDebugToolCloseBtn.gameObject.SetActive(false);
            uidebugtoolComponent.MyDebugToolSideBtnWin.SetActive(false);
        });

        uidebugtoolComponent.MyDebugToolCreateCharacterBtn.onClick.AddListener(() => {
            MyGame.MyGameSystem.MyCharacterSystem.InstanceCharacter(false);
        });
        
        uidebugtoolComponent.MyDebugToolConsoleSelectBtn.onClick.AddListener(() => {
            uidebugtoolComponent.MyConsoleWin.SetActive(true);
            uidebugtoolComponent.MyDebugToolFunctionWin.SetActive(false);
        });
        
        uidebugtoolComponent.MyDebugToolFunctionSelectBtn.onClick.AddListener(() => {
            uidebugtoolComponent.MyConsoleWin.SetActive(false);
            uidebugtoolComponent.MyDebugToolFunctionWin.SetActive(true);
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