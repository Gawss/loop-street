using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

namespace LoopStreet.Game.Main
{
    public class InterfaceController : MonoBehaviour
    {
        public GameObject introPanel;
        public GameObject onboardingPanel;
        public GameObject tasksPanel;
        public GameObject finishedPanel;

        public void FadeInTxtChildren(GameObject parent)
        {
            foreach (var text in parent.GetComponentsInChildren<TextMeshProUGUI>())
            {
                text.DOFade(1f, 1f);
            }
        }

        public void FadeOutTxtChildren(GameObject parent)
        {
            foreach (var text in parent.GetComponentsInChildren<TextMeshProUGUI>())
            {
                text.DOFade(0f, 1f);
            }
        }
    }
}