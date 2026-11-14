using UnityEngine;

public class InstantAnimationData : AnimationData
{
    private void Start()
    { 
        AnimationManager.Instance.AddAnimation(this);
        StartAnimation();
    }
}
