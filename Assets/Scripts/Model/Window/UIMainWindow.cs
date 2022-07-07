using UnityEngine;

public class UIMainWindow : Window {
    private Game game;
    private UIMainComponent uiMainComponent;
    private UIMainGameObj uiMainGameObj;
    public override void Init(Game game, Data data) {
        // 初始化界面
        uiMainGameObj = game.MyGameObjFeature.Get<UIMainGameObj>(data.InstanceID);
        uiMainComponent = uiMainGameObj.MyData.MyObj.transform.GetComponent<UIMainComponent>();

        this.game = game;

        // 默认界面开启
        Open();
        
        uiMainComponent.MyButton.onClick.AddListener(() => {

            // 读取场景配置 创建场景实例
            game.MyGameSystem.MyEnvironmentSystem.InstanceEnvironment();
            
            // 场景物体
            game.MyGameSystem.MyItemSystem.InstanceSceneItem();

            // 读取玩家生成点 创建主玩家
            game.MyGameSystem.MyCharacterSystem.InstanceCharacter(true);

            // 关闭界面
            Close();
        });
    }

    public override void Open() {
        // 开启界面
        uiMainGameObj.Display();
    }

    public override void Update() {
        // 更新界面
    }

    public override void Close() {
        // 关闭界面
        uiMainGameObj.Hide();
    }

    public override void Clear() {
        // 清除界面
    }
}