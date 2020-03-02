using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI scoreLabel;
    [SerializeField] private MemoryCard originalCard;
    [SerializeField] private Sprite[] images;

    [SerializeField] private Sprite[] deckOne;
    [SerializeField] private Sprite[] deckTwo;

    private MemoryCard _firstRevealed;
    private MemoryCard _seconRevealed;
    private int _score = 0;

    public bool canReveal {
        get { return _seconRevealed == null; }
    }
    public const int gridRows = 4;
    public const int gridCols = 4;
    public const float offsetX = 1.3f;
    public const float offsetY = 1.5f;

    void Start () {
        images = deckTwo;

        Vector3 startPos = originalCard.transform.position;

        int[] numbers = { 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3 };
        numbers = ShuffleArray (numbers);

        int cardNum = 1;
        for (int i = 0; i < gridCols; i++) {
            for (int j = 0; j < gridRows; j++) {
                MemoryCard card;
                if (i == 0 && j == 0) {
                    card = originalCard;
                } else {
                    card = Instantiate (originalCard, new Vector3 ((i * offsetX) + startPos.x, -(j * offsetY) + startPos.y, startPos.z), Quaternion.identity);
                }

                //   int index = j*gridCols+1 ;
                //Because this index from the book isnt working im using a cardNum
                int id = numbers[cardNum];
                cardNum++;
                card.SetCard (id, images[id], images[0]);
            }
        }
    }

    private int[] ShuffleArray (int[] numbers) {
        int[] newArray = numbers.Clone () as int[];
        for (int i = 0; i < newArray.Length; i++) {
            int tmp = newArray[i];
            int r = Random.Range (i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    public void CardRevealed (MemoryCard card) {
        if (_firstRevealed == null) {
            _firstRevealed = card;
        } else {
            _seconRevealed = card;
            Debug.Log ("Match? " + (_firstRevealed.id == _seconRevealed.id));
            StartCoroutine (CheckMatch ());
        }
    }

    private IEnumerator CheckMatch () {
        if (_firstRevealed.id == _seconRevealed.id) {
            _score++;
            scoreLabel.text = "Score: " + _score;
        } else {
            yield return new WaitForSeconds (0.5f);
            _firstRevealed.Unreveal ();
            _seconRevealed.Unreveal ();
        }
        _firstRevealed = null;
        _seconRevealed = null;
    }

    public void Restart () {
        SceneManager.LoadScene (0);
    }
}