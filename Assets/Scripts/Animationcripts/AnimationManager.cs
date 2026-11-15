using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls all animations in the scene, being able to trigger them when another script calls
/// </summary>
public class AnimationManager : MonoBehaviour
{
    public static AnimationManager Instance;

    [SerializeField]
    private Dictionary<string, AnimationData> animationObjects =
        new Dictionary<string, AnimationData>();

    [SerializeField]
    private Dictionary<string, AnimationController> animationControllers =
        new Dictionary<string, AnimationController>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddAnimation(AnimationData anim)
    {
        animationObjects.Add(anim.Name, anim);
    }

    public void AddAnimation(AnimationController animController)
    {
        animationControllers.Add(animController.Name, animController);
    }

    public bool TriggerAnimation(string animationKey)
    {
        if (animationObjects.ContainsKey(animationKey))
        {
            animationObjects[animationKey].StartAnimation();
            return true;
        }
        return false;
    }

    public bool TriggerAnimation(string animationKey, Action onComplete)
    {
        if (animationObjects.TryGetValue(animationKey, out var anim))
        {
            anim.StartAnimation(onComplete, null);
            return true;
        }
        return false;
    }

    public bool TriggerAnimation(string animationKey, Spells spell)
    {
        if (animationControllers.ContainsKey(animationKey))
        {
            animationControllers[animationKey].StartAnimation(spell);
            return true;
        }
        return false;
    }

    public bool HasAnimation(string animationKey) { return animationObjects.ContainsKey(animationKey); }
}
