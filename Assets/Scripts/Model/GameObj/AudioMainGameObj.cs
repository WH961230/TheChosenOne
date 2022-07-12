public class AudioMainGameObj : GameObj {
    private SOAudioMainSetting soAudioMainSetting;

    public SOAudioMainSetting SoAudioMainSetting {
        get { return soAudioMainSetting; }
    }

    private AudioMainComponent audioMainComponent;
    private AudioMainData audiomainData;

    private AudioSystem MyAudioSystem {
        get { return game.MyGameSystem.MyAudioSystem; }
    }

    private Game game;

    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.game = game;
        audiomainData = (AudioMainData) data;
        soAudioMainSetting = MyAudioSystem.MySoAudioMainSetting;
        audioMainComponent = MyObj.transform.GetComponent<AudioMainComponent>();
        audioMainComponent.AudioSource.PlayOneShot(soAudioMainSetting.BackMusic);
    }

    public override void Clear() {
        base.Clear();
    }

    public override void Update() {
        base.Update();
    }
}