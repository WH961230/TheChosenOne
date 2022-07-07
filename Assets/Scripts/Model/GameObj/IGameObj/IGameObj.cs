public interface IGameObj {
    void Init(Game game, Data data);
    void Update();
    void Clear();
    T GetData<T>() where T : Data ;
}