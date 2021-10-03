using UnityEngine;
using UnityEngine.EventSystems;

public class DeselectButtonScript : MonoBehaviour
{
    private EventSystem _eventSystem;

    private void Start()
    {
        _eventSystem = GetComponent<EventSystem>();
    }

    public void DeselectButton()
    {
        if (_eventSystem == null)
            return;
        _eventSystem.SetSelectedGameObject(null);
    }
}