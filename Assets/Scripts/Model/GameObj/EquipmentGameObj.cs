public class EquipmentGameObj : GameObj {
    private EquipmentComponent equipmentComponent;
    private EquipmentData equipmentData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        equipmentData = (EquipmentData)data;
        equipmentComponent = (EquipmentComponent) MyComponent;
        equipmentData.MySign = equipmentComponent.MyEquipmentSign;
        equipmentData.MyLevel = equipmentComponent.MyEquipmentLevel;
    }

    public override void Clear() {
        base.Clear();
    }

    public override void Update() {
        base.Update();
    }
}