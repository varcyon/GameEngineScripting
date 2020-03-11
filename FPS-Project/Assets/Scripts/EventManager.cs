using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager: MonoBehaviour {
    public delegate void ButtonAction();
    public static event ButtonAction WeaponSwap;
    public GameObject boss;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update() {
        if (Input.GetMouseButtonDown(1)) {
            if (WeaponSwap != null) 
                WeaponSwap();
        }
        if(boss == null){
            Win();
        }
    }
    IEnumerator Win(){
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(3);
    }


}
