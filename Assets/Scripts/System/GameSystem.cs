using UnityEngine;

public class GameSystem {

    #region 配置

    public SOGameSetting soGameSetting;

    #endregion

    #region 上级

    private Game game;

    public GameObjFeature MyGameObjFeature {
        get {
            return game.MyGameObjFeature;
        }
    }

    public WindowFeature MyWindowFeature {
        get {
            return game.MyWindowFeature;
        }
    }

    public EntityFeature MyEntityFeature {
        get {
            return game.MyEntityFeature;
        }
    }

    #endregion

    #region 子系统

    private CharacterSystem characterSystem = new CharacterSystem();
    public CharacterSystem MyCharacterSystem {
        get {
            return characterSystem;
        }
    }

    private EnvironmentSystem environmentSystem = new EnvironmentSystem();
    public EnvironmentSystem MyEnvironmentSystem {
        get {
            return environmentSystem;
        }
    }

    private UISystem uISystem = new UISystem();
    public UISystem MyUISystem {
        get {
            return uISystem;
        }
    }

    private CameraSystem cameraSystem = new CameraSystem();
    public CameraSystem MyCameraSystem {
        get {
            return cameraSystem;
        }
    }

    private ItemSystem itemSystem = new ItemSystem();
    public ItemSystem MyItemSystem {
        get {
            return itemSystem;
        }
    }

    private InputSystem inputSystem = new InputSystem();
    public InputSystem MyInputSystem {
        get {
            return inputSystem;
        }
    }
    
    private AudioSystem audioSystem = new AudioSystem();
    public AudioSystem MyAudioSystem {
        get {
            return audioSystem;
        }
    }

    #endregion

    #region 消息

    public GameMessageCenter MyGameMessageCenter {
        get {
            return game.MyGameMessageCenter;
        }
    }

    #endregion

    public void Init(Game game) {
        this.game = game;
        this.soGameSetting = Resources.Load<SOGameSetting>(PathData.SOGameSettingPath);

        InstanceSwitch();
        InstanceRoot();

        uISystem.Init(this);
        environmentSystem.Init(this);
        itemSystem.Init(this);
        cameraSystem.Init(this);
        characterSystem.Init(this);
        inputSystem.Init(this);
        audioSystem.Init(this);
    }

    public void Update() {
        uISystem.Update();
        environmentSystem.Update();
        itemSystem.Update();
        cameraSystem.Update();
        characterSystem.Update();
        inputSystem.Update();
        audioSystem.Update();
    }

    public void FixedUpdate() {
        uISystem.FixedUpdate();
        environmentSystem.FixedUpdate();
        itemSystem.FixedUpdate();
        cameraSystem.FixedUpdate();
        characterSystem.FixedUpdate();
        inputSystem.FixedUpdate();
        audioSystem.FixedUpdate();
    }

    public void LateUpdate() {
        uISystem.LateUpdate();
        environmentSystem.LateUpdate();
        itemSystem.LateUpdate();
        cameraSystem.LateUpdate();
        characterSystem.LateUpdate();
        inputSystem.LateUpdate();
        audioSystem.LateUpdate();
    }

    public void Clear() {
        uISystem.Clear();
        environmentSystem.Clear();
        itemSystem.Clear();
        cameraSystem.Clear();
        characterSystem.Clear();
        inputSystem.Clear();
        audioSystem.Clear();
    }

    private void InstanceSwitch() {
        GameData.IsShowLog = game.IsShowLog;
        GameData.IsOfficial = game.IsOfficial;
    }

    private void InstanceRoot() {
        var gameRoot = new GameObject("GameRoot").transform;

        GameData.UIRoot = Object.Instantiate(soGameSetting.UIRoot).transform;
        GameData.UIRoot.name = "UIRoot";
        GameData.UIRoot.SetParent(gameRoot);

        GameData.CharacterRoot = new GameObject("CharacterRoot").transform;
        GameData.CharacterRoot.SetParent(gameRoot);

        GameData.AudioRoot = new GameObject("AudioRoot").transform;
        GameData.AudioRoot.SetParent(gameRoot);

        GameData.EnvironmentRoot = new GameObject("EnvironmentRoot").transform;
        GameData.EnvironmentRoot.SetParent(gameRoot);

        GameData.ItemRoot = new GameObject("ItemRoot").transform;
        GameData.ItemRoot.SetParent(gameRoot);

        GameData.CameraRoot = new GameObject("CameraRoot").transform;
        GameData.CameraRoot.SetParent(gameRoot);

        GameData.LightRoot = new GameObject("LightRoot").transform;
        GameData.LightRoot.SetParent(gameRoot);
        
        GameData.AudioRoot = new GameObject("AudioRoot").transform;
        GameData.AudioRoot.SetParent(gameRoot);
    }

    public int InstanceWindow<T1, T2, T3>(Data data) where T1 : IWindow, new()
        where T2 : GameObj, new()
        where T3 : Entity, new() {
        data.InstanceID = data.MyObj.GetInstanceID();
        data.MyType.IsWindowPrefab = true;
        game.MyGameObjFeature.Register<T2>(data);
        game.MyWindowFeature.Register<T1>(data);
        game.MyEntityFeature.Register<T3>(data);
        return data.InstanceID;
    }

    public int InstanceGameObj<T1, T2>(Data data) where T1 : GameObj, new() where T2 : Entity, new() {
        data.InstanceID = data.MyObj.GetInstanceID();
        game.Get<GameObjFeature>().Register<T1>(data);
        game.Get<EntityFeature>().Register<T2>(data);
        return data.InstanceID;
    }

    public void InstanceEntity<T>(Data data) where T : Entity, new() {
        game.Get<EntityFeature>().Register<T>(data);
    }
}