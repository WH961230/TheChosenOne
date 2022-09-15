using TMPro;
using UnityEngine;

public class UIBackpackWindow : Window {
    private UIBackpackComponent bpComp;

    private CharacterData MyMainCharacterData {
        get { return MyGS.Get<CharacterSystem>().GetEntity(GameData.MainCharacterId).GetData(); }
    }

    private bool IsOpenBackpackWindow;

    public override void Init(Game game, GameObj gameObj) {
        base.Init(game, gameObj);
        var tempGO = (UIBackpackGameObj)gameObj;

        bpComp = tempGO.GetComp();
        MyGame.MyGameMessageCenter.Register<int>(GameMessageConstants.UISYSTEM_UIBACKPACK_REFRESH, MsgRefresh);

        ActiveGO(bpComp.MyUIBackpackWindow.gameObject, false);
        ActiveGO(bpComp.MyUIBackpackBtn.gameObject, true);

        AddButtonListener();
    }

    public override void Clear() {
        MyGame.MyGameMessageCenter.UnRegister<int>(GameMessageConstants.UISYSTEM_UIBACKPACK_REFRESH, MsgRefresh);
        base.Clear();
    }

    public override void Update() {
        base.Update();
        if (MyGS.Get<InputSystem>().GetKeyDown(KeyCode.Tab)) {
            var window = bpComp.MyUIBackpackWindow;
            if (IsOpenBackpackWindow) {
                CloseBp();
            } else {
                OpenBp();
            }
        }
    }

    private void OpenBp() {
        ActiveGO(bpComp.MyUIBackpackWindow, true);
        ActiveGO(bpComp.MyUIBackpackBtn.gameObject, false);
        Refresh();
        IsOpenBackpackWindow = true;
    }

    private void CloseBp() {
        ActiveGO(bpComp.MyUIBackpackWindow, false);
        ActiveGO(bpComp.MyUIBackpackBtn.gameObject, true);
        MyGame.MyGameMessageCenter.Dispather(GameMessageConstants.UISYSTEM_UICHARACTER_REFRESH);
        IsOpenBackpackWindow = false;
    }

    private void DropSideWeap() {
        DropSideWeapon();
        RefreshWeapon();
    }

    private void AddButtonListener() {
        RegisterEvent(bpComp.MyUIBackpackBtn.onClick, OpenBp);
        RegisterEvent(bpComp.MyUIBackpackCloseBtn.onClick, CloseBp);

        var mainWeapon = bpComp.MyUIBackpackMainWeaponImages;
        for (int i = 0; i < mainWeapon.Length; i++) {
            int ii = i;
            mainWeapon[i].MyButton.onClick.AddListener(() => {
                DropMainWeapon(ii);
                RefreshWeapon();
            });
        }

        RegisterEvent(bpComp.MyUIBackpackSideWeaponImage.MyButton.onClick, DropSideWeap);

        var equipment = bpComp.MyUIBackpackEquipmentImages;
        for (int i = 0; i < equipment.Length; i++) {
            int ii = i;
            equipment[i].MyButton.onClick.AddListener(() => {
                DropEquipment(ii);
                RefreshEquipment();
            });
        }

        for (int i = 0; i < bpComp.MyUIBackpackConsumeImages.Count; i++) {
            RegisterEvent(bpComp.MyUIBackpackConsumeImages[i].MyButton.onClick, RefreshSceneItem);
        }
    }

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
        return MyGS.Get<BackpackSystem>().GetEntity(bpId);
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
                // sprite = MyGS.MyItemSystem.GetItemData(id).MyBackpackSprite;
            } else {
                sprite = null;
            }

            // comp.MyUIBackpackConsumeImages[i].MyButton.image.sprite = sprite;
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
                sprite = MyGS.Get<WeaponSystem>().GetEntity(id).GetData().MySprite;
            }

            bpComp.MyUIBackpackMainWeaponImages[i].MyButton.image.sprite = sprite;
        }

        var sideWeaponid = backpackEntity.GetSideWeaponId();
        sprite = null;
        if (sideWeaponid != 0) {
            sprite = MyGS.Get<WeaponSystem>().GetEntity(sideWeaponid).GetData().MySprite;
        }

        bpComp.MyUIBackpackSideWeaponImage.MyButton.image.sprite = sprite;
    }

    private void RefreshEquipment() {
        var backpackEntity = GetBackpackEntity();
        var ids = backpackEntity.GetEquipmentIds();
        for (int i = 0; i < ids.Length; i++) {
            var id = ids[i];
            Sprite sprite = null;
            if (id != 0) {
                sprite = MyGS.Get<EquipmentSystem>().GetEtity(id).GetData().MySprite;
            }

            bpComp.MyUIBackpackEquipmentImages[i].MyButton.image.sprite = sprite;
        }
    }

    #endregion
}