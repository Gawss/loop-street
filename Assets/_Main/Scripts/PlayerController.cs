using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityStandardAssets.Characters.ThirdPerson;

namespace LoopStreet.Game{
    public class PlayerController : MonoBehaviour
    {
        public InputAction inputMovement;
        public InputAction inputRun;

        private NavMeshAgent _playerAgent;

        public ThirdPersonCharacter _character;

        public ParticleSystem m_RippleParticle;

        public float m_walkingSpeed;
        public float m_runningSpeed;

        public bool alwaysRun;
        private void Start() 
        {
            _playerAgent = GetComponent<NavMeshAgent>();
            _playerAgent.speed = m_walkingSpeed;
            InitializeInputs();
        }

        private void Update() {
            if(_playerAgent.remainingDistance > _playerAgent.stoppingDistance)
            {
                _character.Move(_playerAgent.velocity, false, false);
            }
            else
            {
                //Stop Moving Animations
                _character.Move(Vector3.zero, false, false);
            }
        }

        private void InitializeInputs()
        {
            inputMovement.performed += PerformMovePlayer;
            inputMovement.Enable();

            inputRun.performed += PerformRun;
            inputRun.Enable();
        }

        private void PerformMovePlayer(InputAction.CallbackContext context)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                MovePlayer(hitInfo.point);
                m_RippleParticle.transform.position = hitInfo.point;
                m_RippleParticle.transform.rotation = Quaternion.LookRotation(hitInfo.normal, Vector3.up);
                m_RippleParticle.Play();
            }

            _playerAgent.speed = alwaysRun? m_runningSpeed : m_walkingSpeed;

        }

        private void MovePlayer(Vector3 newPosition)
        {
            _playerAgent.SetDestination(newPosition);
        }

        private void PerformRun(InputAction.CallbackContext context)
        {
            _playerAgent.speed = m_runningSpeed;
        }
    }
}
