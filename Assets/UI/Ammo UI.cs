using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    private void OnEnable()
    {
        PlayerShooting.SpendAmmo += UpdateUI;
    }

    private void OnDisable()
    {
        PlayerShooting.SpendAmmo -= UpdateUI;
    }

    private void UpdateUI(int clipCount, int currentAmmo)
    {
        _text.text = $"{clipCount} / {currentAmmo}";
    }
}
