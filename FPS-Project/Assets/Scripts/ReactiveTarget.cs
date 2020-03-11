using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReactiveTarget : MonoBehaviour {
        Animator animator;
        [SerializeField] private int health;
        public bool isAlive = true;
        private void Start () {

            animator = GetComponent<Animator> ();

        }

        public void TakeDamage (int amount) {
            health -= amount;
            if (health <= 0) {
                isAlive = false;
            }
            if (!isAlive) {
                if (this.CompareTag ("Player")) {
                        Loss ();
                    } else {

                        Death ();
                    }
                }
            }

        public void Heal(int amount){
            health += amount;
        }
        

    private void Loss()
    {
        SceneManager.LoadScene(4);
    }

    public void Death () {
                WanderingAI behavior = GetComponent<WanderingAI> ();
                StationaryShooter behavior2 = GetComponent<StationaryShooter> ();
                BossAI bossBehavior = GetComponent<BossAI> ();
                if (behavior != null) {
                    behavior.SetAlive (false);
                    animator.SetTrigger ("Death");
                }
                if (behavior2 != null) {
                    animator.SetTrigger ("Death");
                }
                if (bossBehavior != null) {
                    bossBehavior.enabled = false;
                    animator.SetBool ("Walk Forward", false);
                    animator.SetTrigger ("Die");
                }
                StartCoroutine (Die ());
            }

            private IEnumerator Die () {
                //  this.transform.Rotate(-75,0,0);
                yield return new WaitForSeconds (1f);
                Destroy (this.gameObject);
            }
        }