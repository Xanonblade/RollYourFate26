using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPOV;

    private enum Spells
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CastSpell()
    {

    }
}
