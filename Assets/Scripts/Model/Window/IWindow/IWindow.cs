public interface IWindow {
    void Init(Game game, GameObj gameObj);
    void Open();
    void Update();
    void FixedUpdate();
    void LateUpdate();
    void Close();
    void Clear();
}