using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{   
    [Header ("Buttons")]
    [SerializeField] private Button _mainMenuExitButton;
    [SerializeField] private Button _mainMenuInventoryButton;
    [SerializeField] private Button _mainMenuPlayButton;
    [SerializeField] private Button _mainMenuSettingsButton;

    [Space]
    [Header ("Canvases")]
    [SerializeField] private GameObject _gameCanvas;
    [SerializeField] private GameObject _mainMenuCanvas;
    [SerializeField] private GameObject _settingsCanvas;
    [SerializeField] private GameObject _inventoryCanvas;

    [Space]
    [Header ("Scripts")]
    [SerializeField] private DeathPanel _deathScreenScript;
    [SerializeField] private SettingsPanel _settingsPanel;

    void OnValidate()
    {
        if (_mainMenuPlayButton == null) _mainMenuPlayButton = transform.Find("ui_button_main_menu_play")?.GetComponent<Button>();

        if (_mainMenuInventoryButton == null) _mainMenuInventoryButton = transform.Find("ui_button_main_menu_inventory")?.GetComponent<Button>();

        if (_mainMenuExitButton == null) _mainMenuExitButton = transform.Find("ui_button_main_menu_exit")?.GetComponent<Button>();

        if (_mainMenuSettingsButton == null) _mainMenuSettingsButton = transform.Find("ui_button_main_menu_settings")?.GetComponent<Button>();
    }

    void Start()
    {
        OpenCanvas(_mainMenuCanvas);
        _settingsPanel.LoadLanguage();
        _settingsPanel.LoadVolume();
        
        _mainMenuPlayButton.onClick.RemoveAllListeners();
        _mainMenuExitButton.onClick.RemoveAllListeners();
        _mainMenuInventoryButton.onClick.RemoveAllListeners();
        _mainMenuSettingsButton.onClick.RemoveAllListeners();

        _mainMenuPlayButton.onClick.AddListener(() => OpenCanvas(_gameCanvas));
        _mainMenuSettingsButton.onClick.AddListener(() => OpenCanvas(_settingsCanvas));
        _mainMenuExitButton.onClick.AddListener(OnExitClicked);
        _mainMenuInventoryButton.onClick.AddListener(() => OpenCanvas(_inventoryCanvas));
    }

    public void OpenCanvas(GameObject selectedCanvas)
    {
        _settingsCanvas.SetActive(false);
        _mainMenuCanvas.SetActive(false);
        _gameCanvas.SetActive(false);
        _inventoryCanvas.SetActive(false);

        selectedCanvas.SetActive(true);
    }

    void OnExitClicked()
    {
        Application.Quit();
    }

    public void OpenMainMenuCanvas()
    {
        OpenCanvas(_mainMenuCanvas);
    }
}
