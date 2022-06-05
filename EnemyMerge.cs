using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMerge : MonoBehaviour
{
    public GameObject enemy;
    public GameObject audioSource;
    public AudioClip mergeClip;

    //When combining, the AI adds it's health and the health of the other enemy together and creates a new object with that HP.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && other.gameObject.GetComponent<EnemyStats>().merge)
        {
            float combinedHP = (other.gameObject.GetComponent<EnemyStats>().CurrentHealth + this.gameObject.GetComponent<EnemyStats>().CurrentHealth + 20);
            float combinedPowerLevel = (other.gameObject.GetComponent<EnemyStats>().powerLVL + this.gameObject.GetComponent<EnemyStats>().powerLVL);
            Destroy(other.gameObject);
            GameObject.FindGameObjectWithTag("Respawn").GetComponent<AISpawnTest>().SpawnEnemy(this.gameObject.transform, combinedHP,combinedPowerLevel);

            AudioSource a = Instantiate(audioSource, gameObject.transform.position, Quaternion.identity).GetComponent<AudioSource>();
            a.pitch = Random.Range(0.75f, 1.5f);
            a.volume = Random.Range(0.5f, 0.75f);
            a.clip = mergeClip;
            a.Play();
            Destroy(a, 2f);

            Destroy(this.gameObject);
        }
    }
}
