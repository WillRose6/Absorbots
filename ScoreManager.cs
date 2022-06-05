using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //Creates Viewable Variables
    public GameObject m_IAmmoPrefab;
    public GameObject m_HealthPrefab;
    public GameObject m_DPointsPrefab;

    //Creates Hidden Variables
    [HideInInspector] public static ScoreManager m_Instance;
    [HideInInspector] public int m_PointMultiplier;
    [HideInInspector] public float m_Score = -1;

    //Makes Manager Singleton
    void Awake()
    {
        MakeSingleton();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        IncrementScore("Minion", gameObject);
        revertPMult();
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

    public void IncrementScore(string tag, GameObject self)
    {
        if (tag == "Minion")
        {
            m_Score += (10 * m_PointMultiplier);
        }
        else if (tag == "Enemy")
        {
            m_Score += (50 * m_PointMultiplier * self.GetComponent<EnemyStats>().powerLVL);
            DropPickup(self);
        }
        else
        {
            m_Score += (500 * m_PointMultiplier * self.GetComponent<EnemyStats>().powerLVL);
        }
        UI.instance.SetScore(m_Score);
    }

    public void DropPickup(GameObject self)
    {
        float toDrop = Random.Range(1, 31);
        Vector3 position = new Vector3(self.transform.position.x, self.transform.position.y + 1.2f, self.transform.position.z);
        if (toDrop == 28)
        {
            GameObject drop = Instantiate(m_DPointsPrefab, position, self.transform.rotation) as GameObject;
        }
        else if (toDrop == 29)
        {
            GameObject drop = Instantiate(m_IAmmoPrefab, position, self.transform.rotation) as GameObject;
        }
        else if(toDrop == 30)
        {
            GameObject drop = Instantiate(m_HealthPrefab, position, self.transform.rotation) as GameObject;
        }
    }

    public void doublePMult()
    {
        m_PointMultiplier = 2;
        Invoke("revertPMult", 20f);
    }

    public void revertPMult()
    {
        m_PointMultiplier = 1;
    }

    public void ActivateIAmmo()
    {
        RangedWeapon gun = References.instance.gun;
        gun.m_Infinite = true;
        UI.instance.updateAmmo(gun.templates[gun.GetCurrentTemplateIndex()].magazineSize, gun.templates[gun.GetCurrentTemplateIndex()].magazineSize);
        Invoke("DeactivateIAmmo", 20f);
    }

    public void DeactivateIAmmo()
    {
        RangedWeapon gun = References.instance.gun;
        gun.m_Infinite = false;
    }

    public void ChangeScore(float score)
    {
        m_Score += (score * m_PointMultiplier);
        UI.instance.SetScore(m_Score);
    }
}