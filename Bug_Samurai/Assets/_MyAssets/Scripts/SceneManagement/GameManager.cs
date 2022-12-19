using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    private void OnEnable() {
        PlayerHealth.OnPlayerDeaths += ReloadGame;
    }

    private void OnDisable() {
        PlayerHealth.OnPlayerDeaths -= ReloadGame;
    }

    void ReloadGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
