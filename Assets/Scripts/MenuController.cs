using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    AudioManager am;

    public void LoadGame() {
        FindObjectOfType<AudioManager>().Play("press");
        SceneManager.LoadScene("SampleScene");
    }

    public void startGame() {
        FindObjectOfType<AudioManager>().Play("press");
        SceneManager.LoadScene("Cutscene");
    }

    public void enterCharacterSelect() {
        FindObjectOfType<AudioManager>().Play("press");
        SceneManager.LoadScene("CharacterSelect");
    }
}
