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

    SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Texture2D texture = new Texture2D(SPELL_NUMBER * ICON_PIXEL_SIZE, ICON_PIXEL_SIZE);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Sprite.Create(texture, new Rect(), new Vector2(0, 0));
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

        // Encode texture into PNG format
        byte[] bytes = spriteRenderer.sprite.texture.EncodeToPNG();

        // Define the save path (persistentDataPath works across all platforms)
        string folderPath = Application.persistentDataPath;
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string filePath = Path.Combine(folderPath, "CombinedTexture" + ".png");

        // Write the bytes to disk
        File.WriteAllBytes(filePath, bytes);
        Debug.Log("Saved texture to: " + filePath);
    }
}
