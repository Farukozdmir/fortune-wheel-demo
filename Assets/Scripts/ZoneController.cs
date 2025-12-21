using UnityEngine;
using UnityEngine.UI;

public class ZoneController : MonoBehaviour
{

    [SerializeField] private Image _wheelImage;
    [SerializeField] private Image _wheelIndicatorImage;
    
    [Space]
    [Header ("Wheel Sprites")]
    [SerializeField] private Sprite _bronzeWheelSprite;
    [SerializeField] private Sprite _silverWheelSprite;
    [SerializeField] private Sprite _goldenWheelSprite;

    [Space]
    [Header ("Wheel Indicator Sprites")]
    [SerializeField] private Sprite _bronzeWheelIndicatorSprite;
    [SerializeField] private Sprite _silverWheelIndicatorSprite;
    [SerializeField] private Sprite _goldenWheelIndicatorSprite;

    // Bools
    private bool _isSuperZone;
    public bool IsSuperZone {get {return _isSuperZone;}}
    private bool _isSafeZone;
    public bool IsSafeZone {get {return _isSafeZone;}}

    
    public void UpdateZone()
    {
        int spinCount = GameManager.instance.CurrentRound;
        if (spinCount % 30 == 0)
        { 
            _isSuperZone = true;
            _isSafeZone = false;

            ApplyVisual(_goldenWheelSprite , _goldenWheelIndicatorSprite);
        }
        else if (spinCount % 5 == 0) 
        {
            _isSafeZone = true;
            _isSuperZone = false;

            ApplyVisual(_silverWheelSprite , _silverWheelIndicatorSprite);
        }    
        else
        {
            _isSafeZone = false;
            _isSuperZone = false;

            ApplyVisual(_bronzeWheelSprite , _bronzeWheelIndicatorSprite);
        }
    }

    private void ApplyVisual(Sprite wheelSprite, Sprite indicatorSprite)
    {
        _wheelImage.sprite = wheelSprite;
        _wheelIndicatorImage.sprite = indicatorSprite;
    }

}
