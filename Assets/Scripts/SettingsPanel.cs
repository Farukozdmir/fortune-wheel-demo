using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    private const string MasterVolumePrefKey = "MASTER_VOLUME";
    private const string LanguagePrefKey = "LANGUAGE_CODE";

    [SerializeField] MainMenuPanel _mainMenuPanel;
    [SerializeField] Button _settingsExitButton;
    [SerializeField] Button _turkishButton;
    [SerializeField] Button _englishButton;
    [SerializeField] Slider _audioVolumeSlider;
    [SerializeField] AudioMixer _audioMixer;

    void OnValidate()
    {
        if (_settingsExitButton == null) _settingsExitButton = transform.Find("ui_button_settings_exit").GetComponent<Button>();
        if (_englishButton == null) _englishButton = transform.Find("ui_button_settings_language_english").GetComponent<Button>();
        if (_turkishButton == null) _turkishButton = transform.Find("ui_button_settings_language_turkish").GetComponent<Button>();
    }

    void Start()
    {
        _settingsExitButton.onClick.RemoveAllListeners();
        _englishButton.onClick.RemoveAllListeners();
        _turkishButton.onClick.RemoveAllListeners();
        
        _settingsExitButton.onClick.AddListener(_mainMenuPanel.OpenMainMenuCanvas);
        _englishButton.onClick.AddListener(() => SetLanguage("en"));
        _turkishButton.onClick.AddListener(() => SetLanguage("tr"));

        LoadLanguage();
        LoadVolume();
        _audioVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
    }

    // Volume

    public void SetMasterVolume(float sliderValue)
    {
        float volume = (_audioVolumeSlider.value*100) - 80;
        _audioMixer.SetFloat("MasterVolume", volume);

        PlayerPrefs.SetFloat(MasterVolumePrefKey, sliderValue);
        PlayerPrefs.Save();
    }

    public void LoadVolume()
    {
        float sliderValue = PlayerPrefs.GetFloat(MasterVolumePrefKey, 0.75f);

        _audioVolumeSlider.SetValueWithoutNotify(sliderValue);

        float volumeDb = (sliderValue * 100f) - 80f;
        _audioMixer.SetFloat("MasterVolume", volumeDb);
    }


    // Language

    private void SetLanguage(string code)
    {
        var locale = LocalizationSettings.AvailableLocales.GetLocale(code);
        if (locale == null)
        {
            Debug.LogWarning($"Locale not found: {code}");
            return;
        }

        LocalizationSettings.SelectedLocale = locale;

        PlayerPrefs.SetString(LanguagePrefKey, code);
        PlayerPrefs.Save();
    }

    public void LoadLanguage()
    {
        if (!PlayerPrefs.HasKey(LanguagePrefKey))
            return;

        string code = PlayerPrefs.GetString(LanguagePrefKey);
        var locale = LocalizationSettings.AvailableLocales.GetLocale(code);

        if (locale != null)
            LocalizationSettings.SelectedLocale = locale;
    }
}
