using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest_Open : MonoBehaviour
{
    // nice things that can be in the chest
    public GameObject[] goodTreasures;

    // not so nice things that can be in the chest
    public GameObject[] badTreasures;

    // this object's animator
    private Animator _anim;

    // this Object's SpriteRenderer and Transform
    private SpriteRenderer _sr;
    private Transform _tf;

    // code script of this chest
    private Chest_Code _codeScript;

    // sprite renderers of the keys objects
    private SpriteRenderer[] _keysSR;

    // you can specify in the Inspector a number of tries after which a bad treasure will spawn
    public int numOfTries;

    // you can specify what secret code it spawns with in the Inspector
    // 0 = red, 1 = green, 2 = blue, anything else = random
    public int[] secretCode;

    // Use this for initialization
    void Start()
    {
        _anim = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
        _tf = GetComponent<Transform>();
        _codeScript = GetComponentInChildren<Chest_Code>();

        _codeScript.setChestCode(secretCode);
    }

    // public method for easier calling of the method in codeScript
    public void hitChest()
    {
        _codeScript.hitChest();
    }

    public void openChest(bool goodGuess)
    {
        StartCoroutine(openChestCoroutine(goodGuess));
    }

    private void spawnTreasure(bool goodGuess)
    {
        // if player guessed quickly, spawn a good treasure
        if (goodGuess)
        {
            int index = Random.Range(0, goodTreasures.Length);
            Instantiate(goodTreasures[index], gameObject.transform.position, Quaternion.identity);
        }
        else
        {
            int index = Random.Range(0, badTreasures.Length);
            Instantiate(badTreasures[index], gameObject.transform.position, Quaternion.identity);
        }
    }

    // opens the chest, spawn a treasure and dosposes of the chest object
    IEnumerator openChestCoroutine(bool goodGuess)
    {
        // small delay before opening
        yield return new WaitForSeconds(0.1f);

        // play opening animation
        _anim.SetTrigger("open");

        // delay for animation to end
        yield return new WaitForSeconds(0.67f);

        // spawn the treasure
        spawnTreasure(goodGuess);

        // fade the chest to make it disappear
        Color c = _sr.color;
        _keysSR = _codeScript.getKeysSR();
        for (float f = 1.0f; f >= 0.25f; f -= Time.deltaTime)
        {
            c.a = f;
            _sr.color = c;
            _keysSR[0].color = c;
            _keysSR[1].color = c;
            _keysSR[2].color = c;
            yield return null;
        }

        // there's some bugs when destroying the object immediately,
        // so I'm moving it somewhere else and killing it after a second
        _tf.position = new Vector3(-10000, -10000, -10000);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject, 0.5f);
    }

    public int getNumOfTries()
    {
        return numOfTries;
    }
}
