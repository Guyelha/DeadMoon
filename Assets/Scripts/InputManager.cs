using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{ 
    
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;



    [SerializeField] PlayerMovement movement;
    [SerializeField] PlayerLook look;
    [SerializeField] Gun gun;
    public GameObject rifleGameOject;
    public GameObject crossHair;

    //Aiming and Zoom
    [SerializeField] private bool isAiming;
    [SerializeField] private bool canZoom;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float timeToZoom = 0.3f;
    [SerializeField] private float zoomFOV = 45f;
    private float deafultFOV;
    private Coroutine zoomRoutine;

    

    Coroutine fireCoroutine;

    

    void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        deafultFOV = playerCamera.fieldOfView;
         
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        movement = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerLook>();
        onFoot.Jump.performed += ctx => movement.Jump();
        onFoot.Crouch.performed += ctx => movement.Crouch();
        onFoot.StartSprint.performed += e => movement.StartSprinting();
        onFoot.StartSprint.canceled += e => movement.StopSprinting();
        
        onFoot.Shoot.started += _ => StartFiring();
        onFoot.Shoot.canceled += _ => StopFiring();
        onFoot.Aim.performed += e => AimingPressed();
        onFoot.ReleaseAim.performed += e => AimingReleased();

    }

    
    void FixedUpdate()
    {
        movement.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    void StartFiring()
    {
        fireCoroutine = StartCoroutine(gun.RapidFire());
    }

    void StopFiring()
    {
        if(fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
        }
    }

    private void AimingPressed()
    {
        isAiming = true;
        crossHair.SetActive(false);
        gun.GetComponent<Animator>().Play("ADS");
        if(zoomRoutine != null)
        {
            StopCoroutine(zoomRoutine);
            zoomRoutine = null;
        }
        zoomRoutine = StartCoroutine(ToggleZoom(true));
    }

    private void AimingReleased()
    {
        isAiming= false;
        crossHair.SetActive(true);
        gun.GetComponent<Animator>().Play("Stop_ADS");
        if (zoomRoutine != null)
        {
            StopCoroutine(zoomRoutine);
            zoomRoutine = null;
        }
        zoomRoutine = StartCoroutine(ToggleZoom(false));
    }

    private IEnumerator ToggleZoom(bool isEnter)
    {
        float targetFOV = isEnter ? zoomFOV : deafultFOV;
        float startingFOV = playerCamera.fieldOfView;
        float timeElapsed = 0;

        while (timeElapsed < timeToZoom)
        {
            playerCamera.fieldOfView = Mathf.Lerp(startingFOV, targetFOV, timeElapsed / timeToZoom );
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        playerCamera.fieldOfView = targetFOV;
        zoomRoutine = null;
    }

    

    

    private void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
