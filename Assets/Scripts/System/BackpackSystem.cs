using UnityEngine;

public class BackpackSystem : GameSys {
    private BackpackEntity myBackpackEntity;

    private BackpackEntity MyBackpackEntity {
        get {
            if (null == myBackpackEntity) {
                myBackpackEntity = GetBackpackEntity(MyGameSystem.MyCharacterSystem.GetMainCharacterData().BackpackInstanceId);
            }

            return myBackpackEntity;
        }
    }
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

    #region 添加物品

    private void MsgAdd(int id, int layer) {
        bool isSuc = false;
        switch (layer) {
            case 9:
                isSuc = AddSceneItem(id);
                break;
            case 12:
                isSuc = AddWeapon(id);
                break;
            case 13:
                isSuc = AddEquipment(id);
                break;
        }
    }

    // 将物品放入背包数据中
    private bool AddSceneItem(int id) {
        if (MyBackpackEntity.PickSceneItem(id)) {
            LogSystem.Print($"拾取物品：{id}");
            MyGameSystem.MyGameMessageCenter.Dispather(GameMessageConstants.SCENEITEMSYSTEM_HIDE, id);
            return true;
        }

        return false;
    }

    private bool AddWeapon(int id) {
        if (MyBackpackEntity.PickWeapon(id)) {
            LogSystem.Print($"拾取武器：{id}");
            MyGameSystem.MyGameMessageCenter.Dispather(GameMessageConstants.WEAPONSYSTEM_HIDE, id);
            return true;
        }

        return false;
    }

    private bool AddEquipment(int id) {
        if (MyBackpackEntity.PickEquipment(id)) {
            LogSystem.Print($"拾取装备：{id}");
            MyGameSystem.MyGameMessageCenter.Dispather(GameMessageConstants.EQUIPMENTSYSTEM_HIDE, id);
            return true;
        }

        return false;
    }

    #endregion

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