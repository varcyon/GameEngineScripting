using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
   public void StartGame(){
       SceneManager.LoadScene(1);
    }

    public void BackToMain(){
        SceneManager.LoadScene(0);
    }

    public void Quit(){
        Application.Quit();
    }
}
