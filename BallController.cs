using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BallController : MonoBehaviour
{
    float forwardInput;
    float horizontalInput;

    Rigidbody bRB;
    public float speed = 15;
    public float turnSpeed = 20;
    public GameObject focalPoint;
    public GameObject focalCam;
    //public Vector3 offset = new Vector3();
    public bool isCountable;
    Counter counter;
    public TextMeshProUGUI multiplierText;
    int multiplier;

    //ScoreSO scoreSO;
    [SerializeField] private ScoreSO ScoreSheet;

    GameManager gameManager;
    bool fullGame;

    // Start is called before the first frame update
    void Start()
    {
        bRB = GetComponent<Rigidbody>();
        isCountable = true;
        counter = GameObject.Find("TargetC").GetComponent<Counter>();
        multiplier = ScoreSheet.multi;
    }

    // Update is called once per frame
    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");              

    }
    private void FixedUpdate()
    {
        bRB.AddForce(focalPoint.transform.forward * speed * forwardInput);
        focalPoint.transform.RotateAround(transform.position, Vector3.up * horizontalInput, 50 * Time.deltaTime);
        focalCam.transform.RotateAround(transform.position, Vector3.up * horizontalInput, 50 * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Multiplier")
        {
            Destroy(other.gameObject);
            Debug.Log("Ball + 1");
            ScoreSheet.multi += 1;
            multiplier = ScoreSheet.multi;
            multiplierText.text = "Multiplier : " + multiplier + " X";

            
        }
        else if (other.gameObject.tag == "Target" && isCountable)
        {
            
            counter.CountAdd(ScoreSheet.multi);
            isCountable = false;
        }
        
    }
}
