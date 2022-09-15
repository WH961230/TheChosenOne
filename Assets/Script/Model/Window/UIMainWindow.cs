using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIMainWindow : Window {
    private UIMainGameObj mainGO;
    private UIMainComponent mainComp;
    private Button.ButtonClickedEvent btnClickEvent;

    public override void Init(Game game, GameObj gameObj) {
        base.Init(game, gameObj);
        // 初始化界面
        mainGO = (UIMainGameObj)gameObj;
        mainComp = mainGO.GetComp();

        // 加载相机
        MyGS.Get<CameraSystem>().InstanceCamera(CameraType.MainCamera);

        // 取消鼠标可视化
        Cursor.visible = false;

        // 默认界面开启
        OpenAll();

        btnClickEvent = mainComp.MyButton.onClick;

        // 注册事件
        RegisterEvent(btnClickEvent, MainBtnAction);

        // 调用事件
        Invoke(btnClickEvent);
    }

    private void MainBtnAction() {
        // 加载 DebugUI
        MyGS.Get<UISystem>().InstanceUIDebugToolWindow();

        // 动画状态机系统
        if (GameData.AnimatorId == 0) {
            GameData.AnimatorId = MyGS.Get<AnimatorSystem>().InstanceAnimator().InstanceID;
        }

        // 加载灯光
        MyGS.Get<EnvironmentSystem>().InstanceLight();

        // 加载建筑、地面bb
        MyGS.Get<EnvironmentSystem>().InstanceMapBuilding();

        // 加载场景可拾取物体
        MyGS.Get<ItemSystem>().InstanceMapItem();

        // 读取玩家生成点 创建主玩家
        if (GameData.MainCharacterId == 0) {
            GameData.MainCharacterId = MyGS.Get<CharacterSystem>().InstanceCharacter(true).InstanceID;
        }

        // 加载角色 UI
        MyGS.Get<UISystem>().InstanceUICharacterWindow();

        // 加载地图 UI
        MyGS.Get<UISystem>().InstanceUIMapWindow();

        // 加载背包 UI
        MyGS.Get<UISystem>().InstanceUIBackpackWindow();

        // 加载贴士 UI
        MyGS.Get<UISystem>().InstanceUITipWindow();

        // 播放背景音效（替代资源）
        MyGS.Get<AudioSystem>().InstanceAudioMain();

        // 关闭界面
        CloseAll();
    }

    public override void OpenAll() {
        mainGO.Display();
    }

    public override void Update() {
    }

    public override void CloseAll() {
        mainGO.Hide();
    }

    public override void Clear() {
        UnRegisterEvent(btnClickEvent, MainBtnAction);
    }
}