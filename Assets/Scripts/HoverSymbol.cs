using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HoverSymbol : MonoBehaviour
{
    const int ICON_PIXEL_SIZE = 200;
    const int SPELL_NUMBER = 8;

    [SerializeField] Sprite burnIcon;
    [SerializeField] Sprite growIcon;
    [SerializeField] Sprite pullIcon;
    [SerializeField] Sprite zapIcon;
    [SerializeField] Sprite openIcon;

    SpriteRenderer[] sprites;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTexture(List<Spells> validSpells)
    {
        List<Sprite> validSymbols = new();

        foreach (Spells spell in validSpells)
        {
            switch(spell)
            {
                case Spells.Burn:
                    validSymbols.Add(burnIcon);
                    break;
                case Spells.Grow:
                    validSymbols.Add(growIcon);
                    break;
                case Spells.Pull:
                    validSymbols.Add(pullIcon);
                    break;
                case Spells.Unlock:
                    validSymbols.Add(openIcon);
                    break;
                case Spells.Zap:
                    validSymbols.Add(zapIcon);
                    break;
            }
        }

        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.gameObject.SetActive(false);
        }

        for (int i = 0; i < validSymbols.Count; i++)
        {
            sprites[i].transform.position = transform.position + transform.right * (-1 * ((validSymbols.Count - 1) / 2f) + 1 * i);

            sprites[i].gameObject.SetActive(true);
            sprites[i].sprite = validSymbols[i];
        }
        /*
        for (int i = 0; i < validSymbols.Count; i++)
        {
            for (int j = 0; j < ICON_PIXEL_SIZE; j++)
            {
                for (int k = 0; k < ICON_PIXEL_SIZE; k++)
                {
                    spriteRenderer.sprite.texture.SetPixel(SPELL_NUMBER * ICON_PIXEL_SIZE / 2 - ICON_PIXEL_SIZE * validSymbols.Count / 2 + ICON_PIXEL_SIZE * i + j, k, validSymbols[i].GetPixel(j, k));
                }
            }
        }

        spriteRenderer.sprite.texture.Apply();
        */
    }
}
