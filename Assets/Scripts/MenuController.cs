using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    AudioManager am;
    
    private void Start()
    {
        
    }

    public void startGame() {
        SceneManager.LoadScene("Cutscene");
    }

    public void enterCharacterSelect() {
        SceneManager.LoadScene("CharacterSelect");
    }
}
