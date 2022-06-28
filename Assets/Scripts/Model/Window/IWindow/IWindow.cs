public interface IWindow {
    void Init(Game game, Data data);
    void Open();
    void Update();
    void Close();
    void Clear();
}