public class BackpackEntity : Entity {
    private BackpackData backpackData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.backpackData = (BackpackData)data;
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }

    /// <summary>
    /// 获取场景物体到背包 1、消耗品 2、武器 3、
    /// </summary>
    public bool PickSceneItem(int sceneItemId) {
        // 判断物品的类型 物品 武器 
        var type = MyGame.MyGameSystem.MyItemSystem.GetSceneItemType(sceneItemId);
        if (AddSceneItem(type, sceneItemId)) {
            return true;
        }

        return false;
    }

    private bool AddSceneItem(SceneItemType type, int sceneItemId) {
        switch (type) {
            case SceneItemType.Consume:
                backpackData.MySceneItemConsumeIds[0] = sceneItemId;
                return true;
            case SceneItemType.Equipment:
                backpackData.MySceneItemEquipmentIds[0] = sceneItemId;
                return true;
        }

        return false;
    }
}