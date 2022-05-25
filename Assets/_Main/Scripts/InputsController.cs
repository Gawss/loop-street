using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LoopStreet.Game.Main
{
    public class InputsController : MonoBehaviour
    {
        public InputAction inputClick;

        Ray hoverRay;
        RaycastHit hoverHitInfo;

        // Start is called before the first frame update
        void Start()
        {
            InitializeInputs();
        }

        private void InitializeInputs()
        {
            inputClick.performed += PerformMovePlayer;
            inputClick.Enable();
        }

        private void PerformMovePlayer(InputAction.CallbackContext context)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                Debug.Log(hitInfo.transform.gameObject.name);
                if (hitInfo.transform.gameObject.GetComponent<ChController>())
                {
                    GameManager.Instance.SetPlayerType(hitInfo.transform.gameObject.GetComponent<ChController>());
                }
                else
                {
                    GameManager.Instance.currentCharacter.ShowRipple(hitInfo);
                    GameManager.Instance.currentCharacter.SetTargetDestination(hitInfo.point);
                }
            }
        }
    }
}