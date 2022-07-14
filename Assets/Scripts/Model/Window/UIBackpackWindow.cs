public class UIBackpackWindow : Window {
    private CharacterData mainCharacterData;
    private CharacterComponent mainCharacterComponent;
    private CharacterGameObj mainCharacterGameObj;
    private UIBackpackComponent uibackpackComponent;
    private Game game;

    public override void Init(Game game, Data data) {
        this.game = game;
        var obj = game.MyGameObjFeature.Get<UIBackpackGameObj>(data.InstanceID).MyData.MyObj;
        uibackpackComponent = obj.transform.GetComponent<UIBackpackComponent>();
        mainCharacterGameObj = game.MyGameSystem.MyGameObjFeature.Get<CharacterGameObj>(GameData.MainCharacater);
        mainCharacterComponent = mainCharacterGameObj.GetComponent<CharacterComponent>();
        mainCharacterData = mainCharacterGameObj.GetData<CharacterData>();

        var window = uibackpackComponent.MyUIBackpackWindow;
        var openBtn = uibackpackComponent.MyUIBackpackBtn;
        var closeBtn = uibackpackComponent.MyUIBackpackCloseBtn;
        var mainDropBtn_1 = uibackpackComponent.MyUIBackpackMainWeaponDropBtn_1;
        var mainDropBtn_2 = uibackpackComponent.MyUIBackpackMainWeaponDropBtn_2;
        var sideDropBtn = uibackpackComponent.MyUIBackpackSideWeaponDropBtn;

        var equipmentBtn_1 = uibackpackComponent.MyUIBackpackEquipment_1;
        var equipmentBtn_2 = uibackpackComponent.MyUIBackpackEquipment_2;
        var equipmentBtn_3 = uibackpackComponent.MyUIBackpackEquipment_3;
        var equipmentBtn_4 = uibackpackComponent.MyUIBackpackEquipment_4;

        openBtn.gameObject.SetActive(true);
        window.gameObject.SetActive(false);

        // 打开背包
        openBtn.onClick.AddListener(() => {
            DisplaySceneItemInfo();
            window.gameObject.SetActive(true);
            openBtn.gameObject.SetActive(false);
        });

        // 主武器丢弃
        mainDropBtn_1.onClick.AddListener(() => {
            DropSceneItemMainWeapon(0);
            DisplaySceneItemInfo();
        });

        mainDropBtn_2.onClick.AddListener(() => {
            DropSceneItemMainWeapon(1);
            DisplaySceneItemInfo();
        });

        // 副武器丢弃
        sideDropBtn.onClick.AddListener(() => {
            DropSceneItemSideWeapon();
            DisplaySceneItemInfo();
        });
        
        

        // 关闭界面
        closeBtn.onClick.AddListener(() => {
            openBtn.gameObject.SetActive(true);
            window.gameObject.SetActive(false);
        });
    }

    // 丢弃主武器信息
    private void DropSceneItemMainWeapon(int index) {
        // 主武器
        var id = mainCharacterData.MySceneItemMainWeaponIds[index];
        if (mainCharacterData.RemoveSceneItemMainWeapon(id)) {
            // 丢弃主武器
            game.MyGameObjFeature.Get<SceneItemGameObj>(id).ShowObj(GameData.GetGround(mainCharacterComponent.transform.position));
        }
    }

    private void DropSceneItemSideWeapon() {
        var id = mainCharacterData.MySceneItemSideWeaponId;
        if (mainCharacterData.RemoveSceneItemSideWeapon()) {
            // 丢弃主武器
            game.MyGameObjFeature.Get<SceneItemGameObj>(id).ShowObj(GameData.GetGround(mainCharacterComponent.transform.position));
        }
    }

    private void DropSceneItemEquipment(int index) {
        var id = mainCharacterData.MySceneItemEquipmentIds[index];
        if (mainCharacterData.RemoveSceneItemEquipment(id, index)) {
            // 丢弃主武器
            game.MyGameObjFeature.Get<SceneItemGameObj>(id).ShowObj(GameData.GetGround(mainCharacterComponent.transform.position));
        }
    }

    // 展示物品信息
    private void DisplaySceneItemInfo() {
        var gameObjFeature = game.MyGameSystem.MyGameObjFeature;
        // 主武器
        for (var i = 0; i < mainCharacterData.MySceneItemMainWeaponIds.Length; i++) {
            var tempId = mainCharacterData.MySceneItemMainWeaponIds[i];
            if (tempId == 0) {
                uibackpackComponent.MyUIBackpackMainWeaponImages[i].sprite = null;
            } else {
                var tempSprite = gameObjFeature.Get<SceneItemGameObj>(tempId).GetData<SceneItemData>().MyBackpackSprite;
                uibackpackComponent.MyUIBackpackMainWeaponImages[i].sprite = tempSprite;
            }
        }

        // 副武器
        var sceneItemId2 = mainCharacterData.MySceneItemSideWeaponId;
        if (sceneItemId2 == 0) {
            uibackpackComponent.MyUIBackpackSideWeaponImage.sprite = null;
        } else {
            var sceneItemData = gameObjFeature.Get<SceneItemGameObj>(sceneItemId2).GetData<SceneItemData>().MyBackpackSprite;
            uibackpackComponent.MyUIBackpackSideWeaponImage.sprite = sceneItemData;
        }

        // 装备
        for (int i = 0; i < mainCharacterData.MySceneItemEquipmentIds.Length; i++) {
            var tempId = mainCharacterData.MySceneItemEquipmentIds[i];
            if (tempId == 0) {
                uibackpackComponent.MyUIBackpackEquipmentImages[i].sprite = null;
            } else {
                var tempSprite = gameObjFeature.Get<SceneItemGameObj>(tempId).GetData<SceneItemData>().MyBackpackSprite;
                uibackpackComponent.MyUIBackpackEquipmentImages[i].sprite = tempSprite;
            }
        }

        // 玩家物品
        for (var i = 0; i < mainCharacterData.MySceneItemConsumeIds.Length; i++) {
            var tempId = mainCharacterData.MySceneItemConsumeIds[i];
            if (tempId == 0) {
                uibackpackComponent.MyUIBackpackConsumeImages[i].sprite = null;
            } else {
                var tempSprite = gameObjFeature.Get<SceneItemGameObj>(tempId).GetData<SceneItemData>().MyBackpackSprite;
                uibackpackComponent.MyUIBackpackConsumeImages[i].sprite = tempSprite;
            }
        }

        // 附近物体
    }

    public override void Open() {
    }

    public override void Update() {
    }

    public override void Close() {
    }

    public override void Clear() {
    }
}