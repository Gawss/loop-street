using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoopStreet.Game.Main
{
    public class CartoonMessage : MonoBehaviour
    {

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(Camera.main.transform);
        }
    }
}