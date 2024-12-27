using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainWindow : MonoBehaviour
{
    
    [SerializeField]private  TMP_Dropdown engineDropdown;
    [SerializeField]private  TMP_Dropdown bodyDropdown;

    [SerializeField] private EngineConfig[] engineConfigs;
    [SerializeField] private BodyConfig[] bodyConfigs;
    
    [SerializeField]private TMP_Text weightText;
    [SerializeField] private Button startButton;
    
    [SerializeField]private TMP_Text cantStartText;

    
    private EngineConfig _currentEngine;
    private BodyConfig _currentBody;
    
    
    
    

    private void Start()
    {
        engineDropdown.onValueChanged.AddListener(ChangeEngine);
        bodyDropdown.onValueChanged.AddListener(ChangeBody);
        startButton.onClick.AddListener(StartGame);

        bodyDropdown.options.Clear();
        engineDropdown.options.Clear();

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
        
        SetUpTextWeight();

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
        return _currentBody.weight + _currentEngine.weight;
    }

    private void StartGame()
    {
        if (!IsOverweight())
        {
            SceneConnection.LoadGameScene(_currentEngine, _currentBody);
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
