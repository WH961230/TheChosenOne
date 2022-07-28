using UnityEngine;

public class UICharacterWindow : Window {
    private UICharacterComponent uicharacterComponent;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        var obj = game.MyGameObjFeature.Get<UICharacterGameObj>(data.InstanceID).MyData.MyObj;
        uicharacterComponent = obj.transform.GetComponent<UICharacterComponent>();
        uicharacterComponent.UIWeaponLeft.onClick.AddListener(() => {
            var bpEntity = MyGame.MyGameSystem.MyBackpackSystem.GetBackpackEntity(MyGame.MyGameSystem.MyCharacterSystem.GetMainCharacterData().BackpackInstanceId);
            bpEntity.SetCurMainWeapon(0);
        });
        uicharacterComponent.UIWeaponRight.onClick.AddListener(() => {
            var bpEntity = MyGame.MyGameSystem.MyBackpackSystem.GetBackpackEntity(MyGame.MyGameSystem.MyCharacterSystem.GetMainCharacterData().BackpackInstanceId);
            bpEntity.SetCurMainWeapon(1);
        });
        MyGame.MyGameMessageCenter.Register(GameMessageConstants.UISYSTEM_UICHARACTER_REFRESH, MsgRefresh);
        Refresh();
    }

    public override void Clear() {
        MyGame.MyGameMessageCenter.UnRegister(GameMessageConstants.UISYSTEM_UICHARACTER_REFRESH, MsgRefresh);
        base.Clear();
    }

    private void MsgRefresh() {
        Refresh();
    }

    public void Refresh() {
        RefreshWeapon();
    }

    private void RefreshWeapon() {
        var backpackEntity = MyGame.MyGameSystem.MyBackpackSystem.GetBackpackEntity(MyGame.MyGameSystem.MyCharacterSystem.GetMainCharacterData().BackpackInstanceId);
        var mainWeaponIds = backpackEntity.GetMainWeaponIds();
        Sprite sprite = null;
        for (int i = 0; i < mainWeaponIds.Length; i++) {
            var tempId = mainWeaponIds[i];
            if (tempId != 0) {
                sprite = MyGame.MyGameSystem.MyWeaponSystem.GetWeaponData(tempId).MySprite;
                if (i == 0) {
                    uicharacterComponent.UIWeaponLeft.image.sprite = sprite;
                } else if (i == 1) {
                    uicharacterComponent.UIWeaponRight.image.sprite = sprite;
                }
            } else {
                if (i == 0) {
                    uicharacterComponent.UIWeaponLeft.image.sprite = null;
                } else if (i == 1) {
                    uicharacterComponent.UIWeaponRight.image.sprite = null;
                }
            }
        }
    }

    public override void Open() {
    }

    public override void Update() {
    }

    public override void Close() {
    }
}