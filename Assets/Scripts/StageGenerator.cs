using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    public Transform topLeft;
    public Transform bottomLeft;
    public Transform topRight;

    public int rows = 5;
    public int columns = 5;

    List<List<Transform>> spawnpoints;
    GameObject empty = new GameObject();

    // Start is called before the first frame update
    void Start()
    {
        if (rows <= 0 || columns <= 0) {
            Debug.LogError("Rows or Columns cannot be <= zero");
        }

        float vertDistance = (topLeft.position.y - bottomLeft.position.y) / (rows - 1);
        float horizDistance = (topRight.position.x - topLeft.position.x) / (columns -1);

        for (int i = 0; i < rows; ++i) {
            for (int j = 0; j < columns; ++j) { 
                //Transform pos = 
                //Instantiate(empty, )
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
