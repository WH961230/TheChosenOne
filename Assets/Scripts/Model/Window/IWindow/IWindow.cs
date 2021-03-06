public interface IWindow {
    void Init(Game game, Data data);
    void Open();
    void Update();
    void FixedUpdate();
    void LateUpdate();
    void Close();
    void Clear();
}