public class UIMainGameObj : GameObj {
    public UIMainComponent GetComp() {
        return base.GetComp() as UIMainComponent;
    }
}