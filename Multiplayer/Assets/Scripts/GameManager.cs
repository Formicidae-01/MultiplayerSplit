using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject endScreen;
    public Text gameOverText;

    public bool gameIsOver;

    public PlayerTank[] players;

    public void ReloadScene()
    {
        GameIsOver(false);
        SceneManager.LoadScene(0);
    }

    public void FinishGame(TankColor loser)
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].enabled = false;
        }

        GameIsOver(true);

        switch (loser)
        {
            case TankColor.GREEN:
                {
                    gameOverText.text = "VERMELHO VENCEU!";
                }
                break;

            case TankColor.RED:
                {
                    gameOverText.text = "VERDE VENCEU!";
                }
                break;
        }

        endScreen.SetActive(true);
    }

    public void GameIsOver(bool value)
    {
        gameIsOver = value;
    }
}
