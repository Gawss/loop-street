using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

namespace LoopStreet.Game.Main
{
    public class GameManager : MonoBehaviour
    {

        public ChController currentCharacter;
        public CinemachineFreeLook cinemachineFreelook;
        public ParticleSystem Soul_ParticleSystem;
        public ParticleSystem CHSoul_ParticleSystem;

        private static GameManager _instance;

        public static GameManager Instance { get { return _instance; } }

        public GameState gameState;


        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        public void SetPlayerType(ChController newCharacter)
        {
            Soul_ParticleSystem.gameObject.SetActive(true);
            Soul_ParticleSystem.transform.position = currentCharacter.transform.position;

            cinemachineFreelook.Follow = Soul_ParticleSystem.transform;
            cinemachineFreelook.LookAt = Soul_ParticleSystem.transform;

            Soul_ParticleSystem.transform.DOMove(newCharacter.transform.position, 0.5f).OnComplete(() => {
                
                CHSoul_ParticleSystem.transform.SetParent(newCharacter.transform);
                CHSoul_ParticleSystem.transform.localPosition = Vector3.zero;

                currentCharacter = newCharacter;
                cinemachineFreelook.Follow = currentCharacter.transform;
                cinemachineFreelook.LookAt = currentCharacter.transform;
                Soul_ParticleSystem.gameObject.SetActive(false);
            });
        }
    }

    public enum playerType
    {
        mainHuman,
        Rino,
        oldMan
    }
}