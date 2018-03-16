using UnityEngine;
using System.Collections;

public class Collision : MonoBehaviour
{
    public Transform SheetLines;

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col);
        Transform line = Instantiate(SheetLines) as Transform;
        if (col.gameObject.tag == "player" && Input.GetKeyDown(KeyCode.S))
        {
            Physics2D.IgnoreCollision(line.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            Debug.Log("S");

        }
    }
}