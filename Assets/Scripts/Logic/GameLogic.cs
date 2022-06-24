using System.Collections.Generic;
using UnityEngine;

public class GameLogic {
    private GameObjPool GOP = new GameObjPool();
    public void Init(Game game) {
        var cubeData = new CubeData() {
            Name = "cubeTest",
            MyObj = GOP.Add(new GameObject("cubeTest")),// 游戏物体使用资源池的方式加载保存
        }; 
        // 创建一个 cube
        game.Get<GameObjFeature>().Register<CubeGameObj>(cubeData);
        game.Get<EntityFeature>().Register<CubeEntity>(cubeData);
    }

    class GameObjPool {
        private Dictionary<int, GameObject> AllGameObject = new Dictionary<int, GameObject>();
        public GameObject Add(GameObject myGameObj) {
            AllGameObject.Add(myGameObj.GetInstanceID(), myGameObj);
            myGameObj.SetActive(true);
            return myGameObj;
        }

        public GameObject Get(int id) {
            if (AllGameObject.TryGetValue(id, out var obj)) {
                return obj;
            }

            return null;
        }

        public void Clear(int id) {
            if (AllGameObject.TryGetValue(id, out var obj)) {
                obj.SetActive(false);
            }
        }

        public void Destory(int id) {
            if (AllGameObject.TryGetValue(id, out var obj)) {
                Object.Destroy(obj);
                AllGameObject.Remove(id);
            }
        }
    }
}