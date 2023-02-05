using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rose : MonoBehaviour
{
    PlayerMovement pm;
    public GameObject timeBar;
    public Slider timer;

    private void Start()
    {
        timer = timeBar.GetComponent<Slider>();
        timeBar.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pm = collision.gameObject.GetComponent<PlayerMovement>();
            pm.getRose(this);
            timer.maxValue = pm.roseDelay;
            timer.value = pm.roseDelay;
            timeBar.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pm = collision.gameObject.GetComponent<PlayerMovement>();
            pm.dropRose(this);
            timeBar.SetActive(false);
        }
    }
}
