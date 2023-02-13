using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    #region events
    public delegate void StartTouch(Vector2 position, float time);
    public static event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public static event StartTouch OnEndTouch;
    #endregion

    private Controls controls;
    private Camera mainCamera;
    private void Awake()
    {
        controls = new Controls();
        mainCamera = Camera.main;

    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
    void Start()
    {
        controls.Main.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        controls.Main.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }
    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnStartTouch != null)
            OnStartTouch(ScreenToWorld(mainCamera, controls.Main.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
    }
    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnEndTouch != null)
            OnEndTouch(ScreenToWorld(mainCamera, controls.Main.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);
    }
    public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        position.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(position);
    }
}
