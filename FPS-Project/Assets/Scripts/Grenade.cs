using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {
    // public bool isStatic;
    // public bool isReference;
    [SerializeField] int damage = 5;
    [SerializeField] float radius = 2;
    [SerializeField] float power = 10f;

    private void OnCollisionEnter (Collision other) {
        if (other.gameObject.name != "Barel" && other.gameObject.tag != "Player") {
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere (explosionPos, radius);
            foreach (Collider hit in colliders) {
                Rigidbody rb = hit.GetComponent<Rigidbody> ();
                if (rb != null) {
                    rb.AddExplosionForce (power, explosionPos, radius, 1f, ForceMode.Impulse);
                    ReactiveTarget target = hit.GetComponent<ReactiveTarget> ();
                    if (target != null) {
                        target.TakeDamage(damage);
                    }
                }
                Destroy (gameObject);
            }
        }
    }

    // private void Start() {
    //     if(isReference){
    //         var shootScript = FindObjectOfType<GrenadeTrajectory>();
    //         shootScript.ResisterGrenade(this);
    //     }
    // }
}