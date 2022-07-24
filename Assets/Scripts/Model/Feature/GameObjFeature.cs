using UnityEngine;

public class GameObjFeature : IFeature {
    private GameObjManager gameObjManager = new GameObjManager();
    private Game game;

    public void Init(Game game) {
        this.game = game;
    }

    public void Update() {
        gameObjManager.Update();
    }

    public void FixedUpdate() {
        gameObjManager.FixedUpdate();
    }

    public virtual void LateUpdate() {
        gameObjManager.LateUpdate();
    }

    public void Clear() {
        gameObjManager.RemoveAll();
    }

    // 物体注册 自动注入 实体注册
    public void Register<T>(Data data) where T : GameObj, new() {
        // LogSystem.Print($"注册 GameObj => data.Name: {data.MyName}");
        gameObjManager.Register<T>(game, data);
    }

    public void Remove(int id) {
        gameObjManager.Remove(id);
    }

    public void Remove(GameObj gameObj) {
        gameObjManager.Remove(gameObj);
    }

    public T Get<T>(int id) where T : GameObj, new() {
        return (T) gameObjManager.Get(id);
    }
}