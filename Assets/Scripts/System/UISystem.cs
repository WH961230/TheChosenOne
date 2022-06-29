using UnityEngine;

public class UISystem : GameSys {
    private GameSystem gameSystem;
    public override void Init(GameSystem gameSystem) {
        this.gameSystem = gameSystem;
        gameSystem.InstanceWindow<UIMainWindow, UIMainGameObj, UIMainEntity>(new UIMainData() {
            MyName = "mainScreen",
            MyType = new DataType() {IsWindowPrefab = true,},
            MyObj = Object.Instantiate(gameSystem.soGameSetting.UIMainPrefab),
            MyTranInfo = new TranInfo() {
                MyPos = Vector3.zero,
                MyRot = new Quaternion(0, 0, 0, 0),
                MyRootTran = GameData.UIRoot,
            }
        });
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }
}