using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls all animations in the scene, being able to trigger them when another script calls
/// </summary>
public class AnimationManager : MonoBehaviour
{
    public static AnimationManager Instance;

    [SerializeField] private Dictionary<string, AnimationData> animationObjects = new Dictionary<string, AnimationData>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddAnimation(AnimationData anim)
    {
        animationObjects.Add(anim.Name, anim);
    }

    public bool TriggerAnimation(string animationKey)
    {
        Debug.Log(animationObjects.Count);
        if (animationObjects.ContainsKey(animationKey))
        {
            animationObjects[animationKey].StartAnimation();
            return true;
        }
        return false;
    }
}
