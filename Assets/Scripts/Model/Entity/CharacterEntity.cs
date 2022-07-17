﻿public class CharacterEntity : Entity {
    private CharacterData characterData;
    public BackpackData MyBackpackData;

    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.characterData = (CharacterData)data;
        MyBackpackData = game.MyGameSystem.MyBackpackSystem.GetBackpackData(characterData.BackpackInstanceId);
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }

    public void AddBackpackData() {
        
    }
}