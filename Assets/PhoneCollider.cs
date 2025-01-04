using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PhoneCollider : MonoBehaviour
{
    public delegate void TriggerEvent(Collider collider);

    void OnTriggerEnter(Collider other)
    {
        OnPhoneTriggerEnter?.Invoke(other);
    }

    void OnTriggerExit(Collider other)
    {
        OnPhoneTriggerExit?.Invoke(other);
    }

    void OnTriggerStay(Collider other)
    {
        OnPhoneTriggerStay?.Invoke(other);
    }

    public static event TriggerEvent OnPhoneTriggerEnter;
    public static event TriggerEvent OnPhoneTriggerStay;
    public static event TriggerEvent OnPhoneTriggerExit;
}
