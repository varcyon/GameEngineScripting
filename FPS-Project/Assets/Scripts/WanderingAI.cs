using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour {
    private int damage = 2;
    public float speed = 3.0f;
    public float obstacleRange = 5.0f;
    private bool alive;
    private float idleTime = 1f;
    private float timer;
    private bool wait;
    private RaycastHit hit;
    private float fieldOfView = 90f;
    private Animator animator;
    [SerializeField]
    private SphereCollider viewRange;
    private bool canAttack = true;
    private float attackSpeed =2f;

    void Start () {
        alive = true;
        animator = GetComponent<Animator> ();
    }
    void Update () {
        if (wait) {
            animator.SetBool ("Walking", false);
            timer += Time.deltaTime;
        }
        if (alive && !wait) {
            animator.SetBool ("Walking", true);
            transform.Translate (0, 0, speed * Time.deltaTime);
        }

        if (Physics.Raycast (transform.position + transform.up, transform.forward, out hit, obstacleRange)) {
            if (hit.transform.CompareTag ("Wall")) {
                wait = true;
                if (timer > idleTime) {
                    float angle = Random.Range (-180, 180);
                    transform.Rotate (0, angle, 0);
                    wait = false;
                    timer = 0f;
                }
            }
        }
    }
    private void OnTriggerStay (Collider other) {
        if (other.gameObject.CompareTag ("Player")) {
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle (direction, transform.forward);
            if (angle < fieldOfView * .5f) {
                RaycastHit hit;
                if (Physics.Raycast (transform.position + transform.up, direction.normalized, out hit, viewRange.radius)) {
                    if (canAttack && hit.collider.gameObject.CompareTag ("Player")) {
                        animator.SetTrigger ("ATTACK");
                        other.GetComponent<PlayerCharacter>().Hurt(damage);
                        canAttack = false;
                        StartCoroutine(SetAttackTrue());
                    }
                }
            }

        }

    }
    IEnumerator SetAttackTrue(){
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }
    public void SetAlive (bool _alive) {
        alive = _alive;
    }
}