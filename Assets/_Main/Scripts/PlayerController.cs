using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityStandardAssets.Characters.ThirdPerson;
using Polyperfect.Animals;

namespace LoopStreet.Game.Main 
{
    public class PlayerController : ChController
    {

        public ThirdPersonCharacter _character;

        public override void MoveCharacter()
        {
            base.MoveCharacter();
            _character.Move(_playerAgent.velocity, false, false);
        }

        public override void StopCharacter()
        {
            base.StopCharacter();
            _character.Move(Vector3.zero, false, false);
        }
    }
}
