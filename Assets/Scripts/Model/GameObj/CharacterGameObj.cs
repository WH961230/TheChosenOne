public class CharacterGameObj : GameObj {
    private CharacterData characterData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        characterData = (CharacterData)data;
    }

    public override void Clear() {
        base.Clear();
    }

    public override void Update() {
        base.Update();
    }
}