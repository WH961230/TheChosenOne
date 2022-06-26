using UnityEngine;

public class PlayerGameObj : GameObj {
    private PlayerData playerData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        playerData = (PlayerData)data;
    }

    public override void Clear() {
        base.Clear();
    }

    public override void Update() {
        base.Update();
    }
}