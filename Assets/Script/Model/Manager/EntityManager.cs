using System.Collections.Generic;

public class EntityManager : IManager {
    private Dictionary<int, Entity> entityDict = new Dictionary<int, Entity>();
    private List<Entity> entitys = new List<Entity>();

    public void Register<T>(Game game, Data data) where T : Entity, new() {
        Entity entity = new T();
        entityDict.Add(data.InstanceID, entity);
        entitys.Add(entity);
        entity.Init(game, data);
    }

    public void Remove(int id) {
        int index = Find(id);
        if (index != -1) {
            Entity instance = entitys[index];
            entityDict.Remove(id);
            entitys.RemoveAt(index);
            instance.Clear();
        }
    }

    public void Remove(Entity entity) {
        int index = Find(entity);
        if (index != -1) {
            entityDict.Remove(entity.MyData.InstanceID);
            entitys.RemoveAt(index);
            entity.Clear();
        }
    }

    public void RemoveAll() {
        for (int i = 0; i < entitys.Count; i++) {
            entitys[i].Clear();
        }

        entitys.Clear();
        entityDict.Clear();
    }

    private int Find(int id) {
        if (entityDict.TryGetValue(id, out Entity ret)) {
            int count = entitys.Count;
            for (int i = 0; i < count; i++) {
                if(entitys[i] == ret) {
                    return i;
                }
            }
        }
        
        return -1;
    }

    private int Find(Entity entity) {
        int count = entitys.Count;
        for (int i = 0; i < count; i++) {
            if (entitys[i] == entity) {
                return i;
            }
        }
        return -1;
    }

    public Entity Get(int id) {
        if (entityDict.TryGetValue(id, out Entity ret)) {
            return ret;
        }

        return null;
    }

    public void Update() {
        for (int i = 0; i < entitys.Count; i++) {
            entitys[i].Update();
        }
    }

    public void FixedUpdate() {
        for (int i = 0; i < entitys.Count; i++) {
            entitys[i].FixedUpdate();
        }
    }

    public void LateUpdate() {
        for (int i = 0; i < entitys.Count; i++) {
            entitys[i].LateUpdate();
        }
    }
}