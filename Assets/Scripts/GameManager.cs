using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public GameObject pauseCanvas;
    public Slider volumeSlider;
    public Button restartButton;
    public GameObject titleScreen;
    public AudioSource backgroundMusic;

    public bool isGameActive = false;
    public bool isGamePaused = false;
    public int livesAmount = 3;

    private int score = 0;
    private float spawnRate = 1.0f;
    private int livesCount = 3;
    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameActive && Input.GetKeyDown(KeyCode.Space))
        {
            isGamePaused = !isGamePaused;
            Time.timeScale = isGamePaused ? 0 : 1;
            pauseCanvas.SetActive(isGamePaused);
        }
    }

    public void UpdateScore(int scoreAmount)
    {
        score += scoreAmount;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLivesText()
    {
        livesText.text = "Lives: " + livesCount;
    }

    public void RemoveLife()
    {
        livesCount -= 1;
        UpdateLivesText();

        if (livesCount == 0)
        {
            GameOver();
        }
    }

    public void ChangeVolume(float volume)
    {
        backgroundMusic.volume = volume;
    }

    private void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        spawnRate /= difficulty;
        livesCount = livesAmount;
        UpdateLivesText();

        UpdateScore(0);
        StartCoroutine(SpawnTarget());
        titleScreen.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
}
