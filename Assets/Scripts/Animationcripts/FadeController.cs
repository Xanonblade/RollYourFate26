using System;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    Image solidColor;

    int flag = 0;
    float timer = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        solidColor = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flag == 1) { 
            timer += Time.deltaTime;

            if (timer >= 2.0f)
            {
                solidColor.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, (timer - 2) / 1f);
            }
            else if (timer >= 3.0f)
            {
                timer = 0;
                IncrementFlag();
            }
        }
        else if (flag == 2) 
        { 
            timer += Time.deltaTime;
            solidColor.color = Color.Lerp(Color.white, Color.black, timer / 1.0f);

        }
    }

    public void IncrementFlag()
    {
        flag++;
    }
}
