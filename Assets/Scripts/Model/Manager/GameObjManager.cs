using System.Collections.Generic;

public class GameObjManager : IManager {
    private Dictionary<int, GameObj> gameObjDict = new Dictionary<int, GameObj>();
    private List<GameObj> gameObjs = new List<GameObj>();

    public void Register<T>(Game game, Data data) where T : GameObj, new() {
        GameObj instance = new T();
        gameObjDict.Add(data.InstanceID, instance);
        gameObjs.Add(instance);
        instance.Init(game, data);
    }

    public void Remove(int id) {
        int index = Find(id);
        if (index != -1) {
            GameObj instance = gameObjs[index];
            gameObjDict.Remove(id);
            gameObjs.RemoveAt(index);
            instance.Clear();
        }
    }

    public void Remove(GameObj entity) {
        int index = Find(entity);
        if (index != -1) {
            gameObjDict.Remove(entity.MyData.InstanceID);
            gameObjs.RemoveAt(index);
            entity.Clear();
        }
    }

    public void RemoveAll() {
        for (int i = 0; i < gameObjs.Count; i++) {
            gameObjs[i].Clear();
        }

        gameObjs.Clear();
        gameObjDict.Clear();
    }

    private int Find(int id) {
        if (gameObjDict.TryGetValue(id, out GameObj ret)) {
            int count = gameObjs.Count;
            for (int i = 0; i < count; i++) {
                if(gameObjs[i] == ret) {
                    return i;
                }
            }
        }
        
        return -1;
    }

    private int Find(GameObj gameObj) {
        int count = gameObjs.Count;
        for (int i = 0; i < count; i++) {
            if (gameObjs[i] == gameObj) {
                return i;
            }
        }
        return -1;
    }

    public GameObj Get(int id) {
        if (gameObjDict.TryGetValue(id, out GameObj ret)) {
            return ret;
        }

        return null;
    }

    public void Update() {
        for (int i = 0; i < gameObjs.Count; i++) {
            gameObjs[i].Update();
        }
    }
}