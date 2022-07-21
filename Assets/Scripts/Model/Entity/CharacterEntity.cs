public class CharacterEntity : Entity {
    private CharacterData characterData;

    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.characterData = (CharacterData)data;
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }
}