using UnityEngine;

public class BackpackSystem : GameSys {
    public override void Init(GameSystem gameSystem) {
        base.Init(gameSystem);
        MyGameSystem.MyGameMessageCenter.Register<int, int>(GameMessageConstants.BACKPACKSYSTEM_ADD, MsgAdd);
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

    private void MsgAdd(int id, int layer) {
        switch (layer) {
            case 9:
                AddSceneItem(id);
                break;
            case 12:
                AddWeapon(id);
                break;
        }
    }

    // 将物品放入背包数据中
    private void AddSceneItem(int id) {
        var backpackEntity = GetBackpackEntity(MyGameSystem.MyCharacterSystem.GetMainCharacterData().BackpackInstanceId);
        if (backpackEntity.PickSceneItem(id)) {
            LogSystem.Print($"拾取物品：{id}");
            MyGameSystem.MyItemSystem.GetSceneItemGameObj(id).Hide();
        }
    }

    private void AddWeapon(int id) {
        var backpackEntity = GetBackpackEntity(MyGameSystem.MyCharacterSystem.GetMainCharacterData().BackpackInstanceId);
        if (backpackEntity.PickWeapon(id)) {
            LogSystem.Print($"拾取武器：{id}");
            MyGameSystem.MyWeaponSystem.GetWeaponGameObj(id).Hide();
        }
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

    public BackpackData GetBackpackData(int id) {
        return GetBackpackEntity(id).GetData<BackpackData>();
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