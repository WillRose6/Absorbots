using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI instance;
    [SerializeField]
    private Image crosshair;
    [SerializeField]
    private TMPro.TextMeshProUGUI ammoText;
    [SerializeField]
    private TMPro.TextMeshProUGUI gunName;
    [SerializeField]
    private TMPro.TextMeshProUGUI healthText;
    [SerializeField]
    private GameObject UpgradeScreen;
    [SerializeField]
    private GameObject mainUIObj;
    [SerializeField]
    private Animator upgradeAnimator;
    [SerializeField]
    private AnimationClip HideUpgradeScreen;
    [SerializeField]
    private TMPro.TextMeshProUGUI roundText;
    [SerializeField]
    private TMPro.TextMeshProUGUI scoreText;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject gameOverMenu;
    [SerializeField]
    private TMPro.TextMeshProUGUI Tooltip;
    [SerializeField]
    private GameObject WinScreen;
    [SerializeField]
    private TMPro.TextMeshProUGUI Powerup;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("UI instance already defined!");
            return;
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {

        crosshair.enabled = false;
        Powerup.gameObject.SetActive(false);
        ToggleAmmoText(false);
        ToggleGunName(false);
        ToggleCrosshair(true);
    }

    private void Update()
    {
        SetHP(GameObject.FindGameObjectWithTag("Player").GetComponent<LivingBeing>().CurrentHealth);
    }

    public void ToggleCrosshair(bool toggle)
    {
        crosshair.enabled = toggle;
    }

    public void updateAmmo(int amountLeft, int magazineSize)
    {
        if(amountLeft < 0)
        {
            amountLeft = 0;
        }
        ammoText.text = amountLeft + "/" + magazineSize;
    }

    public void ToggleAmmoText(bool Toggle)
    {
        ammoText.gameObject.SetActive(Toggle);
    }

    public void ToggleGunName(bool Toggle)
    {
        gunName.gameObject.SetActive(Toggle);
    }

    public void SetGunName(string name)
    {
        gunName.text = name;
    }

    public void SetHP(float health)
    {
        healthText.text = health.ToString() + " HP";
    }

    public void SetRoundNum(int num)
    {
        roundText.text = "Wave " + num.ToString();
    }

    public void SetScore(float score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public IEnumerator ToggleUpgradeScreen(bool Toggle)
    {
        if (Toggle)
        {
            mainUIObj.SetActive(false);
            UpgradeScreen.SetActive(true);
            upgradeAnimator.SetTrigger("ShowScreen");
        }
        else
        {
            upgradeAnimator.SetTrigger("HideScreen");
            yield return new WaitForSeconds(HideUpgradeScreen.length);
            mainUIObj.SetActive(true);
            UpgradeScreen.SetActive(false);
        }
    }

    public void TogglePause(bool Toggle)
    {
        pauseMenu.SetActive(Toggle);
    }

    public void GameOver()
    {
        GameManager.m_Instance.TogglePause();
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(true);
    }

    public void ToggleTooltip(bool Toggle)
    {
        Tooltip.gameObject.SetActive(Toggle);
    }

    public void SetTooltip(string Text)
    {
        Tooltip.text = Text;
    }

    public void WinGame()
    {
        mainUIObj.SetActive(false);
        WinScreen.SetActive(true);
    }

    IEnumerator TogglePowerup()
    {
        Powerup.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        Powerup.gameObject.SetActive(false);
    }

    public void SetPowerupText(string Effect)
    {
        Powerup.text = Effect + " Picked Up!";
        StartCoroutine(TogglePowerup());
        
    }
}
