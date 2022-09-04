using UnityEngine;

public class CharacterSystem : GameSys {
    #region 创建

    public CharacterData InstanceCharacter(bool isMainCharacter) {
        CharacterData characterData = new CharacterData() {
            MyName = "Character",
            MyObj = Object.Instantiate(SoData.MySOCharacter.GetCharacterPrefab()),
            MyRootTran = GameData.CharacterRoot,
            MyTranInfo = new TranInfo() {
                MyPos = SoData.MySOCharacter.MyCharacterInfo.MyCharacterPoint, 
                MyRot = SoData.MySOCharacter.MyCharacterInfo.MyCharacterQuaternion,
            },
            CameraInstanceId = isMainCharacter ? MyGS.CameraS.InstanceCamera(CameraType.MainCharacterCamera) : 0,
            BackpackInstanceId = MyGS.BackpackS.InstanceBackpack(),
            IsMainCharacter = isMainCharacter,
        };
        InstanceCharacter(characterData);
        return characterData;
    }

    private void InstanceCharacter(CharacterData characterData) {
        MyGS.InstanceGameObj<CharacterGameObj, CharacterEntity>(characterData);
    }

    public CharacterGameObj GetGO(int id) {
        return GetGameObj<CharacterGameObj>(id);
    }
    
    public CharacterEntity GetEntity(int id) {
        return GetEntity<CharacterEntity>(id);
    }

    #endregion
}