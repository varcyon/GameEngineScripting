using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
    public List<Transform> positions = new List<Transform> ();
    public List<Vector3> wPositions = new List<Vector3> ();
    Vector3 targetPos;
    int positionNum = 0;

    public float tolerance;
    public float speed;
    public float delayTime;
    float delayStart;
    public bool automatic = true;
    void Start () {
        for (int i = 0; i < positions.Count; i++) {
            wPositions.Add (positions[i].position);
        }

        if (positions.Count > 0) {
            targetPos = positions[0].position;
        }
        tolerance = speed * Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate () {

        if (transform.position != targetPos) {
            MovePlatform ();
        } else {
            UpdateTarget ();
        }
    }

    void MovePlatform () { // Determine the direction it is heading
        Vector3 heading = targetPos - transform.position;
        transform.position = Vector3.MoveTowards (transform.position, wPositions[positionNum], speed * Time.deltaTime);
        // transform.position += (heading / heading.magnitude) * speed * Time.deltaTime;
        if (heading.magnitude < tolerance) {
            transform.position = targetPos;
            delayStart = Time.time;
        }
    }

    void UpdateTarget () {
        if (automatic) {
            if (Time.time - delayStart > delayTime) {
                NextPos ();
            }
        }
    }

    private void NextPos () {
        positionNum++;
        if (positionNum >= wPositions.Count) {

            positionNum = 0;
        }

        targetPos = wPositions[positionNum];

    }

    private void OnTriggerEnter (Collider other) {
        if (other.CompareTag ("Player")) {
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit (Collider other) {
        if (other.CompareTag ("Player")) {
            other.transform.parent = null;
        }
    }
}