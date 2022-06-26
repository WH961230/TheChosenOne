using System.Collections.Generic;
using UnityEngine;

public class GameSystem {
    private GameObjPool GOP = new GameObjPool();
    private Game game;
    public void Init(Game game) {
        this.game = game;

        // 具体实例
        Instance<CubeGameObj, CubeEntity>(new CubeData() {
            Name = "cubeTest",
            MyObj = GOP.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)),
            MyTranInfo = new TranInfo() {
                MyPos = new Vector3(0,2,0),
                MyRot = new Quaternion(0, 0, 0, 0)
            }
        });

        Instance<CubeGameObj, CubeEntity>(new CubeData() {
            Name = "cubeTest2",
            MyObj = GOP.Add(GameObject.CreatePrimitive(PrimitiveType.Sphere)),
            MyTranInfo = new TranInfo() {
                MyPos = new Vector3(2,0,0),
                MyRot = new Quaternion(0, 0, 0, 0)
            }
        });
    }

    /// <summary>
    /// 实例物体
    /// </summary>
    private void Instance<T1, T2>(Data data) where T1 : GameObj, new() where T2 : Entity, new() {
        data.InstanceID = data.MyObj.GetInstanceID();
        game.Get<GameObjFeature>().Register<T1>(data);
        game.Get<EntityFeature>().Register<T2>(data);
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