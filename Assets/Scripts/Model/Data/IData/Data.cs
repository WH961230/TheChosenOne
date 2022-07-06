using UnityEngine;

public class Data : IData{
    // 基本参数
    public string MyName;
    public Transform MyRootTran; // 父物体
    public DataType MyType;
    public GameObject MyObj; // 使用资源池的方式加载

    public bool IfInitMyObj = true; // 是否加载期间使用初始化位置信息 默认使用
    public TranInfo MyTranInfo;

    public int InstanceID = -1; // 从0开始算第一个
}

public struct TranInfo {
    public Vector3 MyPos; // 位置
    public Quaternion MyRot; // 方向
}

public struct DataType {
    public bool IsWindowPrefab; // 是否是界面物体
}