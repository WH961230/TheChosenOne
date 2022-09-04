public static class LayerData {
    public const int CharacterLayer = 8;
    public const int CharacterLayerMask = 1 << 8;
    public const int SceneBuildingLayer = 10;
    public const int SceneBuildingLayerMask = 1 << 10;
    public const int VehicleLayer = 11;
    public const int VehicleLayerMask = 1 << 11;
    public const int WeaponLayer = 12;
    public const int WeaponLayerMask = 1 << 12;
    public const int EquipmentLayer = 13;
    public const int EquipmentLayerMask = 1 << 13;
    public const int ConsumeLayer = 14;
    public const int ConsumeLayerMask = 1 << 14;

    #region Custom

    public const int PickItemLayerMask = WeaponLayerMask | EquipmentLayerMask | ConsumeLayerMask;

    #endregion
}