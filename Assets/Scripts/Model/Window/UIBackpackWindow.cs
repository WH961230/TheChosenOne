using UnityEngine;
using UnityEngine.UI;

public class UIBackpackWindow : Window {
    private CharacterData mainCharacterData;
    private UIBackpackComponent uibackpackComponent;
    private Game game;
    public override void Init(Game game, Data data) {
        this.game = game;
        var obj = game.MyGameObjFeature.Get<UIBackpackGameObj>(data.InstanceID).MyData.MyObj;
        uibackpackComponent = obj.transform.GetComponent<UIBackpackComponent>();
        mainCharacterData = game.MyGameSystem.MyGameObjFeature.Get<CharacterGameObj>(GameData.MainCharacater).GetData<CharacterData>();

        var window = uibackpackComponent.MyUIBackpackWindow;
        var openBtn = uibackpackComponent.MyUIBackpackBtn;
        var closeBtn = uibackpackComponent.MyUIBackpackCloseBtn;
        var mainDropBtns = uibackpackComponent.MyUIBackpackMainWeaponDropBtns;
        var sideDropBtn = uibackpackComponent.MyUIBackpackSideWeaponDropBtn;

        openBtn.gameObject.SetActive(true);
        window.gameObject.SetActive(false);

        // 打开背包
        openBtn.onClick.AddListener(() => {
            DisplaySceneItemInfo();
            window.gameObject.SetActive(true);
            openBtn.gameObject.SetActive(false);
        });

        // 主武器丢弃
        for (var i = 0; i < mainDropBtns.Length; i++) {
            mainDropBtns[i].onClick.AddListener(() => {
                DropSceneItemMainWeapon(i);
                DisplaySceneItemInfo();
            });
        }
        
        // 副武器丢弃
        sideDropBtn.onClick.AddListener(() => {
            DropSceneItemSideWeapon();
            DisplaySceneItemInfo();
        });

        closeBtn.onClick.AddListener(() => {
            openBtn.gameObject.SetActive(true);
            window.gameObject.SetActive(false);
        });
    }

    // 丢弃主武器信息
    private void DropSceneItemMainWeapon(int index) {
        // 主武器
        mainCharacterData.RemoveSceneItemMainWeapon(mainCharacterData.MySceneItemMainWeaponIds[index]);
    }

    private void DropSceneItemSideWeapon() {
        mainCharacterData.RemoveSceneItemSideWeapon();
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