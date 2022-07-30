using UnityEngine;

public class CharacterSystem : GameSys {
    private GameSystem gameSystem;

    public override void Init(GameSystem gameSystem) {
        base.Init(gameSystem);
        this.gameSystem = gameSystem;
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
        if (GameData.MainCharacterId == 0) {
            return null;
        }

        return MyGameSystem.MyGameObjFeature.Get<CharacterGameObj>(GameData.MainCharacterId);
    }

    public CharacterComponent GetMainCharacterComponent() {
        if (null == GetMainCharacterGameObj()) {
            return null;
        }

        return GetMainCharacterGameObj().GetComponent<CharacterComponent>();
    }

    public CharacterEntity GetMainCharacterEntity() {
        if (GameData.MainCharacterId == 0) {
            return null;
        }

        return MyGameSystem.MyEntityFeature.Get<CharacterEntity>(GameData.MainCharacterId);
    }

    public CharacterData GetMainCharacterData() {
        if (null == GetMainCharacterEntity()) {
            return null;
        }

        return GetMainCharacterEntity().GetData<CharacterData>();
    }

    #endregion

    #region 创建

    public int InstanceCharacter(bool isMainCharacter) {
        var backpackId = gameSystem.MyBackpackSystem.InstanceBackpack();
        var characterCameraId = gameSystem.MyCameraSystem.InstanceCamera(CameraType.MainCharacterCamera);
        return InstanceCharacter(new CharacterData() {
            MyName = "Character",
            MyObj = Object.Instantiate(SOData.MySOCharacter.GetCharacterPrefab()),
            MyRootTran = GameData.CharacterRoot,
            IfInitMyObj = false,
            MyTranInfo = new TranInfo() {
                MyPos = SOData.MySOCharacter.MyCharacterInfo.MyCharacterPoint, MyRot = SOData.MySOCharacter.MyCharacterInfo.MyCharacterQuaternion,
            },
            CameraInstanceId = characterCameraId,
            BackpackInstanceId = backpackId, // 背包
            IsMainCharacter = isMainCharacter,
        });
    }

    private int InstanceCharacter(CharacterData characterData) {
        return gameSystem.InstanceGameObj<CharacterGameObj, CharacterEntity>(characterData);
    }

    #endregion

    #region 移除

    

    #endregion
}