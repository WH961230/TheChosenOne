﻿public class BuildingGameObj : GameObj {
    private BuildingData scenebuildingData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        scenebuildingData = (BuildingData)data;
    }
}