using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
    public GameObject rose;
    public GameObject barrier;

    public int minStems = 2;
    public int maxStems = 5;

    public int minBarriers = 4;
    public int maxBarriers = 8;

    List<(int, int)> coordinateList;
    List<GameObject> activeObjects;

    // Start is called before the first frame update
    void Start()
    {
        activeObjects = new List<GameObject>();

        spawnpoints = new List<List<Vector2>>();
        GameObject empty = new GameObject();

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

        generate();
    }

    void generate() {
        coordinateList = new List<(int, int)>();
        int row = Random.Range(0, rows);
        int col = Random.Range(0, columns);
        coordinateList.Add((col, row));
        GameObject ls = Instantiate(rose, spawnpoints[row][col], Quaternion.identity);
        activeObjects.Add(ls);

        int numOfStems = Random.Range(minStems, maxStems + 1);
        for (int i = 0; i < numOfStems; ++i)
        {
            row = Random.Range(0, rows);
            col = Random.Range(0, columns);

            if (!coordinateList.Contains((col, row)))
            {
                coordinateList.Add((col, row));
                GameObject s = Instantiate(stem, spawnpoints[row][col], Quaternion.identity);
                activeObjects.Add(s);
            }
            else
            {
                --i;
            }
        }

        int numOfBarriers = Random.Range(minBarriers, maxBarriers + 1);
        for (int i = 0; i < numOfBarriers; ++i)
        {
            row = Random.Range(0, rows);
            col = Random.Range(0, columns);

            if (!coordinateList.Contains((col, row)))
            {
                coordinateList.Add((col, row));
                GameObject b = Instantiate(barrier, spawnpoints[row][col], Quaternion.identity);
                activeObjects.Add(b);
            }
            else
            {
                --i;
            }
        }
    }

    void wipe() {
        foreach (GameObject g in activeObjects) {
            Destroy(g);
        }
        activeObjects.Clear();
    }

    public void cycle() {
        wipe();
        generate();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            cycle();
        }
    }
}
