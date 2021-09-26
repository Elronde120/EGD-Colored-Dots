using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public bool playTrackZeroOnStart = false;
    public AudioClip[] MusicTracks => musicTracks;

    [SerializeField] private AudioClip[] musicTracks = new AudioClip[0];
    private AudioSource _source;

    private Coroutine smoothLerpVolumeCoroutine;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        instance = this;
    }

    /// <summary>
    /// Switches the current track to the given track, plays immediately
    /// </summary>
    /// <param name="trackIndex">The track to play on <see cref="musicTracks"/></param>
    /// <param name="playImmediate">Should the audio source play after switching</param>
    /// <param name="delay">How long to delay playing, if playImmediate is true</param>
    /// <returns>True if switching and playing was successful. False otherwise</returns>
    public bool SwitchTrack(int trackIndex, bool playImmediate, ulong delay = ulong.MinValue)
    {
        if (trackIndex < 0 || trackIndex >= musicTracks.Length)
        {
            return false;
        }

        if (_source == null)
            return false;

        Stop();
        _source.clip = musicTracks[trackIndex];
        if(playImmediate)
            Resume(delay);
        
        return true;

    }

    /// <summary>
    /// Lerps the volume from startVolume to endVolume over time T
    /// </summary>
    /// <remarks>
    /// Calls a coroutine to lerp, will return immediately 
    /// </remarks>
    /// <param name="startVolume">The start volume</param>
    /// <param name="endVolume">The end volume</param>
    /// <param name="T">How long to lerp for</param>
    public void SmoothChangeVolume(float startVolume, float endVolume, float T)
    {
        if (smoothLerpVolumeCoroutine != null)
        {
            Debug.Log("here");
            return;
        }
        smoothLerpVolumeCoroutine = StartCoroutine(_LerpVolumeCor(startVolume, endVolume, T));
    }
    
    /// <summary>
    /// Lerps the volume from current volume to endVolume over time T
    /// </summary>
    /// <remarks>
    /// Calls a coroutine to lerp, will return immediately 
    /// </remarks>
    /// <param name="endVolume">The end volume</param>
    /// <param name="T">How long to lerp for</param>
    public void SmoothChangeVolume(float endVolume, float T)
    {
        if (smoothLerpVolumeCoroutine != null)
        {
            return;
        }
        smoothLerpVolumeCoroutine = StartCoroutine(_LerpVolumeCor(_source.volume, endVolume, T));
    }

    private IEnumerator _LerpVolumeCor(float startVolume, float endVolume, float T)
    {
        _source.volume = startVolume;
        float currentTime = 0f;
        while (Math.Abs(_source.volume - endVolume) > 0.001f)
        {
            _source.volume = Mathf.Lerp(startVolume, endVolume, currentTime / T);
            currentTime += Time.deltaTime;
            yield return null;
        }

        smoothLerpVolumeCoroutine = null;
    }

    /// <summary>
    /// Stops the music immediately, if playing
    /// </summary>
    public void Stop()
    {
        _source.Stop();
    }

    /// <summary>
    /// Resumes music playing if stopped. Must have a audio clip prepared.
    /// </summary>
    /// <remarks>
    /// Call <see cref="SwitchTrack"/> if unsure if audio clip is prepared.
    /// </remarks>
    /// <param name="delay"></param>
    public void Resume(ulong delay = ulong.MinValue)
    {
        _source.Play(delay);
    }

    /// <summary>
    /// Sets the volume of the player. Clamped between 0-1 inclusive.
    /// </summary>
    /// <param name="newVolume">The new volume level</param>
    public void SetVolume(float newVolume)
    {
        _source.volume = Mathf.Clamp(newVolume, 0f, 1f);
    }


    public float GetTimeRemaining()
    {
        if (_source.clip == null)
        {
            return -99f;
        }
        
        return _source.clip.length - _source.time;
    }
    

}
