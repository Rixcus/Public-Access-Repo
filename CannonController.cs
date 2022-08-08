using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CannonController : MonoBehaviour
{
    public GameObject canBase;
    public GameObject canPivot;
    public Collider lidCol;
    public Collider triggerCol;
    public float vertical;
    public float horizontal;
    public float rotateSpeed;
    public float angleSpeed;
    int startBallCount;
    int endBallCount;
    public int testBallCount = 10;
    public GameObject cannonBall;
    //Vector3 ballPos = new Vector3(-.4f, 1, 0);
    public float cannonPow;
    public Transform spawnZone;
    Rigidbody cannonBod;
    BallCountKeeper bCK;
    AudioSource audioSource;
    public AudioClip fireClip;
    //float rotateRange = 35;
    public ParticleSystem flash;
    public ParticleSystem smoke;
    CannonTCounter cTargetCounter;
    int Count = 0;
    public TextMeshProUGUI CounterText;
    LineRenderer aimLine;
    bool aiming = true;

    public Camera mainCam;
    public Camera ballCam;
    public bool ballCanEnable;


    // Start is called before the first frame update
    void Start()
    {
        
        cannonBod = GameObject.Find("Pipe").GetComponent<Rigidbody>();
        bCK = GetComponent<BallCountKeeper>();
        cTargetCounter = GameObject.Find("Cannon Target").GetComponent<CannonTCounter>();
        audioSource = GetComponent<AudioSource>();
        //startBallCount = bCK.cannonStageBallCountStart;
        LoadCannon();
        aimLine = GameObject.Find("New Trajectory Line").GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            Fire();                        
        }
        if (Input.GetButtonDown("Fire3"))
        {
            AimLine();
        }



    }
    private void FixedUpdate()
    {
        
        canBase.transform.Rotate(Vector3.up * horizontal * rotateSpeed * Time.deltaTime);
        
        
        canPivot.transform.Rotate(Vector3.right * vertical * angleSpeed * Time.deltaTime);
        
               
        
    }
    void AimLine()
    {
        if (aiming)
        {
            aimLine.gameObject.SetActive(false);
            aiming = false;
        }
        else if (!aiming)
        {
            aimLine.gameObject.SetActive(true);
            aiming = true;
        }
    }
    

    void LoadCannon()
    {
        //for(int i = 0; i < startBallCount; i++)
        //{
            //Instantiate(cannonBall, GenerateSpawnPosition(), cannonBall.transform.rotation);
            //Instantiate(cannonBall, spawnZone.position, cannonBall.transform.rotation);
        //}
        for (int i = 0; i < testBallCount; i++)
        {
            Instantiate(cannonBall, GenerateSpawnPosition(), cannonBall.transform.rotation);
            
        }

    }
    Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(.16f, -.16f);
        float spawnPosY = Random.Range(1.27f, 1.7f);
        float spawnPosZ = Random.Range(-.03f, 1.4f);


        Vector3 randomPos = new Vector3 (spawnPosX, spawnPosY, spawnPosZ);
        return randomPos;
    }
    void Fire()
    {

        CounterText.text = "Ball Count : " + Count;

        lidCol.gameObject.SetActive(false);
        //triggerCol.gameObject.SetActive(true);

        Rigidbody cRB = cannonBall.GetComponent<Rigidbody>();

        Collider[] colliders = Physics.OverlapCapsule(triggerCol.transform.position + new Vector3(-.4f, 0, .5f), triggerCol.transform.position + new Vector3(-.4f, 0, 1.8f), 10f);
        foreach( Collider hit in colliders)
        {
           Rigidbody rb = hit.GetComponent<Rigidbody>();
            
           if (hit.CompareTag("Cannon Ball"))
            {

                audioSource.PlayOneShot(fireClip);
                flash.Play();
                smoke.Play(); 
                rb.AddForce(Vector3.forward * cannonPow, ForceMode.Impulse);
                //rb.AddExplosionForce(cannonPow, new Vector3(0, 0, 0), 2.5f);

                if(ballCanEnable)
                {
                    mainCam.gameObject.SetActive(false);
                    ballCam.gameObject.SetActive(true);
                }
                

                

            }
        }       


    }
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(new Vector3(0, 0, 0), 2.5f);
        //Gizmos.DrawWireSphere(triggerCol.transform.position + new Vector3(-.4f, 0, .5f), .4f);
        //Gizmos.DrawLine(new Vector3(0, 1.5f, 0), new Vector3(0, 1.5f, 100));

    }
    
}
