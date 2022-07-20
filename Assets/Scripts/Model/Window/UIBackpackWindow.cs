public class UIBackpackWindow : Window {
    private CharacterData mainCharacterData;
    private CharacterComponent mainCharacterComponent;
    private CharacterGameObj mainCharacterGameObj;
    private UIBackpackComponent uibackpackComponent;

    public override void Init(Game game, Data data) {
        base.Init(game, data);
        var obj = game.MyGameObjFeature.Get<UIBackpackGameObj>(data.InstanceID).MyData.MyObj;
        uibackpackComponent = obj.transform.GetComponent<UIBackpackComponent>();

        MyGame.MyGameMessageCenter.Register<int>(GameMessageConstants.UISYSTEM_UIBACKPACK_REFRESH, MsgRefresh);
        
        AddButtonListener();
    }

    public override void Clear() {
        MyGame.MyGameMessageCenter.UnRegister(GameMessageConstants.UISYSTEM_UIBACKPACK_REFRESH);
    }

    private void AddButtonListener() {
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

        openBtn.onClick.AddListener(() => {
            Refresh();
            window.gameObject.SetActive(true);
            openBtn.gameObject.SetActive(false);
        });

        mainDropBtn_1.onClick.AddListener(() => {
            DropSceneItemMainWeapon(0);
            RefreshWeapon();
        });

        mainDropBtn_2.onClick.AddListener(() => {
            DropSceneItemMainWeapon(1);
            RefreshWeapon();
        });

        sideDropBtn.onClick.AddListener(() => {
            DropSceneItemSideWeapon();
            RefreshWeapon();
        });

        equipmentBtn_1.onClick.AddListener(() => {
            DropSceneItemEquipment(0);
            RefreshEquipment();
        });
        equipmentBtn_2.onClick.AddListener(() => {
            DropSceneItemEquipment(1);
            RefreshEquipment();
        });
        equipmentBtn_3.onClick.AddListener(() => {
            DropSceneItemEquipment(2);
            RefreshEquipment();
        });
        equipmentBtn_4.onClick.AddListener(() => {
            DropSceneItemEquipment(3);
            RefreshEquipment();
        });

        // 关闭界面
        closeBtn.onClick.AddListener(() => {
            openBtn.gameObject.SetActive(true);
            window.gameObject.SetActive(false);
        });
    }

    private void MsgRefresh(int layer) {
        switch (layer) {
            case 9:
                RefreshSceneItem();
                break;
            case 12:
                RefreshWeapon();
                break;
            case 13:
                RefreshEquipment();
                break;
        }
    }

    private void DropSceneItemMainWeapon(int index) {
        var id = mainCharacterData.MySceneItemMainWeaponIds[index];
        if (mainCharacterData.RemoveSceneItemMainWeapon(id)) {
            MyGame.MyGameObjFeature.Get<SceneItemGameObj>(id).ShowObj(GameData.GetGround(mainCharacterComponent.transform.position));
        }
    }

    private void DropSceneItemSideWeapon() {
        var id = mainCharacterData.MySceneItemSideWeaponId;
        if (mainCharacterData.RemoveSceneItemSideWeapon()) {
            MyGame.MyGameObjFeature.Get<SceneItemGameObj>(id).ShowObj(GameData.GetGround(mainCharacterComponent.transform.position));
        }
    }

    private void DropSceneItemEquipment(int index) {
        var id = mainCharacterData.MySceneItemEquipmentIds[index];
        if (mainCharacterData.RemoveSceneItemEquipment(id, index)) {
            MyGame.MyGameObjFeature.Get<SceneItemGameObj>(id).ShowObj(GameData.GetGround(mainCharacterComponent.transform.position));
        }
    }

    private void Refresh() {
        RefreshEquipment();
        RefreshWeapon();
        RefreshSceneItem();
    }

    private void RefreshSceneItem() {
        var characterBackpackId = MyGame.MyGameSystem.MyCharacterSystem.GetMainCharacterData().BackpackInstanceId;
        var data = MyGame.MyGameSystem.MyBackpackSystem.GetBackpackData(characterBackpackId);
        for (int i = 0; i < data.MySceneItemConsumeIds.Count; i++) {
            var id = data.MySceneItemConsumeIds[i];
            var sprite = MyGame.MyGameSystem.MyItemSystem.GetSceneItemData(id).MyBackpackSprite;
            uibackpackComponent.MyUIBackpackConsumeImages[i].sprite = sprite;
        }
    }

    private void RefreshWeapon() {
        var characterBackpackId = MyGame.MyGameSystem.MyCharacterSystem.GetMainCharacterData().BackpackInstanceId;
        var data = MyGame.MyGameSystem.MyBackpackSystem.GetBackpackData(characterBackpackId);
        for (int i = 0; i < data.MySceneItemMainWeaponIds.Length; i++) {
            var id = data.MySceneItemMainWeaponIds[i];
            var sprite = MyGame.MyGameSystem.MyWeaponSystem.GetWeaponData(id).MyWeaponSprite;
            uibackpackComponent.MyUIBackpackMainWeaponImages[i].sprite = sprite;
        }

        uibackpackComponent.MyUIBackpackSideWeaponImage.sprite = MyGame.MyGameSystem.MyWeaponSystem.GetWeaponData(data.MySceneItemSideWeaponId).MyWeaponSprite;
    }

    private void RefreshEquipment() {
        var characterBackpackId = MyGame.MyGameSystem.MyCharacterSystem.GetMainCharacterData().BackpackInstanceId;
        var data = MyGame.MyGameSystem.MyBackpackSystem.GetBackpackData(characterBackpackId);
        for (int i = 0; i < data.MySceneItemEquipmentIds.Length; i++) {
            var id = data.MySceneItemEquipmentIds[i];
            var sprite = MyGame.MyGameSystem.MyEquipmentSystem.GetEquipmentData(id).MySprite;
            uibackpackComponent.MyUIBackpackEquipmentImages[i].sprite = sprite;
        }
    }

    public override void Open() {
    }

    public override void Update() {
    }

    public override void Close() {
    }
}