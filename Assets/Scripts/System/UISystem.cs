using UnityEngine;

public class UISystem : GameSys {
    private GameSystem gameSystem;
    public override void Init(GameSystem gameSystem) {
        this.gameSystem = gameSystem;
        InstanceUIMainWindow();
    }

    public override void Update() {
        base.Update();
    }
    
    public override void FixedUpdate() {
        base.Update();
    }

    public override void LateUpdate() {
        base.LateUpdate();
    }

    public override void Clear() {
        base.Clear();
    }

    private void InstanceUIMainWindow() {
        gameSystem.InstanceWindow<UIMainWindow, UIMainGameObj, UIMainEntity>(new UIMainData() {
            MyName = "MainWindow",
            MyObj = Object.Instantiate(gameSystem.soGameSetting.UIMainPrefab),
            MyRootTran = GameData.UIRoot,
            MyTranInfo = new TranInfo() {
                MyPos = Vector3.zero,
                MyRot = new Quaternion(0, 0, 0, 0),
            }
        });
    }

    public void InstanceUITipWindow() {
        gameSystem.InstanceWindow<UITipWindow, UITipGameObj, UITipEntity>(new UITipData() {
            MyName = "TipWindow",
            MyObj = Object.Instantiate(gameSystem.soGameSetting.UITipPrefab),
            MyRootTran = GameData.UIRoot,
        });
    }

    public void InstanceUIBackpackWindow() {
        gameSystem.InstanceWindow<UIBackpackWindow, UIBackpackGameObj, UIBackpackEntity>(new UIBackpackData() {
            MyName = "BackpackWindow",
            MyObj = Object.Instantiate(gameSystem.soGameSetting.UIBackpackPrefab),
            MyRootTran = GameData.UIRoot,
        });
    }

    public void InstanceUICharacterWindow() {
        gameSystem.InstanceWindow<UICharacterWindow, UICharacterGameObj, UICharacterEntity>(new UICharacterData() {
            MyName = "CharacterWindow",
            MyObj = Object.Instantiate(gameSystem.soGameSetting.UICharacterPrefab),
            MyRootTran = GameData.UIRoot,
            MyTranInfo = new TranInfo() {
                MyPos = Vector3.zero,
                MyRot = new Quaternion(0, 0, 0, 0),
            }
        });
    }

    public void InstanceUIMapWindow() {
        gameSystem.InstanceWindow<UIMapWindow, UIMapGameObj, UIMapEntity>(new UIMapData() {
            MyName = "MapWindow",
            MyObj = Object.Instantiate(gameSystem.soGameSetting.UIMapPrefab),
            MyRootTran = GameData.UIRoot,
        });
    }

    public void InstanceUIDebugToolWindow() {
        gameSystem.InstanceWindow<UIDebugToolWindow, UIDebugToolGameObj, UIDebugToolEntity>(new UIDebugToolData() {
            MyObj = Object.Instantiate(gameSystem.soGameSetting.UIDebugToolPrefab),
            MyName = "DebugToolWindow",
            MyRootTran = GameData.UIRoot,
        });
    }
}