using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageRecognition : MonoBehaviour
{
    public GameObject prefab;

    ARTrackedImageManager _arTrackedImageManager;
    GameObject _instance;
    Transform _trackedImage;

    void Awake()
    {
        _arTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    void Update()
    {
        if (_trackedImage == null)
            return;
        Debug.DrawLine(_trackedImage.position, Vector3.one, Color.red);
    }

    void OnEnable()
    {
        _arTrackedImageManager.trackedImagesChanged += OnImageChange;
    }

    void OnDisable()
    {
        _arTrackedImageManager.trackedImagesChanged -= OnImageChange;
    }

    void OnImageChange(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var img in args.added)
        {
            _trackedImage = img.transform;

            if (!_instance)
            {
                _instance = Instantiate(prefab, _trackedImage.parent, false);
                _instance.transform.localPosition = _trackedImage.localPosition;
            }
            else
            {
                _instance.transform.position = _trackedImage.localPosition;
            }

            Debug.Log(img.name);
        }
    }
}
