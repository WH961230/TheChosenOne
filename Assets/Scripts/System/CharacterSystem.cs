using UnityEngine;

public class CharacterSystem : GameSys {
    private SOCharacterSetting soCharacterSetting;
    public SOCharacterSetting MySoCharacterSetting {
        get {
            return soCharacterSetting;
        }
    }

    private GameSystem gameSystem;
    public override void Init(GameSystem gameSystem) {
        this.gameSystem = gameSystem;
        soCharacterSetting = Resources.Load<SOCharacterSetting>(PathData.SOCharacterSettingPath);
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }

    public void InstanceCharacter(CharacterData characterData) {
        gameSystem.InstanceGameObj<CharacterGameObj, CharacterEntity>(characterData);
    }
}