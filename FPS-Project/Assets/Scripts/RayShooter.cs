using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour {

    private Camera camera;
    [SerializeField] int damage = 1;
    [SerializeField] GameObject laserShot;
    [SerializeField] GameObject shootPoint;
    RaycastHit hit;
    float range = 1000f;
    AudioSource audioSource;
    public AudioClip laserSfx;
    void Start () {
        camera = GetComponentInParent<Camera> ();
        audioSource = GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update () {
        if (Input.GetMouseButtonDown (0)) {
            Ray ray = camera.ScreenPointToRay (Input.mousePosition);
            if (Physics.Raycast (ray, out hit, range)) {
                GameObject laser = Instantiate(laserShot,shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;
                laser.GetComponent<ShotBehavior>().SetTarget(hit.point);
                audioSource.PlayOneShot(laserSfx,0.3f);
                Destroy(laser, 2.5f);
            }
        }
    }

   
    void OnGUI () {
        int size = 12;
        float posX = camera.pixelWidth / 2 - size / 4;
        float posY = camera.pixelHeight / 2 - size / 2;
        GUI.Label (new Rect (posX, posY, size, size), "*");
    }
}