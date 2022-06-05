using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public string Effect;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0.3f, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Effect == "Double Points")
            {
                if (ScoreManager.m_Instance.m_PointMultiplier != 2)
                {
                    ScoreManager.m_Instance.doublePMult();
                    References.instance.ui.SetPowerupText(Effect);
                }
                else
                {
                    Instantiate(gameObject);
                }
                
            }else if (Effect == "Health")
            {
                GameObject player = (GameObject.FindGameObjectWithTag("Player"));
                player.GetComponent<LivingBeing>().ChangeHealth(50);
                References.instance.ui.SetPowerupText(Effect);
            }
            else if (Effect == "Infinite Ammo")
            {
                if (!References.instance.gun.m_Infinite)
                {
                    RangedWeapon r = References.instance.gun;
                    r.Ammo[r.GetCurrentTemplateIndex()] = r.templates[r.GetCurrentTemplateIndex()].magazineSize;
                    ScoreManager.m_Instance.ActivateIAmmo();
                    References.instance.ui.SetPowerupText(Effect);
                }
                else
                {
                    Instantiate(gameObject);
                }
            }
            Destroy(gameObject);
        }      
    }
}
