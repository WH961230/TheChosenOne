using UnityEngine;

public class AnimatorSystem : GameSys {
    public AnimatorData InstanceAnimator() {
        AnimatorData animatorData = new AnimatorData() {
            MyName = "Animator", 
            MyObj = new GameObject("Animator"), 
            MyRootTran = GameData.EntityRoot, 
            IsActive = false,
        };

        InstanceAnimator(animatorData);
        return animatorData;
    }

    private void InstanceAnimator(AnimatorData data) {
        MyGS.InstanceEntity<AnimatorEntity>(data);
    }

    public AnimatorEntity GetEntity() {
        return MyGS.EntityFeature.Get<AnimatorEntity>(GameData.AnimatorId);
    }
}