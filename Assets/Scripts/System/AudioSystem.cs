using UnityEngine;

public class AudioSystem : GameSys {
    private GameSystem gameSystem;

    public override void Init(GameSystem gameSystem) {
        base.Init(gameSystem);
        this.gameSystem = gameSystem;
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
            MyObj = Object.Instantiate(SOData.MySOAudioMainSetting.AudioMain),
            MyRootTran = GameData.AudioRoot,
        });
    }

    private void InstanceAudioMain(AudioMainData audioMainData) {
        gameSystem.InstanceGameObj<AudioMainGameObj, AudioMainEntity>(audioMainData);
    }
}