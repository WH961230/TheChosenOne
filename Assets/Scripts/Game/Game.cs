using UnityEngine;

public class Game : MonoBehaviour {
    #region 序列化公共参数

    public int IsShowLog; // 0:不打印日志 1:普通日志 2:错误日志 3:全部日志

    #endregion
    
    #region 数据

    private GameManager gameManager = new GameManager();

    #endregion

    #region 系统

    private GameSystem gameSystem = new GameSystem();

    public GameSystem MyGameSystem {
        get { return gameSystem; }
    }

    #endregion

    #region 模块

    private GameObjFeature myGameObjFeature;
    public GameObjFeature MyGameObjFeature {
        get {
            if (null == myGameObjFeature) {
                myGameObjFeature = Get<GameObjFeature>();
            }

            return myGameObjFeature;
        }
    }

    private WindowFeature myWindowFeature;
    public WindowFeature MyWindowFeature {
        get {
            if (null == myWindowFeature) {
                myWindowFeature = Get<WindowFeature>();
            }

            return myWindowFeature;
        }
    }

    private EntityFeature myEntityFeature;
    public EntityFeature MyEntityFeature {
        get {
            if (null == myEntityFeature) {
                myEntityFeature = Get<EntityFeature>();
            }

            return myEntityFeature;
        }
    }

    #endregion

    #region 消息

    private GameMessageCenter gameMessageCenter = new GameMessageCenter();
    public GameMessageCenter MyGameMessageCenter {
        get {
            return gameMessageCenter;
        }
    }

    #endregion
    void Start() {
        // game 序列化设置
        GameData.IsShowLog = IsShowLog;

        // 配置初始化 - 供全局使用
        InitSOData();

        // 游戏根目录初始化
        InitRoot();

        // 数据模块模块注册
        Register<WindowFeature>();
        Register<GameObjFeature>();
        Register<EntityFeature>();

        // 逻辑初始化
        gameSystem.Init(this);
    }

    private void InitRoot() {
        var gameRoot = new GameObject("GameRoot").transform;

        GameData.UIRoot = Instantiate(SOData.MySOGameSetting.UIRoot).transform;
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
    }

    private void InitSOData() {
        SOData.Init();
    }

    void Clear() {
        Remove<WindowFeature>();
        Remove<GameObjFeature>();
        Remove<EntityFeature>();
        gameSystem.Clear();
    }

    void Update() {
        gameSystem.Update();
        gameManager.Update();
    }

    private void FixedUpdate() {
        gameSystem.FixedUpdate();
        gameManager.FixedUpdate();
    }

    private void LateUpdate() {
        gameSystem.LateUpdate();
        gameManager.LateUpdate();
    }

    private void Register<T>() where T : IFeature, new() {
        gameManager.Register<T>(this);
    }

    private void Remove<T>() where T : IFeature, new() {
        gameManager.Remove<T>();
    }

    private T Get<T>() where T : IFeature {
        return gameManager.Get<T>();
    }
}