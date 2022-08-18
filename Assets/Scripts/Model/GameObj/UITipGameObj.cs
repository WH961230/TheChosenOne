﻿public class UITipGameObj : GameObj {
    private UITipData uitipData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        uitipData = (UITipData)data;
    }

    public UITipComponent GetComp() {
        return base.GetComp() as UITipComponent;
    }
}