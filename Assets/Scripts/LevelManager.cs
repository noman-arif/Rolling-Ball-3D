using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject levelMenu;                                            //Level Menu reference holder
    public GameObject startUI;                                              //StartMenu Reference holder varaible
    public Button[] buttonList;                                             //Array contain List of LevelButton
    private AudioManager playAudio;                                         //variable to access audioManager script
    private string level;                                                   //level variable
    private int unlockLevel;                                                //unlocklevel variable
    //Start function call once
    private void Start()
    {
        playAudio = GameObject.FindObjectOfType<AudioManager>();            //Finding the AudioManager Script
        unlockLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);               //Unlock Level 1 already when game start for the very first time
        ButtonInteract();                                                   //call buttonFunction 
    }
    //Unlock Button Function
    private void ButtonInteract()
    {
        for (int i = 0; i < buttonList.Length; i++)                         //loop run till the last element in button array
        {
            buttonList[i].interactable = false;                             //and make all button non interactable
        }
        for (int i = 0; i < unlockLevel; i++)                               //loop will run till the last level store in it.
        {
            buttonList[i].interactable = true;                              //and activate all those level button
        }
    }
    //Show or pop up level menu where you can select the unlocked levels
    public void LevelMenu()
    {
        levelMenu.SetActive(true);                                          //Active the Level Button UI
        playAudio.ButtonAudio();                                            //Play Button Click Audio
    }
    //Function to Quit and Application
    public void QuitApp()
    {
        Application.Quit();                                                 //Quit App
        playAudio.ButtonAudio();                                            //PlayButtonSound
    }
    //Function is Responsible for Scene or Level Shifter 
    public void LevelOpener(int l)                                          //get parameter from button click
    {
        level = "Level" + l;                                                //store it by concatenate in a string varaible
        StartCoroutine(SelectLevel());                                      //Start Coroutine to add a loading delay for 
    }
    //Function is used to go back to main page by close level selection page
    public void BackMenu()
    {
        levelMenu.gameObject.SetActive(false);                              //Close level UI
        playAudio.ButtonAudio();                                            //Play Button Sound
    }
    //Select level Function
    IEnumerator SelectLevel()
    {
        playAudio.ButtonAudio();                                            //PlayButtonSound
        yield return new WaitForSeconds(0.3f);                              //Wait for 0.3 sec
        SceneManager.LoadScene(level);                                      //then load the level
    }
}
