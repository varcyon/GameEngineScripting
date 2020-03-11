using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FPSInput: MonoBehaviour {
    private CharacterController _charController;
    public float speed = 6.0f;
    public float gravity = -9.8f;
    public float terminalVeocity = -10.0f;
    public float minFall = -1.5f;
    public float jumpHeight = 15.0f;
    private float _vertSpeed;
    public List<GameObject> weapons = new List<GameObject>();
    int currentWeaponSlot = 0;

    void Start() {
        _charController = GetComponent<CharacterController>();
        _vertSpeed = minFall;
    }

    void OnEnable() {
        EventManager.WeaponSwap += SwapWeapon;
    }

    void OnDisable() {
        EventManager.WeaponSwap -= SwapWeapon;
    }

    void SwapWeapon() {
        if (currentWeaponSlot == 0) {
            weapons[currentWeaponSlot].SetActive(false);
            weapons[currentWeaponSlot + 1].SetActive(true);
            currentWeaponSlot =1;
        } else if (currentWeaponSlot == 1 ){
            weapons [currentWeaponSlot].SetActive(false);
            weapons[currentWeaponSlot - 1].SetActive(true);
            currentWeaponSlot = 0;
        }
        Debug.Log("Swapped weapon");
    }

    void Update() {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement = transform.TransformDirection(movement);

        if (_charController.isGrounded) {
            if (Input.GetButtonDown("Jump")) {
                _vertSpeed = jumpHeight;
            } else {
                _vertSpeed = minFall;
            }
        } else {
            _vertSpeed += gravity * 5 * Time.deltaTime;
            if (_vertSpeed < terminalVeocity) {
                _vertSpeed = terminalVeocity;
            }
        }
        movement.y = _vertSpeed;
        movement *= Time.deltaTime;
        _charController.Move(movement);

    }
}
