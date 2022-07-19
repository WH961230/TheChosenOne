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

    public bool PickSceneItem(int id) {
        // 判断物品的类型 物品 武器 
        var type = MyGame.MyGameSystem.MyItemSystem.GetSceneItemType(id);
        if (AddSceneItem(type, id)) {
            return true;
        }

        return false;
    }

    public bool PickWeapon(int id) {
        var type = MyGame.MyGameSystem.MyWeaponSystem.GetWeaponType(id);
        if (AddWeapon(type, id)) {
            return true;
        }

        return false;
    }

    private bool AddSceneItem(SceneItemType type, int sceneItemId) {
        switch (type) {
            case SceneItemType.Consume:
                // 判断物体 进行叠加
                backpackData.MySceneItemConsumeIds.Add(sceneItemId);
                return true;
            case SceneItemType.Equipment:
                
                backpackData.MySceneItemEquipmentIds[0] = sceneItemId;
                return true;
        }

        return false;
    }

    private bool AddWeapon(WeaponType type, int id) {
        switch (type) {
            case WeaponType.MainWeapon:
                backpackData.MySceneItemMainWeaponIds[0] = id;
                return true;
            case WeaponType.SideWeapon:
                backpackData.MySceneItemSideWeaponId = id;
                return true;
        }

        return false;
    }
}