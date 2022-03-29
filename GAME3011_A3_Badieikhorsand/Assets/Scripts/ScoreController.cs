using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreController : MonoBehaviour
{

    public Text TimerText;
    public Text ScoreText;
    public GameObject GameBoardCanvas;
    public Text GOTimerText;
    public Text GOScoreText;
    public GameObject GOCanvas;
    public static Slider Slider;
    private float timer;
    public static float score;
    public  static float difficultyMultiplier;

    private void Start()
    {
        Slider=FindObjectOfType<Slider>();
    }
    void Update()
    {
        timer += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer % 60F);
        int milliseconds = Mathf.FloorToInt((timer * 100F) % 100F);
        TimerText.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
        ScoreText.text = "Score:" + score;
        Slider.value -= Time.deltaTime * difficultyMultiplier;
        if (Slider.value <= 0)
        {
            GOTimerText.text = TimerText.text;
            GOScoreText.text = ScoreText.text;
            GOCanvas.SetActive(true);
            GameBoardCanvas.SetActive(false);
            Time.timeScale = 0f;


        }
    }
    public static void IncreaseScore(int value)
    {
        score += value;
        Slider.value += 10 / difficultyMultiplier;
       
    }
    public static void setMultimplier(float value)
    {
        difficultyMultiplier = value;

    }

}
