using UnityEngine;

public class Game : MonoBehaviour {
    private GameManager gameManager = new GameManager();
    void Start() {
        Register<WindowFeature>();
        Register<GameObjFeature>();
        Register<EntityFeature>();
    }

    void Clear() {
        Remove<WindowFeature>();
        Remove<GameObjFeature>();
        Remove<EntityFeature>();
    }

    void Update() {
        gameManager.Update();
    }

    private void Register<T>() where T : IFeature, new() {
        gameManager.Register<T>(this);
    }

    private void Remove<T>() where T : IFeature, new() {
        gameManager.Remove<T>();
    }

    public T Get<T>() where T : IFeature {
        return gameManager.Get<T>();
    }
}