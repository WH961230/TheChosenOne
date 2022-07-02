using UnityEngine;

public class GameSystem {
    public SOGameSetting soGameSetting;
    private Game game;

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

    private DebugToolSystem debugToolSystem = new DebugToolSystem();
    public DebugToolSystem MyDebugToolSystem {
        get {
            return debugToolSystem;
        }
    }

    public void Init(Game game) {
        this.game = game;
        this.soGameSetting = Resources.Load<SOGameSetting>(PathData.SOGameSettingPath);

        InstanceRoot();

        uISystem.Init(this);
        environmentSystem.Init(this);
        characterSystem.Init(this);
        debugToolSystem.Init(this);
    }

    public void Update() {
        uISystem.Update();
        environmentSystem.Update();
        characterSystem.Update();
        debugToolSystem.Update();
    }

    public void Clear() {
        uISystem.Clear();
        environmentSystem.Clear();
        characterSystem.Clear();
        debugToolSystem.Clear();
    }

    private void InstanceRoot() {
        var gameRoot = new GameObject("GameRoot").transform;

        GameData.UIRoot = Object.Instantiate(soGameSetting.UIRoot).transform;
        GameData.UIRoot.name = "UIRoot";
        GameData.UIRoot.SetParent(gameRoot);

        GameData.ItemRoot = new GameObject("ItemRoot").transform;
        GameData.ItemRoot.SetParent(gameRoot);

        GameData.AudioRoot = new GameObject("AudioRoot").transform;
        GameData.AudioRoot.SetParent(gameRoot);

        GameData.EnvironmentRoot = new GameObject("EnvironmentRoot").transform;
        GameData.EnvironmentRoot.SetParent(gameRoot);

        GameData.CameraRoot = new GameObject("CameraRoot").transform;
        GameData.CameraRoot.SetParent(gameRoot);

        GameData.MainCamera = Object.Instantiate(soGameSetting.MainCamera).transform.GetComponent<Camera>();
        GameData.MainCamera.transform.SetParent(GameData.CameraRoot);

        GameData.LightRoot = new GameObject("LightRoot").transform;
        GameData.LightRoot.SetParent(gameRoot);

        GameData.MainLight = Object.Instantiate(soGameSetting.MainLight).transform.GetComponent<Light>();
        GameData.MainLight.transform.SetParent(GameData.LightRoot);
    }

    public void InstanceWindow<T1, T2, T3>(Data data) where T1 : IWindow, new()
        where T2 : GameObj, new()
        where T3 : Entity, new() {
        data.InstanceID = data.MyObj.GetInstanceID();
        game.Get<GameObjFeature>().Register<T2>(data);
        game.Get<WindowFeature>().Register<T1>(data);
        game.Get<EntityFeature>().Register<T3>(data);
    }

    public void InstanceGameObj<T1, T2>(Data data) where T1 : GameObj, new() where T2 : Entity, new() {
        data.InstanceID = data.MyObj.GetInstanceID();
        game.Get<GameObjFeature>().Register<T1>(data);
        game.Get<EntityFeature>().Register<T2>(data);
    }

    public void InstanceEntity<T>(Data data) where T : Entity, new() {
        game.Get<EntityFeature>().Register<T>(data);
    }
}