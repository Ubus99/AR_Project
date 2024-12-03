using UnityEngine;

public class DebugRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, 10 * Time.deltaTime, Space.World);
    }
}
