using UnityEngine;

public class AudioSystem : GameSys {
    private SOAudioMainSetting soAudioMainSetting;

    public SOAudioMainSetting MySoAudioMainSetting {
        get { return soAudioMainSetting; }
    }

    private GameSystem gameSystem;

    public override void Init(GameSystem gameSystem) {
        base.Init(gameSystem);
        this.gameSystem = gameSystem;
        soAudioMainSetting = Resources.Load<SOAudioMainSetting>(PathData.SOAudioMainSettingPath);
    }

    public override void Update() {
        base.Update();
    }

    public override void FixedUpdate() {
        base.FixedUpdate();
    }

    public override void Clear() {
        base.Clear();
    }

    public override void LateUpdate() {
        base.LateUpdate();
    }

    public void InstanceAudioMain() {
        InstanceAudioMain(new AudioMainData() {
            MyName = "AudioMain",
            MyObj = Object.Instantiate(soAudioMainSetting.AudioMain),
            MyRootTran = GameData.AudioRoot,
        });
    }

    private void InstanceAudioMain(AudioMainData audioMainData) {
        gameSystem.InstanceGameObj<AudioMainGameObj, AudioMainEntity>(audioMainData);
    }
}