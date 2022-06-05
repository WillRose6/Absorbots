using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    [Header("Gun")]
    public ParticleSystem[] fireEffects;
    public Transform shootSpot;
    private float fireTime;
    public GameObject gunTrailPrefab;
    public ParticleSystem hitEffect;
    public List<GunTemplate> templates;
    private GunTemplate currentTemplate;
    public int[] ammo;
    private bool[] reloading;
    public Transform projectileLaunchPosition;
    public GameObject audioSource;
    [HideInInspector] public bool m_Infinite = false;
    public GunTemplate grenadeLauncherTemplate;
    public GunTemplate rifleTemplate;

    public int[] Ammo { get => ammo; set => ammo = value; }

    private void Start()
    {
        ammo = new int[3];
        ammo[0] = templates[0].magazineSize;

        reloading = new bool[3];
        for (int i = 0; i < reloading.Length; i++)
        {
            reloading[i] = false;
        }

        EType = ETypeOfWeapon.GUN;
        currentTemplate = templates[0];
        fireTime = currentTemplate.fireRate;
        ammo[GetCurrentTemplateIndex()] = currentTemplate.magazineSize;
    }

    public override void Interact()
    {
        base.Interact();
        UI.instance.ToggleAmmoText(true);
        UI.instance.ToggleGunName(true);
        UpdateUI();
    }

    protected override void Update()
    {
        base.Update();
        fireTime -= Time.deltaTime;
        if (transform.parent)
        {
            transform.localRotation = Quaternion.identity;
            transform.localPosition = Vector3.zero;
        }
    }

    public override void UseItem()
    {
        if (!reloading[GetCurrentTemplateIndex()])
        {
            if (fireTime < 0)
            {
                fireTime = currentTemplate.fireRate;
                base.UseItem();

                for (int i = 0; i < fireEffects.Length; i++)
                {
                    fireEffects[i].Play();
                }

                AudioSource a = Instantiate(audioSource, transform.position, transform.rotation).GetComponent<AudioSource>();
                a.clip = currentTemplate.fireSound;
                a.Play();
                Destroy(a.gameObject, 2f);

                if (currentTemplate.attackType == GunTemplate.AttackType.Raycast)
                {
                    heldByPlayer.ShootingUtility.ShootRaycast(shootSpot, gunTrailPrefab, hitEffect, currentTemplate.damage);
                }
                else if(currentTemplate.attackType == GunTemplate.AttackType.Projectile)
                {
                    heldByPlayer.ShootingUtility.ShootProjectile(currentTemplate.projectile, projectileLaunchPosition, currentTemplate.damage);
                }

                if (!m_Infinite)
                {
                    ammo[GetCurrentTemplateIndex()] -= currentTemplate.ammoReductionPerShot;
                    UI.instance.updateAmmo(ammo[GetCurrentTemplateIndex()], currentTemplate.magazineSize);
                }                

                if(ammo[GetCurrentTemplateIndex()] <= 0)
                {
                    StartCoroutine(reload(heldByPlayer.Items.ReloadSpeedMultiplier));
                }
            }
        }
    }

    public IEnumerator reload(float reloadSpeedMultiplier)
    {
        if (ammo[GetCurrentTemplateIndex()] < currentTemplate.magazineSize)
        {
            reloading[GetCurrentTemplateIndex()] = true;

            float elapsed = 0;

            AudioSource a = Instantiate(audioSource, transform.position, transform.rotation).GetComponent<AudioSource>();
            a.clip = currentTemplate.reloadSound;
            a.Play();
            Destroy(a.gameObject, 2f);

            while (elapsed < currentTemplate.reloadTime)
            {
                elapsed += Time.deltaTime * reloadSpeedMultiplier;
                yield return null;
            }

            reloading[GetCurrentTemplateIndex()] = false;
            ammo[GetCurrentTemplateIndex()] = currentTemplate.magazineSize;
            UI.instance.updateAmmo(ammo[GetCurrentTemplateIndex()], currentTemplate.magazineSize);
        }
    }

    public void UpdateUI()
    {
        UI.instance.updateAmmo(ammo[GetCurrentTemplateIndex()], currentTemplate.magazineSize);
        UI.instance.SetGunName(currentTemplate.name);
    }

    public void ChangeMode()
    {
        int currentIndex = GetCurrentTemplateIndex();
        currentIndex = (currentIndex != templates.Count-1) ? currentIndex + 1 : 0; 

        currentTemplate = templates[currentIndex];
        UpdateUI();
        for (int i = 0; i < reloading.Length; i++)
        {
            reloading[i] = false;
        }
        StopAllCoroutines();
    }
    
    public int GetCurrentTemplateIndex()
    {
        return templates.IndexOf(currentTemplate);
    }

    public void AddRifle()
    {
        templates.Add(rifleTemplate);
        ammo[templates.Count-1] = rifleTemplate.magazineSize;
    }

    public void AddGrenadeLauncher()
    {
        templates.Add(grenadeLauncherTemplate);
        ammo[templates.Count-1] = grenadeLauncherTemplate.magazineSize;
    }
}
