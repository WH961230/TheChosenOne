﻿using UnityEngine;

public class CharacterSystem : GameSys {

    public override void Init(GameSystem gameSystem) {
        base.Init(gameSystem);
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
        return GetCharacterGameObj(id).GetComp<CharacterComponent>();
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

        return GetMainCharacterGameObj().GetComp<CharacterComponent>();
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

    public CharacterData InstanceCharacter(bool isMainCharacter) {
        CharacterData characterData = new CharacterData() {
            MyName = "Character",
            MyObj = Object.Instantiate(SOData.MySOCharacter.GetCharacterPrefab()),
            MyRootTran = GameData.CharacterRoot,
            MyTranInfo = new TranInfo() {
                MyPos = SOData.MySOCharacter.MyCharacterInfo.MyCharacterPoint, 
                MyRot = SOData.MySOCharacter.MyCharacterInfo.MyCharacterQuaternion,
            },
            CameraInstanceId = isMainCharacter ? MyGameSystem.MyCameraSystem.InstanceCamera(CameraType.MainCharacterCamera) : 0,
            BackpackInstanceId = MyGameSystem.MyBackpackSystem.InstanceBackpack(),
            IsMainCharacter = isMainCharacter,
        };
        InstanceCharacter(characterData);
        return characterData;
    }

    private void InstanceCharacter(CharacterData characterData) {
        MyGameSystem.InstanceGameObj<CharacterGameObj, CharacterEntity>(characterData);
    }

    #endregion

    #region 移除

    

    #endregion
}