using TMPro;
using UnityEngine;
using DG.Tweening;
public class RoundCounterWheelController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _roundText_0;
    [SerializeField] private TextMeshProUGUI _roundText_1;
    [SerializeField] private TextMeshProUGUI _roundText_2;
    [SerializeField] private TextMeshProUGUI _roundText_3;

    [SerializeField] private RectTransform _wheelTransform;


    public void SetWheel()
    {
        _wheelTransform.rotation = Quaternion.Euler(Vector3.zero);

        _roundText_0.text = (GameManager.instance.CurrentRound - 1).ToString();
        _roundText_1.text = GameManager.instance.CurrentRound.ToString();
        _roundText_2.text = (GameManager.instance.CurrentRound + 1).ToString();
        _roundText_3.text = (GameManager.instance.CurrentRound + 2).ToString();

    }

    public void NextRound()
    {
        _wheelTransform
            .DORotate(new Vector3(0f, 0f, 45f), 2f, RotateMode.Fast)
            .SetEase(Ease.OutCubic)
            .OnComplete(SetWheel);
    }
}
