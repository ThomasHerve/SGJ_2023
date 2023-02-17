using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Unity.MultiPlayerGame.Shared;

namespace Unity.MultiPlayerGame.Menu
{
    public class MenuNavigation : MonoBehaviour
    {
        public Selectable DefaultSelection;
        private EventSystem _eventSystem;

        void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _eventSystem = GetComponent<EventSystem>();
            _eventSystem.SetSelectedGameObject(null);
        }

        void LateUpdate()
        {
            if (_eventSystem.currentSelectedGameObject == null)
            {
                if (Input.GetButtonDown(GameConstants.k_ButtonNameSubmit)
                    || Input.GetAxisRaw(GameConstants.k_AxisNameHorizontal) != 0
                    || Input.GetAxisRaw(GameConstants.k_AxisNameVertical) != 0)
                {
                    _eventSystem.SetSelectedGameObject(DefaultSelection.gameObject);
                }
            }
        }
    }
}