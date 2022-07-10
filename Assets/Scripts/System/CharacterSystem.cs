using UnityEngine;

public class CharacterSystem : GameSys {
    private SOCharacterSetting soCharacterSetting;

    public SOCharacterSetting MySoCharacterSetting {
        get { return soCharacterSetting; }
    }

    private GameSystem gameSystem;

    public override void Init(GameSystem gameSystem) {
        this.gameSystem = gameSystem;
        soCharacterSetting = Resources.Load<SOCharacterSetting>(PathData.SOCharacterSettingPath);
        gameSystem.MyCameraSystem.InstanceCamera(CameraType.MainCharacterCamera);
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }

    public void InstanceCharacter(bool isMainCharacter) {
        var instanceId = InstanceCharacter(new CharacterData() {
            MyName = "Character",
            MyObj = Object.Instantiate(MySoCharacterSetting.GetCharacterPrefab()),
            MyRootTran = GameData.CharacterRoot,

            IfInitMyObj = false,
            MyTranInfo = new TranInfo() {
                MyPos = MySoCharacterSetting.MyCharacterInfo.MyCharacterPoint,
                MyRot = MySoCharacterSetting.MyCharacterInfo.MyCharacterQuaternion,
            },

            IsMainCharacter = isMainCharacter,
        });
        // 如果是主角色
        if (isMainCharacter) {
            // 加载角色 UI
            gameSystem.MyUISystem.InstanceUICharacterWindow();
            // 加载地图 UI
            gameSystem.MyUISystem.InstanceUIMapWindow();
            if (GameData.MainCharacater == -1) {
                // 赋值全局 主角色
                GameData.MainCharacater = instanceId;
                // 赋值全局主角色组件
                GameData.MainCharacterComponent = gameSystem.MyGameObjFeature.Get<CharacterGameObj>(instanceId)
                    .GetData<CharacterData>().GetComponent<CharacterComponent>();
            }
        }
    }

    private int InstanceCharacter(CharacterData characterData) {
        return gameSystem.InstanceGameObj<CharacterGameObj, CharacterEntity>(characterData);
    }
}