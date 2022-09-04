using System.Collections.Generic;
using UnityEngine;

public class AnimatorData : Data {
    private Dictionary<int, Animator> MyAnimatorController = new Dictionary<int, Animator>();

    public bool RegisterAnimatorController(int id, Animator animator) {
        if (!MyAnimatorController.ContainsKey(id)) {
            MyAnimatorController.Add(id, animator);
            return true;
        }

        return false;
    }

    public bool TryGetAnimatorController(int id, out Animator animator) {
        if (MyAnimatorController.ContainsKey(id)) {
            animator = MyAnimatorController[id];
            return true;
        }

        animator = null;
        return false;
    }

    public void RemoveAnimatorController(int id) {
        if (MyAnimatorController.ContainsKey(id)) {
            MyAnimatorController.Remove(id);
        }
    }
}