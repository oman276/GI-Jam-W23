using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endAnimation : MonoBehaviour
{

    public GameObject endText;

    public GameObject buttonone;

    public GameObject buttontwo;
    public GameObject crossFade;
    public bool isGameOver = false;

    public int whichplayer = 1;

    public Animator animator;

    public Animator crossFader;

    bool doitonce = false;

    public bool endofAnimation = false;

    // Start is called before the first frame update
    void Start()
    {
        crossFader = GetComponentInChildren<Animator> ();

        endText.SetActive(false);
        buttonone.SetActive(false);
        buttontwo.SetActive(false);
        crossFade.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if(isGameOver == true && doitonce == false) {
            doitonce = true;
            StartCoroutine(MyCoroutine());
        }
        
    }

      private IEnumerator MyCoroutine() {
        crossFade.SetActive(true);
        if(whichplayer == 1) {
            animator.Play("farmereyes");
            yield return new WaitForSeconds(0.2f);
            crossFader.Play("crossfade_end");
            yield return new WaitForSeconds(2.0f);
            animator.Play("endeyes");
        } else {
            animator.Play("wolfeyes");
            yield return new WaitForSeconds(0.2f);
            crossFader.Play("crossfade_end");
            yield return new WaitForSeconds(2.0f);
            animator.Play("endeyes");
        }
        yield return new WaitForSeconds(3.0f);
        endText.SetActive(true);
        buttonone.SetActive(true);
        buttontwo.SetActive(true);

        endofAnimation = true;
    }
}
