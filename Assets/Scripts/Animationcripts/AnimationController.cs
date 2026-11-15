using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private AnimationData[] animationData;
    [SerializeField] private string controllerID;

    public string Name { get { return controllerID; } }

    private void Awake()
    {
        if (animationData == null || animationData.Length <= 0) { return; }
        animationData = GetComponentsInChildren<SpellAnimationData>();
    }

    private void Start(){
    }

    public void StartAnimation(Spells spell)
    {
        foreach (SpellAnimationData animData in animationData)
        {
            if (animData.Spell == spell)
            {
                animData.StartAnimation();
                Debug.Log($"{spell.ToString()} as triggered an animation.");
                return;
            }
        }
        throw new System.Exception($"BRUH THIS CONTROLLER DONT EVEN GOT THE SPELL {spell.ToString()}");
    }
}
