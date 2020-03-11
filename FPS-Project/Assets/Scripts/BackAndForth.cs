using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForth : MonoBehaviour {
    public float speed = 3f;
    public float maxZ = 16f;
    public float minZ = -16f;
    private Vector3 startPos;
    private int direction = 1;
    private float dist;

    private void Start() {
        startPos = transform.position;
    }
    void Update () {
        transform.Translate (0, 0, direction * speed * Time.deltaTime);
        dist = Vector3.Distance(startPos,transform.position);
        bool bounced = false;
        if (dist > maxZ) {
            startPos = transform.position;
            direction = -direction;
            bounced = true;
        }
        if (bounced) {
            transform.Translate (0, 0, direction * speed * Time.deltaTime);
        }
    }
}