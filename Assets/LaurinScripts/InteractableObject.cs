using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Spells
{
    Burn,
    Grow,    //Just scales an object, makes it bigger
    Pull,    //Moving objects in air
    Water,
    Zap,
    Goopify, //Squash object vertically, stretch it out horizontally
    Unlock,  //Unlock anything locked, whether a box, door, etc
    Cleave   //Cuts objects in half
}

[RequireComponent(typeof(Collider))]
public class InteractableObject : MonoBehaviour
{
    protected Dictionary<Spells, ISpellAnimation> validSpells;
    public int ValidSpellsCount { get { return validSpells.Where((x) => x.Value.IncomingFlag == flag).Count(); } }
    private HoverSymbol hoverVisual;
    private bool hovered = false;
    private MeshRenderer mesh;
    
    /// <summary>
    /// Use this to set conditions on when the action can be performed
    /// </summary>
    protected int flag; 

    void Start()
    {
        ISpellAnimation[] anims = GetComponents<ISpellAnimation>();
        validSpells = new Dictionary<Spells, ISpellAnimation>();
        mesh = GetComponent<MeshRenderer>();

        //populate animations to play
        for (int i = 0; i < anims.Length; i++)
        {
            validSpells.Add(anims[i].Spell, anims[i]);
        }

        hoverVisual = GetComponentInChildren<HoverSymbol>();
        if (hoverVisual == null)
        {
            throw new Exception("No hover!!");
        }

        hoverVisual.UpdateTexture(
            validSpells
                .Where((x) => x.Value.IncomingFlag == flag)
                .Select((x) => x.Key)
                .ToList());
    }

    // Update is called once per frame
    void Update()
    {
        if (!hovered)
        {
            hoverVisual.gameObject.SetActive(false);
            if (mesh.materials.Length > 1)
                mesh.materials[1].color = Color.black;
        }
        else
        {
            hoverVisual.gameObject.SetActive(true);
            if (mesh.materials.Length > 1)
                mesh.materials[1].color = Color.yellow;
        }
    }

    public void CastSpellOnThis(Spells spell)
    {
        //play animation and update flags, checks if the spell is valid for that object
        if (validSpells.ContainsKey(spell)) {
            if (validSpells[spell].IncomingFlag == flag)
            {
                validSpells[spell].StartAnimation();
                flag = validSpells[spell].OutgoingFlag;
            }
            
        }

        hoverVisual.UpdateTexture(
            validSpells
                .Where((x) => x.Value.IncomingFlag == flag)
                .Select((x) => x.Key)
                .ToList());
    }

    public void ToggleHover()
    {
        hovered = !hovered;
    }
}
