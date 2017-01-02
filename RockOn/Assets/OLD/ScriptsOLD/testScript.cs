using System.Collections;
using UnityEngine;

public class testScript : MonoBehaviour
{

    Transform player;
    Transform enemy;
    LineRenderer lr;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemy = GetComponent<Transform>();
        lr = GetComponent<LineRenderer>();
    }

    private void OnMouseOver()
    {
        
        Debug.Log("mouse over ");
        // Debug.DrawLine(player.position, enemy.position, Color.cyan, 1.0f);
        if (Input.GetButtonDown("Regular_Attack"))
        {

            lr.SetPosition(0, player.position);
            lr.SetPosition(1, enemy.position);

            Debug.Log("click");
            //Debug.DrawLine(player.position, enemy.position, Color.cyan, 1.0f);


            StartCoroutine("removeLine");
        }
         

    }

    IEnumerator removeLine()
    {
        for (float f = 0.0f; f<=0.15f; f+=Time.deltaTime)
        {
            lr.SetPosition(0, player.position);
            lr.SetPosition(1, enemy.position);
            yield return null;
        }

        lr.SetPosition(0, player.position);
        lr.SetPosition(1, player.position);

    }
}