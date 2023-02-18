using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    float timer;
    public float delay = 0.2f;

    private void Start()
    {
        timer = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Time.time - timer >= delay) {
            FindObjectOfType<AudioManager>().Play("carrot impact");
            timer = Time.time;
        }
    }
}
