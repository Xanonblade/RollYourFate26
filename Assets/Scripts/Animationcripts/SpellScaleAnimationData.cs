using UnityEngine;

public class SpellScaleAnimationData : ScaleAnimationData, ISpellAnimation
{
    [SerializeField] private Spells spell;

    /// <summary>
    /// The spell this animation reacts to
    /// </summary>
    public Spells Spell { get { return spell; } }

    int ISpellAnimation.IncomingFlag => IncomingFlag;

    int ISpellAnimation.OutgoingFlag => OutgoingFlag;

    private void Start() { }
}
