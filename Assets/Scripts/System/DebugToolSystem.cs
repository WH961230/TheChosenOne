using UnityEngine;

public class DebugToolSystem : GameSys {
    private SOUIDebugToolSetting souiDebugToolSetting;
    private GameSystem gameSystem;
    public override void Init(GameSystem gameSystem) {
        base.Init(gameSystem);
        this.gameSystem = gameSystem;
        souiDebugToolSetting = Resources.Load<SOUIDebugToolSetting>(PathData.SOUIDebugToolSettingPath);

        InstanceUIDebugTool();
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }

    private void InstanceUIDebugTool() {
        InstanceUIDebugTool(new UIDebugToolData() {
            MyObj = Object.Instantiate(souiDebugToolSetting.MyUIDebugToolWin),
            MyName = "DebugTool",
            MyRootTran = GameData.UIRoot,
        });
    }

    private void InstanceUIDebugTool(UIDebugToolData uiDebugToolData) {
        gameSystem.InstanceWindow<UIDebugToolWindow, UIDebugToolGameObj, UIDebugToolEntity>(uiDebugToolData);
    }
}