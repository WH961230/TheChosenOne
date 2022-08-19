using UnityEngine;

public class UICharacterWindow : Window {
    private UICharacterComponent MyComp;
    public override void Init(Game game, GameObj gameObj) {
        base.Init(game, gameObj);
        var tempGO = (UICharacterGameObj)gameObj;
        MyComp = tempGO.GetComp();
        var playerData = MyGS.CharacterS.GetEntity(GameData.MainCharacterId).GetData();
        MyComp.UIWeaponLeft.onClick.AddListener(() => {
            var bpEntity = MyGS.BackpackS.GetEntity(playerData.BackpackInstanceId);
            bpEntity.SetCurMainWeapon(0);
        });
        MyComp.UIWeaponRight.onClick.AddListener(() => {
            var bpEntity = MyGS.BackpackS.GetEntity(playerData.BackpackInstanceId);
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
        var playerData = MyGS.CharacterS.GetEntity(GameData.MainCharacterId).GetData();
        var backpackEntity = MyGS.BackpackS.GetEntity(playerData.BackpackInstanceId);
        var mainWeaponIds = backpackEntity.GetMainWeaponIds();
        Sprite sprite = null;
        for (int i = 0; i < mainWeaponIds.Length; i++) {
            var tempId = mainWeaponIds[i];
            if (tempId != 0) {
                sprite = MyGS.WeapS.GetEntity(tempId).GetData().MySprite;
                if (i == 0) {
                    MyComp.UIWeaponLeft.image.sprite = sprite;
                } else if (i == 1) {
                    MyComp.UIWeaponRight.image.sprite = sprite;
                }
            } else {
                if (i == 0) {
                    MyComp.UIWeaponLeft.image.sprite = null;
                } else if (i == 1) {
                    MyComp.UIWeaponRight.image.sprite = null;
                }
            }
        }
    }
}