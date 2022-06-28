using UnityEngine;

public class CharacterSystem : GameSys {
    private SOCharacterSetting soCharacterSetting;
    // private GameObjPool GOP = new GameObjPool();
    private GameSystem gameSystem;
    public override void Init(GameSystem gameSystem) {
        this.gameSystem = gameSystem;
        soCharacterSetting = Resources.Load<SOCharacterSetting>(PathData.SOCharacterSettingPath);
        InstanceCharacter();
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }

    private void InstanceCharacter() {
        gameSystem.InstanceGameObj<CharacterGameObj, CharacterEntity>(new CharacterData() {
            MyName = "cubeTest",
            MyObj = Object.Instantiate(soCharacterSetting.CharacterPrefab),
            MyTranInfo = new TranInfo() {
                MyPos = new Vector3(0,2,0),
                MyRot = new Quaternion(0, 0, 0, 0),
                MyRootTran = GameData.ItemRoot,
            }
        });

        gameSystem.InstanceGameObj<CharacterGameObj, CharacterEntity>(new CharacterData() {
            MyName = "cubeTest2",
            MyObj = Object.Instantiate(soCharacterSetting.CharacterPrefab),
            MyTranInfo = new TranInfo() {
                MyPos = new Vector3(2,0,0),
                MyRot = new Quaternion(0, 0, 0, 0),
                MyRootTran = GameData.ItemRoot,
            }
        });
    }

    // class GameObjPool {
    //     private Dictionary<int, GameObject> pools = new Dictionary<int, GameObject>();
    //     public GameObject Add(GameObject myGameObj) {
    //         pools.Add(myGameObj.GetInstanceID(), myGameObj);
    //         myGameObj.SetActive(true);
    //         return myGameObj;
    //     }
    //
    //     public GameObject Get(int id) {
    //         if (pools.TryGetValue(id, out var obj)) {
    //             return obj;
    //         }
    //
    //         return null;
    //     }
    //
    //     public void Clear(int id) {
    //         if (pools.TryGetValue(id, out var obj)) {
    //             obj.SetActive(false);
    //         }
    //     }
    //
    //     public void Destory(int id) {
    //         if (pools.TryGetValue(id, out var obj)) {
    //             Object.Destroy(obj);
    //             pools.Remove(id);
    //         }
    //     }
    // }
}