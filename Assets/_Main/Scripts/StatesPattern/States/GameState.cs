using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoopStreet.Game.Main;

[CreateAssetMenu(fileName ="GameState", menuName ="States/GameState")]
public class GameState : ScriptableObject
{
    public List<bool> charactersFinished;
}
