using UnityEngine;

public class AudioSystem : GameSys {
    public void InstanceAudioMain() {
        InstanceAudioMain(new AudioMainData() {
            MyName = "AudioMain",
            MyObj = Object.Instantiate(SoData.MySOAudioMainSetting.AudioMain),
            MyRootTran = GameData.AudioRoot,
        });
    }

    private void InstanceAudioMain(AudioMainData audioMainData) {
        MyGS.InstanceGameObj<AudioMainGameObj, AudioMainEntity>(audioMainData);
    }
}