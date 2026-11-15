using UnityEngine;
using static UnityEngine.UI.Image;

public class ItemDetection : MonoBehaviour
{
    private void Update()
    {
        //TODO: change ray to have vertical rotation
        Ray fireRay = new Ray(transform.position, transform.forward);

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
            }
        }
    }
}
