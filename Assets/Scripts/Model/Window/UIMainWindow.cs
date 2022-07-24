public class UIMainWindow : Window {
    public UIMainComponent MyUiMainComponent;
    private UIMainGameObj uiMainGameObj;

    public override void Init(Game game, Data data) {
        base.Init(game, data);
        // 初始化界面
        uiMainGameObj = MyGame.MyGameObjFeature.Get<UIMainGameObj>(data.InstanceID);
        MyUiMainComponent = uiMainGameObj.MyData.MyObj.transform.GetComponent<UIMainComponent>();

        // 默认界面开启
        Open();
        MyUiMainComponent.MyButton.onClick.AddListener(() => {
            // 加载 DebugUI
            MyGame.MyGameSystem.MyUISystem.InstanceUIDebugToolWindow();

            // 加载灯光
            MyGame.MyGameSystem.MyEnvironmentSystem.InstanceLight();

            // 加载建筑、地面
            MyGame.MyGameSystem.MyEnvironmentSystem.InstanceEnvironment();

            // 加载场景可拾取物体
            MyGame.MyGameSystem.MyItemSystem.InstanceSceneItem();

            // 加载武器
            MyGame.MyGameSystem.MyWeaponSystem.InstanceWeapon();

            // 加载装备
            MyGame.MyGameSystem.MyEquipmentSystem.InstanceEquipment();

            // 读取玩家生成点 创建主玩家
            if (GameData.MainCharacterId == 0) {
                GameData.MainCharacterId = game.MyGameSystem.MyCharacterSystem.InstanceCharacter(true);
            }

            // 加载角色 UI
            MyGame.MyGameSystem.MyUISystem.InstanceUICharacterWindow();

            // 加载地图 UI
            MyGame.MyGameSystem.MyUISystem.InstanceUIMapWindow();

            // 加载背包 UI
            MyGame.MyGameSystem.MyUISystem.InstanceUIBackpackWindow();

            // 加载贴士 UI
            MyGame.MyGameSystem.MyUISystem.InstanceUITipWindow();

            // 播放背景音效（替代资源）
            game.MyGameSystem.MyAudioSystem.InstanceAudioMain();

            // 关闭界面
            Close();
        });

        MyUiMainComponent.MyButton.onClick.Invoke();
    }

    public override void Open() {
        uiMainGameObj.Display();
    }

    public override void Update() {
    }

    public override void Close() {
        uiMainGameObj.Hide();
    }

    public override void Clear() {
    }
}