using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{   
    // Buttons
    private Button _mainMenuExitButton;
    private Button _mainMenuInventoryButton;
    private Button _mainMenuPlayButton;
    private Button _mainMenuSettingsButton;

    [Space]
    [Header ("Canvases")]
    [SerializeField] private GameObject _gameCanvas;
    [SerializeField] private GameObject _mainMenuCanvas;

    [Space]
    [Header ("Scripts")]
    [SerializeField] private DeathPanel _deathScreenScript;

    void OnValidate()
    {
        if (_mainMenuPlayButton == null) _mainMenuPlayButton = transform.Find("ui_button_main_menu_play")?.GetComponent<Button>();

        if (_mainMenuInventoryButton == null) _mainMenuInventoryButton = transform.Find("ui_button_main_menu_inventory")?.GetComponent<Button>();

        if (_mainMenuExitButton == null) _mainMenuExitButton = transform.Find("ui_button_main_menu_exit")?.GetComponent<Button>();

        if (_mainMenuSettingsButton == null) _mainMenuSettingsButton = transform.Find("ui_button_main_menu_settings")?.GetComponent<Button>();
    }

    void OnEnable()
    {
        _mainMenuPlayButton.onClick.AddListener(StartGame);
    }

    void OnDisable()
    {
        _mainMenuPlayButton.onClick.RemoveListener(StartGame);
    }
    public void StartGame()
    {   
        _gameCanvas.SetActive(true);
        _deathScreenScript.Restart();
        _mainMenuCanvas.SetActive(false);
    }
}
