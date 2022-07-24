using UnityEngine;

public class BackpackSystem : GameSys {
    public override void Init(GameSystem gameSystem) {
        base.Init(gameSystem);
    }

    public override void Update() {
        base.Update();
    }

    public override void FixedUpdate() {
        base.FixedUpdate();
    }

    public override void Clear() {
        base.Clear();
    }

    public override void LateUpdate() {
        base.LateUpdate();
    }

    #region 获取

    public BackpackGameObj GetBackpackGameObj(int id) {
        return MyGameSystem.MyGameObjFeature.Get<BackpackGameObj>(id);
    }

    public BackpackComponent GetBackpackComponent(int id) {
        return GetBackpackGameObj(id).GetComponent<BackpackComponent>();
    }

    public BackpackEntity GetBackpackEntity(int id) {
        return MyGameSystem.MyEntityFeature.Get<BackpackEntity>(id);
    }

    #endregion

    #region 创建

    public int InstanceBackpack() {
        return InstanceBackpack(new BackpackData() {
            MyName = "Character",
            MyObj = new GameObject("backpackObj"),
            MyRootTran = GameData.CharacterRoot,
            IfInitMyObj = false,
        });
    }

    private int InstanceBackpack(BackpackData backpackData) {
        return MyGameSystem.InstanceGameObj<BackpackGameObj, BackpackEntity>(backpackData);
    }

    #endregion
}