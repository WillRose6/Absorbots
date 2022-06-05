using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Creates Viewable Variables
    public int score;
    public WaveManager m_WaveManager;
    public ScoreManager m_ScoreManager;
    private bool paused = false;
    public CameraShake m_CameraShake;
    private bool gameWon;

    //Creates Hidden Variables
    [HideInInspector] public static GameManager m_Instance;
    [HideInInspector] public bool m_UpgradeChosen;

    public bool GameWon { get => gameWon; set => gameWon = value; }

    //Makes Manager Singleton
    void Awake()
    {
        MakeSingleton();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void MakeSingleton()
    {
        if (m_Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            m_Instance = this;
        }
    }

    public void ShowUpgradeScreen()
    {
        m_UpgradeChosen = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StartCoroutine(UI.instance.ToggleUpgradeScreen(true));
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Inputs.ToggleLock(true);
    }

    public void HideUpgradeScreen()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(UI.instance.ToggleUpgradeScreen(false));
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.Inputs.ToggleLock(false);
        player.SetHealth(player.MaxHealth);
        m_UpgradeChosen = true;
    }

    public void TogglePause()
    {
        if (gameWon == false)
        {
            paused = !paused;
            UI.instance.TogglePause(paused);
            PlayerInputs inputs = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputs>();

            if (paused)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                inputs.ToggleLock(true);
                Time.timeScale = 0;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                inputs.ToggleLock(false);
                Time.timeScale = 1;
            }
        }
    }

    public void ShakeCamera()
    {
        m_CameraShake.CreateShake();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void WinGame()
    {
        References.instance.player.Inputs.ToggleLock(true);
        UI.instance.WinGame();
        m_WaveManager.StopAllCoroutines();
        gameWon = true;
        KillAllEnemies();
    }

    public void KillAllEnemies()
    {
        GameObject[] objs = GameObject.FindObjectsOfType<GameObject>();

        foreach(GameObject g in objs)
        {
            if (g.layer == LayerMask.NameToLayer("Enemy") || g.layer == LayerMask.NameToLayer("Boss") || g.layer == LayerMask.NameToLayer("Minion"))
            {
                Destroy(g);
            }
        }
    }
}
