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

public class InteractableObject : MonoBehaviour
{
    protected Dictionary<Spells, AnimationData> validSpells;
    private HoverSymbol hoverVisual;
    private float timer = 0.1f;
    
    /// <summary>
    /// Use this to set conditions on when the action can be performed
    /// </summary>
    protected int flag; 

    void Start()
    {
        AnimationData[] anims = GetComponents<AnimationData>();
        validSpells = new Dictionary<Spells, AnimationData>();

        //populate animations to play
        for (int i = 0; i < anims.Length; i++)
        {
            validSpells.Add((Spells)Enum.Parse(typeof(Spells), anims[i].Name), anims[i]);
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
        timer -= Time.deltaTime;  
        if (timer <= 0)
        {
            hoverVisual.gameObject.SetActive(false);
        }
        else
        {
            hoverVisual.gameObject.SetActive(true);
        }
    }

    public void CastSpellOnThis(Spells spell)
    {
        //play animation and update flags
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

    public void Hover()
    {
        timer = 0.1f;
    }
}
