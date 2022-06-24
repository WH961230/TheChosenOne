using UnityEngine;

public class Data : IData{
    // 基本参数
    public string Name;
    public GameObject MyObj;// 使用资源池的方式加载
    public int InstanceID = -1;// 从0开始算第一个
}