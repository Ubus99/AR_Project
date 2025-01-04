using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HintPanel : MonoBehaviour
{
    public TextMeshProUGUI label;
    public Button button;

    void Awake()
    {
        if (!label)
            label = GetComponentInChildren<TextMeshProUGUI>();

        if (!button)
            button = GetComponentInChildren<Button>();

        button.onClick.AddListener(CloseHintPanel);
    }

    void OnDisable()
    {
        button.onClick.RemoveListener(CloseHintPanel);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Show(string hint)
    {
        label.text = hint;
        gameObject.SetActive(true);
    }

    void CloseHintPanel()
    {
        gameObject.SetActive(false);
    }
}
