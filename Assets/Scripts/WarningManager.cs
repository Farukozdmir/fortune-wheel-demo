using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningManager : MonoBehaviour
{
    [SerializeField] private GameObject _warningPrefab;

    public void AddWarning(string warningKey)
    {
        GameObject currentWarning = Instantiate(_warningPrefab , transform);

        LocalizedTMPText localizedText = currentWarning.transform.GetChild(0).GetComponent<LocalizedTMPText>();

        localizedText._entryKey = warningKey;
        localizedText.Refresh();

        Destroy(currentWarning , 3f);
    }
}
