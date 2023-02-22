// using UnityEngine;
// using System.Collections;

// [RequireComponent(typeof(BoxCollider2D))]
// public class PlayerMovement : MonoBehaviour
// {

//     public bool spawnCondition = false;
//     public GameObject playerObj;
//     private BoxCollider2D boxCollider;
//     //Rigidbody2D rb;

//     private Vector2 velocity;
//     Vector2 lastVec;

//     GameObject activeStem = null;

//     bool objectHeld = false;
//     public GameObject heldCarrot;
//     public GameObject thrownCarrot;

//     public float spawnDistance = 0.3f;
//     public float throwForce = 10f;

//     public GameObject makeSlash;
    
//     public LayerMask playerMask;
//     public float pushDistance = 0.3f;
//     public float pushRadius = 0.5f;
//     public float pushforce = 5f;
//     public float pushDelay = 0.15f;

//     public float speed = 200f;

//     public float moveSpeed = 110f;

//     float moveLimiter = 0.7f;

//     public Rigidbody2D rb;

//     public Animator animator;

//     Vector2 movement;

//     StageGenerator sg;
//     GameManager gm;


//     bool activeRose = false;
//     float roseStartTime;
//     public float roseDelay = 2f;

//     Rose currentRose;

//     bool movementEnabled = true;

//     TutorialManager tm;
//     //public bool tutorialActive = false;

//     /// <summary>
//     /// Set to true when the character intersects a collider beneath
//     /// them in the previous frame.
//     /// </summary>
//     private bool grounded;

//     public bool player1 = true;
//     string hAxis;
//     string vAxis;

//     bool pressbool() {
//         if (player1)
//         {
//             return Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E);
//         }
//         else { 
//             return Input.GetKeyDown(KeyCode.U) || Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.Space);
//         }
//     }

//     bool holdbool()
//     {
//         if (player1)
//         {
//             return Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E);
//         }
//         else
//         {
//             return Input.GetKey(KeyCode.U) || Input.GetKey(KeyCode.O) || Input.GetKey(KeyCode.Space);
//         }
//     }

//     private void Awake()
//     {      
//         boxCollider = GetComponent<BoxCollider2D>();
//         rb = GetComponent<Rigidbody2D>();
//         heldCarrot.SetActive(false);
//         makeSlash.SetActive(false);
//         sg = FindObjectOfType<StageGenerator>();
//         gm = FindObjectOfType<GameManager>();
        
//         if (player1)
//         {
//             hAxis = "Horizontal";
//             vAxis = "Vertical";
//         }
//         else {
//             hAxis = "Horizontal 2";
//             vAxis = "Vertical 2";
//         }

//         tm = FindObjectOfType<TutorialManager>();
//     }

//     private void Update()
//     {
//         // MOVEMENT
//         movement.x = Input.GetAxisRaw(hAxis);
//         movement.y = Input.GetAxisRaw(vAxis);
  
//         animator.SetFloat("Horizontal", movement.x);
//         animator.SetFloat("Vertical", movement.y);
//         animator.SetFloat("Speed", movement.magnitude);

//         if (tm && (movement.x != 0 || movement.y != 0)) {
//             tm.WASDactivated(player1);
//         }

//         // ITEM GRAB/THROW
//         if (pressbool())
//         {
//             //If standing next to a rose, grab the rose
//             if (activeRose)
//             {
//                 FindObjectOfType<AudioManager>().Play("rose pull");
//                 roseStartTime = Time.time;
//                 currentRose.timer.value = roseDelay;
//             }
//             //Otherwise, if standing next to a stem amd not holding a carrot
//             else if (activeStem && !objectHeld)
//             {
//                 FindObjectOfType<AudioManager>().Play("carrot pickup");
//                 Destroy(activeStem);
//                 objectHeld = true;
//                 heldCarrot.SetActive(true);
//                 if (tm) {
//                     tm.stemActivated(player1);
//                 }
//             }
//             //Otherwise, if an object is held, throw it
//             else if (objectHeld)
//             {
//                 if (!tm || tm.stage >= 4)
//                 {
//                     FindObjectOfType<AudioManager>().Play("throw");

//                 Vector2 spawnPos = new Vector2(this.transform.position.x, this.transform.position.y) +
//                     (lastVec.normalized * spawnDistance);
//                 GameObject tc = Instantiate(thrownCarrot, spawnPos, Quaternion.identity);
//                 tc.GetComponent<Rigidbody2D>().AddForce(lastVec.normalized * throwForce);

