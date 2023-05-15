using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HurryUp 
{
    public class AudioManager : MonoBehaviour
    {
        public AudioSource bgmSource;

        public float audioVolume = 1f;

        public float audioEffectVolume = 1f;

        public List<AudioSource> audioSources = new List<AudioSource>();

        public bool isMute = false;

        public void PlayBGMAudio(AudioClip bgm) 
        {
            bgmSource.clip = bgm;

            bgmSource.Play();

            bgmSource.volume = isMute ? 0 : audioVolume;
        }

        public void StopBGMAudio() 
        {
            bgmSource.Stop();
        }

        public void PlayEffectAudio(AudioClip clip,Vector3 pos) 
        {
            StartCoroutine(WaitTimeAudioPlay(clip,pos));
        }

        IEnumerator WaitTimeAudioPlay(AudioClip clip, Vector3 pos) 
        {
            //生成一个
            var effectObject = new GameObject();
            effectObject.transform.position = pos;
            effectObject.transform.SetParent(transform);

            var effectSource = effectObject.AddComponent<AudioSource>();

            effectSource.loop = false;
            effectSource.volume = isMute ? 0 : audioEffectVolume;
            effectSource.clip = clip;

            effectSource.Play();
            audioSources.Add(effectSource);
            while (effectSource.isPlaying)
            {
                yield return null;
            }
            audioSources.Remove(effectSource);
            Destroy(effectObject);
        }

        public void UpdateAllAudioSourceVolum() 
        {
            bgmSource.volume = isMute ? 0 : audioVolume;

            if (audioSources.Count != 0)
            {

                foreach (var item in audioSources)
                {
                    item.volume = isMute ? 0 : audioEffectVolume;
                }
            }

        }

    }

}
