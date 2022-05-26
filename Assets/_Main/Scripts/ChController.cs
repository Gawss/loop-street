using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace LoopStreet.Game.Main
{
    public class ChController : MonoBehaviour
    {
        public playerType _playerType;
        public NavMeshAgent _playerAgent;
        public float _movementSpeed;

        public ParticleSystem _RippleParticle;

        public GameObject textMsg;

        public Location targetLocation;

        public UnityEvent OnEnter;
        public UnityEvent OnFinish;

        public bool canInteract = true;
        public string finalTxt;

        public TextMeshProUGUI taskText;

        public int messageDistance = 5;

        private void Start()
        {
            _playerAgent = GetComponent<NavMeshAgent>();
            _playerAgent.speed = _movementSpeed;

            GameManager.Instance.gameState.charactersFinished.Add(false);
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

            if(this != GameManager.Instance.currentCharacter)
            {
                if(Vector3.Distance(transform.position, GameManager.Instance.currentCharacter.transform.position) < messageDistance)
                {
                    textMsg.SetActive(true);
                }
                else
                {
                    textMsg.SetActive(false);
                }
            }
            else if(canInteract)
            {
                textMsg.SetActive(false);
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

        public void SetText()
        {
            textMsg.GetComponentInChildren<TextMeshProUGUI>().text = finalTxt;
        }

        public virtual void MoveCharacter()
        {

        }

        public virtual void StopCharacter()
        {

        }

        public virtual void EndJourney()
        {
            targetLocation.gameObject.SetActive(false);
            SetText();
            canInteract = false;
            if(taskText) taskText.fontStyle = FontStyles.Strikethrough;

            GameManager.Instance.gameState.charactersFinished[(int)_playerType] = true;
            OnFinish?.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Location>())
            {
                if(other.GetComponent<Location>().type == this._playerType)
                {
                    EndJourney();
                }
            }
        }
    }
}