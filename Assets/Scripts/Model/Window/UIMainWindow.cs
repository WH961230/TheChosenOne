using UnityEngine;

public class UIMainWindow : Window {
    private Game game;
    private UIMainComponent uiMainComponent;
    private UIMainGameObj uiMainGameObj;
    public override void Init(Game game, Data data) {
        // 初始化界面
        uiMainGameObj = (UIMainGameObj) game.Get<GameObjFeature>().Get(data.InstanceID);
        uiMainComponent = uiMainGameObj.MyData.MyObj.transform.GetComponent<UIMainComponent>();

        this.game = game;

        // 默认界面开启
        Open();
        
        uiMainComponent.MyButton.onClick.AddListener(() => {
            // 日志
            Debug.Log("创建角色");

            // 创建玩家
            game.MyGameSystem.MyCharacterSystem.InstanceCharacter(new CharacterData() {
                MyName = "cubeTest",
                MyObj = Object.Instantiate(game.MyGameSystem.MyCharacterSystem.MySoCharacterSetting.CharacterPrefab),
                MyTranInfo = new TranInfo() {
                    MyPos = new Vector3(0,2,0),
                    MyRot = new Quaternion(0, 0, 0, 0),
                    MyRootTran = GameData.ItemRoot,
                }
            });

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