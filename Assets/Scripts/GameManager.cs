using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    StageGenerator stageGenerator;

    public float roundLength = 5f;
    float prevTime;

    void Start()
    {
        prevTime = Time.time;
        stageGenerator = FindObjectOfType<StageGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - prevTime >= roundLength) {
            stageGenerator.cycle();
            prevTime = Time.time;
        }
    }
}
