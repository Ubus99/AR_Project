using System;
using UnityEngine;

namespace UI
{
    public class PrinterUIController : MonoBehaviour
    {

        public GameObject loginScreen;
        public GameObject mainMenuScreen;
        public GameObject printScreen;
        public GameObject errorScreen;

        // Start is called before the first frame update
        void Start()
        {
            SwitchScreen("Login");
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SwitchScreen(string screen)
        {
            loginScreen.SetActive(false);
            mainMenuScreen.SetActive(false);
            printScreen.SetActive(false);
            errorScreen.SetActive(false);

            if (!Enum.TryParse(screen, out Screens screenEnum))
                return;

            switch (screenEnum)
            {

                case Screens.Login:
#if UNITY_EDITOR
                    Debug.Log("screens reset");
#endif
                    loginScreen.SetActive(true);
                    break;
                case Screens.MainMenu:
#if UNITY_EDITOR
                    Debug.Log("NFC button pressed");
#endif
                    mainMenuScreen.SetActive(true);
                    break;
                case Screens.Print:
#if UNITY_EDITOR
                    Debug.Log("print button pressed");
#endif
                    printScreen.SetActive(true);
                    break;
                case Screens.Error:
#if UNITY_EDITOR
                    Debug.Log("printStart button pressed");
#endif
                    errorScreen.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(screen), screen, null);
            }
        }

        enum Screens
        {
            Login,
            MainMenu,
            Print,
            Error,
        }
    }
}
