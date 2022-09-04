public interface IWindow {
    void Init(Game game, GameObj gameObj);
    void OpenAll();
    void Update();
    void FixedUpdate();
    void LateUpdate();
    void CloseAll();
    void Clear();
}