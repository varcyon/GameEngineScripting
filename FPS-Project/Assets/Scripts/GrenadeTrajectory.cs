using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct RegisteredGrenades {
    public Grenade real;
    public Grenade hidden;
}

public class GrenadeTrajectory : MonoBehaviour {
    public GameObject grenade;
    public Transform referenceGrenade;
    private Scene mainScene;
    private Scene physicsScene;

    public GameObject marker;
    private List<GameObject> markers = new List<GameObject> ();
    private Dictionary<string, RegisteredGrenades> allGrenades = new Dictionary<string, RegisteredGrenades> ();

    public GameObject objectsToSpawn;

    void Start () {
        Physics.autoSimulation = false;
        mainScene = SceneManager.GetActiveScene ();
        physicsScene = SceneManager.CreateScene ("Physics-Scene", new CreateSceneParameters (LocalPhysicsMode.Physics3D));

        PreparePhysicsScene ();
    }
    void FixedUpdate () {
        if (Input.GetKeyDown (KeyCode.Q)) {

            ShowTrajectory ();

        }

        mainScene.GetPhysicsScene ().Simulate (Time.fixedDeltaTime);
    }

    public void ResisterGrenade (Grenade grenade) {
        if (!allGrenades.ContainsKey (grenade.gameObject.name)) {
            allGrenades[grenade.gameObject.name] = new RegisteredGrenades ();
        }
        var grenades = allGrenades[grenade.gameObject.name];
        if (string.Compare (grenade.gameObject.scene.name, physicsScene.name) == 0) {
            grenades.hidden = grenade;
        } else {
            grenades.real = grenade;
        }

        allGrenades[grenade.gameObject.name] = grenades;
    }

    public void PreparePhysicsScene () {
        SceneManager.SetActiveScene (physicsScene);
        GameObject g = GameObject.Instantiate (objectsToSpawn);
        g.transform.name = "ReferenceGrenade";
        g.GetComponent<Grenade> ().isReference = true;
        Destroy (g.GetComponent<MeshRenderer> ());

        SceneManager.SetActiveScene (mainScene);
    }

    public void CreateMovementMarkers () {
        foreach (var grenadeType in allGrenades) {
            var grenades = grenadeType.Value;
            Grenade hidden = grenades.hidden;

            GameObject g = GameObject.Instantiate (marker, hidden.transform.position, Quaternion.identity);
            g.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
            markers.Add (g);
        }
    }

    public void ShowTrajectory () {
        SyncGrenades ();
        ClearTrajectory ();

        allGrenades["ReferenceGrenade"].hidden.transform.rotation = referenceGrenade.transform.rotation;
        allGrenades["ReferenceGrenade"].hidden.GetComponent<Rigidbody> ().velocity = referenceGrenade.transform.TransformDirection (Vector3.up * 30f);
        allGrenades["ReferenceGrenade"].hidden.GetComponent<Rigidbody> ().useGravity = true;

        int steps = (int) (2f / Time.fixedDeltaTime);
        for (int i = 0; i < steps; i++) {
            physicsScene.GetPhysicsScene ().Simulate (Time.fixedDeltaTime);
            CreateMovementMarkers ();
        }
    }

    public void SyncGrenades () {
        foreach (var grenadeType in allGrenades) {
            var grenades = grenadeType.Value;

            Grenade visual = grenades.real;
            Grenade hidden = grenades.hidden;
            var rb = hidden.GetComponent<Rigidbody> ();

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            hidden.transform.position = visual.transform.position;
            hidden.transform.rotation = visual.transform.rotation;
        }
    }

    public void ClearTrajectory () {
        foreach (var GO in markers) {
            Destroy (GO);

        }
        markers.Clear ();
    }
}