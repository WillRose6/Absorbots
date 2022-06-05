using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    //Create Variables To Edit
    public Player m_Player;
    public float m_WaveDelay;
    public float m_WaveLength;
    public GameObject m_MinionPrefab;
    public GameObject m_EnemyPrefab;
    public GameObject m_BossPrefab;
    public Transform[] m_SpawnPoints;
    public Transform m_BossSpawn;
    public const int BASE_HEALTH = 45;
    public const float SPAWN_TIME = 10f;
    public const float BASE_SPAWN = 20f;

    //Create Hidden Variables
    [HideInInspector] public static WaveManager m_Instance;
    [HideInInspector] public int m_WaveNumber;
    [HideInInspector] public int m_RWaveNumber;
    [HideInInspector] public int m_SWaveNumber;
    [HideInInspector] public WaitForSeconds m_WaveWait;
    [HideInInspector] public WaitForSeconds m_WaveTime;
    [HideInInspector] public bool m_Spawned;

    //Makes Manager Singleton
    void Awake()
    {
        MakeSingleton();
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

    // Start is called before the first frame update
    void Start()
    {
        m_WaveWait = new WaitForSeconds(m_WaveDelay);
        m_WaveTime = new WaitForSeconds(m_WaveLength);
        StartCoroutine(GameLoop());
    }

    //Basic Gameplay Loop
    public IEnumerator GameLoop()
    {
        yield return StartCoroutine(StandardWavePlaying());
        yield return StartCoroutine(StandardWavePlaying());
        yield return StartCoroutine(StandardWavePlaying());
        yield return StartCoroutine(MinionWavePlaying());
        yield return StartCoroutine(BossWavePlaying());
        GameManager.m_Instance.ShowUpgradeScreen();

        while (!GameManager.m_Instance.m_UpgradeChosen)
        {
            yield return null;
        }

        StartCoroutine(GameLoop());
        
    }

    public void setWaveNumber(int waveNumber)
    {
        this.m_WaveNumber = waveNumber;
    }

    IEnumerator WaveStarting()
    {
        m_WaveNumber++;
        UI.instance.SetRoundNum(m_WaveNumber);
        yield return m_WaveWait;
    }

    //Spawns Enemies One By One At Given SpawnPoints
    IEnumerator MinionWavePlaying()
    {
        yield return StartCoroutine(WaveStarting());
        float toSpawn = Mathf.Floor(BASE_SPAWN * 1.5f * (Mathf.Pow(1.5f, m_SWaveNumber)));
        int j = 0;
        for (int i = 0; j < toSpawn; i++)
        {
            if (i == m_SpawnPoints.Length)
            {
                i = 0;
            }
            GameObject enemy = Instantiate(m_MinionPrefab, m_SpawnPoints[i].position + new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3)), m_SpawnPoints[i].rotation) as GameObject;
            enemy.GetComponent<LivingBeing>().ChangeHealth((BASE_HEALTH - 25) + (m_SWaveNumber + 1) * 10);
           j++;
            yield return new WaitForSeconds(SPAWN_TIME / toSpawn);
        }
    
        m_SWaveNumber++;
        yield return m_WaveTime;        
    }

    IEnumerator BossWavePlaying()
    {
        float combinedHP = 0.0f;
        float combinedPL = 0.0f;
        GameObject[] enemies = (GameObject.FindGameObjectsWithTag("Minion"));
        for (int i = 0; i < enemies.Length; i++)
        {
            combinedHP += enemies[i].GetComponent<LivingBeing>().CurrentHealth;
            combinedPL += enemies[i].GetComponent<AIPassiveStats>().powerLVL;
            Destroy(enemies[i]);
        }

        yield return StartCoroutine(WaveStarting());

        GameObject boss = Instantiate(m_BossPrefab, m_BossSpawn.position, m_BossSpawn.rotation) as GameObject;
        boss.GetComponent<LivingBeing>().ChangeHealth(combinedHP);
        boss.GetComponent<EnemyStats>().powerLVL = combinedPL;

        StartCoroutine(References.instance.bossDoor.OpenDoor());

        while ((GameObject.FindGameObjectsWithTag("Boss").Length) != 0)
        {
            yield return null;
        }
    }

    IEnumerator StandardWavePlaying()
    {
        yield return StartCoroutine(WaveStarting());
        float toSpawn = Mathf.Floor(BASE_SPAWN * (Mathf.Pow(1.2f, m_RWaveNumber)));
        int j = 0;
        for (int i = 0; j < toSpawn; i++)
        {
            if (i == m_SpawnPoints.Length)
            {
                i = 0;
            }
            GameObject enemy = Instantiate(m_EnemyPrefab, m_SpawnPoints[i].position + new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3)), m_SpawnPoints[i].rotation) as GameObject;
            enemy.GetComponent<LivingBeing>().ChangeHealth(BASE_HEALTH + (m_RWaveNumber + 1) * 5);
            j++;
            yield return new WaitForSeconds(SPAWN_TIME / toSpawn);
        }

        while ((GameObject.FindGameObjectsWithTag("Enemy").Length) != 0)
        {
            yield return null;
        }
        m_RWaveNumber++;    
    }
}
