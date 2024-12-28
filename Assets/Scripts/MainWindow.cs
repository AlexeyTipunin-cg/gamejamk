using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainWindow : MonoBehaviour
{
    
    [SerializeField]private  TMP_Dropdown engineDropdown;
    [SerializeField]private  TMP_Dropdown bodyDropdown;
    [SerializeField]private  TMP_Dropdown weapon1Dropdown;
    [SerializeField]private  TMP_Dropdown weapon2Dropdown;
    [SerializeField]private  TMP_Dropdown weapon3Dropdown;
    [SerializeField]private  TMP_Dropdown weapon4Dropdown;
    [SerializeField]private  TMP_Dropdown weapon5Dropdown;
    [SerializeField]private  TMP_Dropdown weapon6Dropdown;



    [SerializeField] private EngineConfig[] engineConfigs;
    [SerializeField] private BodyConfig[] bodyConfigs;
    [SerializeField] private WeaponConfig[] weaponConfigs;
    
    [SerializeField]private TMP_Text weightText;
    [SerializeField] private Button startButton;
    
    [SerializeField]private TMP_Text cantStartText;
    
    [SerializeField]private Image[] weaponsImage;

    [SerializeField] private WeaponSlotConfig[] _weaponSlotConfigs;
    
    [SerializeField] private List<TMP_Dropdown> weaponsDropdowns = new List<TMP_Dropdown>();

    
    private EngineConfig _currentEngine;
    private BodyConfig _currentBody;

    private WeaponData[] weapons;
    
    private void Start()
    {
        weapons = new WeaponData[_weaponSlotConfigs.Length];
        
        engineDropdown.onValueChanged.AddListener(ChangeEngine);
        bodyDropdown.onValueChanged.AddListener(ChangeBody);
        
        weapon1Dropdown.onValueChanged.AddListener(Weapon1Dropdown);
        weapon2Dropdown.onValueChanged.AddListener(Weapon2Dropdown);
        weapon3Dropdown.onValueChanged.AddListener(Weapon3Dropdown);
        weapon4Dropdown.onValueChanged.AddListener(Weapon4Dropdown);
        weapon5Dropdown.onValueChanged.AddListener(Weapon5Dropdown);
        weapon6Dropdown.onValueChanged.AddListener(Weapon6Dropdown);
        
        weaponsDropdowns.Add(weapon1Dropdown);
        weaponsDropdowns.Add(weapon2Dropdown);
        weaponsDropdowns.Add(weapon3Dropdown);
        weaponsDropdowns.Add(weapon4Dropdown);
        weaponsDropdowns.Add(weapon5Dropdown);
        weaponsDropdowns.Add(weapon6Dropdown);

        
        startButton.onClick.AddListener(StartGame);

        foreach (TMP_Dropdown dropdown in weaponsDropdowns)
        {
            dropdown.options.Clear();
        }

        cantStartText.text = "Перевес. Подберите компоненты по весу и мощности двигателя";
        cantStartText.gameObject.SetActive(false);

        foreach (BodyConfig bodyConfig in bodyConfigs)
        {
            bodyDropdown.options.Add(new TMP_Dropdown.OptionData(bodyConfig.bodyName));
        }
        
        bodyDropdown.SetValueWithoutNotify(0);
        bodyDropdown.RefreshShownValue();
        
        _currentEngine = engineConfigs[0];
        
        foreach (EngineConfig engineConfig in engineConfigs)
        {
            engineDropdown.options.Add(new TMP_Dropdown.OptionData(engineConfig.engineName));
        }
        
        engineDropdown.SetValueWithoutNotify(0);
        engineDropdown.RefreshShownValue();
        
        _currentBody = bodyConfigs[0];

        foreach (TMP_Dropdown dropdown in weaponsDropdowns)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData("Пусто"));
            foreach (var weaponConfig in weaponConfigs)
            {
                dropdown.options.Add(new TMP_Dropdown.OptionData(weaponConfig.name));
            }
            dropdown.SetValueWithoutNotify(0);
            dropdown.RefreshShownValue();
        }
        
        foreach (Image image in weaponsImage)
        {
            image.gameObject.SetActive(false);
        }
        
        SetUpTextWeight();

    }
    
    private void Weapon1Dropdown(int arg0)
    {
        SetupWeapon(arg0, 0);
    }
    
    private void Weapon2Dropdown(int arg0)
    {
        SetupWeapon(arg0, 1);
    }
    
    private void Weapon3Dropdown(int arg0)
    {
        SetupWeapon(arg0, 2);
    }
    
    private void Weapon4Dropdown(int arg0)
    {
        SetupWeapon(arg0, 3);
    }
    
    private void Weapon5Dropdown(int arg0)
    {
        SetupWeapon(arg0, 4);
    }
    
    private void Weapon6Dropdown(int arg0)
    {
        SetupWeapon(arg0, 5);
    }

    private void SetupWeapon(int arg0, int slotIndex)
    {
        if (arg0 == 0)
        {
            var config =  _weaponSlotConfigs.First(config => config.index == slotIndex);
            weaponsImage[config.index].gameObject.SetActive(false);
            weapons[slotIndex] = null;
        }
        else
        {
            SetWeaponConfig(arg0, slotIndex);
        }
        
        SetUpTextWeight();
    }
    
    private void SetWeaponConfig(int value, int weaponIndex)
    {
        var weapon = weaponConfigs[value - 1];

        var config = _weaponSlotConfigs.First(config => config.index == weaponIndex);
        weaponsImage[config.index].sprite = weapon.weaponSprite;
        ((RectTransform)weaponsImage[config.index].transform).anchoredPosition = config.positionInConstructWindow;
        ((RectTransform)weaponsImage[config.index].transform).localRotation = Quaternion.Euler(0, 0, config.angleInConstructWindow);
        ((RectTransform)weaponsImage[config.index].transform).localScale = new Vector3(config.scale, config.scale, 1);
        weaponsImage[config.index].gameObject.SetActive(true);
        
        weapons[weaponIndex] = new WeaponData()
        {
            slotIndex = weaponIndex,
            weaponConfig = weapon
        };
    }

    private void ChangeEngine(int arg0)
    {
        _currentEngine = engineConfigs[arg0];
        Debug.Log("Engine selected: " + _currentEngine);
        
        SetUpTextWeight();

    }

    private void ChangeBody(int arg0)
    {
        _currentBody = bodyConfigs[arg0];
        Debug.Log("Body selected: " + _currentBody);
        
        SetUpTextWeight();

    }

    private void SetUpTextWeight()
    {
        weightText.text = $"Вес: {CalculateWeight()}/{_currentEngine.enginePower}";

    }

    private float CalculateWeight()
    {
        float weaponWeightSum = 0;
        foreach (WeaponData weapon in weapons)
        {
            if (weapon != null)
            {
                weaponWeightSum += weapon.weight;
            }
        }
        
        return _currentBody.weight + _currentEngine.weight + weaponWeightSum;
    }

    private void StartGame()
    {
        if (!IsOverweight())
        {
            SceneConnection.LoadGameScene(_currentEngine, _currentBody, weapons);
        }
    }

    private void Update()
    {
        if (IsOverweight())
        {
            cantStartText.gameObject.SetActive(true);
        }
        else
        {
            cantStartText.gameObject.SetActive(false);
        }
    }

    private bool IsOverweight()
    {
        return CalculateWeight() > _currentEngine.enginePower;
    }
}
