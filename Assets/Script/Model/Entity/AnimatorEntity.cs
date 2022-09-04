using UnityEngine;

public class AnimatorEntity : Entity {
    private AnimatorData animatorData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.animatorData = (AnimatorData)data;
    }

    public bool RegisterAnimator(int id, Animator animator) {
        return animatorData.RegisterAnimatorController(id, animator);
    }
}