using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class ChargerUIController : MonoBehaviour
    {
        public List<RectTransform> panels = new List<RectTransform>();
        public bool done { get; set; }

        // Start is called before the first frame update
        void Start()
        {
            if (panels.Count > 0)
                SwitchScreen(panels[0]);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SwitchScreen(RectTransform screen)
        {
            foreach (var panel in panels)
            {
                panel.gameObject.SetActive(panel == screen);
            }
        }
    }
}
