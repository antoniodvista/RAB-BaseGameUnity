using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    [Header("Attributes")]
    [SerializeField] float speed;
    [SerializeField] float jumpPadForce = 100f;
    [SerializeField] float superJumpMultiplier = 2;

    [Header("Text Elements")] 
    [SerializeField] TMP_Text countText;
    [SerializeField] TMP_Text winText;
    [SerializeField] TMP_Text timeText;

    [Header("Time")]
    [SerializeField] float startingTime;
    [SerializeField] string min;
    [SerializeField] string sec;

    [SerializeField] AudioClip coinSFX, growSFX, jumpSFX, unbreakableSFX, breakWoodSFX;
    private AudioSource audioSource;

    //  rb init in start
    private Rigidbody rb;

    //  set to 0 at start
    private int count;

    //  gamestate bool
    private bool gameOver;

    // Audio
    
    //  called on first frame
    void Start()
    {
        //  player components
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        count = 0;
        SetCountText();
        winText.text = "";
        startingTime = Time.time;
        gameOver = false;
    }

    //  updates every frame
    private void Update()
    {
        //  stops timer if gameover is true
        if (gameOver)
            return;

        float timer = Time.time - startingTime; // local variable to updated time
        min = ((int)timer / 60).ToString();     // calculates minutes
        sec = (timer % 60).ToString("f0");      // calculates seconds

        timeText.text = "Elapsed Time: " + min + ":" + sec;
    }

    //  updates every frame for physics
    void FixedUpdate()
    {
        //  gets wasd inputs
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //  roll a ball mechanics
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddRelativeForce(movement * speed);
    }

    //  called on entering game object collision
    private void OnTriggerEnter(Collider other)
    {
        //  pick up
        if (other.gameObject.tag == "PickUp")
        {
            Destroy(other.gameObject);
            count++;
            SetCountText();

            //  sound effect
            audioSource.clip = coinSFX;
            audioSource.Play();

            if (count >= 10 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LevelTwo"))
            {
                //  game win and UI changes
                gameOver = true;
                timeText.color = Color.green;
                winText.text = "You win!";
                speed = 0;
                Destroy(GameObject.FindWithTag("Broom"));
                WinScreen();
            }
        }

        //  deathzone
        if (other.gameObject.CompareTag("DeathZone"))
        {
            SceneManager.LoadScene("GameOver");
        }

        //  breakable objects
        if (other.gameObject.CompareTag("Breakable") && transform.localScale.x > 1)
        {
            Destroy(other.gameObject);
            audioSource.clip = breakWoodSFX;
            audioSource.Play();
        } 
        else if (other.gameObject.CompareTag("Breakable") && transform.localScale.x == 1)
        {
            audioSource.clip = unbreakableSFX;
            audioSource.Play();
        }

        //  grow pad
        if (other.gameObject.CompareTag("Grow"))
        {
            if (transform.localScale.x < 2f)
            {
                transform.localScale *= 2f;
                audioSource.clip = growSFX;
                audioSource.Play();
            }
        }

        //  shrink pad
        if (other.gameObject.CompareTag("Shrink"))
        {
            if (transform.localScale.x > 1f)
            {
                transform.localScale *= 0.5f;    
            }
        }

        //  jump pad
        if (other.gameObject.CompareTag("Jump"))
        {
            rb.AddForce(new Vector3(0.0f, jumpPadForce, 0.0f));
            audioSource.clip = jumpSFX;
            audioSource.Play();
        }
        
        //  super jump pad
        if (other.gameObject.CompareTag("SuperJump"))
        {
            rb.AddForce(new Vector3(0.0f, jumpPadForce * superJumpMultiplier, 0.0f));
            audioSource.clip = jumpSFX;
            audioSource.Play();
        }

        //  drop off area
        if (other.gameObject.CompareTag("End"))
        {
            if (count >= 10 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LevelOne"))
            {
                //  game win and UI changes
                gameOver = true;
                timeText.color = Color.green;
                winText.text = "You win!";
                speed = 0;
                Destroy(GameObject.FindWithTag("Broom"));
                StartCoroutine(NextLevel());
            }
            else 
            {
                Debug.Log("Not enough pickups");
            }
        }
    }

    //  updates counter
    void SetCountText()
    {
        countText.text = "Pick Ups: " + count.ToString();
    }

    // goes to win screen in 3 seconds
    void WinScreen()
    {
        SceneManager.LoadScene("WIN");
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("LevelTwo");
    }
}
