using UnityEngine;

public class UICharacterWindow : Window {
    private UICharacterComponent ChaComp;
    public override void Init(Game game, GameObj gameObj) {
        base.Init(game, gameObj);

        var tempGO = (UICharacterGameObj)gameObj;

        ChaComp = tempGO.GetComp();

        var playerData = MyGS.CharacterS.GetEntity(GameData.MainCharacterId).GetData();

        ChaComp.UIWeaponLeft.onClick.AddListener(() => {
            var bpEntity = MyGS.BackpackS.GetEntity(playerData.BackpackInstanceId);
            bpEntity.SetCurMainWeapon(0);
        });

        ChaComp.UIWeaponRight.onClick.AddListener(() => {
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

    private void Refresh() {
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
                    ChaComp.UIWeaponLeft.image.sprite = sprite;
                } else if (i == 1) {
                    ChaComp.UIWeaponRight.image.sprite = sprite;
                }
            } else {
                if (i == 0) {
                    ChaComp.UIWeaponLeft.image.sprite = null;
                } else if (i == 1) {
                    ChaComp.UIWeaponRight.image.sprite = null;
                }
            }
        }
    }
}