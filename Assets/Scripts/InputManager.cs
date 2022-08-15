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

    public bool isAiming;
    

    Coroutine fireCoroutine;

    

    void Awake()
    {
         
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

    }

    private void AimingReleased()
    {
        isAiming= false;
        crossHair.SetActive(true);
        gun.GetComponent<Animator>().Play("New State");
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
