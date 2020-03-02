using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour {
    [SerializeField] private GameObject cardBack;
    [SerializeField] private Sprite image;
    [SerializeField] private SceneController controller;

    private int _id;
    public int id {
        get { return _id; }
    }

    public void OnMouseDown () {
        if (cardBack.activeSelf && controller.canReveal) {
            cardBack.SetActive (false);
            controller.CardRevealed (this);
        }
    }

    public void Unreveal () {
        cardBack.SetActive (true);
    }

    public void SetCard (int id, Sprite image, Sprite cb) {
        _id = id;
        cardBack.GetComponent<SpriteRenderer>().sprite = cb;
        GetComponentInChildren<SpriteRenderer> ().sprite = image;
    }
}