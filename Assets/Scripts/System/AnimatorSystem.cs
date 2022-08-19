using UnityEngine;

public class AnimatorSystem : GameSys {
    public override void Init(GameSystem gameSystem) {
        base.Init(gameSystem);
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

    #region 增

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

    #endregion

    #region 查

    public AnimatorEntity GetEntity() {
        return MyGS.EntityFeature.Get<AnimatorEntity>(GameData.AnimatorId);
    }

    #endregion
}