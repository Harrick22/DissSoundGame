using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    private List<EventInstance> eventInstances;

    private List<StudioEventEmitter> eventEmitters;

    private EventInstance ambienceEventInstance;

    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more thna one audio manager in the scene");
        }
        instance = this;

        eventInstances = new List<EventInstance>();

    }

    private void Start()
    {
        InitializeAmbience(FMODEvents.instance.ambience);
    }

   /* void Update ()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundevent, GetComponent<Transform>(), GetComponent<Rigidbody>());
        PlaySound();
    }

    void Playsound()
    {
        if (Input.GetKeyDown(presstoplaysound))
        {
            soundevent.start();
        }
    } */

    private void InitializeAmbience(EventReference ambienceEventReference)
    {
        ambienceEventInstance = CreateInstance(ambienceEventReference);
        ambienceEventInstance.start();
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
        
    }

    /*instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform)); //What I added
        instance.start();
        instance.release();
    */

    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    private void CleanUp()
    {
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }

    }
    private void OnDestroy()
    {
        CleanUp();
    }
}
