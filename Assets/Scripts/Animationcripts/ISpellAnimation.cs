using UnityEngine;

public interface ISpellAnimation
{
    Spells Spell { get; }
    int IncomingFlag { get; }
    int OutgoingFlag { get; }
    void StartAnimation();
}