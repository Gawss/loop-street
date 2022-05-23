using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoopStreet.Game.Main
{
    public class AnimalController : ChController
    {
        public Animator animalAnimator;

        public override void MoveCharacter()
        {
            base.MoveCharacter();
            animalAnimator.SetBool("isWalking", true);
        }

        public override void StopCharacter()
        {
            base.StopCharacter();
            animalAnimator.SetBool("isWalking", false);
        }
    }
}