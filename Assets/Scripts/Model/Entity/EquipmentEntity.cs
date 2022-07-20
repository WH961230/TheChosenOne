public class EquipmentEntity : Entity {
    private EquipmentData equipmentData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.equipmentData = (EquipmentData)data;
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }
}