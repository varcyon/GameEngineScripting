﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour {
    public int health;

    private void Start () {
        health = 10;
    }

    public void Hurt (int damage) {
        health -= damage;
        Debug.Log ("Health: " + health);
    }
}