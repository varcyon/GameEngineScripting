using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {
    public int healAmount = 2;
    public int damageAmount = 1;
    void OnTriggerEnter (Collider other) {
        if (other.CompareTag ("Player")) {

            if (gameObject.name == "Heal") {
                other.GetComponent<ReactiveTarget> ().Heal (healAmount);
                Destroy (this.gameObject);
            }
            if (gameObject.name == "Damage") {

                other.GetComponent<ReactiveTarget> ().TakeDamage (damageAmount);
                Destroy (this.gameObject);
            }
        }
    }
}