using UnityEngine;

public class ItemGameObj : GameObj {
    private ItemComponent itemComponent;
    private ItemData sceneitemData;

    public override void Init(Game game, Data data) {
        base.Init(game, data);
        sceneitemData = (ItemData) data;
        itemComponent = (ItemComponent) MyComp;
    }
}