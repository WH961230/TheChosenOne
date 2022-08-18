using UnityEngine;

public class UIBackpackWindow : Window {
    public UIBackpackComponent comp;

    private CharacterData MyMainCharacterData {
        get { return MyGame.MyGameSystem.MyCharacterSystem.GetEntity(GameData.MainCharacterId).GetData(); }
    }

    private bool IsOpenBackpackWindow;

    public override void Init(Game game, GameObj gameObj) {
        base.Init(game, gameObj);
        var tempGO = (UIBackpackGameObj)gameObj;
        comp = tempGO.GetComp();
        MyGame.MyGameMessageCenter.Register<int>(GameMessageConstants.UISYSTEM_UIBACKPACK_REFRESH, MsgRefresh);
        AddButtonListener();
    }

    public override void Clear() {
        MyGame.MyGameMessageCenter.UnRegister<int>(GameMessageConstants.UISYSTEM_UIBACKPACK_REFRESH, MsgRefresh);
        base.Clear();
    }

    public override void Update() {
        base.Update();
        if (MyGame.MyGameSystem.MyInputSystem.GetKeyDown(KeyCode.Tab)) {
            var window = comp.MyUIBackpackWindow;
            if (IsOpenBackpackWindow) {
                var openBtn = comp.MyUIBackpackBtn;
                openBtn.gameObject.SetActive(true);
                window.gameObject.SetActive(false);
                MyGame.MyGameMessageCenter.Dispather(GameMessageConstants.UISYSTEM_UICHARACTER_REFRESH);
                IsOpenBackpackWindow = false;
            } else {
                var openBtn = comp.MyUIBackpackBtn;
                window.gameObject.SetActive(true);
                openBtn.gameObject.SetActive(false);
                Refresh();
                IsOpenBackpackWindow = true;
            }
        }
    }

    #region 监听

    private void AddButtonListener() {
        var window = comp.MyUIBackpackWindow;
        window.gameObject.SetActive(false);
        var openBtn = comp.MyUIBackpackBtn;
        openBtn.gameObject.SetActive(true);
        openBtn.onClick.AddListener(() => {
            window.gameObject.SetActive(true);
            openBtn.gameObject.SetActive(false);
            Refresh();
            IsOpenBackpackWindow = true;
        });
        var closeBtn = comp.MyUIBackpackCloseBtn;
        // 关闭界面
        closeBtn.onClick.AddListener(() => {
            openBtn.gameObject.SetActive(true);
            window.gameObject.SetActive(false);
            MyGame.MyGameMessageCenter.Dispather(GameMessageConstants.UISYSTEM_UICHARACTER_REFRESH);
            IsOpenBackpackWindow = false;
        });
        var mainWeapon = comp.MyUIBackpackMainWeaponImages;
        for (int i = 0; i < mainWeapon.Length; i++) {
            int ii = i;
            mainWeapon[i].MyButton.onClick.AddListener(() => {
                DropMainWeapon(ii);
                RefreshWeapon();
            });
        }

        var sideWeapon = comp.MyUIBackpackSideWeaponImage;
        sideWeapon.MyButton.onClick.AddListener(() => {
            DropSideWeapon();
            RefreshWeapon();
        });
        var equipment = comp.MyUIBackpackEquipmentImages;
        for (int i = 0; i < equipment.Length; i++) {
            int ii = i;
            equipment[i].MyButton.onClick.AddListener(() => {
                DropEquipment(ii);
                RefreshEquipment();
            });
        }

        for (int i = 0; i < comp.MyUIBackpackConsumeImages.Count; i++) {
            int ii = i;
            comp.MyUIBackpackConsumeImages[i].MyButton.onClick.AddListener(() => {
                RefreshSceneItem();
            });
        }
    }

    #endregion

    #region 丢弃

    private void DropMainWeapon(int index) {
        var backpackEntity = GetBackpackEntity();
        backpackEntity.DropMainWeapon(index);
    }

    private void DropSideWeapon() {
        var backpackEntity = GetBackpackEntity();
        backpackEntity.DropSideWeapon();
    }

    private void DropEquipment(int index) {
        var backpackEntity = GetBackpackEntity();
        backpackEntity.DropEquipment(index);
    }

    private BackpackEntity GetBackpackEntity() {
        var bpId = MyMainCharacterData.BackpackInstanceId;
        return MyGame.MyGameSystem.MyBackpackSystem.GetEntity(bpId);
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

    private void Refresh() {
        RefreshEquipment();
        RefreshWeapon();
        RefreshSceneItem();
    }

    private void RefreshSceneItem() {
        var backpackEntity = GetBackpackEntity();
        var ids = backpackEntity.GetSceneItemIds(); // 物品
        var level = backpackEntity.GetEquipmentLevel(); // 容量
        if (ids.Count > level) {
            LogSystem.PrintE("物品数量大于容量！");
        }

        for (int i = 0; i < level; i++) {
            Sprite sprite;
            if (ids.Count > i) {
                var id = ids[i];
                sprite = MyGame.MyGameSystem.MyItemSystem.GetItemData(id).MyBackpackSprite;
            } else {
                sprite = null;
            }

            comp.MyUIBackpackConsumeImages[i].MyButton.image.sprite = sprite;
        }
    }

    private void RefreshWeapon() {
        var backpackEntity = GetBackpackEntity();
        var mainWeaponIds = backpackEntity.GetMainWeaponIds();
        Sprite sprite = null;
        for (int i = 0; i < mainWeaponIds.Length; i++) {
            sprite = null;
            var id = mainWeaponIds[i];
            if (id != 0) {
                sprite = MyGame.MyGameSystem.MyWeaponSystem.GetWeaponData(id).MySprite;
            }

            comp.MyUIBackpackMainWeaponImages[i].MyButton.image.sprite = sprite;
        }

        var sideWeaponid = backpackEntity.GetSideWeaponId();
        sprite = null;
        if (sideWeaponid != 0) {
            sprite = MyGame.MyGameSystem.MyWeaponSystem.GetWeaponData(sideWeaponid).MySprite;
        }

        comp.MyUIBackpackSideWeaponImage.MyButton.image.sprite = sprite;
    }

    private void RefreshEquipment() {
        var backpackEntity = GetBackpackEntity();
        var ids = backpackEntity.GetEquipmentIds();
        for (int i = 0; i < ids.Length; i++) {
            var id = ids[i];
            Sprite sprite = null;
            if (id != 0) {
                sprite = MyGame.MyGameSystem.MyEquipmentSystem.GetEquipmentData(id).MySprite;
            }

            comp.MyUIBackpackEquipmentImages[i].MyButton.image.sprite = sprite;
        }
    }

    #endregion
}