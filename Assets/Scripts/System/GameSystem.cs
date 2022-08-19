public class GameSystem {
    #region 全局系统

    private Game game;

    public GameObjFeature GOFeature {
        get { return game.MyGameObjFeature; }
    }

    public WindowFeature WinFeature {
        get { return game.MyWindowFeature; }
    }

    public EntityFeature EntityFeature {
        get { return game.MyEntityFeature; }
    }

    #endregion

    #region 子系统

    private CharacterSystem characterS = new CharacterSystem();

    public CharacterSystem CharacterS {
        get { return characterS; }
    }

    private WeaponSystem weapS = new WeaponSystem();

    public WeaponSystem WeapS {
        get { return weapS; }
    }

    private EnvironmentSystem envirS = new EnvironmentSystem();

    public EnvironmentSystem EnvirS {
        get { return envirS; }
    }

    private UISystem uIS = new UISystem();

    public UISystem UIS {
        get { return uIS; }
    }

    private CameraSystem cameraS = new CameraSystem();

    public CameraSystem CameraS {
        get { return cameraS; }
    }

    private ItemSystem itemS = new ItemSystem();

    public ItemSystem ItemS {
        get { return itemS; }
    }

    private InputSystem inputS = new InputSystem();

    public InputSystem InputS {
        get { return inputS; }
    }

    private AudioSystem audioS = new AudioSystem();

    public AudioSystem AudioS {
        get { return audioS; }
    }
    
    private BackpackSystem backpackS = new BackpackSystem();

    public BackpackSystem BackpackS {
        get { return backpackS; }
    }

    private EquipmentSystem equipmentS = new EquipmentSystem();

    public EquipmentSystem EquipmentS {
        get { return equipmentS; }
    }

    private AnimatorSystem animatorS = new AnimatorSystem();

    public AnimatorSystem AnimatorS {
        get {
            return animatorS;
        }
    }
    
    private ConsumeSystem consumeS = new ConsumeSystem();

    public ConsumeSystem ConsumeS {
        get {
            return consumeS;
        }
    }

    private BulletSystem bulletS = new BulletSystem();

    public BulletSystem BulletS {
        get {
            return bulletS;
        }
    }
    
    private EffectSystem effectS = new EffectSystem();

    public EffectSystem EffectS {
        get {
            return effectS;
        }
    }
    
    #endregion

    public void Init(Game game) {
        this.game = game;
        animatorS.Init(this);
        envirS.Init(this);
        effectS.Init(this);
        itemS.Init(this);
        cameraS.Init(this);
        characterS.Init(this);
        equipmentS.Init(this);
        weapS.Init(this);
        bulletS.Init(this);
        consumeS.Init(this);
        inputS.Init(this);
        audioS.Init(this);
        backpackS.Init(this);
        uIS.Init(this);
    }

    public void Update() {
        animatorS.Update();
        envirS.Update();
        effectS.Update();
        itemS.Update();
        cameraS.Update();
        characterS.Update();
        equipmentS.Update();
        weapS.Update();
        bulletS.Update();
        consumeS.Update();
        inputS.Update();
        audioS.Update();
        backpackS.Update();
        uIS.Update();
    }

    public void FixedUpdate() {
        animatorS.FixedUpdate();
        envirS.FixedUpdate();
        effectS.FixedUpdate();
        itemS.FixedUpdate();
        cameraS.FixedUpdate();
        characterS.FixedUpdate();
        equipmentS.FixedUpdate();
        weapS.FixedUpdate();
        bulletS.FixedUpdate();
        consumeS.FixedUpdate();
        inputS.FixedUpdate();
        audioS.FixedUpdate();
        backpackS.FixedUpdate();
        uIS.FixedUpdate();
    }

    public void LateUpdate() {
        animatorS.LateUpdate();
        envirS.LateUpdate();
        effectS.LateUpdate();
        itemS.LateUpdate();
        cameraS.LateUpdate();
        characterS.LateUpdate();
        equipmentS.LateUpdate();
        weapS.LateUpdate();
        bulletS.LateUpdate();
        consumeS.LateUpdate();
        inputS.LateUpdate();
        audioS.LateUpdate();
        backpackS.LateUpdate();
        uIS.LateUpdate();
    }

    public void Clear() {
        animatorS.Clear();
        envirS.Clear();
        effectS.Clear();
        itemS.Clear();
        cameraS.Clear();
        characterS.Clear();
        equipmentS.Clear();
        weapS.Clear();
        bulletS.Clear();
        consumeS.Clear();
        inputS.Clear();
        audioS.Clear();
        backpackS.Clear();
        uIS.Clear();
    }

    public int InstanceWindow<T1, T2, T3>(Data data) where T1 : IWindow, new() where T2 : GameObj, new() where T3 : Entity, new() {
        data.InstanceID = data.MyObj.GetInstanceID();
        game.MyEntityFeature.Register<T3>(data);
        var go = game.MyGameObjFeature.Register<T2>(data);
        game.MyWindowFeature.Register<T1>(go);
        return data.InstanceID;
    }

    public int InstanceGameObj<T1, T2>(Data data) where T1 : GameObj, new() where T2 : Entity, new() {
        data.InstanceID = data.MyObj.GetInstanceID();
        game.MyEntityFeature.Register<T2>(data);
        game.MyGameObjFeature.Register<T1>(data);
        return data.InstanceID;
    }

    public void InstanceEntity<T>(Data data) where T : Entity, new() {
        data.InstanceID = data.MyObj.GetInstanceID();
        game.MyEntityFeature.Register<T>(data);
    }
}