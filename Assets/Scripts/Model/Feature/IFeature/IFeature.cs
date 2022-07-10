public interface IFeature {
    void Init(Game game);
    void Update();
    void FixedUpdate();
    void LateUpdate();
    void Clear();
}