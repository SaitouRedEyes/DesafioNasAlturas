using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject obstacle;
    private List<GameObject> obstacles = new List<GameObject>();
    private float obstaclesGenerationTimer = 3.0f;
    private float obstaclesTimeController;
    [SerializeField]
    private Image gameOverUI;
    private PlayerController player;
    [SerializeField]
    private AudioSource mainAudio;
    private bool gameOver;
    public bool IsGameOver {get { return gameOver; }}    
    private Text score, highscore;
    public int Score { get {return int.Parse(score.text);} set {if (value == 1) score.text = (int.Parse(score.text) + 1).ToString();}}
    private const float maxDifficultTime = 60;
    private float gameDifficult = 0;
    private float tempTime = -1;
    private float gameElapsedTime = 0;
    [SerializeField]
    private Sprite[] medals;
    [SerializeField]
    private Image medal;
    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();        
        
        GetHighscore(); 
    }

    // Update is called once per frame
    void Update()
    {
        GameDifficult();
        ObstaclesGenerator();
    }
    
    public void GameOver()
    {   
        mainAudio.Stop();          
        gameOver = true;
        gameOverUI.gameObject.SetActive(true);
        HighScore();           
    }

    public void StartGame()
    {   
        gameOver = false;
        gameOverUI.gameObject.SetActive(false);
        score.text = "0";
        player.Restart();
        DestroyObstacles();
        mainAudio.Play();
        obstaclesGenerationTimer = 3.0f;
        tempTime = -1;
        gameElapsedTime = Time.time;
    }
    private void ObstaclesGenerator()
    {
        obstaclesTimeController -= Time.deltaTime;

        if(obstaclesTimeController < 0)
        {
            obstacles.Add(Instantiate(obstacle, obstacle.transform.position, Quaternion.identity));
            obstaclesTimeController = obstaclesGenerationTimer;
        }
    }

    private void DestroyObstacles()
    {        
        foreach(GameObject obstacle in obstacles) Destroy(obstacle);
        obstacles.Clear();
    }

    private void GetHighscore()
    {
        try { PlayerPrefs.GetInt("Highscore"); } 
        catch(Exception e) {  Debug.Log(e); PlayerPrefs.SetInt("Highscore", 0);}        
    }

    private void HighScore()
    {
        int highscoreSaved = PlayerPrefs.GetInt("Highscore");
        highscore = GameObject.FindGameObjectWithTag("Highscore").GetComponent<Text>();
        highscore.text = highscoreSaved.ToString();

        if(int.Parse(score.text) > highscoreSaved) 
        {
            PlayerPrefs.SetInt("Highscore", int.Parse(score.text));
            highscore.text = score.text;
            medal.sprite = medals[0];
        }
        else if(int.Parse(score.text) >= highscoreSaved / 2) medal.sprite = medals[1];
        else medal.sprite = medals[2];
    }

    private void GameDifficult()
    {
        gameDifficult = (int)Time.time - (int)gameElapsedTime;
        gameDifficult = Mathf.Min(maxDifficultTime, gameDifficult);
        
        if(gameDifficult % (maxDifficultTime / 3) == 0 && gameDifficult != tempTime) 
        {
            obstaclesGenerationTimer -= 0.5f;
            tempTime = gameDifficult;
        }
    }
}
