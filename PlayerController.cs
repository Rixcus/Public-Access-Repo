using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public List<WheelCollider> wheels;
    
    public Camera cam1;
    public Camera cam2;
    public GameObject GameOverUI;
    public TextMeshProUGUI WinText;
    public bool multi;
    public RoadShifter roadShifter;
    public TextMeshProUGUI score;
    Rigidbody playerRB;

    //private variables
    [SerializeField] private float horsePower = 30.0f;
    private float turnSpeed = 45.0f;
    private float horizontalInput;
    private float forwardInput;
    private float wheelSpeed = 800;
    
    Vector3 startpos = new Vector3(4.09f, 0, 0);
    int bunp;
    int FS;
    float TRTimer;
    MeshCollider MC;
    float PowTimer = 30;

    [SerializeField] GameObject centerOfMass;
    [SerializeField] TextMeshProUGUI spdometerText;
    [SerializeField] float speed;
    [SerializeField] TextMeshProUGUI rpmText;
    [SerializeField] float RPM;
    int wheelsOnGround;

    // Start is called before the first frame update
    void Start()
    {
        cam1.enabled = true;
        cam2.enabled = false;
        MC = GetComponent<MeshCollider>();
        playerRB = GetComponent<Rigidbody>();
        playerRB.centerOfMass = centerOfMass.transform.localPosition;

                
    }
    //toggle state set by opening options buttons, allows for different text display on single or multi game over
    public void single()
    {
        multi = false;
    }
    public void multiplayer()
    {
        multi = true;
    }
    void OnCollisionEnter(Collision col)
    {
        //bunp is joke name but purposfulyl disticnt variable for points scored from collisions

        if (col.gameObject.tag == "plus")
        {
            bunp += 1;            
        }
        if (col.gameObject.tag == "minus")
        {
            bunp += -1;
        }
        if (col.gameObject.tag == "Player")
        {
            bunp += -1;
        }
    }
    public void ChangeSpeed()
    {

        horsePower += 30;
        PowTimer -= Time.deltaTime;
        if (PowTimer <= 0)
        {
            horsePower -= 30;
        }


    }

    // Update is called once per frame
    void Update()
    {
        //score variables, time based on fixed update as opposed to frames, distance from start, and a collision points variable
        float T = Time.fixedUnscaledTime;
        float D = Vector3.Distance(startpos, transform.position);
        int S = (int)T + (int)D + bunp;
        score.text = "Score: " + S;
        //FS declared to be used outside the update function
        FS = S; 

        // fetch player inputs
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        
        

        

        if (Input.GetButtonDown("Jump"))
        {
            //camera switch between main and pov cams
            cam1.enabled = !cam1.enabled;
            cam2.enabled = !cam2.enabled;
        }
    }

    private void FixedUpdate()
    {
       if (IsOnGround())
       {
            //move vehicle based on player inputs
            //previous form: Move the vehicle forward (transform.Translate(Vector3.forward * Time.deltaTime * speed)
            playerRB.AddRelativeForce(Vector3.forward * horsePower * forwardInput);
            //trotates vehicle with player input as opposed to translate (strafing) 
            transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);

            speed = (int)(playerRB.velocity.magnitude * 2.237f);
            spdometerText.SetText("Speed " + speed + " MPH");
            //RPM = (speed % 30) * 20;
            //rpmText.SetText("RPM " + RPM + " (fake wheel rpm)");

       }

    }

    bool IsOnGround()
    {
        wheelsOnGround = 0;
        foreach(WheelCollider wheel in wheels)
        {
            if (wheel.isGrounded)
                wheelsOnGround++;
        }
        if (wheelsOnGround == 4)
        {
            return true;           
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
            roadShifter.SpawnTriggerEntered();
        
        
    }

    //script to run when object is destroyed
    void OnDestroy()
    {
        if(multi)
        {
        WinText.text = "Player 2 Wins";
        GameOverUI.SetActive(true);
        Time.timeScale = 0;
        }
        if(!multi)
        {
            WinText.text = "Good Run   Final Score " + FS;
            GameOverUI.SetActive(true);
            Time.timeScale = 0;
            
        }
    }
}
