using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace LoopStreet.Game{
    public class PlayerController : MonoBehaviour
    {
        public InputAction inputMovement;
        
        private NavMeshAgent _playerAgent;

        private void Start() 
        {
            _playerAgent = GetComponent<NavMeshAgent>();
            InitializeInputs();
        }

        private void Update() {
            
        }

        private void InitializeInputs()
        {
            inputMovement.performed += PerformMovePlayer;
            inputMovement.Enable();
        }

        private void PerformMovePlayer(InputAction.CallbackContext context)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hitInfo;

            if(Physics.Raycast(ray, out hitInfo))
            {
                MovePlayer(hitInfo.point);
            }
        }

        private void MovePlayer(Vector3 newPosition)
        {
            _playerAgent.SetDestination(newPosition);
        }
    }
}
