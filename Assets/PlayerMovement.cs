using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerMovement : MonoBehaviour
{

    private BoxCollider2D boxCollider;
    Rigidbody2D rb;

    private Vector2 velocity;
    Vector2 lastVec;

    GameObject activeStem = null;

    bool objectHeld = false;
    public GameObject heldCarrot;
    public GameObject thrownCarrot;

    public float spawnDistance = 0.3f;
    public float throwForce = 10f;

    StageGenerator sg;

    /// <summary>
    /// Set to true when the character intersects a collider beneath
    /// them in the previous frame.
    /// </summary>
    private bool grounded;


    public bool player1 = true;


    private void Awake()
    {      
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        heldCarrot.SetActive(false);
        sg = FindObjectOfType<StageGenerator>();
    }

    private void Update()
    {
        // MOVEMENT
        float moveInputX = Input.GetAxisRaw("Horizontal");
        float moveInputY = Input.GetAxisRaw("Vertical");

        velocity.x = moveInputX * speed * Time.deltaTime;
        velocity.y = moveInputY * speed * Time.deltaTime;

        rb.velocity = velocity;
        if (velocity != Vector2.zero) {
            lastVec = velocity;
        }


        //ITEM GRAB/THROW
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)) {
            if (activeStem && !objectHeld) {
                Destroy(activeStem);
                objectHeld = true;
                heldCarrot.SetActive(true);
            }
            else if (objectHeld)
            {
                objectHeld = false;
                heldCarrot.SetActive(false);

                Vector2 spawnPos = new Vector2(this.transform.position.x, this.transform.position.y) + 
                    (lastVec.normalized * spawnDistance);
                GameObject tc = Instantiate(thrownCarrot, spawnPos, Quaternion.identity);
                tc.GetComponent<Rigidbody2D>().AddForce(lastVec.normalized * throwForce);
                sg.activeObjects.Add(tc);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject.tag);
    }

    public void getStem(GameObject s) {
        activeStem = s;
    }
    public void dropStem(GameObject s)
    {
        if (activeStem == s) {
            activeStem = null;
        }
    }
}