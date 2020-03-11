using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour {
    public GameObject player;
    public Rigidbody rigidbody;
    public Animator animator;
    public float speed = 1;
    public float distance;
    public bool canAttack;
    public int attackRate;
    private float attackTimer;
    public int damage = 2;

    void Start () {
        player = GameObject.FindGameObjectWithTag ("Player");
        rigidbody = GetComponent<Rigidbody> ();
        animator = GetComponent<Animator> ();
    }

    void Update () {
        attackTimer += Time.deltaTime;
        if (attackTimer > attackRate) {
            canAttack = true;
        }
        transform.LookAt (player.transform);
        transform.position = Vector3.MoveTowards (transform.position, player.transform.position, speed * Time.deltaTime);
        distance = Vector3.Distance (transform.position, player.transform.position);

        if (distance <= 8f && canAttack) {
            Attack ();
            canAttack = false;
            attackTimer = 0;

        } else if (distance > 8f) {
            animator.SetBool ("Walk Forward", true);
            speed = 2;

        }
    }

    private void Attack () {
        animator.SetBool ("Walk Forward", false);
        speed = 0;
        animator.SetTrigger ("Stab Attack");
        player.GetComponent<PlayerCharacter> ().Hurt (damage);

    }
}