//                 Vector2 carrotvec = tc.transform.position;
//                 // move carrot if out of bounds
//                 if(tc.transform.position.x > 7.6F) {
//                     carrotvec.x = 7.6F;
//                     tc.transform.position = carrotvec;
//                 } else if(tc.transform.position.x < -7.51F) {
//                     carrotvec.x = -7.51F;
//                     tc.transform.position = carrotvec;
//                 }

//                 if(tc.transform.position.y > 4.14F) {
//                     carrotvec.y = 4.14F;
//                     tc.transform.position = carrotvec;
//                 } else if(tc.transform.position.y < -3.84F) {
//                     carrotvec.y = -3.84F;
//                     tc.transform.position = carrotvec;
//                 }

//                 if (sg) {
//                         sg.activeObjects.Add(tc);
//                 }
//             }
//             }
//         //     //Otherwise, check to punch
//         //     else {
           
//         //         Vector2 checkPos = new Vector2(this.transform.position.x, this.transform.position.y - 0.05f) +
//         //             (lastVec.normalized * pushDistance);
//         //         Collider2D result = Physics2D.OverlapCircle(checkPos, pushRadius, playerMask);
//         //         if (result) {
//         //             FindObjectOfType<AudioManager>().Play("melee");
//         //             result.gameObject.GetComponent<PlayerMovement>().StartCoroutine("RestrictMovement");
//         //             result.gameObject.GetComponent<Rigidbody2D>().AddForce(lastVec.normalized * pushforce);
//         //             result.gameObject.GetComponent<PlayerMovement>().StartCoroutine("playPushWait");

//         //             if (tm)
//         //             {
//         //                 tm.meleeActivated(player1);
//         //             }
//         //         }
//         //     }
//         }

//         // HELD ACTIVATION KEY for rose
//         if (holdbool() && activeRose)
//         {

//             if (currentRose)
//             {
//                 currentRose.timer.value = (roseDelay - (Time.time - roseStartTime));
//             }
//             if (Time.time - roseStartTime >= roseDelay)
//             {
//                 if (tm)
//                 {
//                     FindObjectOfType<AudioManager>().Play("round win");
//                     currentRose.gameObject.SetActive(false);
//                     tm.roseActivated(player1);
//                 }
//                 else
//                 {
//                     if (player1)
//                     {
//                         gm.pluckedby1();
//                     }
//                     else
//                     {
//                         gm.pluckedby2();
//                     }
//                     sg.cycle();
//                     activeRose = false;
//                     gm.prevTime = Time.time;
//                 }
//             }
//         } else if (holdbool() && !activeRose) {
//             FindObjectOfType<AudioManager>().Stop("rose pull");
//             roseStartTime = Time.time;
//         }

//         Vector2 p = playerObj.transform.position;
//         // move player if out of bounds
//         if(playerObj.transform.position.x > 7.606092F) {
//             p.x = 7.606092F;
//             playerObj.transform.position = p;
//         } else if(playerObj.transform.position.x < -7.562734F) {
//             p.x = -7.562734F;
//             playerObj.transform.position = p;
//         }

//         if(playerObj.transform.position.y > 3.54686F) {
//             p.y = 3.54686F;
//             playerObj.transform.position = p;
//         } else if(playerObj.transform.position.y < -3.250088F) {
//             p.y = -3.250088F;
//             playerObj.transform.position = p;
//         }


//     }

//     void FixedUpdate()
//     {

//         if (movement.x != 0 && movement.y != 0) // Check for diagonal movement
//         {
//             // limit movement speed diagonally, so you move at 70% speed
//             movement.x *= moveLimiter;
//             movement.y *= moveLimiter;
//         }

//         if (movementEnabled)
//         {
//             rb.velocity = movement * moveSpeed * Time.fixedDeltaTime;
//         }

//         if (movement != Vector2.zero)
//         {
//             lastVec = movement;
//         }

//     }

//     public IEnumerator playPushWait()
//     {
//         makeSlash.SetActive(true);
//         yield return new WaitForSeconds(0.5f);
//         makeSlash.SetActive(false);
//     }

//     public IEnumerator RestrictMovement()
//     {
//         movementEnabled = false;
//         yield return new WaitForSeconds(pushDelay);
//         movementEnabled = true;
//     }

//     public void getStem(GameObject s) {
//         activeStem = s;
//     }
//     public void dropStem(GameObject s)
//     {
//         if (activeStem == s) {
//             activeStem = null;
//         }
//     }

//     public void getRose(Rose r) {
//         activeRose = true;
//         currentRose = r;
//     }

