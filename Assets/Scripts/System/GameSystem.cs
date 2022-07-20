using System;
using System.Collections.Generic;

public class GameSystem {
    #region 上级

    private Game game;

    public GameObjFeature MyGameObjFeature {
        get { return game.MyGameObjFeature; }
    }

    public WindowFeature MyWindowFeature {
        get { return game.MyWindowFeature; }
    }

    public EntityFeature MyEntityFeature {
        get { return game.MyEntityFeature; }
    }

    #endregion

    #region 子系统

    private CharacterSystem characterSystem = new CharacterSystem();

    public CharacterSystem MyCharacterSystem {
        get { return characterSystem; }
    }

    private WeaponSystem weaponSystem = new WeaponSystem();

    public WeaponSystem MyWeaponSystem {
        get { return weaponSystem; }
    }

    private EnvironmentSystem environmentSystem = new EnvironmentSystem();

    public EnvironmentSystem MyEnvironmentSystem {
        get { return environmentSystem; }
    }

    private UISystem uISystem = new UISystem();

    public UISystem MyUISystem {
        get { return uISystem; }
    }

    private CameraSystem cameraSystem = new CameraSystem();

    public CameraSystem MyCameraSystem {
        get { return cameraSystem; }
    }

    private ItemSystem itemSystem = new ItemSystem();

    public ItemSystem MyItemSystem {
        get { return itemSystem; }
    }

    private InputSystem inputSystem = new InputSystem();

    public InputSystem MyInputSystem {
        get { return inputSystem; }
    }

    private AudioSystem audioSystem = new AudioSystem();

    public AudioSystem MyAudioSystem {
        get { return audioSystem; }
    }
    
    private BackpackSystem backpackSystem = new BackpackSystem();

    public BackpackSystem MyBackpackSystem {
        get { return backpackSystem; }
    }

    private EquipmentSystem equipmentSystem = new EquipmentSystem();

    public EquipmentSystem MyEquipmentSystem {
        get { return equipmentSystem; }
    }

    private AnimatorSystem animatorSystem = new AnimatorSystem();

    public AnimatorSystem MyAnimaterSystem {
        get {
            return animatorSystem;
        }
    }

    public List<GameSys> systems = new List<GameSys>();

    #endregion

    #region 消息

    public GameMessageCenter MyGameMessageCenter {
        get { return game.MyGameMessageCenter; }
    }

    #endregion

    public void Init(Game game) {
        this.game = game;
        uISystem.Init(this);
        environmentSystem.Init(this);
        itemSystem.Init(this);
        cameraSystem.Init(this);
        characterSystem.Init(this);
        weaponSystem.Init(this);
        inputSystem.Init(this);
        audioSystem.Init(this);
        backpackSystem.Init(this);
        animatorSystem.Init(this);
        equipmentSystem.Init(this);
    }

    public void Update() {
        uISystem.Update();
        environmentSystem.Update();
        itemSystem.Update();
        cameraSystem.Update();
        characterSystem.Update();
        weaponSystem.Update();
        inputSystem.Update();
        audioSystem.Update();
        backpackSystem.Update();
        animatorSystem.Update();
        equipmentSystem.Update();
    }

    public void FixedUpdate() {
        uISystem.FixedUpdate();
        environmentSystem.FixedUpdate();
        itemSystem.FixedUpdate();
        cameraSystem.FixedUpdate();
        characterSystem.FixedUpdate();
        weaponSystem.FixedUpdate();
        inputSystem.FixedUpdate();
        audioSystem.FixedUpdate();
        backpackSystem.FixedUpdate();
        animatorSystem.FixedUpdate();
        equipmentSystem.FixedUpdate();
    }

    public void LateUpdate() {
        uISystem.LateUpdate();
        environmentSystem.LateUpdate();
        itemSystem.LateUpdate();
        cameraSystem.LateUpdate();
        characterSystem.LateUpdate();
        weaponSystem.LateUpdate();
        inputSystem.LateUpdate();
        audioSystem.LateUpdate();
        backpackSystem.LateUpdate();
        animatorSystem.LateUpdate();
        equipmentSystem.LateUpdate();
    }

    public void Clear() {
        uISystem.Clear();
        environmentSystem.Clear();
        itemSystem.Clear();
        cameraSystem.Clear();
        characterSystem.Clear();
        weaponSystem.Clear();
        inputSystem.Clear();
        audioSystem.Clear();
        backpackSystem.Clear();
        animatorSystem.Clear();
        equipmentSystem.Clear();
    }

    public int InstanceWindow<T1, T2, T3>(Data data) where T1 : IWindow, new() where T2 : GameObj, new() where T3 : Entity, new() {
        data.InstanceID = data.MyObj.GetInstanceID();
        data.IsWindowPrefab = true;
        game.MyGameObjFeature.Register<T2>(data);
        game.MyWindowFeature.Register<T1>(data);
        game.MyEntityFeature.Register<T3>(data);
        return data.InstanceID;
    }

    public int InstanceGameObj<T1, T2>(Data data) where T1 : GameObj, new() where T2 : Entity, new() {
        data.InstanceID = data.MyObj.GetInstanceID();
        game.MyGameObjFeature.Register<T1>(data);
        game.MyEntityFeature.Register<T2>(data);
        return data.InstanceID;
    }

    public void InstanceEntity<T>(Data data) where T : Entity, new() {
        game.MyEntityFeature.Register<T>(data);
    }
}