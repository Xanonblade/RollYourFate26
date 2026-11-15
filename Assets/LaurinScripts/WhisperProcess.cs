using UnityEngine;
using FuzzySharp;
using System.Linq;

public class WhisperProcess : MonoBehaviour
{
    [SerializeField]
    private RunWhisper model;

    private string finalSpeech = "";
    public string FinalSpeech { get { return finalSpeech; } }

    private float time;
    private bool hasStarted = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame, runs model as often as possible
    void Update()
    {
        

        //Knows when async processes are complete
        //  Sets speech to most updated, then run model again
        //  Goal is to have speech update as often as possible to be more accurate in terms of recency
        if (model.SafeToInteract && hasStarted)
        {
            hasStarted = false;
            finalSpeech = model.OutString;
            //Debug.Log(finalSpeech);
            //_ = model.WhisperSession();
            //model.SafeToInteract = false;
        }
    }

    public void StartParse()
    {
        if (model.SafeToInteract)
        {
            _ = model.WhisperSession();
            model.SafeToInteract = false;
            hasStarted = true;
        }
       
    }

    /// <summary>
    /// Checks if user speech is equal to an existing spell
    /// </summary>
    /// <param name="spell">The spell enum that's actually cast on the object</param>
    /// <returns>True if speech matches an existing spell, false otherwise</returns>
    public bool MatchSpell(out Spells spell)
    {
        //Compare speech to spell enum
        string[] spellArray = Spells.GetNames(typeof(Spells));
        string speechFixed = new string(finalSpeech.Where(c => !char.IsPunctuation(c)).ToArray());

        if (finalSpeech != "") Debug.Log(finalSpeech);
        finalSpeech = "";

        //Fuzzy comparisons
        for (int i = 0; i < spellArray.Length; i++)
        {
            //90% similarity
            if (Fuzz.PartialTokenSetRatio(speechFixed.ToLowerInvariant(), spellArray[i].ToLowerInvariant()) > 90)
            {
                spell = (Spells)i;
                return true;
            }
        }

        spell = Spells.Burn;
        return false;
    }
}
