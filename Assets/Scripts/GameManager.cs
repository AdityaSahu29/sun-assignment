using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public SpwanManager circleSpawner;
    public GameObject restartScreen;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void RestartGame()
    { 
        restartScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
