public class GameComponent : GameComp {
    private GravityComponent gravityComponent = new GravityComponent();

    public GravityComponent MyGravityComponent {
        get {
            return gravityComponent;
        }
    }
    public override void Init(Game game) {
        base.Init(game);
        gravityComponent.Init(game);
    }

    public override void Update() {
        base.Update();
        gravityComponent.Update();
    }

    public override void Clear() {
        gravityComponent.Clear();
        base.Clear();
    }
}