using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClefColumn_Code : MonoBehaviour
{
    // SpriteRenderer of the "key" object that shows the code sequence of the door
    public SpriteRenderer keySR;

    // sprites of the key object
    public Sprite[] sprites;

    // default grey sprite for the key object
    public Sprite defaultSprite;

    // the code sequence that opens the door
    private int[] _closedDoorCode;

    // index of the current key object
    private int _currentIndex;

    // Transform of the Highlighter
    public Transform highlighterTF;

    // script of this column
    private ClefColumn_PlayCode _columnScript;

    void Start()
    {
        _columnScript = GetComponentInParent<ClefColumn_PlayCode>();

        // point to the first key of the code sequence
        _currentIndex = 0;

        updateHighlighterPosition();
        keySR.color = Color.grey;
        highlighterTF.gameObject.SetActive(false);
    }

    public void startShowingCode(int[] theCode)
    {
        _closedDoorCode = theCode;
        // Debug.Log("Clef showing the code, it's: [ " + _closedDoorCode[0] + " " + _closedDoorCode[1] + " " + _closedDoorCode[2] + " " + _closedDoorCode[3] + " ]");
        
        // enable the highlighter
        // highlighterTF.gameObject.SetActive(true);

        // start showing the code
        StartCoroutine(showSecretCode());
    }

    IEnumerator showSecretCode()
    {
        while (!_columnScript.isDoorOpen())
        {
            // update key object with each sprite from the code
            foreach (int index in _closedDoorCode)
            {
                // update the sprite
                keySR.sprite = sprites[index];
                keySR.color = Color.white;

                // wait for some time
                yield return new WaitForSeconds(0.7f);

                // change sprite to default before showing the next one
                keySR.sprite = defaultSprite;
                keySR.color = Color.grey;

                // wait for some time
                yield return new WaitForSeconds(0.128f);
            }

            // short pause before showing the code again
            yield return new WaitForSeconds(0.828f);
        }
    }

    // changes current index to the next key
    private void nextKey()
    {
        // increment index
        _currentIndex += 1;

        // if reached end of sequence - go back to beginning
        if (_currentIndex > 3)
        {
            _currentIndex = 0;
        }

        updateHighlighterPosition();
    }

    public SpriteRenderer getKeySR()
    {
        return keySR;
    }

    private void updateHighlighterPosition()
    {
        highlighterTF.position = keySR.transform.position;
    }
}
