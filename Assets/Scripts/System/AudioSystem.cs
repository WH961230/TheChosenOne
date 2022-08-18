using UnityEngine;

public class AudioSystem : GameSys {
    public void InstanceAudioMain() {
        InstanceAudioMain(new AudioMainData() {
            MyName = "AudioMain",
            MyObj = Object.Instantiate(SOData.MySOAudioMainSetting.AudioMain),
            MyRootTran = GameData.AudioRoot,
        });
    }

    private void InstanceAudioMain(AudioMainData audioMainData) {
        MyGameSystem.InstanceGameObj<AudioMainGameObj, AudioMainEntity>(audioMainData);
    }
}