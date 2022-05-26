using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using System.Linq;
using UnityEngine.SceneManagement;
using Polyperfect.Common;

namespace LoopStreet.Game.Main
{
    public class GameManager : MonoBehaviour
    {

        public ChController currentCharacter;
        public CinemachineFreeLook cinemachineFreelook;
        public ParticleSystem Soul_ParticleSystem;
        public ParticleSystem CHSoul_ParticleSystem;
        public Transform startTransform;

        private static GameManager _instance;

        public static GameManager Instance { get { return _instance; } }

        public GameState gameState;

        public InterfaceController interfaceController;
        public SoundController soundController;


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

            gameState.charactersFinished = new List<bool>();
        }

        private void Start()
        {            
            StartCoroutine(LevelIntro(interfaceController.introPanel, 5f));
        }

        public void SetPlayerType(ChController newCharacter)
        {
            Soul_ParticleSystem.gameObject.SetActive(true);
            Soul_ParticleSystem.transform.position = currentCharacter.transform.position;

            soundController.PlaySoulChange();

            newCharacter.targetLocation.gameObject.SetActive(true);

            cinemachineFreelook.Follow = Soul_ParticleSystem.transform;
            cinemachineFreelook.LookAt = Soul_ParticleSystem.transform;

            Soul_ParticleSystem.transform.DOMove(newCharacter.transform.position, 0.5f).OnComplete(() => {
                
                CHSoul_ParticleSystem.transform.SetParent(newCharacter.transform);
                CHSoul_ParticleSystem.transform.localPosition = Vector3.zero;

                currentCharacter = newCharacter;
                cinemachineFreelook.Follow = currentCharacter.transform;
                cinemachineFreelook.LookAt = currentCharacter.transform;
                Soul_ParticleSystem.gameObject.SetActive(false);

                newCharacter.OnEnter?.Invoke();
            });
        }

        IEnumerator LevelIntro(GameObject obj, float _time)
        {
            startTransform.DOMove(currentCharacter.transform.position, _time);
            yield return new WaitForSeconds(_time);
            interfaceController.FadeOutTxtChildren(obj);

            yield return new WaitForSeconds(2f);

            SetPlayerType(currentCharacter);
            interfaceController.FadeInTxtChildren(interfaceController.onboardingPanel);
        }

        public void VictoryCondition()
        {

            if(gameState.charactersFinished.Any(c=> c == false)){
                return;
            }

            interfaceController.finishedPanel.SetActive(true);
            interfaceController.FadeOutTxtChildren(interfaceController.tasksPanel);
            interfaceController.FadeInTxtChildren(interfaceController.finishedPanel);
        }

        public void MuteAnimalSounds(bool state)
        {
            Common_AudioManager.instance.muteSound = state;
        }

        public void CloseApp()
        {
            SceneManager.LoadScene(0);
        }
    }

    public enum playerType
    {
        spirit,
        child,
        man,
        secondMan,
        oldMan,
        Rinho,
        Cat
    }
}