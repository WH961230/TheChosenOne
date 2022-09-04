public class EquipmentGameObj : GameObj {
    private EquipmentComponent equipmentComponent;
    private EquipmentData equipmentData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        equipmentData = (EquipmentData)data;
        equipmentComponent = (EquipmentComponent) Comp;
        equipmentData.MySign = equipmentComponent.MySign;
        equipmentData.MyLevel = equipmentComponent.MyEquipmentLevel;
    }

    public EquipmentComponent GetComp() {
        return base.GetComp() as EquipmentComponent;
    }
}