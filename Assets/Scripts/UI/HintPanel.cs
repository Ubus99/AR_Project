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
        Debug.Log("Show hint panel");
    }

    public void Show(string hint)
    {
        label.text = hint;
        Show();
    }

    public void CloseHintPanel()
    {
        gameObject.SetActive(false);
    }
}
