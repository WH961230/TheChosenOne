public class WeaponGameObj : GameObj {
    private WeaponComponent weaponComponent;
    private WeaponData weaponData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        weaponData = (WeaponData)data;
        MyComponent = MyObj.transform.GetComponent<WeaponComponent>();
        weaponComponent = (WeaponComponent) MyComponent;
        weaponData.MyWeaponSign = weaponComponent.MyWeaponSign;

        MyGame.MyGameMessageCenter.Register<int>(GameMessageConstants.WEAPONSYSTEM_DROP, MsgWeaponDrop);
        MyGame.MyGameMessageCenter.Register<int>(GameMessageConstants.WEAPONSYSTEM_HIDE, MsgWeaponHide);
    }

    private void MsgWeaponHide(int id) {
        if (id == weaponData.InstanceID) {
            Hide();
        }
    }

    private void MsgWeaponDrop(int id) {
        if (id == weaponData.InstanceID) {
            Display();
            Drop();
        }
    }

    public override void Drop() {
        base.Drop();
        var characterComponent = MyGame.MyGameSystem.MyCharacterSystem.GetMainCharacterComponent();
        weaponComponent.transform.position = GameData.GetGround(characterComponent.transform.position);
    }
}