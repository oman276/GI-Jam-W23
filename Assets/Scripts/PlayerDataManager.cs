using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager instance;

    public string player1name = "";
    public string player2name = "";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
