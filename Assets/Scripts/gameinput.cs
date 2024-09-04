using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class gameinput : MonoBehaviour
{
    public Action I_counter;
    public Action I_container;
    public Action I_cutting;
    public Action I_trash;
    public Action I_stove;
    public Action I_plates;
    public Action I_delivery;

    private Playermovment playerInputActions;

    private void Awake()
    {
        playerInputActions = new Playermovment();
    }

    private void OnEnable()
    {
        playerInputActions.Movement.interact.Enable();
        playerInputActions.Movement.interact.performed += OnInteract;
    }

    private void OnDisable()
    {
        playerInputActions.Movement.interact.performed -= OnInteract;
        playerInputActions.Movement.interact.Disable();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        I_counter?.Invoke();
        I_container?.Invoke();
        I_cutting?.Invoke();
        I_trash?.Invoke();
        I_stove?.Invoke();
        I_plates?.Invoke();
        I_delivery?.Invoke();
    }
}