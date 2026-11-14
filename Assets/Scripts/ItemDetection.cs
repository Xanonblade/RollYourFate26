using UnityEngine;
using static UnityEngine.UI.Image;

public class ItemDetection : MonoBehaviour
{
    private void Update()
    {
        Ray fireRay = new Ray(transform.position, transform.forward);

        RaycastHit hit;
        if (Physics.Raycast(fireRay, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.CompareTag("KeyItem"))
            {
                // Call Information
                Debug.Log("Hit Key Item");
            }
        }
    }
}
