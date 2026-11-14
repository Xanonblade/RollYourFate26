using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Looker : MonoBehaviour
{
    public float lookSpeed = 30f;

    public float maxPitch = 60f;
    public float minPitch = -65f;

    public float maxYaw = 80f;
    public float minYaw = -80f;

    public Camera playerCam;

    private float pitch;
    private float yaw;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        minPitch = LevelManager.GetMinPitch;
        maxPitch = LevelManager.GetMaxPitch;

        minYaw = LevelManager.GetMinYaw;
        maxYaw = LevelManager.GetMaxYaw;
    }

    public void OnPlayerLook(InputAction.CallbackContext context)
    {
        Vector3 input = context.ReadValue<Vector2>() * Time.deltaTime * lookSpeed;

        yaw += input.x;
        yaw = Mathf.Clamp(yaw, minYaw, maxYaw);

        pitch -= input.y;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        playerCam.transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);
    }
}
