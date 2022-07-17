using UnityEngine;

public class BackpackSystem : GameSys {
    public override void Init(GameSystem gameSystem) {
        base.Init(gameSystem);
        MyGameSystem.MyGameMessageCenter.Register<int>(GameMessageConstants.BACKPACKSYSTEM_ADDSCENEITEM, MsgAddSceneItem);
        MyGameSystem.MyGameMessageCenter.Register<int>(GameMessageConstants.BACKPACKSYSTEM_ADDWEAPON, MsgAddSceneItem);
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

    private void MsgAddWeapon(int weaponId) {
        // 添加物体到背包
        AddWeapon(weaponId);
        //  隐藏游戏场景物体
        MyGameSystem.MyWeaponSystem.GetWeaponGameObj(weaponId).Hide();
    }

    private void AddWeapon(int weaponId) {
        
    }

    private void MsgAddSceneItem(int sceneItemId) {
        // 添加物体到背包
        AddSceneItem(sceneItemId);
        
    }

    // 将物品放入背包数据中
    private void AddSceneItem(int sceneItemId) {
        // 获取主角背包实体
        var backpackEntity = GetBackpackEntity(MyGameSystem.MyCharacterSystem.GetMainCharacterData().BackpackInstanceId);
        // 拾取到背包
        if (backpackEntity.PickSceneItem(sceneItemId)) {
            LogSystem.Print($"拾取物品：{sceneItemId}");
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