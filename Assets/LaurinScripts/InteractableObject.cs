using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField]
    protected List<Spells> validSpells;
    
    protected int flag; 

    public List<Spells> ValidSpells => validSpells;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
