using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryShooter : MonoBehaviour {
    [SerializeField]
    private GameObject fireball;
    [SerializeField]
    private GameObject shootPoint;
    [SerializeField]
    private float fieldOfView = 110;
    private SphereCollider viewRange;
    [SerializeField]
    private int attackSpeed;
    [SerializeField]
    private float fireballSpeed;
    private float attackTimer;
    private bool canFire;
    private Animator animator;


    void Start () {

        attackTimer = 0f;
    }

    // Update is called once per frame
    void Update () {
        attackTimer += Time.deltaTime;
        animator = GetComponent<Animator> ();
        viewRange = GetComponent<SphereCollider> ();

    }
    private void OnTriggerStay (Collider other) {
        if (other.gameObject.CompareTag ("Player")) {
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle (direction, transform.forward);
            if (angle < fieldOfView * .5f) {
                RaycastHit hit;
                if (Physics.Raycast (transform.position + transform.up, direction.normalized, out hit, viewRange.radius)) {
                    if (hit.collider.gameObject.CompareTag ("Player")) {

                        fire ();
                    }
                }
            }
        }
    }

    void fire () {
        if (attackTimer > attackSpeed) {
            animator.SetTrigger ("Fire");
            GameObject shot = Instantiate (fireball, shootPoint.transform.position, Quaternion.identity);
            Rigidbody shotRB = shot.GetComponent<Rigidbody> ();
            shotRB.AddForce (transform.forward * fireballSpeed, ForceMode.VelocityChange);
            Destroy (shot, 2f);
            attackTimer = 0f;
        }
    }

  

}