using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerMovement : MonoBehaviour
{

    private BoxCollider2D boxCollider;
    //Rigidbody2D rb;

    private Vector2 velocity;
    Vector2 lastVec;

    GameObject activeStem = null;

    bool objectHeld = false;
    public GameObject heldCarrot;
    public GameObject thrownCarrot;

    public float spawnDistance = 0.3f;
    public float throwForce = 10f;

    public LayerMask playerMask;
    public float pushDistance = 0.3f;
    public float pushRadius = 0.5f;
    public float pushforce = 5f;
    public float pushDelay = 0.15f;

    public float speed = 200f;

    public float moveSpeed = 110f;

    float moveLimiter = 0.7f;

    public Rigidbody2D rb;

    public Animator animator;

    Vector2 movement;

    StageGenerator sg;
    GameManager gm;


    bool activeRose = false;
    float roseStartTime;
    public float roseDelay = 2f;

    Rose currentRose;

    bool movementEnabled = true;

    /// <summary>
    /// Set to true when the character intersects a collider beneath
    /// them in the previous frame.
    /// </summary>
    private bool grounded;

    public bool player1 = true;
    string hAxis;
    string vAxis;

    bool pressbool() {
        if (player1)
        {
            return Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E);
        }
        else { 
            return Input.GetKeyDown(KeyCode.U) || Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.Space);
        }
    }

    bool holdbool()
    {
        if (player1)
        {
            return Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E);
        }
        else
        {
            return Input.GetKey(KeyCode.U) || Input.GetKey(KeyCode.O) || Input.GetKey(KeyCode.Space);
        }
    }

    private void Awake()
    {      
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        heldCarrot.SetActive(false);
        sg = FindObjectOfType<StageGenerator>();
        gm = FindObjectOfType<GameManager>();
        
        if (player1)
        {
            hAxis = "Horizontal";
            vAxis = "Vertical";
        }
        else {
            hAxis = "Horizontal 2";
            vAxis = "Vertical 2";
        }
    }

    private void Update()
    {
        // MOVEMENT
        movement.x = Input.GetAxisRaw(hAxis);
        movement.y = Input.GetAxisRaw(vAxis);
  
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.magnitude);

        // ITEM GRAB/THROW
        if (pressbool())
        {
            //If standing next to a rose, grab the rose
            if (activeRose)
            {
                roseStartTime = Time.time;
                currentRose.timer.value = roseDelay;
            }
            //Otherwise, if standing next to a stem amd not holding a carrot
            else if (activeStem && !objectHeld)
            {
                Destroy(activeStem);
                objectHeld = true;
                heldCarrot.SetActive(true);
            }
            //Otherwise, if an object is held, throw it
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
            //Otherwise, check to punch
            else {
                Vector2 checkPos = new Vector2(this.transform.position.x, this.transform.position.y - 0.05f) +
                    (lastVec.normalized * pushDistance);
                Collider2D result = Physics2D.OverlapCircle(checkPos, pushRadius, playerMask);
                if (result) {
                    result.gameObject.GetComponent<PlayerMovement>().StartCoroutine("RestrictMovement");
                    result.gameObject.GetComponent<Rigidbody2D>().AddForce(lastVec.normalized * pushforce);
                }
            }
        }

        //HELD ACTIVATION KEY for rose
        if (holdbool() && activeRose)
        {

            if (currentRose)
            {
                currentRose.timer.value = (roseDelay - (Time.time - roseStartTime));
            }
            if (Time.time - roseStartTime >= roseDelay)
            {
                if (player1)
                {
                    gm.pluckedby1();
                }
                else
                {
                    gm.pluckedby2();
                }
                sg.cycle();
                activeRose = false;
                gm.prevTime = Time.time;
            }
        }
        else if (holdbool() && !activeRose) {
            roseStartTime = Time.time;
        }
    }

    void FixedUpdate()
    {
        if (movement.x != 0 && movement.y != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            movement.x *= moveLimiter;
            movement.y *= moveLimiter;
        }

        if (movementEnabled)
        {
            rb.velocity = movement * moveSpeed * Time.fixedDeltaTime;
        }

        if (movement != Vector2.zero)
        {
            lastVec = movement;
        }
    }

    public IEnumerator RestrictMovement()
    {
        movementEnabled = false;
        yield return new WaitForSeconds(pushDelay);
        movementEnabled = true;
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

    public void getRose(Rose r) {
        activeRose = true;
        currentRose = r;
    }

    public void dropRose(Rose r) {
        if (activeRose == r) {
            activeRose = false;
            currentRose = null;
        }
    }
}