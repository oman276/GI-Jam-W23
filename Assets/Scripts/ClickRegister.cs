using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickRegister : MonoBehaviour
{
    public string thisName;
    CharacterSelectManager csm;

    private void Start()
    {
        csm = FindObjectOfType<CharacterSelectManager>();
    }

    private void OnMouseDown()
    {
        csm.ObjectClicked(thisName);
    }
}
