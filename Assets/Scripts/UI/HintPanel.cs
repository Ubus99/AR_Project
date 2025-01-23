using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HintPanel : MonoBehaviour
    {

        static HintPanel _instance; // could lead to issues, but required for flexibility

        public TextMeshProUGUI label;
        public Button button;

        public static bool ready
        {
            get => _instance != null;
        }

        void Awake()
        {
            if (!label)
                label = GetComponentInChildren<TextMeshProUGUI>();

            if (!button)
                button = GetComponentInChildren<Button>();

            button.onClick.AddListener(CloseHintPanel);

            if (!label || !button || _instance)
                return;
            _instance = this;
        }

        void OnDisable()
        {
            button.onClick.RemoveListener(CloseHintPanel);
        }

        public static void Show()
        {
            if (!_instance)
            {
                Debug.LogError("HintPanel not instantiated");
                return;
            }
            _instance.gameObject.SetActive(true);
            Debug.Log("Show hint panel");
        }

        public static void Show(string hint)
        {
            if (!_instance)
            {
                Debug.LogError("HintPanel not instantiated");
                return;
            }

            _instance.label.text = hint;
            Show();
        }

        public static void CloseHintPanel()
        {
            if (!_instance)
            {
                Debug.LogError("HintPanel not instantiated");
                return;
            }
            _instance.gameObject.SetActive(false);
        }
    }
}
