public interface IGameObj {
    void Init(Game game, Data data);
    void Update();
    void FixedUpdate();
    void LateUpdate();
    void Clear();
    T GetData<T>() where T : Data ;
}