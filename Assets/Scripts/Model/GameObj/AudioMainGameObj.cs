public class AudioMainGameObj : GameObj {
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
        audioMainComponent = MyObj.transform.GetComponent<AudioMainComponent>();
        audioMainComponent.AudioSource.PlayOneShot(SOData.MySOAudioMainSetting.BackMusic);
    }
}