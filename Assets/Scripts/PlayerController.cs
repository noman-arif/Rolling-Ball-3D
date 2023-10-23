using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;                                                     //rigidbody variable 
    public float playerSpeed = 10f;                                                 //player movement speed
    private float vertInput;                                                        //float variable for vertical input
    public GameObject focalPoint;                                                   //focal Gameobject varaible
    private bool isGround;                                                          //bool Variable for Checking Ground
    public float jumpForce = 5f;                                                    //player Jump Force variable                                    
    public float rangeY = 10;                                                         //float variable for range 
    private bool isEnd;                                                             //bool varaible 
    private Vector3 playerPos;                                                      //player position vector
    public float pushForce = 5f;                                                    //push Force Variable
    public bool powerUp;                                                            //bool to check power ability
    public GameObject powerRing;                                                    //power ability ring 
    private ScoreManager score;                                                     //varaible to access scoremanager script
    private SpawnManager spawn;                                                     //variable to access spawnmanager script
    private AudioManager audioManager;                                              //varaible to access audiomanager script
    public ParticleSystem coinPart;
    public ParticleSystem checkPointPart;
    public ParticleSystem heartPart;
    public ParticleSystem hitPart;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = transform.position;                                                                 //assign playerpos variable starting position of player
        powerUp = false;
        isGround = false;
        isEnd = false;
        playerRb = GetComponent<Rigidbody>();                                                           //getting player RigidBody Component
        score = GameObject.FindAnyObjectByType<ScoreManager>().GetComponent<ScoreManager>();             //Find Score Manager script to Use its Functions
        spawn = GameObject.FindObjectOfType<SpawnManager>().GetComponent<SpawnManager>();               //Finding SpawnManager Script to use it functions
        audioManager = GameObject.FindObjectOfType<AudioManager>().GetComponent<AudioManager>();        //Finding AudioManager Script to use its Functions
        audioManager.audioSource.PlayOneShot(audioManager.musicSound, 0.1f);                            //PlayBackGround Music 
    }
    // Update is called once per frame
    void Update()
    {
        vertInput = Input.GetAxis("Vertical");                                                          //Getting Vertical Keyboard Input
        if (Input.GetKeyDown(KeyCode.Space) && isGround != true)                                        //Condition for Jump 
        {
            PlayerJump();                                                                               //make player to jump by call jump function
        }
        if (transform.position.y <= -rangeY)                                                            //check rangeY of player when fall from platform
        {
            FreezePlayer();                                                                             //at given height after falling it will freeze
        }
        powerRing.transform.position = transform.position + new Vector3(0, -0.4f, 0);                   //Power Ring Follow Player with given Offset

    }
    //Fixed Update Function Call Fixed TimeStamp
    private void FixedUpdate()
    {

        if (!isEnd)                                                                                     //as long as level is not end player can move
        {
            Movement();                                                                                 //call movement function
        }
    }
    //Player Jump Function
    private void PlayerJump()
    {
        isGround = true;                                                                                //true to bound player to jump one not multiple jump in air
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);                                   //add Upward Force 
        audioManager.audioSource.PlayOneShot(audioManager.jumpSound, 1f);                               //Jump Sound Played
    }
    //Player Movement Function
    private void Movement()
    {
        playerRb.AddForce(focalPoint.transform.forward * vertInput * playerSpeed);                      //Apply force to player in the direction of Camera
        //MoveRotation
    }
    //Freeze Player 
    public void FreezePlayer()
    {
        playerRb.isKinematic = true;                                                                    //stop player 
    }
    //De Freeze Function
    public void DeFreezePlayer()
    {
        playerRb.isKinematic = false;                                                                  //make player to move
    }
    //Hit Particle Function When Player hit Enemy And Box
    public void HitParticles()
    {
        Instantiate(hitPart, transform.position, hitPart.transform.rotation);                          //spawn Hit Particle
    }

    //Collison Function
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("LevelEnd"))                                               //if collide with Collider with Level End Tag
        {
            audioManager.audioSource.Stop();                                                           //Stop BackGound Audio
            audioManager.audioSource.PlayOneShot(audioManager.levelPass, 1f);                          //place level end Audio
            score.levelEndUI.gameObject.SetActive(true);                                               //Level End UI will PopUp
            isEnd = true;                                                                              //Set true mean player is no longer able to move
            FreezePlayer();                                                                            //freeze player 
        }
        else if (collision.gameObject.CompareTag("Ground"))                                            // if collide with collider name ground 
        {
            isGround = false;                                                                          //then player can jump again
        }
        else if (collision.gameObject.CompareTag("Box"))                                               //if collide with collider name or tag Box
        {
            audioManager.HitSound();                                                                   //play hit Sound
            HitParticles();                                                                            //spawn Hit particles
        }
        else if (collision.gameObject.CompareTag("Enemy") && !powerUp)                                  //if collide with and not power ability is picked
        {
            audioManager.HitSound();                                                                   //play hit Sound
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();                        //Get enemy RigidBody Component
            Vector3 pushEnemy = collision.gameObject.transform.position - transform.position;          //getting vector by subtracting postion
            enemyRb.AddForce(pushEnemy * pushForce, ForceMode.Impulse);                                // apply force in that direction
            HitParticles();                                                                            //hit particle will play
        }
        else if (collision.gameObject.CompareTag("Enemy") && powerUp)                                  //if collide with enemy collide and has power ability
        {
            audioManager.HitSound();                                                                   //play hit Sound
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();                        //Get Enemy RigidBody
            Vector3 pushEnemy = collision.gameObject.transform.position - transform.position;          //Calculate the distance between both player and enemy
            enemyRb.AddForce(pushEnemy * pushForce * 2, ForceMode.Impulse);                              //apply double force
            HitParticles();                                                                            //Play hit particle
        }
    }
    //Trigger Function
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coins"))                                                      // if trigger with coin
        {
            Destroy(other.gameObject);                                                                 //coin object destroy
            score.AddCoin();                                                                           //call addcoin to add coin
            audioManager.audioSource.PlayOneShot(audioManager.coinCollect, 1f);                        //play coin collect audio
            Instantiate(coinPart, other.transform.position, coinPart.transform.rotation);              //spawn particle
        }
        else if (other.gameObject.CompareTag("Die"))                                                   //if trigger Die
        {
            StartCoroutine(RespawnPlayer());                                                           //Start a Coroutine to Respawn Player
        }
        else if (other.gameObject.CompareTag("CheckPoint"))                                            //if trigger with CheckPoint
        {
            playerPos = transform.position;                                                            //Assign Player Current Position to the PlayerPos vector3
            audioManager.audioSource.PlayOneShot(audioManager.checkPoint, 1f);                         //play CheckPoint Audio
            Instantiate(checkPointPart, other.transform.position, checkPointPart.transform.rotation);  //Spawn Particles
        }
        else if (other.gameObject.CompareTag("Power"))                                                 //if trigger with power 
        {
            powerUp = true;                                                                            //true the bool mean player can hit enemy by double force
            Destroy(other.gameObject);                                                                 //destroy powerobject
            powerRing.gameObject.SetActive(true);                                                      //powerring appear around player 
            StartCoroutine(PowerIsUp());                                                               //start PowerUp routine
        }
        else if (other.gameObject.CompareTag("Life"))                                                  //if tigger with Life 
        {
            audioManager.audioSource.PlayOneShot(audioManager.lifeSound, 1f);                          //Play Life Collect Sound
            score.AddLives();                                                                          //call addlive function to add live
            Destroy(other.gameObject);                                                                 //destroy live object
            Instantiate(heartPart, other.transform.position + new Vector3(0, 1f, 0), heartPart.transform.rotation); //spawn particles
        }
        if (other.gameObject.CompareTag("ActiveEnemy"))                                                //if trigger with ActiveEnemy
        {
            spawn.SpawnEnemy();                                                                        //Call Function and Awake the Enemy 
        }

    }
    //Respawn Player Function 
    IEnumerator RespawnPlayer()
    {
        score.LostLives();                                                                             //player lost live
        yield return new WaitForSeconds(2f);                                                           //wait for 2 second
        transform.position = playerPos;                                                                //spawn player in the position save in playerpos
        playerRb.isKinematic = false;                                                                  //make player to move
        if (score.isGameOver != false)                                                                 //if game is not over mean player live is not zero
        {
            score.gameOver.SetActive(false);                                                           //make GameOverUI to false
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);                                //Reload The Current Scene
        }
    }
    //Power Function
    IEnumerator PowerIsUp()
    {
        audioManager.audioSource.PlayOneShot(audioManager.powerUp, 1f);                                //play power Up sound
        yield return new WaitForSeconds(7f);
        audioManager.audioSource.PlayOneShot(audioManager.powerDown, 1f);                              //play down Sound
        powerRing.gameObject.SetActive(false);                                                         //turn off ring appear around player
        powerUp = false;                                                                               //false mean player can not hit enemy by double force
    }
}
