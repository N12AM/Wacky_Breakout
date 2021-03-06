using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInitializer : MonoBehaviour
{
       void Awake()
       {
              if (!AudioManager.Initialized)
              {
                     AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                     AudioManager.Initialize(audioSource);
                     DontDestroyOnLoad(gameObject);
              }
              else
              {
                     Destroy(gameObject);
              }
       }
}
