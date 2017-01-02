using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest_Open : MonoBehaviour
{
    // items that can be in the chest
    public GameObject[] treasures;

    // this object's animator
    private Animator _anim;

    // this Object's SpriteRenderer and Transform
    private SpriteRenderer _sr;
    private Transform _tf;

    // code script of this chest
    private Chest_Code _codeScript;

    // Use this for initialization
    void Start()
    {
        _anim = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
        _tf = GetComponent<Transform>();
        _codeScript = GetComponentInChildren<Chest_Code>();
    }

    public void hitChest()
    {
        _codeScript.checkCurrentKey();
    }

    public void openChest()
    {
        StartCoroutine("chestInteraction");
    }

    private void spawnTreasure()
    {
        int index = Random.Range(0, treasures.Length);
        Instantiate(treasures[index], gameObject.transform.position, Quaternion.identity);
    }

    IEnumerator chestInteraction()
    {
        // small delay before opening
        for (float f = 0.1f; f >= 0; f -= Time.deltaTime)
        {
            yield return null;
        }

        //open the chest
        _anim.SetTrigger("open");

        // delay before disposing of the chest
        for (float f = 1.0f; f >= 0; f -= Time.deltaTime)
        {
            yield return null;
        }

        // spawn the treasure
        spawnTreasure();

        // fade the chest
        Color c = _sr.color;
        for (float f = 1.0f; f >= 0.0f; f -= Time.deltaTime)
        {
            c.a = f;
            _sr.color = c;
            yield return null;
        }

        // there's some bugs when destroying the object immediately,
        // so I'm moving it somewhere else and killing it after a second
        _tf.position = new Vector3(-10000, -10000, -10000);

        for (float f = 1.0f; f >= 0; f -= Time.deltaTime)
        {
            yield return null;
        }
        Destroy(gameObject);
    }
}
