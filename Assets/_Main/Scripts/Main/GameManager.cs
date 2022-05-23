using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace LoopStreet.Game.Main
{
    public class GameManager : MonoBehaviour
    {

        public ChController currentCharacter;
        public CinemachineFreeLook cinemachineFreelook;

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
            currentCharacter = newCharacter;
            cinemachineFreelook.Follow = currentCharacter.transform;
            cinemachineFreelook.LookAt = currentCharacter.transform;
        }
    }

    public enum playerType
    {
        mainHuman,
        Rino
    }
}