using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SpinButton : MonoBehaviour
{
    private Button _spinButton;
    [SerializeField] WheelScript _wheelScript;
    

    void OnValidate()
    {
        if (_spinButton == null)
            _spinButton = gameObject.GetComponent<Button>();

        if (_wheelScript == null)
            Debug.LogWarning("_wheelScript reference is missing!!");
    }

    void Awake()
    {
        if (_wheelScript == null)
        {
            Debug.LogError("SpinButton: WheelScript is not assigned!", this);
            return;
        }

        _spinButton.onClick.AddListener(_wheelScript.StartSpin);
    }

    void OnDestroy()
    {   if( _wheelScript != null)
            _spinButton.onClick.RemoveListener(_wheelScript.StartSpin);
    }
}
