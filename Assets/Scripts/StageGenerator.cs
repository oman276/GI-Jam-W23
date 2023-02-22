using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageGenerator : MonoBehaviour
{


    public GameObject playerObj1;
    public GameObject playerObj2;

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


    public BoxCollider2D playeroneCollider;
    public BoxCollider2D playertwoCollider;

    public BoxCollider2D roseCollider;

    List<(int, int)> coordinateList;
    public List<GameObject> activeObjects;

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

        
        int row = Random.Range(1, rows-1);
        int col = Random.Range(1, columns-1);
        coordinateList.Add((col, row));

        float distanceOne = Vector2.Distance(playerObj1.transform.position, spawnpoints[row][col]);
        float distanceTwo = Vector2.Distance(playerObj2.transform.position, spawnpoints[row][col]);

        while(distanceOne <= 2.0f || distanceTwo <= 2.0f)
        {
            coordinateList.Remove((col, row));
            row = Random.Range(1, rows-1);
            col = Random.Range(1, columns-1);  
            coordinateList.Add((col, row));
            distanceOne = Vector2.Distance(playerObj1.transform.position, spawnpoints[row][col]);
            distanceTwo = Vector2.Distance(playerObj2.transform.position, spawnpoints[row][col]);
        }

        GameObject r = Instantiate(rose, spawnpoints[row][col], Quaternion.identity);

        activeObjects.Add(r);


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

            distanceOne = Vector2.Distance(playerObj1.transform.position, spawnpoints[row][col]);
            distanceTwo = Vector2.Distance(playerObj2.transform.position, spawnpoints[row][col]);

            int temp = 0;

            while(distanceOne <= 1.5f || distanceTwo <= 1.5f )
            {
                temp++;
                row = Random.Range(0, rows);
                col = Random.Range(0, columns);
                distanceOne = Vector2.Distance(playerObj1.transform.position, spawnpoints[row][col]);
                distanceTwo = Vector2.Distance(playerObj2.transform.position, spawnpoints[row][col]);
            }

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
        FindObjectOfType<AudioManager>().Play("round over");
        wipe();
        generate();
    }

}
