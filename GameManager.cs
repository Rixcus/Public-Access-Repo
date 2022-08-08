using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameActive = true;
    bool isPaused = false;
    public GameObject pauseUI;
    public GameObject gameplayUI;
    public Camera mainCam;
    public Camera ballCam;
    public Camera zoomCam;
    public TextMeshProUGUI helpText;
    bool isZoomed = false;
    bool isHelp = true;
    bool isBallCam = true;
    public GameObject extraOptions;
    bool extraOpt = true;
    public GameObject notYet;

    public AudioClip destroClip;
    public AudioClip pointClip;
    public AudioClip confirmClip;
    public AudioClip backClip;
    AudioSource adS;

    public bool fullGame;

    // Start is called before the first frame update
    void Start()
    {
        adS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused && isGameActive)
        {            
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused && !isGameActive)
        {            
            UnPause();
        }
    }
    public void PlayDestro()
    {
        adS.PlayOneShot(destroClip);
    }
    public void PlayPoint()
    {
        adS.PlayOneShot(pointClip);
    }
    public void PauseGame()
    {
        isPaused = true;
        isGameActive = false;
        Time.timeScale = 0;
        pauseUI.gameObject.SetActive(true);
        gameplayUI.gameObject.SetActive(false);
        adS.PlayOneShot(confirmClip);
    }
    public void UnPause()
    {
        isPaused = false;
        isGameActive = true;
        Time.timeScale = 1;
        pauseUI.gameObject.SetActive(false);
        gameplayUI.gameObject.SetActive(true);
        adS.PlayOneShot(backClip);
    }
    public void ZoomCamToggle()
    {
        if(isZoomed)
        {
            zoomCam.gameObject.SetActive(false);
            mainCam.gameObject.SetActive(true);
            isZoomed = false;
            adS.PlayOneShot(confirmClip);
        }
        else if (!isZoomed)
        {

            mainCam.gameObject.SetActive(false);
            zoomCam.gameObject.SetActive(true);            
            isZoomed = true;
            adS.PlayOneShot(backClip);
        }
    }
    public void HelpToggle()
    {
        if(isHelp)
        {
            helpText.gameObject.SetActive(false);
            isHelp = false;
            adS.PlayOneShot(backClip);
        }
        else if (!isHelp)
        {
            helpText.gameObject.SetActive(true);
            isHelp = true;
            adS.PlayOneShot(confirmClip);
        }
    }
    public void BallCamToggle()
    {
        CannonController cannonController = GameObject.Find("Cannon").GetComponent<CannonController>();
        if(isBallCam)
        {
            cannonController.ballCanEnable = false;
            isBallCam = false;
            adS.PlayOneShot(confirmClip);

        }
        else if (!isBallCam)
        {
            cannonController.ballCanEnable = true;
            isBallCam = true;
            adS.PlayOneShot(confirmClip);
        }
    }
    public void ExtraOptionsToggle()
    {
        if(extraOpt)
        {
            extraOptions.gameObject.SetActive(false);
            extraOpt = false;
            adS.PlayOneShot(backClip);
        }
        else if(!extraOpt)
        {
            extraOptions.gameObject.SetActive(true);
            extraOpt = true;
            adS.PlayOneShot(confirmClip);
        }
    }
    public void StandardGame()
    {
        SceneManager.LoadScene("You are Momentum Ball");
        fullGame = true;
    }
    public void BeTheBall()
    {
        SceneManager.LoadScene("You are Momentum Ball");
        fullGame = false;
    }
    public void BeTheCannon()
    {
        SceneManager.LoadScene("You are Cannon");
        fullGame = false;
    }
    public void BeTheDropper()
    {
        SceneManager.LoadScene("You are Dropper");
        fullGame = false;
        
    }
    public void BeTheTerrain()
    {
        SceneManager.LoadScene("You are Terrain");
        fullGame = false;
        
    }
    public void KeyRebind()
    {
        NotYetImp();
        adS.PlayOneShot(backClip);
    }
    public void Credits()
    {
        NotYetImp();
        adS.PlayOneShot(backClip);
    }
    public void NotYetImp()
    {
        notYet.SetActive(true);
        StartCoroutine(NotYetTimer());
    }
    IEnumerator NotYetTimer()
    {
        yield return new WaitForSeconds(3);
        notYet.SetActive(false);

    }
    public void DebugRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        adS.PlayOneShot(backClip);
    }


    public void RestartGame()
    {
        SceneManager.LoadScene("Main Menu");
        adS.PlayOneShot(backClip);
    }
    public void QuitGame()
    {
        Application.Quit();
        adS.PlayOneShot(destroClip);
    }
    
}
