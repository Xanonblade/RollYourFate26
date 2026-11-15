using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HoverSymbol : MonoBehaviour
{
    const int ICON_PIXEL_SIZE = 100;
    const int SPELL_NUMBER = 8;

    [SerializeField] Texture2D burnIcon;
    [SerializeField] Texture2D growIcon;

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
        List<Texture2D> validSymbols = new();

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
            sprites[i].sprite = Sprite.Create(validSymbols[i], new Rect(0, 0, ICON_PIXEL_SIZE, ICON_PIXEL_SIZE), Vector2.zero);
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
