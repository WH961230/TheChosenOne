using UnityEngine;

public class BackpackSystem : GameSys {
    #region 获取

    public BackpackGameObj GetGO(int id) {
        return GetGameObj<BackpackGameObj>(id);
    }

    public BackpackEntity GetEntity(int id) {
        return GetEntity<BackpackEntity>(id);
    }

    #endregion

    #region 创建

    public int InstanceBackpack() {
        return InstanceBackpack(new BackpackData() {
            MyName = "Backpack",
            MyObj = new GameObject("backpackObj"),
            MyRootTran = GameData.CharacterRoot,
        });
    }

    private int InstanceBackpack(BackpackData backpackData) {
        return MyGS.InstanceGameObj<BackpackGameObj, BackpackEntity>(backpackData);
    }

    #endregion
}