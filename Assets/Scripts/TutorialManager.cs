using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    //STAGE 1: WASD 
    //STAGE 2: Grab Rose
    //STAGE 3: Pick up carrot
    //Wall goes down from 3->4
    //STAGE 4: Throw Carrot
    //STAGE 5: Melee
    //STAGE 6: Begin Game
    public int stage = 1;
    (int p1, int p2) triggered = (0, 0);

    public TextMeshProUGUI p1Text;
    public TextMeshProUGUI p2Text;
    public TextMeshProUGUI centeredText;
    public GameObject nextButton;

    public GameObject p1Rose;
    public GameObject p2Rose;
    
    public GameObject p1Stem;
    public GameObject p2Stem;

    public GameObject dividerWall;

    private void Start()
    {
        p1Text.text = "use WASD to move";
        p2Text.text = "use IJKL or ARROW KEYS to move";

        p1Text.gameObject.SetActive(true);
        p2Text.gameObject.SetActive(true);
        centeredText.gameObject.SetActive(false);
        nextButton.SetActive(false);

        p1Rose.SetActive(false);
        p2Rose.SetActive(false);
        
        p1Stem.SetActive(false);
        p2Stem.SetActive(false);

        dividerWall.SetActive(true);
    }

    void moveUpStage() {
        ++stage;
        triggered.p1 = 0;
        triggered.p2 = 0;

        if (stage == 2)
        {
            p1Text.text = "hold Q or R when standing on rose to uproot";
            p2Text.text = "hold U or O or SPACEBAR when standing on rose to uproot";

            p1Rose.SetActive(true);
            p2Rose.SetActive(true);
        }
        else if (stage == 3)
        {
            p1Text.text = "press Q or R when standing near stem to uproot the carrot";
            p2Text.text = "press U or O or SPACEBAR when standing near stem to uproot the carrot";

            p1Stem.SetActive(true);
            p2Stem.SetActive(true);
        }
        else if (stage == 4)
        {
            p1Text.text = "press Q or R when holding a carrot to throw at your enemy";
            p2Text.text = "press U or O or SPACEBAR when holding a carrot to throw at your enemy";

            dividerWall.SetActive(false);
        }

        else if (stage == 5)
        {
            p1Text.text = "press Q or R when near an enemy to melee";
            p2Text.text = "press U or O or SPACEBAR when near an enemy to melee";
        }

        else if (stage == 6) {
            p1Text.gameObject.SetActive(false);
            p2Text.gameObject.SetActive(false);

            centeredText.text = "Uproot five roses before your opponent to win!";
            centeredText.gameObject.SetActive(true);

            nextButton.SetActive(true);
        }
    }


    public void WASDactivated(bool player1) {
        if (stage == 1) {
            if (player1)
            {
                triggered.p1 = 1;
            }
            else {
                triggered.p2 = 1;
            }

            if (triggered.p1 == 1 && triggered.p2 == 1) {
                moveUpStage();
            }
        }
    }

    public void roseActivated(bool player1)
    {
        if (stage == 2)
        {
            if (player1)
            {
                triggered.p1 = 1;
            }
            else
            {
                triggered.p2 = 1;
            }

            if (triggered.p1 == 1 && triggered.p2 == 1)
            {
                moveUpStage();
            }
        }
    }

    public void stemActivated(bool player1)
    {
        if (stage == 3)
        {
            if (player1)
            {
                triggered.p1 = 1;
            }
            else
            {
                triggered.p2 = 1;
            }

            if (triggered.p1 == 1 && triggered.p2 == 1)
            {
                moveUpStage();
            }
        }
    }

    public void throwActivated(bool player1)
    {
        if (stage == 4)
        {
            if (player1)
            {
                triggered.p1 = 1;
            }
            else
            {
                triggered.p2 = 1;
            }

            if (triggered.p1 == 1 && triggered.p2 == 1)
            {
                moveUpStage();
            }
        }
    }

    public void meleeActivated(bool player1)
    {
        if (stage == 5)
        {
            if (player1)
            {
                triggered.p1 = 1;
            }
            else
            {
                triggered.p2 = 1;
            }

            if (triggered.p1 == 1 && triggered.p2 == 1)
            {
                moveUpStage();
            }
        }
    }

    public void startGame() {
        SceneManager.LoadScene("SampleScene");
    }
}
