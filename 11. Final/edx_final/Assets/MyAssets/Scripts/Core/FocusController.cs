using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace core
{
    public class FocusController : MonoBehaviour
    {
        [SerializeField] private EventSystem _eventSystem;
        private GameObject _lastSelected;

        private void Update()
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                SetFocusToObject(_lastSelected);
            }
            else
            {
                _lastSelected = _eventSystem.currentSelectedGameObject;
            }
        }

        public void SetFocusToObject(GameObject focusObj)
        {
            _eventSystem.SetSelectedGameObject(focusObj);
        }

    }
}