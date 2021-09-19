using System;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using util;
using static util.GameState;
using Debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour
{
    private static GameState gameState = MENU;
    
    private int score;
    private int balls = 3;
    
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI ballsText;
    
    [SerializeField] private GameObject menu;
    
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject paddlePrefab;
    
    [SerializeField] private GameObject[] levels;
    private GameObject level;
    
    private int currentLevel = 0;
    
    private GameObject currentBall;
    
    private static GameManager instance;
    
    void Start()
    {
        if (gameState == GAMEOVER)
        {
            titleText.SetText(gameState.ToString());
            highScoreText.SetText(String.Format("Highscore: {0}", instance.score));
        }
        
        instance = this;
    }


    public void Init()
    {
        menu.SetActive(false);
        Cursor.visible = false;
        
        UpdateScore(0);
        LoseBall(0);

        
        Instantiate(paddlePrefab, new Vector3(0, -17, 0), Quaternion.identity);
        
        Play();
    }
    private void Play()
    {
        if (level == null)
        {
            level = Instantiate(levels[currentLevel++], Vector3.zero, Quaternion.identity);
        }
        
        titleText.SetText("");
        currentBall = Instantiate(ballPrefab, new Vector3(0, 8, 0), Quaternion.identity);
        
        gameState = GAME;
    }
    
    void Update()
    {
        if (gameState == GAME)
        {
            if (level.transform.childCount == 0) //bad
            {
                gameState = INIT;
                level = null;
                Destroy(currentBall);
                titleText.SetText(String.Format("Level {0}", currentLevel + 1));
                Invoke("Play", 2);
            }
            else if (currentBall.transform.position.y < -20)
            {
                if (LoseBall(1) <= 0)
                {
                    LoadMenu();
                }
                else
                {
                    gameState = INIT;
                    titleText.SetText(String.Format("Balls left: {0}", balls));
                    Invoke("Play", 2);
                }
            }
        }
    }


    public static void UpdateScore(int points)
    {
        instance.score += points;
        instance.scoreText.SetText(String.Format("Score: {0}", instance.score));
    }

    private int LoseBall(int balls)
    {
        this.balls -= balls;
        ballsText.SetText(String.Format("Balls: {0}", this.balls));
        Destroy(currentBall);
        return this.balls;
    }
    

    private void LoadMenu()
    {
            
        gameState = GameState.GAMEOVER;
        Cursor.visible = true;
        SceneManager.LoadScene("SampleScene");
    }
}
