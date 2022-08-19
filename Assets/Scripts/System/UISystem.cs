using UnityEngine;

public class UISystem : GameSys {
    public override void Init(GameSystem gameSystem) {
        base.Init(gameSystem);
        InstanceUIMainWindow();
    }

    private void InstanceUIMainWindow() {
        MyGS.InstanceWindow<UIMainWindow, UIMainGameObj, UIMainEntity>(new UIMainData() {
            MyName = "MainWindow",
            MyObj = Object.Instantiate(SOData.MySOGameSetting.UIMainPrefab),
            MyRootTran = GameData.UIRoot,
            MyTranInfo = new TranInfo() {
                MyPos = Vector3.zero,
                MyRot = new Quaternion(0, 0, 0, 0),
            }
        });
    }

    public void InstanceUITipWindow() {
        MyGS.InstanceWindow<UITipWindow, UITipGameObj, UITipEntity>(new UITipData() {
            MyName = "TipWindow",
            MyObj = Object.Instantiate(SOData.MySOGameSetting.UITipPrefab),
            MyRootTran = GameData.UIRoot,
        });
    }

    public void InstanceUIBackpackWindow() {
        MyGS.InstanceWindow<UIBackpackWindow, UIBackpackGameObj, UIBackpackEntity>(new UIBackpackData() {
            MyName = "BackpackWindow",
            MyObj = Object.Instantiate(SOData.MySOGameSetting.UIBackpackPrefab),
            MyRootTran = GameData.UIRoot,
        });
    }

    public void InstanceUICharacterWindow() {
        MyGS.InstanceWindow<UICharacterWindow, UICharacterGameObj, UICharacterEntity>(new UICharacterData() {
            MyName = "CharacterWindow",
            MyObj = Object.Instantiate(SOData.MySOGameSetting.UICharacterPrefab),
            MyRootTran = GameData.UIRoot,
            MyTranInfo = new TranInfo() {
                MyPos = Vector3.zero,
                MyRot = new Quaternion(0, 0, 0, 0),
            }
        });
    }

    public void InstanceUIMapWindow() {
        MyGS.InstanceWindow<UIMapWindow, UIMapGameObj, UIMapEntity>(new UIMapData() {
            MyName = "MapWindow",
            MyObj = Object.Instantiate(SOData.MySOGameSetting.UIMapPrefab),
            MyRootTran = GameData.UIRoot,
        });
    }

    public void InstanceUIDebugToolWindow() {
        MyGS.InstanceWindow<UIDebugToolWindow, UIDebugToolGameObj, UIDebugToolEntity>(new UIDebugToolData() {
            MyObj = Object.Instantiate(SOData.MySOGameSetting.UIDebugToolPrefab),
            MyName = "DebugToolWindow",
            MyRootTran = GameData.UIRoot,
        });
    }
}