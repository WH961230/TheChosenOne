using UnityEngine;

public class EntityFeature : IFeature {
    private EntityManager entityManager = new EntityManager();
    private Game game;

    public void Init(Game game) {
        this.game = game;
    }

    public void Clear() {
        entityManager.RemoveAll();
    }

    public void Update() {
        entityManager.Update();
    }

    // 实体注册
    public void Register<T>(Data data) where T : Entity, new() {
        Debug.Log($"注册 Entity => data.Name: {data.MyName}");
        entityManager.Register<T>(game, data);
    }

    public void Remove(int id) {
        entityManager.Remove(id);
    }

    public void Remove(Entity entity) {
        entityManager.Remove(entity);
    }

    public Entity Get(int id) {
        return entityManager.Get(id);
    }
}