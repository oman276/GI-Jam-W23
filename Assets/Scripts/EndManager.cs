using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    string winner = "[WINNER]";
    string loser = "[LOSER]";

    public float delay = 0.05f;

    public TextMeshProUGUI mainBox;
    public GameObject button;

    int state = 0;

    string[] lines = new string[4];

    public GameObject textObj;
    public GameObject nextButtons;

    // Start is called before the first frame update
    void Start()
    {
        //FindObjectOfType<AudioManager>().Stop("theme");
        FindObjectOfType<AudioManager>().Play("love theme");

        //Note: this is dumb
        AudioManager am = FindObjectOfType<AudioManager>();
        if (am)
        {
            winner = am.winner;
            loser = am.loser;
        }
        else {
            Debug.LogWarning("No AM found");
        }

        textObj.SetActive(true);
        nextButtons.SetActive(false);

        //Get winner/loser here

        lines[0] = "Oh! " + winner + "! I didn't know you were attractored to me!";
        lines[1] = "By extractoring these roses from the ground, you have proven to be the cream of the crops!";
        lines[2] = "But " + loser + "... This is the last straw! You have been thinned from the field of competitors!";
        lines[3] = "Come, "+ winner +", my Deere! Kiss me on my large, spinning, deadly blades!";

        StartCoroutine(writeString(lines[0]));
    }


    public IEnumerator writeString(string text) {
        button.SetActive(false);
        string targetText = "";
        mainBox.text = targetText;
        foreach (char c in text){
            yield return new WaitForSeconds(delay);
            targetText += c;
            mainBox.text = targetText;
        }
        button.SetActive(true);
    }

    public void Next() {
        ++state;
        if (state >= 4)
        {
            textObj.SetActive(false);
            nextButtons.SetActive(true);
        }
        else
        {
            StartCoroutine(writeString(lines[state]));
        }
    }

    public void BackToMenu() {
        FindObjectOfType<AudioManager>().Stop("love theme");
        FindObjectOfType<AudioManager>().Play("theme");
        SceneManager.LoadScene("Menu");

    }

    public void BackToGame() {
        FindObjectOfType<AudioManager>().Stop("love theme");
        FindObjectOfType<AudioManager>().Play("theme");
        SceneManager.LoadScene("SampleScene");
    }
}
