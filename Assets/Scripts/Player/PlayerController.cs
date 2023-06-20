using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    private CustomInput playerInput = null;
    private PlayerInput pl;
    private Vector2 moveVector = Vector2.zero;
    private bool aiming = false;
    private float aimSpeedDecrease = 1f;
    private float angle;
    private Rigidbody2D rb;
    private Vector2 aim;
    private Vector2 lastRecordedMovement;
    private Vector2 lastRecordedAim;
    public bool movedRecently = false;
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private float MoveSpeed = 5f;
    
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new CustomInput();
        pl = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        playerInput.Enable();
        playerInput.Player.Move.performed += OnMovementPerformed;
        playerInput.Player.Move.canceled += OnMovementCancelled;
        playerInput.Player.Aim.performed += OnStartAimPerformed;
        playerInput.Player.Shoot.performed += OnShootPerformed;
        playerInput.Player.Aim.canceled += OnStartAimCanceled;
    }
    private void OnDisable() {
        playerInput.Disable();
        playerInput.Player.Move.performed -= OnMovementPerformed;
        playerInput.Player.Move.canceled -= OnMovementCancelled;
        playerInput.Player.Aim.performed -= OnStartAimPerformed;
        playerInput.Player.Aim.canceled -= OnStartAimCanceled;
        
    }
    private void FixedUpdate() {
        if(aiming) {
            if(pl.currentControlScheme == "Gamepad") {
                Vector2 aim = playerInput.Player.AimPosition.ReadValue<Vector2>();
                if(aim == Vector2.zero) {
                    if(movedRecently) {
                        aim = lastRecordedMovement;
                    } else {
                        aim = lastRecordedAim;
                    }
                }
                lastRecordedAim = aim;
                angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;

            } else {
                Vector2 aim = Camera.main.ScreenToWorldPoint(playerInput.Player.MousePosition.ReadValue<Vector2>()) - transform.position;
                angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;

            }
            Debug.Log(angle);

        } else {
            
        }
        transform.rotation = Quaternion.Euler(0, 0, angle);
        rb.velocity = moveVector * MoveSpeed * aimSpeedDecrease;

    }
    private void OnMovementPerformed(InputAction.CallbackContext value) {
        moveVector = value.ReadValue<Vector2>();
        lastRecordedMovement = moveVector;
        if(!aiming) {
            angle = Mathf.Atan2(moveVector.y, moveVector.x) * Mathf.Rad2Deg;
        }
        Debug.Log(moveVector);
        movedRecently = true;


    }
    private void OnMovementCancelled(InputAction.CallbackContext value) {
        moveVector = Vector2.zero;
    }
    private void OnStartAimPerformed(InputAction.CallbackContext value) {
        aiming = value.performed;
        aimSpeedDecrease = 0.1f;
    }
    private void OnStartAimCanceled(InputAction.CallbackContext value) {
        aiming = false;
        aimSpeedDecrease = 1;
        movedRecently = false;

    }
    private void OnShootPerformed(InputAction.CallbackContext value) {
        if(value.performed) {
            if(aiming) {
                Debug.Log("shot by:" + firePoint.position + " " + firePoint.rotation);
            } else {
                Vector2 impreciseAngle;
                if(movedRecently) {
                    impreciseAngle = new Vector2(Random.Range(lastRecordedMovement.x - 0.2f, lastRecordedMovement.x + 0.2f), Random.Range(lastRecordedMovement.y - 0.2f, lastRecordedMovement.y + 0.2f));
                } else {
                    impreciseAngle = new Vector2(Random.Range(lastRecordedAim.x - 0.2f, lastRecordedAim.x + 0.2f), Random.Range(lastRecordedAim.y - 0.2f, lastRecordedAim.y + 0.2f));
                }
                angle = Mathf.Atan2(impreciseAngle.y, impreciseAngle.x) * Mathf.Rad2Deg;
                Debug.Log("shot by:" + firePoint.position + " " + angle);

            }
        }
    }
}
