using UnityEngine;
using UnityEngine.InputSystem;

public class FPLook : MonoBehaviour
{
    public float lookSpeed = 30f;

    public Camera playerCam;

    private float pitch;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnPlayerLook(InputAction.CallbackContext context)
    {
        Vector3 input = context.ReadValue<Vector2>() * Time.deltaTime * lookSpeed;

        transform.rotation = transform.rotation * Quaternion.Euler(0f, input.x, 0f);

        pitch -= input.y;
        pitch = Mathf.Clamp(pitch, -70, 70);
        playerCam.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}
