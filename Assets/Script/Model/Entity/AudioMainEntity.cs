public class AudioMainEntity : Entity {
    private AudioMainData audiomainData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.audiomainData = (AudioMainData)data;
    }
}