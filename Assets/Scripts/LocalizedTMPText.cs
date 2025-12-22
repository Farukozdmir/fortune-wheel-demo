using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

[DisallowMultipleComponent]
[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizedTMPText : MonoBehaviour
{

    [Header("INPUT")]
    [SerializeField] private string _entryKey;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private StringTable _table;

    

    private void OnValidate()
    {
        if (_text == null)
            _text = GetComponent<TextMeshProUGUI>();
    }

    private void Awake()
    {
        LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
        _table = LocalizationSettings.StringDatabase.GetTable("UI");

        Refresh();
    }


    private void Refresh()
    {
        _table = LocalizationSettings.StringDatabase.GetTable("UI");

        if (_text == null || _table == null || string.IsNullOrEmpty(_entryKey))
            return;

        var entry = _table.GetEntry(_entryKey);

        if (entry != null )
        {
            _text.text = entry.GetLocalizedString();
        }
    }

    private void OnLocaleChanged(Locale _)
    {
        Refresh();
    }

    private void OnDestroy()
    {
        LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
    }
}
