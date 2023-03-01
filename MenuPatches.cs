using System;
using System.Collections;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;
using Object = System.Object;

namespace ChangeOfScenery;

[HarmonyPatch(typeof(FejdStartup), nameof(FejdStartup.SetupGui))]
static class FejdStartupSetupGuiPatch
{
    static GameObject _props = null!;
    static GameObject _mMenuFire = null!;
    static void Postfix(FejdStartup __instance)
    {
        Functions.GatherDefaultCamLocations(__instance);
        Functions.GatherDefaultPlayerShit(__instance);
        //Functions.CreateModListUI(); // TODO: Finish creating a mod list UI that was requested by a user

        try
        {
            _props = Utils.FindChild(GameObject.Find("Static").transform, "props").gameObject;
            _mMenuFire = GameObject.Find("MenuFire");
        }
        catch (Exception e)
        {
            ChangeOfSceneryPlugin.ChangeOfSceneryLogger.LogError(e);
        }

        if (_props != null)
        {
            _props.gameObject.SetActive(true);
            Utils.FindChild(_props.transform, "Rocks").gameObject.SetActive(true);
            Utils.FindChild(_props.transform, "ships").gameObject.SetActive(true);
        }
        else
        {
            ChangeOfSceneryPlugin.ChangeOfSceneryLogger.LogError("Props not found");
        }
        
        var fireclone = UnityEngine.Object.Instantiate(_mMenuFire, ChangeOfSceneryPlugin.m_characterPreviewPositionPreset1 + new Vector3(1, 0, 1), Quaternion.identity);
        fireclone.AddComponent<TerrainModifier>().m_levelRadius = 4;
        fireclone.AddComponent<TerrainModifier>().m_smoothRadius = 8;
        fireclone.AddComponent<TerrainModifier>().m_smoothPower = 2;
        fireclone.AddComponent<TerrainModifier>().m_smooth = true;
        fireclone.AddComponent<TerrainModifier>().m_paintCleared = false;
        fireclone.AddComponent<TerrainModifier>().m_paintType = TerrainModifier.PaintType.Paved;
        fireclone.AddComponent<TerrainModifier>().m_paintRadius = 3;

        Functions.UpdateAndSet();
    }
}

[HarmonyPatch(typeof(FejdStartup), nameof(FejdStartup.UpdateCamera))]
static class FejdStartupUpdateCameraPatch
{
    static void Postfix(FejdStartup __instance)
    {
        Functions.UpdateCameraValues();

        if (__instance.m_characterSelectScreen.activeSelf || __instance.m_creditsPanel.activeSelf)
        {
            Utils.GetMainCamera().GetComponent<DepthOfField>().enabled = true;
        }
        else
        {
            Utils.GetMainCamera().GetComponent<DepthOfField>().enabled = false;
        }

    }
}

[HarmonyPatch(typeof(FejdStartup), nameof(FejdStartup.ShowCharacterSelection))]
static class FejdStartupShowCharacterSelectionPatch
{
    static void Prefix(FejdStartup __instance)
    {
        if (ChangeOfSceneryPlugin.UsePreset.Value == ChangeOfSceneryPlugin.Toggle.On)
        {
            __instance.m_characterPreviewPoint.rotation = Quaternion.Euler(0,
                __instance.m_cameraMarkerCharacter.rotation.eulerAngles.y - 180, 0);
            if (__instance.m_playerInstance)
            {
                __instance.m_playerInstance.transform.rotation = Quaternion.Euler(0,
                    __instance.m_cameraMarkerCharacter.rotation.eulerAngles.y - 180, 0);
            }
        }

    }
}

[HarmonyPatch(typeof(FejdStartup),nameof(FejdStartup.OnCharacterStart))]
static class FejdStartupOnCharacterStartPatch
{
    static void Prefix(FejdStartup __instance)
    {
        if (ChangeOfSceneryPlugin.UsePreset.Value == ChangeOfSceneryPlugin.Toggle.On)
        {
            __instance.m_characterPreviewPoint.rotation = Quaternion.Euler(0, __instance.m_cameraMarkerCharacter.rotation.eulerAngles.y - 40, 0);
            if (__instance.m_playerInstance)
            {
                __instance.m_playerInstance.transform.rotation = Quaternion.Euler(0,
                    __instance.m_cameraMarkerCharacter.rotation.eulerAngles.y - 40, 0);
            }
        }
    }
}

[HarmonyPatch(typeof(MusicMan),nameof(MusicMan.TriggerMusic))]
static class MusicManTriggerMusicPatch
{
    public static AudioSource audioSource = null!;
    public static Coroutine? AudioStart;
    
    static void Postfix(MusicMan __instance, ref string name)
    {
        if (name == "menu")
        {
            if (MusicMan.instance != null)
            {
                MusicMan.instance.Reset();
            }

            StreamAudio(ChangeOfSceneryPlugin.customsong.Value);
        }

    }
    
    public static void StreamAudio(string url)
    {
        AudioStart = ChangeOfSceneryPlugin.instance.StartCoroutine(StreamAudioFromURL(url));
    }

    static IEnumerator StreamAudioFromURL(string url)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.UNKNOWN))
        {
            yield return www.SendWebRequest();
            while (!www.isDone)
            {
                yield return new WaitForSeconds(0.1f);
            }
            if (www.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError
                or UnityWebRequest.Result.DataProcessingError)
            {
                ChangeOfSceneryPlugin.ChangeOfSceneryLogger.LogError(www.error);
            }
            else
            {
                AudioClip audioClip = DownloadHandlerAudioClip.GetContent(www);
                MusicMan.instance.FindMusic("menu").m_clips = new[] {audioClip};
                //MusicMan.instance.StartMusic("menu");
                if (MusicMan.instance.m_musicSource.clip != audioClip)
                {
                    if (MusicMan.instance != null)
                    {
                        MusicMan.instance.m_musicSource.clip = audioClip;
                        MusicMan.instance.m_musicSource.enabled = true;
                        if(MusicMan.instance.m_musicSource.isPlaying)
                            MusicMan.instance.m_musicSource.Stop();
                        MusicMan.instance.m_musicSource.Play();
                        MusicMan.instance.m_musicSource.loop = true;
                        MusicMan.instance.m_musicSource.volume = 15;
                        ChangeOfSceneryPlugin.ChangeOfSceneryLogger.LogDebug($"Streaming audio from: {url}.");
                    }
                }
            }
        }
    }
}

[HarmonyPatch(typeof(FejdStartup),nameof(FejdStartup.LoadMainScene))]
static class LoadMainScenePatch
{
    static void Prefix(SceneManager __instance)
    {
        ChangeOfSceneryPlugin.instance.StopCoroutine(MusicManTriggerMusicPatch.AudioStart);
    }
}