using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterSelectManager : MonoBehaviour
{
    PlayerDataManager pdm;
    bool player1select = true;
    public TextMeshProUGUI mainText;
    AudioManager am;

    // Start is called before the first frame update
    void Start()
    {
        pdm = FindObjectOfType<PlayerDataManager>();
        mainText.text = "SELECT CHARACTER: Player One";
        am = FindObjectOfType<AudioManager>();
    }

    public void ObjectClicked(string name) {
        if (player1select)
        {
            player1select = false;
            pdm.player1name = name;
            mainText.text = "SELECT CHARACTER: Player Two";
            am.Play("press");
        }
        else {
            am.Play("press");
            pdm.player2name = name;
            SceneManager.LoadScene("SampleScene");
        }
    }
    
}
