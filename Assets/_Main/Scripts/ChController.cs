using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace LoopStreet.Game.Main
{
    public class ChController : MonoBehaviour
    {
        public playerType _playerType;
        public NavMeshAgent _playerAgent;
        public float _movementSpeed;

        public ParticleSystem _RippleParticle;

        private void Start()
        {
            _playerAgent = GetComponent<NavMeshAgent>();
            _playerAgent.speed = _movementSpeed;
        }

        private void Update()
        {
            if (_playerAgent.remainingDistance > _playerAgent.stoppingDistance)
            {
                MoveCharacter();
            }
            else
            {
                StopCharacter();
            }
        }

        public void SetTargetDestination(Vector3 newPosition)
        {
            _playerAgent.SetDestination(newPosition);
        }

        public void ShowRipple(RaycastHit hitInfo)
        {
            _RippleParticle.transform.position = hitInfo.point;
            _RippleParticle.transform.rotation = Quaternion.LookRotation(hitInfo.normal, Vector3.up);
            _RippleParticle.Play();
        }

        public virtual void MoveCharacter()
        {

        }

        public virtual void StopCharacter()
        {

        }
    }
}