using UnityEngine;
using static UnityEngine.UI.Image;

public class ItemDetection : MonoBehaviour
{
    private void Update()
    {
        Camera current = Camera.main;
        Ray fireRay = new Ray(current.transform.position, current.transform.forward);

        RaycastHit hit;
        if (Physics.Raycast(fireRay, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.CompareTag("KeyItem"))
            {
                // Call Information
                Debug.Log("Hit Key Item");

                //Check if spell is correct in WhisperProcess
                if (gameObject.GetComponent<WhisperProcess>().MatchSpell(out Spells finalSpell))
                {
                    hit.collider.GetComponent<InteractableObject>().CastSpellOnThis(finalSpell);
                }
                hit.collider.GetComponent<InteractableObject>().Hover();
            }
        }
    }
}
