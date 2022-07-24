using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIDebugToolComponent : MonoBehaviour {
    
    //=====================全局========================
    // Debug 工具按钮
    public Button MyDebugToolBtn;

    // 关闭
    public Button MyDebugToolCloseBtn;
    
    // 侧边导航按钮页面
    public GameObject MyDebugToolSideBtnWin;
    
    //=====================功能========================
    
    // 功能界面选择按钮
    public Button MyDebugToolFunctionSelectBtn;

    // 功能界面
    public GameObject MyDebugToolFunctionWin;

    // 创建角色
    public Button MyDebugToolCreateCharacterBtn;

    // 创建 cube 在脚下
    public Button MyDebugToolCreateCubeAtGround;

    //=====================调试========================
    
    // 调试界面选择按钮
    public Button MyDebugToolConsoleSelectBtn;
    
    // 调试界面
    public GameObject MyConsoleWin;
}