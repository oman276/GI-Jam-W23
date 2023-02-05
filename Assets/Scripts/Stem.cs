using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stem : MonoBehaviour
{
    PlayerMovement pm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            pm = collision.gameObject.GetComponent<PlayerMovement>();
            pm.getStem(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pm = collision.gameObject.GetComponent<PlayerMovement>();
            pm.dropStem(this.gameObject);
        }
    }
}
