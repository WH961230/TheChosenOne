using UnityEngine;

public class Data : IData {
    // 基本参数
    public string MyName;
    public string Sign;
    public GameObject MyObj; // 使用资源池的方式加载
    private MonoBehaviour MyComponent; // 组件
    public Transform MyRootTran; // 父物体
    public TranInfo MyTranInfo;
    
    // 物体参数
    public bool IsActive = true; // 默认激活

    // id
    public int InstanceID = -1;
}

public struct TranInfo {
    public Vector3 MyPos; // 位置
    public Quaternion MyRot; // 方向
}