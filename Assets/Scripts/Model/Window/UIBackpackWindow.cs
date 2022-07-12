using UnityEngine;
using UnityEngine.UI;

public class UIBackpackWindow : Window {
    private UIBackpackComponent uibackpackComponent;
    private Game game;
    public override void Init(Game game, Data data) {
        this.game = game;
        var obj = game.MyGameObjFeature.Get<UIBackpackGameObj>(data.InstanceID).MyData.MyObj;
        uibackpackComponent = obj.transform.GetComponent<UIBackpackComponent>();
        var window = uibackpackComponent.MyUIBackpackWindow;
        var openBtn = uibackpackComponent.MyUIBackpackBtn;
        var closeBtn = uibackpackComponent.MyUIBackpackCloseBtn;
        var drop1Btn = uibackpackComponent.MyUIBackpackDrop1Btn;
        var drop2Btn = uibackpackComponent.MyUIBackpackDrop2Btn;
        
        openBtn.gameObject.SetActive(true);
        window.gameObject.SetActive(false);



        openBtn.onClick.AddListener(() => {
            var characterGameObj = game.MyGameSystem.MyGameObjFeature.Get<CharacterGameObj>(GameData.MainCharacater);
            var characterData = characterGameObj.GetData<CharacterData>();

            // 第一物品栏
            if (characterData.MySceneItemIds.Count >= 1) {
                var sceneItemId1 = characterData.MySceneItemIds[0];
                var sceneItemData = game.MyGameSystem.MyGameObjFeature.Get<SceneItemGameObj>(sceneItemId1).GetData<SceneItemData>().MySceneItemPicture;
                SetImage1(sceneItemData);
            }

            // 第二物品栏
            if (characterData.MySceneItemIds.Count >= 2) {
                var sceneItemId2 = characterData.MySceneItemIds[1];
                var sceneItemData = game.MyGameSystem.MyGameObjFeature.Get<SceneItemGameObj>(sceneItemId2).GetData<SceneItemData>().MySceneItemPicture;
                SetImage2(sceneItemData);
            }

            window.gameObject.SetActive(true);
            openBtn.gameObject.SetActive(false);
        });
        
        drop1Btn.onClick.AddListener(() => {
            var characterGameObj = game.MyGameSystem.MyGameObjFeature.Get<CharacterGameObj>(GameData.MainCharacater);
            var characterData = characterGameObj.GetData<CharacterData>();
            characterData.MySceneItemIds.RemoveAt(0);
            SetImage1(null);
        });

        drop2Btn.onClick.AddListener(() => {
            var characterGameObj = game.MyGameSystem.MyGameObjFeature.Get<CharacterGameObj>(GameData.MainCharacater);
            var characterData = characterGameObj.GetData<CharacterData>();
            characterData.MySceneItemIds.RemoveAt(1);
            SetImage2(null);
        });

        closeBtn.onClick.AddListener(() => {
            openBtn.gameObject.SetActive(true);
            window.gameObject.SetActive(false);
        });
    }

    public void SetImage1(Sprite image) {
        uibackpackComponent.MyUIBackpackItemImage1.sprite = image;
    }

    public void SetImage2(Sprite image) {
        uibackpackComponent.MyUIBackpackItemImage2.sprite = image;
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