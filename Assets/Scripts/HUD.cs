using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    private bool spellbookIsOut = false;
    [SerializeField] GameObject spellbook;
    [SerializeField] GameObject text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleSpellbook()
    {
      if (spellbookIsOut)
        {
            if (!spellbook.GetComponent<AutoFlip>().IsFlipping)
            {
                spellbook.SetActive(false);
                text.SetActive(true);
                spellbookIsOut = !spellbookIsOut;
            }
        }
        else
        {
            spellbook.SetActive(true);
            text.SetActive(false);
            spellbookIsOut = !spellbookIsOut;
        }
    }
}
