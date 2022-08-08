using UnityEngine;

public class ItemGameObj : GameObj {
    private ItemComponent itemComponent;
    private ItemData sceneitemData;

    public override void Init(Game game, Data data) {
        base.Init(game, data);
        sceneitemData = (ItemData) data;
        MyComponent = MyObj.transform.GetComponent<ItemComponent>();
        itemComponent = (ItemComponent) MyComponent;
    }

    public override void Hide() {
        base.Hide();
    }

    public override void Clear() {
        base.Clear();
    }

    public override void Update() {
        base.Update();
    }
}