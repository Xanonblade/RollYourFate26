using UnityEngine;

/// <summary>
/// Animation data for objects whose animations differ due to what spell they are hit with
/// </summary>
public class SpellAnimationData : AnimationData
{
    [SerializeField] private Spells spell;

    /// <summary>
    /// The spell this animation reacts to
    /// </summary>
    public Spells Spell { get { return spell; } }

    private void Start() {}
}
