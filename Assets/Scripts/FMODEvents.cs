using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Player SFX")]

    [field: SerializeField] public EventReference playerFootsteps { get; private set; }

    [field: Header ("Ambience")]

    [field: SerializeField] public EventReference ambience { get; private set; }

    [field: Header("SoundEffect")]

    [field: SerializeField] public EventReference Nighttime { get; private set; }

    public static FMODEvents instance {  get; private set; }

   private void Awake()
   { instance = this;
        {
            Debug.LogError("Found more than one FMOD Events instance in the scene.");
        }
        instance = this;
   }
}
