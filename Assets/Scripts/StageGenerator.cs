using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageGenerator : MonoBehaviour
{
    public Transform topLeftTrans;
    public Transform bottomLeftTrans;
    public Transform topRightTrans;

    Vector2 topLeft;
    Vector2 bottomLeft;
    Vector2 topRight;

    public int rows = 5;
    public int columns = 5;

    List<List<Vector2>> spawnpoints;

    public GameObject stem;
    public int minStems = 2;
    public int maxStems = 5;

    class CoordinatePair {
        public int first;
        public int second;

        public CoordinatePair(int _first, int _second) {
            first = _first;
            second = _second;
        }

        public bool Equals(CoordinatePair cp)
        {
            return this.first == cp.first && this.second == cp.second;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        spawnpoints = new List<List<Vector2>>();
        GameObject empty = new GameObject();

        CoordinatePair cp1 = new CoordinatePair(2, 4);
        CoordinatePair cp2 = new CoordinatePair(2, 4);

        if (cp1 == cp2)
        {
            print("cp1, cp2 equal");
        }
        else {
            print("cp1, cp2 not equal");
        }


        CoordinatePair cp3 = new CoordinatePair(2, 4);
        CoordinatePair cp4 = new CoordinatePair(2, 6);

        if (cp3 == cp4)
        {
            print("cp3, cp4 equal");
        }
        else
        {
            print("cp3, cp4 not equal");
        }


        topLeft = topLeftTrans.position;
        bottomLeft = bottomLeftTrans.position;
        topRight = topRightTrans.position;

        if (rows <= 0 || columns <= 0) {
            Debug.LogError("Rows or Columns cannot be <= zero");
        }

        float vertDistance = (topLeft.y - bottomLeft.y) / (rows - 1);
        float horizDistance = (topRight.x - topLeft.x) / (columns -1);

        for (int i = 0; i < rows; ++i) {
            spawnpoints.Add(new List<Vector2>());
            for (int j = 0; j < columns; ++j) {
                Vector2 pos = new Vector2(topLeft.x + (j * horizDistance), 
                    topLeft.y - (i * vertDistance));
                spawnpoints[i].Add(pos);
            }
        }

        int numOfStems = Random.Range(minStems, maxStems + 1);
        for (int i = 0; i < numOfStems; ++i) { 
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
