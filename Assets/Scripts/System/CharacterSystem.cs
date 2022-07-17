using UnityEngine;

public class CharacterSystem : GameSys {
    private GameSystem gameSystem;

    public override void Init(GameSystem gameSystem) {
        base.Init(gameSystem);
        this.gameSystem = gameSystem;
        gameSystem.MyCameraSystem.InstanceCamera(CameraType.MainCharacterCamera);
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }

    #region 获取

    public CharacterGameObj GetCharacterGameObj(int id) {
        return MyGameSystem.MyGameObjFeature.Get<CharacterGameObj>(id);
    }

    public CharacterComponent GetCharacterComponent(int id) {
        return GetCharacterGameObj(id).GetComponent<CharacterComponent>();
    }

    public CharacterEntity GetCharacterEntity(int id) {
        return MyGameSystem.MyEntityFeature.Get<CharacterEntity>(id);
    }

    public CharacterData GetCharacterData(int id) {
        return GetCharacterEntity(id).GetData<CharacterData>();
    }
    
    // 特殊获取
    public CharacterGameObj GetMainCharacterGameObj() {
        if (GameData.MainCharacater == 0) {
            return null;
        }

        return MyGameSystem.MyGameObjFeature.Get<CharacterGameObj>(GameData.MainCharacater);
    }

    public CharacterComponent GetMainCharacterComponent() {
        if (null == GetMainCharacterGameObj()) {
            return null;
        }

        return GetMainCharacterGameObj().GetComponent<CharacterComponent>();
    }

    public CharacterEntity GetMainCharacterEntity() {
        if (GameData.MainCharacater == 0) {
            return null;
        }

        return MyGameSystem.MyEntityFeature.Get<CharacterEntity>(GameData.MainCharacater);
    }

    public CharacterData GetMainCharacterData() {
        if (null == GetMainCharacterEntity()) {
            return null;
        }

        return GetMainCharacterEntity().GetData<CharacterData>();
    }

    #endregion

    #region 创建

    public void InstanceCharacter(bool isMainCharacter) {
        var backpackId = gameSystem.MyBackpackSystem.InstanceBackpack();
        var instanceId = InstanceCharacter(new CharacterData() {
            MyName = "Character",
            MyObj = Object.Instantiate(SOData.MySOCharacter.GetCharacterPrefab()),
            MyRootTran = GameData.CharacterRoot,

            IfInitMyObj = false,
            MyTranInfo = new TranInfo() {
                MyPos = SOData.MySOCharacter.MyCharacterInfo.MyCharacterPoint,
                MyRot = SOData.MySOCharacter.MyCharacterInfo.MyCharacterQuaternion,
            },
            BackpackInstanceId = backpackId, // 背包
            IsMainCharacter = isMainCharacter,
        });

        // 如果是主角色
        if (isMainCharacter) {
            if (GameData.MainCharacater == -1) {
                // 赋值全局 主角色
                GameData.MainCharacater = instanceId;
                // 赋值全局主角色组件
                GameData.MainCharacterComponent = gameSystem.MyGameObjFeature.Get<CharacterGameObj>(instanceId).GetComponent<CharacterComponent>();
            }

            // 加载角色 UI
            gameSystem.MyUISystem.InstanceUICharacterWindow();
            // 加载地图 UI
            gameSystem.MyUISystem.InstanceUIMapWindow();
            // 加载背包 UI
            gameSystem.MyUISystem.InstanceUIBackpackWindow();
            // 加载贴士 UI
            gameSystem.MyUISystem.InstanceUITipWindow();
        }
    }

    private int InstanceCharacter(CharacterData characterData) {
        return gameSystem.InstanceGameObj<CharacterGameObj, CharacterEntity>(characterData);
    }

    #endregion
}