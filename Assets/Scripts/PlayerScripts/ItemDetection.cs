using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.UI.Image;

public class ItemDetection : MonoBehaviour
{
    Collider prevHit;
    private float allowedCheckFrequency = 1f / 6f;
    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;

        Camera current = Camera.main;
        Ray fireRay = new Ray(current.transform.position, current.transform.forward);

        RaycastHit hit;
        if (Physics.Raycast(fireRay, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.CompareTag("KeyItem"))
            {
                
                if (prevHit != hit.collider)
                {
                    // Call Information
                    Debug.Log("Hit Key Item");

                    prevHit?.GetComponent<InteractableObject>().ToggleHover();
                    hit.collider.GetComponent<InteractableObject>().ToggleHover();
                }

                if (timer > allowedCheckFrequency)
                {
                    timer = 0;
                    
                    //Check if spell is correct in WhisperProcess
                    if (gameObject.GetComponent<WhisperProcess>().MatchSpell(out Spells finalSpell))
                    {
                        hit.collider.GetComponent<InteractableObject>().CastSpellOnThis(finalSpell);
                    }
                    
                }

                prevHit = hit.collider;
            }
            else
            {
                prevHit?.GetComponent<InteractableObject>().ToggleHover();
                prevHit = null;
            }
        }
        else
        {
            prevHit?.GetComponent<InteractableObject>().ToggleHover();
            prevHit = null;
        }
    }
}