//     public void dropRose(Rose r) {
//         if (activeRose == r) {
//             activeRose = false;
//             currentRose = null;
//         }
//     }
// }

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerMovement : MonoBehaviour
{

    public GameObject playerObj;
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

    public GameObject makeSlash;

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

    TutorialManager tm;
    //public bool tutorialActive = false;

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
        makeSlash.SetActive(false);
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

        tm = FindObjectOfType<TutorialManager>();
    }

    private void Update()
    {
        // MOVEMENT
        movement.x = Input.GetAxisRaw(hAxis);
        movement.y = Input.GetAxisRaw(vAxis);
  
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.magnitude);

        if (tm && (movement.x != 0 || movement.y != 0)) {
            tm.WASDactivated(player1);
        }

        // ITEM GRAB/THROW
        if (pressbool())
        {
            //If standing next to a rose, grab the rose
            if (activeRose)
            {
                FindObjectOfType<AudioManager>().Play("rose pull");
                roseStartTime = Time.time;
                currentRose.timer.value = roseDelay;
            }
            //Otherwise, if standing next to a stem amd not holding a carrot
            else if (activeStem && !objectHeld)
            {
                FindObjectOfType<AudioManager>().Play("carrot pickup");
                Destroy(activeStem);
                objectHeld = true;
                heldCarrot.SetActive(true);
                if (tm) {
                    tm.stemActivated(player1);
                }
            }
            //Otherwise, if an object is held, throw it
            else if (objectHeld)
            {
                if (!tm || tm.stage >= 4)
                {
                    FindObjectOfType<AudioManager>().Play("throw");
                    

                    if (tm) {
                        tm.throwActivated(player1);
                    }

                    objectHeld = false;
                    heldCarrot.SetActive(false);

                    Vector2 spawnPos = new Vector2(this.transform.position.x, this.transform.position.y) +
                        (lastVec.normalized * spawnDistance);
                    GameObject tc = Instantiate(thrownCarrot, spawnPos, Quaternion.identity);
                    tc.GetComponent<Rigidbody2D>().AddForce(lastVec.normalized * throwForce);

                    Vector2 carrotvec = tc.transform.position;
                    // move carrot if out of bounds
                    if(tc.transform.position.x > 7.6F) {
                        carrotvec.x = 7.6F;
                        tc.transform.position = carrotvec;
                    } else if(tc.transform.position.x < -7.51F) {
                        carrotvec.x = -7.51F;
                        tc.transform.position = carrotvec;
                    }

                    if(tc.transform.position.y > 4.14F) {
                        carrotvec.y = 4.14F;
                        tc.transform.position = carrotvec;
                    } else if(tc.transform.position.y < -3.84F) {
                        carrotvec.y = -3.84F;
                        tc.transform.position = carrotvec;
                    }

                    if (sg) {
                        sg.activeObjects.Add(tc);
                    }
                    
                }
            }
            //Otherwise, check to punch
            else {
           
                Vector2 checkPos = new Vector2(this.transform.position.x, this.transform.position.y - 0.05f) +
                    (lastVec.normalized * pushDistance);
                Collider2D result = Physics2D.OverlapCircle(checkPos, pushRadius, playerMask);
                if (result) {
                    FindObjectOfType<AudioManager>().Play("melee");
                    result.gameObject.GetComponent<PlayerMovement>().StartCoroutine("RestrictMovement");
                    result.gameObject.GetComponent<Rigidbody2D>().AddForce(lastVec.normalized * pushforce);
                    result.gameObject.GetComponent<PlayerMovement>().StartCoroutine("playPushWait");

                    if (tm)
                    {
                        tm.meleeActivated(player1);
                    }
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
                if (tm)
                {
                    FindObjectOfType<AudioManager>().Play("round win");
                    currentRose.gameObject.SetActive(false);
                    tm.roseActivated(player1);
                }
                else
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
        }
        else if (holdbool() && !activeRose) {
            FindObjectOfType<AudioManager>().Stop("rose pull");
            roseStartTime = Time.time;
        }

        if (!holdbool() && activeRose) {
            FindObjectOfType<AudioManager>().Stop("rose pull");
        }

        Vector2 p = playerObj.transform.position;
        // move player if out of bounds
        if(playerObj.transform.position.x > 7.606092F) {
            p.x = 7.606092F;
            playerObj.transform.position = p;
        } else if(playerObj.transform.position.x < -7.562734F) {
            p.x = -7.562734F;
            playerObj.transform.position = p;
        }

        if(playerObj.transform.position.y > 3.54686F) {
            p.y = 3.54686F;
            playerObj.transform.position = p;
        } else if(playerObj.transform.position.y < -3.250088F) {
            p.y = -3.250088F;
            playerObj.transform.position = p;
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

    public IEnumerator playPushWait()
    {
        makeSlash.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        makeSlash.SetActive(false);
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