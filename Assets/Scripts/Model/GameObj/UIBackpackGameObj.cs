public class UIBackpackGameObj : GameObj {
    public UIBackpackComponent GetComp() {
        return base.GetComp() as UIBackpackComponent;
    }
}