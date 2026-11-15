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
            if (hit.collider.gameObject.CompareTag("KeyItem") && hit.collider.GetComponent<InteractableObject>().ValidSpellsCount > 0)
            {
                Time.timeScale = Mathf.Lerp(Time.timeScale, 1/12f, 0.01f);
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 50, 0.01f);

                if (prevHit != hit.collider)
                {
                    // Call Information
                    Debug.Log("Hit Key Item");

                    prevHit?.GetComponent<InteractableObject>().ToggleHover();
                    hit.collider.GetComponent<InteractableObject>().ToggleHover();
                }
                    
                //Check if spell is correct in WhisperProcess
                if (gameObject.GetComponent<WhisperProcess>().MatchSpell(out Spells finalSpell))
                {
                    hit.collider.GetComponent<InteractableObject>().CastSpellOnThis(finalSpell);
                }
                    

                prevHit = hit.collider;
            }
            else
            {
                Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, 0.03f);
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, 0.01f);

                prevHit?.GetComponent<InteractableObject>().ToggleHover();
                prevHit = null;
            }
        }
        else
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, 0.03f);
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, 0.01f);

            prevHit?.GetComponent<InteractableObject>().ToggleHover();
            prevHit = null;
        }
    }
}
