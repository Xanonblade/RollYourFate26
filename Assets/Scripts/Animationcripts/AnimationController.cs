using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private ISpellAnimation[] spellAnimations;
    [SerializeField] private string controllerID;

    public string Name { get { return controllerID; } }

    private void Awake()
    {
        if (spellAnimations == null || spellAnimations.Length <= 0) { return; }

        spellAnimations = GetComponentsInChildren<ISpellAnimation>();
    }

    private void Start(){
    }

    public void StartAnimation(Spells spell)
    {
        foreach (var anim in spellAnimations)
        {
            if (anim.Spell == spell) { anim.StartAnimation(); return; }
        }
        throw new System.Exception($"Controller {controllerID} has no animation for {spell}");
    }
}
