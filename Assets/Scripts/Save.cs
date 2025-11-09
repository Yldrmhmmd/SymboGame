using UnityEngine;

[System.Serializable]
public class Save
{
    public int topTime;
    public bool fullscreen;
    public int musicVolume;
    public int effectsVolume;

    public Save (int topTime, bool fullscreen, int musicVolume, int effectsVolume)
    {
        this.topTime = topTime;
        this.fullscreen = fullscreen;
        this.musicVolume = musicVolume;
        this.effectsVolume = effectsVolume;
    }
}