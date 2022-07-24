using UnityEngine;

public class EquipmentData : Data {
    public string MySign;
    public Sprite MySprite;
    public int MyLevel;
}

public enum EquipmentType {
    Helmet,
    Armour,
    Backpack,
}