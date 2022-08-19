using UnityEngine;

public class EquipmentEntity : Entity {
    private EquipmentData equipmentData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.equipmentData = (EquipmentData)data;
    }

    public EquipmentData GetData() {
        return base.GetData() as EquipmentData;
    }
}