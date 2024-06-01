using UnityEngine;
using TMPro;

public class SelectionTextHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro _hoverText;

    private void Awake()
    {
        if (_hoverText == null)
        {
            _hoverText = GetComponentInChildren<TextMeshPro>();
        }

        // Ensure the text is initially hidden
        _hoverText.gameObject.SetActive(false);
    }

    public void ShowHoverText()
    {
        _hoverText.gameObject.SetActive(true);
        _hoverText.text = "SELECTED";
        _hoverText.color = Color.yellow;
    }

    public void HideHoverText()
    {
        _hoverText.gameObject.SetActive(false);
    }
}

