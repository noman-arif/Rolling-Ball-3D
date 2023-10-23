using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int coins = 0;                                                         //coin count variable
    public int lives = 2;                                                       //lives count variable
    public TextMeshProUGUI coinText;                                            //coin UI varaible
    public TextMeshProUGUI liveText;                                            //live UI variable
    public GameObject menuBoxUI;                                                //Menu UI varaible
    public GameObject levelEndUI;                                               //level UI variable
    public GameObject gameOver;                                                 //GameOver UI variable
    public bool isGameOver;                                                     //bool for check game
    private PlayerController player;                                            //variable to access player script
    private AudioManager playAudio;                                             //variable to access audio script
    private float waitTime = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        coinText.text = coins.ToString();                                       //initialze coin UI with assign Value to displat it on screen
        liveText.text = lives.ToString();                                       //initialze live UI with assign Value to display it on screen
        player = GameObject.FindObjectOfType<PlayerController>();               //finding the player controller script to access or use it  function
        playAudio = GameObject.FindObjectOfType<AudioManager>();                //finding the Audio Manager script to access or use it  function  

    }
    //function for adding Coin
    public void AddCoin()
    {
        coins++;                                                                //increment in Coin
        coinText.text = coins.ToString();                                       //display it on Screen 
    }
    // Function call when player fall from platform
    public void LostLives()
    {
        lives--;                                                                //decrement life
        liveText.text = lives.ToString();                                       //display it on Screen
        if (lives <= 0)                                                         //if live become zero
        {
            playAudio.audioSource.PlayOneShot(playAudio.gameOver, 1f);          //GameOver Sound play
            gameOver.SetActive(true);                                           //Display GameOver UI
            isGameOver = true;                                                  //set bool value to True mean gameover
        }
    }
    //function call for adding life when user collect live Object
    public void AddLives()
    {
        lives++;                                                                //increment life                                         
        liveText.text = lives.ToString();                                       //display it on Screen
    }
    //Function call when player click on Setting UI Button
    public void ActiveMenu()
    {
        menuBoxUI.gameObject.SetActive(true);                                   //Display Menu UI
        playAudio.ButtonAudio();                                                //Play Button Click Audio
        player.FreezePlayer();                                                  //Freeze Player movement
    }
    //Function Call when player Click On Close UI button (X)            
    public void CloseMenu()
    {
        menuBoxUI.gameObject.SetActive(false);                                  //Close Menu UI
        playAudio.ButtonAudio();                                                //Play Button Sound
        player.DeFreezePlayer();                                                //Make Player to Move
    }
    //Function to reload Scene when call
    public void ReloadScene()
    {
        StartCoroutine(ReloadWait());                                           //reload scene with a delay by call numerator function
    }
    //Function to load next level when call 
    public void ShiftScene()
    {
        StartCoroutine(ShiftWait());                                            //shift level with a delay by call a Numerator function
    }
    //Function to call Main Menu Scene
    public void MainMenu()
    {
        StartCoroutine(MainMenuWait());
    }
    //Game Over UI 
    IEnumerator TurnOffGameOver()                                               //It will turn off GameOver UI after 2second then reload the game
    {
        yield return new WaitForSeconds(2f);
        gameOver.SetActive(false);                                              //make UI to false
        ReloadScene();                                                          //call reload Function
    }
    //Main Menu UI numerator function
    IEnumerator MainMenuWait()
    {
        playAudio.ButtonAudio();                                                //playbuttin audio
        yield return new WaitForSeconds(waitTime);                              //wait for 0.3 sec
        SceneManager.LoadScene("UIScene");                                      //Load MainMenu Scene
    }
    // Reload Scene 
    IEnumerator ReloadWait()
    {
        playAudio.ButtonAudio();                                                //playbutton sound
        yield return new WaitForSeconds(waitTime);                              //wait for 0.3 sec
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);             //reload Current Scene
    }
    //Next Level
    IEnumerator ShiftWait()
    {
        playAudio.ButtonAudio();                                                //play button Sound
        yield return new WaitForSeconds(waitTime);                              //wait for 0.3 sec
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   //shift scene or load next level
    }
}
