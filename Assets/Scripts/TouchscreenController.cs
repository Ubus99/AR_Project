using UnityEngine;

public class TouchscreenController : MonoBehaviour
{
    public GameObject loginScreen;
    public GameObject mainMenuScreen;
    public GameObject printScreen;

    // Start is called before the first frame update
    void Start()
    {
        loginScreen.SetActive(true);
        mainMenuScreen.SetActive(false);
        printScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnNfcButtonPressed()
    {
#if UNITY_EDITOR
        Debug.Log("NFC button pressed");
#endif

        loginScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
    }

    public void OnPrintSelected()
    {
#if UNITY_EDITOR
        Debug.Log("Print button pressed");
#endif

        mainMenuScreen.SetActive(false);
        printScreen.SetActive(true);
    }
}
