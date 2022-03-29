using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class Buttons : MonoBehaviour
{
    public ScoreController difficultyMultiplier;
    public GameObject gameBoard;
    public GameObject difficultyCanvas;
    public void OnEasyClicked()
    {
        gameBoard.SetActive(true);
        difficultyCanvas.SetActive(false);
        ScoreController.setMultimplier(0.5f);
    }

    public void OnMediumClicked()
    {
        gameBoard.SetActive(true);
        difficultyCanvas.SetActive(false);
        ScoreController.setMultimplier(1.5f);
    }


    public void OnHardClicked()
    {
        gameBoard.SetActive(true);
        difficultyCanvas.SetActive(false);
        ScoreController.setMultimplier(5.0f);

    }
    public void OnMainMenuClicked()
    {
        Time.timeScale = 1f;
        Application.LoadLevel(Application.loadedLevel);

    }
}
