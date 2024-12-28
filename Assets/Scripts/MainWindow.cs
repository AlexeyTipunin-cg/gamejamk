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


    [SerializeField] private EngineConfig[] engineConfigs;
    [SerializeField] private BodyConfig[] bodyConfigs;
    [SerializeField] private WeaponConfig[] weaponConfigs;
    
    [SerializeField]private TMP_Text weightText;
    [SerializeField] private Button startButton;
    
    [SerializeField]private TMP_Text cantStartText;
    
    [SerializeField]private Image[] weaponsImage;

    [SerializeField] private WeaponSlotConfig[] _weaponSlotConfigs;

    
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
        
        startButton.onClick.AddListener(StartGame);

        bodyDropdown.options.Clear();
        engineDropdown.options.Clear();
        weapon1Dropdown.options.Clear();
        weapon2Dropdown.options.Clear();

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
        
        weapon1Dropdown.options.Add(new TMP_Dropdown.OptionData("Пусто"));

        foreach (WeaponConfig weaponConfig in weaponConfigs)
        {
            weapon1Dropdown.options.Add(new TMP_Dropdown.OptionData(weaponConfig.name));
        }
        
        weapon1Dropdown.SetValueWithoutNotify(0);
        weapon1Dropdown.RefreshShownValue();
        
        weapon2Dropdown.options.Add(new TMP_Dropdown.OptionData("Пусто"));

        foreach (WeaponConfig weaponConfig in weaponConfigs)
        {
            weapon2Dropdown.options.Add(new TMP_Dropdown.OptionData(weaponConfig.name));
        }

        foreach (Image image in weaponsImage)
        {
            image.gameObject.SetActive(false);
        }
        
        weapon2Dropdown.SetValueWithoutNotify(0);
        weapon2Dropdown.RefreshShownValue();
        
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
