using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace LoopStreet.Game.Landing
{
    public class LandingController : MonoBehaviour
    {
        [SerializeField] private InputAction startInputAction;

        [SerializeField] private Volume postprocessing;
        ColorAdjustments _colorAdjustments;

        public RectTransform positionPanel;

        float panelPosition;
        [SerializeField] private float slideTime = 0.5f;

        bool fadeOutCompleted;

        [SerializeField] private bool previousGame;


        // Start is called before the first frame update
        void Start()
        {
            startInputAction.performed += StartGame;
            startInputAction.Enable();

            if(postprocessing != null)
            {
                postprocessing.profile.TryGet(out _colorAdjustments);
            }
        }

        private void OnDisable()
        {
            startInputAction.Disable();
        }

        private void StartGame(InputAction.CallbackContext context)
        {
            
            if (CheckPreviousGames())
            {
                Debug.Log("Previous game detected, going to main menu...");
                SlidePanel(-2560);
            }
            else
            {
                NewGame();
            }
        }

        private bool CheckPreviousGames()
        {
            // If player has stored memories, returns true
            // If it's the first time, start game with new memory

            return previousGame;
        }

        public void NewGame()
        {
            Debug.Log("Previous game non-detected, starting story...");
            DOTween.To(() => _colorAdjustments.postExposure.value, x => _colorAdjustments.postExposure.value = x, -15, 1.5f).OnComplete(() =>
            {
                fadeOutCompleted = true;
            });
            StartCoroutine(LoadScene());
        }

        IEnumerator LoadScene()
        {
            yield return null;

            //Begin to load the Scene you specify
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Main");

            //Don't let the Scene activate until you allow it to
            asyncOperation.allowSceneActivation = false;
            Debug.Log("Pro :" + asyncOperation.progress);

            //When the load is still in progress, output the Text and progress bar
            while (!asyncOperation.isDone)
            {
                Debug.Log("Main Scene is ready.");

                // Check if the load has finished
                if (asyncOperation.progress >= 0.9f && fadeOutCompleted)
                {
                    // show the Scene is ready
                    Debug.Log("It is time to activate the scene");

                    // Activate the scene
                    asyncOperation.allowSceneActivation = true;
                }

                yield return null;
            }
        }

        #region UI Methods

        public void SlidePanel(float value)
        {
            DOTween.To(() => panelPosition, x => panelPosition = x, value, slideTime).OnUpdate(() =>
            {
                positionPanel.anchoredPosition = new Vector3(panelPosition, 0, 0);
            }).SetEase(Ease.InOutSine);
        }

        #endregion
    }
}
