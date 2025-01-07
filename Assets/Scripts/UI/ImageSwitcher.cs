using UnityEngine;

namespace UI
{
    public class ImageSwitcher : MonoBehaviour
    {
        // Array to hold all images (from image0 to image9)
        public GameObject[] images;

        public bool finished { get; set; }

        // Start is called before the first frame update
        void Start()
        {
            // Initially, show image0 and hide all others
            SetImageVisibility(0); // Show image0 initially
        }

        // Method to switch to a specific image based on the button press
        public void SwitchImage(int buttonIndex)
        {
            // Check if the buttonIndex is within valid bounds
            if (buttonIndex >= 0 && buttonIndex < images.Length)
            {
                // Simply set the selected image visible and hide all others
                SetImageVisibility(buttonIndex);
            }
        }

        // Helper method to set visibility of images by index
        void SetImageVisibility(int index)
        {
            // Loop through all images and set them active or inactive
            for (int i = 0; i < images.Length; i++)
            {
                // If it's the selected image, set it to active, else set inactive
                images[i].SetActive(i == index);
            }
        }
    }
}
