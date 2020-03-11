using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : MonoBehaviour {
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject shootPoint;
    [SerializeField] float projectileSpeed = 10;
    public int attackRate = 1;
    private float attackTimer;
    public bool canFire = true;
    public AudioClip grenadeSFX;
    AudioSource audioSource;
    void Start () {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        attackTimer += Time.deltaTime;
        if(attackTimer > attackRate){
            canFire = true;
        }
    
        if (Input.GetMouseButtonDown (0)) {
            if(canFire){
            GameObject go = Instantiate (projectile, shootPoint.transform.position, shootPoint.transform.rotation);
            go.GetComponent<Rigidbody> ().AddForce (transform.forward * projectileSpeed , ForceMode.Impulse);
            audioSource.PlayOneShot(grenadeSFX,0.3f);
            canFire = false;
            attackTimer = 0;
            }
        }
    }
}