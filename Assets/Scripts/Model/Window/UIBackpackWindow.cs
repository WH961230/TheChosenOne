using UnityEngine;

public class UIBackpackWindow : Window {
    public UIBackpackComponent MyUibackpackComponent;

    private CharacterData MyMainCharacterData {
        get { return MyGame.MyGameSystem.MyCharacterSystem.GetMainCharacterData(); }
    }

    public override void Init(Game game, Data data) {
        base.Init(game, data);
        var obj = game.MyGameObjFeature.Get<UIBackpackGameObj>(data.InstanceID).MyData.MyObj;
        MyUibackpackComponent = obj.transform.GetComponent<UIBackpackComponent>();
        MyGame.MyGameMessageCenter.Register<int>(GameMessageConstants.UISYSTEM_UIBACKPACK_REFRESH, MsgRefresh);
        AddButtonListener();
    }

    public override void Clear() {
        MyGame.MyGameMessageCenter.UnRegister(GameMessageConstants.UISYSTEM_UIBACKPACK_REFRESH);
    }

    #region 监听

    private void AddButtonListener() {
        var window = MyUibackpackComponent.MyUIBackpackWindow;
        var openBtn = MyUibackpackComponent.MyUIBackpackBtn;
        var closeBtn = MyUibackpackComponent.MyUIBackpackCloseBtn;
        var mainDropBtn_1 = MyUibackpackComponent.MyUIBackpackMainWeaponDropBtn_1;
        var mainDropBtn_2 = MyUibackpackComponent.MyUIBackpackMainWeaponDropBtn_2;
        var sideDropBtn = MyUibackpackComponent.MyUIBackpackSideWeaponDropBtn;

        var equipmentBtn_1 = MyUibackpackComponent.MyUIBackpackEquipment_1;
        var equipmentBtn_2 = MyUibackpackComponent.MyUIBackpackEquipment_2;
        var equipmentBtn_3 = MyUibackpackComponent.MyUIBackpackEquipment_3;
        var equipmentBtn_4 = MyUibackpackComponent.MyUIBackpackEquipment_4;

        openBtn.gameObject.SetActive(true);
        window.gameObject.SetActive(false);

        openBtn.onClick.AddListener(() => {
            window.gameObject.SetActive(true);
            openBtn.gameObject.SetActive(false);
            Refresh();
        });

        mainDropBtn_1.onClick.AddListener(() => {
            DropMainWeapon(0);
            RefreshWeapon();
        });

        mainDropBtn_2.onClick.AddListener(() => {
            DropMainWeapon(1);
            RefreshWeapon();
        });

        sideDropBtn.onClick.AddListener(() => {
            DropSideWeapon();
            RefreshWeapon();
        });

        equipmentBtn_1.onClick.AddListener(() => {
            DropEquipment(0);
            RefreshEquipment();
        });

        equipmentBtn_2.onClick.AddListener(() => {
            DropEquipment(1);
            RefreshEquipment();
        });

        equipmentBtn_3.onClick.AddListener(() => {
            DropEquipment(2);
            RefreshEquipment();
        });

        equipmentBtn_4.onClick.AddListener(() => {
            DropEquipment(3);
            RefreshEquipment();
        });

        // 关闭界面
        closeBtn.onClick.AddListener(() => {
            openBtn.gameObject.SetActive(true);
            window.gameObject.SetActive(false);
        });
    }

    #endregion

    #region 丢弃

    private void DropMainWeapon(int index) {
        var bpId = MyMainCharacterData.BackpackInstanceId;
        var backpackEntity = MyGame.MyGameSystem.MyBackpackSystem.GetBackpackEntity(bpId);
        var weapId = backpackEntity.GetMainWeaponId(index);
        if (backpackEntity.DropMainWeapon(index)) {
            MyGame.MyGameMessageCenter.Dispather(GameMessageConstants.WEAPONSYSTEM_DROP, weapId);
        }
    }

    private void DropSideWeapon() {
        var bpId = MyMainCharacterData.BackpackInstanceId;
        var backpackEntity = MyGame.MyGameSystem.MyBackpackSystem.GetBackpackEntity(bpId);
        if (backpackEntity.DropSideWeapon()) {
            MyGame.MyGameMessageCenter.Dispather(GameMessageConstants.WEAPONSYSTEM_DROP, backpackEntity.GetSideWeaponId());
        }
    }

    private void DropEquipment(int index) {
        var bpId = MyMainCharacterData.BackpackInstanceId;
        var backpackEntity = MyGame.MyGameSystem.MyBackpackSystem.GetBackpackEntity(bpId);
        if (backpackEntity.DropEquipment(index)) {
        }
    }

    #endregion

    #region 刷新

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

    public void Refresh() {
        RefreshEquipment();
        RefreshWeapon();
        RefreshSceneItem();
    }

    private void RefreshSceneItem() {
        var bpId = MyMainCharacterData.BackpackInstanceId;
        var backpackEntity = MyGame.MyGameSystem.MyBackpackSystem.GetBackpackEntity(bpId);
        var ids = backpackEntity.GetSceneItemIds();
        for (int i = 0; i < ids.Count; i++) {
            var id = ids[i];
            var sprite = MyGame.MyGameSystem.MyItemSystem.GetSceneItemData(id).MyBackpackSprite;
            MyUibackpackComponent.MyUIBackpackConsumeImages[i].sprite = sprite;
        }
    }

    private void RefreshWeapon() {
        var characterBackpackId = MyMainCharacterData.BackpackInstanceId;
        var backpackEntity = MyGame.MyGameSystem.MyBackpackSystem.GetBackpackEntity(characterBackpackId);
        var mainWeaponIds = backpackEntity.GetMainWeaponIds();
        Sprite sprite = null;
        for (int i = 0; i < mainWeaponIds.Length; i++) {
            sprite = null;
            var id = mainWeaponIds[i];
            if (id != 0) {
                sprite = MyGame.MyGameSystem.MyWeaponSystem.GetWeaponData(id).MyWeaponSprite;
            }

            MyUibackpackComponent.MyUIBackpackMainWeaponImages[i].sprite = sprite;
        }

        var sideWeaponid = backpackEntity.GetSideWeaponId();
        sprite = null;
        if (sideWeaponid != 0) {
            sprite = MyGame.MyGameSystem.MyWeaponSystem.GetWeaponData(sideWeaponid).MyWeaponSprite;
        }

        MyUibackpackComponent.MyUIBackpackSideWeaponImage.sprite = sprite;
    }

    private void RefreshEquipment() {
        var characterBackpackId = MyMainCharacterData.BackpackInstanceId;
        var backpackEntity = MyGame.MyGameSystem.MyBackpackSystem.GetBackpackEntity(characterBackpackId);
        var ids = backpackEntity.GetEquipmentIds();
        for (int i = 0; i < ids.Length; i++) {
            var id = ids[i];
            Sprite sprite = null;
            if (id != 0) {
                sprite = MyGame.MyGameSystem.MyEquipmentSystem.GetEquipmentData(id).MySprite;
            }

            MyUibackpackComponent.MyUIBackpackEquipmentImages[i].sprite = sprite;
        }
    }

    #endregion
}