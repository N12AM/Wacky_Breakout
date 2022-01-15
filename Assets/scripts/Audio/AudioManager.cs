using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
       private static bool _initialized = false;
       private static AudioSource _audioSource;
       private static Dictionary<AudioClipName , AudioClip> _audioClips = new Dictionary<AudioClipName, AudioClip>();


       public static bool Initialized => _initialized;

       public static void Initialize(AudioSource source)
       {
              _initialized = true;
              _audioSource = source;
              _audioClips.Add(AudioClipName.StandardBlock, Resources.Load<AudioClip>(@"sound\bounce"));
              _audioClips.Add(AudioClipName.BonusBlock, Resources.Load<AudioClip>(@"sound\bonus"));
              _audioClips.Add(AudioClipName.FreezerBlock, Resources.Load<AudioClip>(@"sound\slowMotion"));
              _audioClips.Add(AudioClipName.SpeederBlock, Resources.Load<AudioClip>(@"sound\speeder"));
              _audioClips.Add(AudioClipName.Bounce, Resources.Load<AudioClip>(@"sound\jump"));
              _audioClips.Add(AudioClipName.Click, Resources.Load<AudioClip>(@"sound\click"));
              _audioClips.Add(AudioClipName.BallDestroyed, Resources.Load<AudioClip>(@"sound\ball"));

       }

       public static void Play(AudioClipName clipName)
       {
              _audioSource.PlayOneShot(_audioClips[clipName]);
       }
    
}
