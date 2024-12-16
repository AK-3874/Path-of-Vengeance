using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelingManager : MonoBehaviour
{
    // Gem EXP Constants
    const int BLUE_GEM = 5;
    const int GREEN_GEM = 3;

    // Level up variables
    int characterLevel = 1, experience = 0, requiredExp = 5, requiredModifier = 10;

    // Health regen
    public HealthRegenUpgrade[] regenUpgrades;
    int regenLevel = 0;

    // Health
    public HealthUpgrade[] healthUpgrades;
    int healthLevel = 0;
    GameManager manager;

    [SerializeField] GameObject shield;
    ElectricShield electricShield;
    [SerializeField] SpinningScythe spinningScythe;

    List<string> abilities = new List<string>();

    [SerializeField] GameObject upgradeMenu, button_1, button_2, button_3;
    [SerializeField] Image img_1, img_2, img_3;
    [SerializeField] Sprite electric, health, regen, sword;
    string button_1_function, button_2_function, button_3_function;

    private void Awake()
    {
        upgradeMenu.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        electricShield = shield.GetComponent<ElectricShield>();
        manager = GameObject.FindObjectOfType<GameManager>();
        abilities.Add("Sword");
        abilities.Add("Shield");
        abilities.Add("Health");
        abilities.Add("Regen");
    }

    // Update is called once per frame
    void Update()
    {
        CheckLevel();
    }

    void AddBlueGem()
    {
        experience += BLUE_GEM;
    }

    void CheckLevel()
    {
        if (experience >= requiredExp)
        {
            experience = 0;
            requiredExp += requiredModifier;
            characterLevel++;
            StartUpgrade();
            Debug.Log("Level up!");
        }
    }

    void StartUpgrade()
    {
        SetUpMenu();
        manager.UpgradePause();
    }

    void UpgradeScythe()
    {
        if (!spinningScythe.isActiveAndEnabled)
        {
            spinningScythe.enabled = true;
        }
        else
        {
            spinningScythe.UpgradeAxe();
        }
        manager.ResumeGame();
        upgradeMenu.SetActive(false);
    }

    void UpgradeShield()
    {
        if (!shield.activeSelf)
        {
            shield.SetActive(true);
        }
        else
        {
            electricShield.UpgradeShield();
        }
        manager.ResumeGame();
        upgradeMenu.SetActive(false);
    }

    void UpgradeHealth()
    {
        healthLevel++;
        if (healthLevel < healthUpgrades.Length)
            GetComponent<Varian>().SetMaxHealth(healthUpgrades[healthLevel].health);
        manager.ResumeGame();
        upgradeMenu.SetActive(false);
    }

    void UpgradeRegen()
    {
        regenLevel++;
        if (regenLevel < regenUpgrades.Length)
            GetComponent<Varian>().SetRegen(regenUpgrades[regenLevel].regenRate);
        manager.ResumeGame();
        upgradeMenu.SetActive(false);
    }

    public string GetHealthUpgradeString()
    {
        return string.Format("Sets health to ", healthUpgrades[healthLevel].health);
    }

    public string GetRegenUpgradeString()
    {
        return string.Format("Sets health regeneration to ", regenUpgrades[regenLevel].regenRate);
    }

    public void Button_1()
    {
        switch (button_1_function)
        {
            case "Sword":
                UpgradeScythe();
                break;
            case "Shield":
                UpgradeShield();
                break;
            case "Health":
                UpgradeHealth();
                break;
            case "Regen":
                UpgradeRegen();
                break;
        }
    }

    public void Button_2()
    {
        switch (button_2_function)
        {
            case "Sword":
                UpgradeScythe();
                break;
            case "Shield":
                UpgradeShield();
                break;
            case "Health":
                UpgradeHealth();
                break;
            case "Regen":
                UpgradeRegen();
                break;
        }
    }

    public void Button_3()
    {
        switch (button_3_function)
        {
            case "Sword":
                UpgradeScythe();
                break;
            case "Shield":
                UpgradeShield();
                break;
            case "Health":
                UpgradeHealth();
                break;
            case "Regen":
                UpgradeRegen();
                break;
        }
    }

    void SetUpMenu()
    {
        int numberOfAbilities = abilities.Count;
        List<string> list = new List<string>(abilities);
        List<string> upgradable = new List<string>();

        int index_1 = UnityEngine.Random.Range(0, numberOfAbilities--);
        upgradable.Add(list[index_1]);
        list.RemoveAt(index_1);

        int index_2 = UnityEngine.Random.Range(0, numberOfAbilities--);
        upgradable.Add(list[index_2]);
        list.RemoveAt(index_2);

        int index_3 = UnityEngine.Random.Range(0, numberOfAbilities);
        upgradable.Add(list[index_3]);

        upgradeMenu.SetActive(true);

        button_1_function = upgradable[0];
        Debug.Log(button_1_function);
        button_1.GetComponentInChildren<Text>().text = button_1_function;

        switch (button_1_function)
        {
            case "Sword":
                img_1.sprite = sword;
                break;
            case "Shield":
                img_1.sprite = electric;
                break;
            case "Health":
                img_1.sprite = health;
                break;
            case "Regen":
                img_1.sprite = regen;
                break;
        }

        button_2_function = upgradable[1];
        Debug.Log(button_2_function);
        button_2.GetComponentInChildren<Text>().text = button_2_function;

        switch (button_2_function)
        {
            case "Sword":
                img_2.sprite = sword;
                break;
            case "Shield":
                img_2.sprite = electric;
                break;
            case "Health":
                img_2.sprite = health;
                break;
            case "Regen":
                img_2.sprite = regen;
                break;
        }

        button_3_function = upgradable[2];
        Debug.Log(button_3_function);
        button_3.GetComponentInChildren<Text>().text = button_3_function;

        switch (button_3_function)
        {
            case "Sword":
                img_3.sprite = sword;
                break;
            case "Shield":
                img_3.sprite = electric;
                break;
            case "Health":
                img_3.sprite = health;
                break;
            case "Regen":
                img_3.sprite = regen;
                break;
        }
    }
}

[System.Serializable]
public class HealthUpgrade
{
    public float health;
}

[System.Serializable]
public class HealthRegenUpgrade
{
    public float regenRate;
}