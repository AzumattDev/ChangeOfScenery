using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ChangeOfScenery;

public class Functions
{
    internal static void GatherDefaultCamLocations(FejdStartup startup)
    {
        ChangeOfSceneryPlugin.defaultCharacterCameraMarker_Position = startup.m_cameraMarkerCharacter.position;
        ChangeOfSceneryPlugin.defaultCreditsCameraMarker_Position = startup.m_cameraMarkerCredits.position;
        ChangeOfSceneryPlugin.defaultGameCameraMarker_Position = startup.m_cameraMarkerGame.position;
        ChangeOfSceneryPlugin.defaultMainCameraMarker_Position = startup.m_cameraMarkerMain.position;
        ChangeOfSceneryPlugin.defaultStartCameraMarker_Position = startup.m_cameraMarkerStart.position;
        ChangeOfSceneryPlugin.defaultSavesCameraMarker_Position = startup.m_cameraMarkerSaves.position;
        
        ChangeOfSceneryPlugin.defaultCharacterCameraMarker_Rotation = startup.m_cameraMarkerCharacter.rotation.eulerAngles;
        ChangeOfSceneryPlugin.defaultCreditsCameraMarker_Rotation = startup.m_cameraMarkerCredits.rotation.eulerAngles;
        ChangeOfSceneryPlugin.defaultGameCameraMarker_Rotation = startup.m_cameraMarkerGame.rotation.eulerAngles;
        ChangeOfSceneryPlugin.defaultMainCameraMarker_Rotation = startup.m_cameraMarkerMain.rotation.eulerAngles;
        ChangeOfSceneryPlugin.defaultStartCameraMarker_Rotation = startup.m_cameraMarkerStart.rotation.eulerAngles;
        ChangeOfSceneryPlugin.defaultSavesCameraMarker_Rotation = startup.m_cameraMarkerSaves.rotation.eulerAngles;
    }

    internal static void GatherDefaultPlayerShit(FejdStartup startup)
    {
        ChangeOfSceneryPlugin._defaultPlayerPreviewPoint = startup.m_characterPreviewPoint;
        ChangeOfSceneryPlugin._defaultPlayerPosition = startup.m_characterPreviewPoint.position;
        ChangeOfSceneryPlugin._defaultPlayerRotation = startup.m_characterPreviewPoint.rotation.eulerAngles;
    }


    internal static void SetToCameraDefaults()
    {
        ChangeOfSceneryPlugin.CharacterCameraMarker_Position.Value = ChangeOfSceneryPlugin.defaultCharacterCameraMarker_Position;
        ChangeOfSceneryPlugin.CreditsCameraMarker_Position.Value = ChangeOfSceneryPlugin.defaultCreditsCameraMarker_Position;
        ChangeOfSceneryPlugin.GameCameraMarker_Position.Value = ChangeOfSceneryPlugin.defaultGameCameraMarker_Position;
        ChangeOfSceneryPlugin.MainCameraMarker_Position.Value = ChangeOfSceneryPlugin.defaultMainCameraMarker_Position;
        ChangeOfSceneryPlugin.StartCameraMarker_Position.Value = ChangeOfSceneryPlugin.defaultStartCameraMarker_Position;
        ChangeOfSceneryPlugin.SavesCameraMarker_Position.Value = ChangeOfSceneryPlugin.defaultSavesCameraMarker_Position;

        ChangeOfSceneryPlugin.CharacterCameraMarker_Rotation.Value = ChangeOfSceneryPlugin.defaultCharacterCameraMarker_Rotation;
        ChangeOfSceneryPlugin.CreditsCameraMarker_Rotation.Value = ChangeOfSceneryPlugin.defaultCreditsCameraMarker_Rotation;
        ChangeOfSceneryPlugin.GameCameraMarker_Rotation.Value = ChangeOfSceneryPlugin.defaultGameCameraMarker_Rotation;
        ChangeOfSceneryPlugin.MainCameraMarker_Rotation.Value = ChangeOfSceneryPlugin.defaultMainCameraMarker_Rotation;
        ChangeOfSceneryPlugin.StartCameraMarker_Rotation.Value = ChangeOfSceneryPlugin.defaultStartCameraMarker_Rotation;
        ChangeOfSceneryPlugin.SavesCameraMarker_Rotation.Value = ChangeOfSceneryPlugin.defaultSavesCameraMarker_Rotation;
        
    }

    internal static void SetToPlayerDefaults()
    {
        if (FejdStartup.instance == null) return;
        FejdStartup.instance.m_characterPreviewPoint = ChangeOfSceneryPlugin._defaultPlayerPreviewPoint;
        if(FejdStartup.instance.m_playerInstance == null) return;
        FejdStartup.instance.m_playerInstance.transform.position = ChangeOfSceneryPlugin._defaultPlayerPosition;
        FejdStartup.instance.m_playerInstance.transform.rotation = Quaternion.Euler(ChangeOfSceneryPlugin._defaultPlayerRotation);
        ChangeOfSceneryPlugin.MPlayerPosition.Value = ChangeOfSceneryPlugin._defaultPlayerPosition;
        ChangeOfSceneryPlugin.MPlayerRotation.Value = ChangeOfSceneryPlugin._defaultPlayerRotation;
    }
    
    
    internal static void SetToCameraPresets()
    {
        ChangeOfSceneryPlugin.CharacterCameraMarker_Position.Value = ChangeOfSceneryPlugin.m_cameraMarkerCharacterPositionPreset1;
        ChangeOfSceneryPlugin.CreditsCameraMarker_Position.Value = ChangeOfSceneryPlugin.m_cameraMarkerCreditsPositionPreset1;
        ChangeOfSceneryPlugin.GameCameraMarker_Position.Value = ChangeOfSceneryPlugin.m_cameraMarkerGamePositionPreset1;
        ChangeOfSceneryPlugin.MainCameraMarker_Position.Value = ChangeOfSceneryPlugin.m_cameraMarkerStartPositionPreset1;
        ChangeOfSceneryPlugin.StartCameraMarker_Position.Value = ChangeOfSceneryPlugin.m_cameraMarkerStartPositionPreset1;
        ChangeOfSceneryPlugin.SavesCameraMarker_Position.Value = ChangeOfSceneryPlugin.m_cameraMarkerSavesPositionPreset1;

        ChangeOfSceneryPlugin.CharacterCameraMarker_Rotation.Value = ChangeOfSceneryPlugin.m_cameraMarkerCharacterRotationPreset1;
        ChangeOfSceneryPlugin.CreditsCameraMarker_Rotation.Value = ChangeOfSceneryPlugin.m_cameraMarkerCreditsRotationPreset1;
        ChangeOfSceneryPlugin.GameCameraMarker_Rotation.Value = ChangeOfSceneryPlugin.m_cameraMarkerGameRotationPreset1;
        ChangeOfSceneryPlugin.MainCameraMarker_Rotation.Value = ChangeOfSceneryPlugin.m_cameraMarkerStartRotationPreset1;
        ChangeOfSceneryPlugin.StartCameraMarker_Rotation.Value = ChangeOfSceneryPlugin.m_cameraMarkerStartRotationPreset1;
        ChangeOfSceneryPlugin.SavesCameraMarker_Rotation.Value = ChangeOfSceneryPlugin.m_cameraMarkerSavesRotationPreset1;
    }

    internal static void SetToPlayerPresets()
    {
        FejdStartup.instance.m_characterPreviewPoint.position = ChangeOfSceneryPlugin.m_characterPreviewPositionPreset1;
        FejdStartup.instance.m_characterPreviewPoint.rotation = Quaternion.Euler(ChangeOfSceneryPlugin._defaultPlayerRotation);
        
        if(FejdStartup.instance.m_playerInstance == null) return;
        FejdStartup.instance.m_playerInstance.transform.position = ChangeOfSceneryPlugin.m_characterPreviewPositionPreset1;
        FejdStartup.instance.m_playerInstance.transform.rotation = Quaternion.Euler(ChangeOfSceneryPlugin._defaultPlayerRotation);
        ChangeOfSceneryPlugin.MPlayerPosition.Value = ChangeOfSceneryPlugin.m_characterPreviewPositionPreset1;
        ChangeOfSceneryPlugin.MPlayerRotation.Value = ChangeOfSceneryPlugin._defaultPlayerRotation;
    }
    
    internal static void UpdateCameraValues()
    {
        if(FejdStartup.instance == null)
            return;
        FejdStartup.instance.m_cameraMarkerCharacter.position = ChangeOfSceneryPlugin.CharacterCameraMarker_Position.Value;
        FejdStartup.instance.m_cameraMarkerCredits.position = ChangeOfSceneryPlugin.CreditsCameraMarker_Position.Value;
        FejdStartup.instance.m_cameraMarkerGame.position = ChangeOfSceneryPlugin.GameCameraMarker_Position.Value;
        FejdStartup.instance.m_cameraMarkerMain.position = ChangeOfSceneryPlugin.MainCameraMarker_Position.Value;
        FejdStartup.instance.m_cameraMarkerStart.position = ChangeOfSceneryPlugin.StartCameraMarker_Position.Value;
        FejdStartup.instance.m_cameraMarkerSaves.position = ChangeOfSceneryPlugin.SavesCameraMarker_Position.Value;
        
        
        FejdStartup.instance.m_cameraMarkerCharacter.rotation = Quaternion.Euler(ChangeOfSceneryPlugin.CharacterCameraMarker_Rotation.Value);
        FejdStartup.instance.m_cameraMarkerCredits.rotation = Quaternion.Euler(ChangeOfSceneryPlugin.CreditsCameraMarker_Rotation.Value);
        FejdStartup.instance.m_cameraMarkerGame.rotation = Quaternion.Euler(ChangeOfSceneryPlugin.GameCameraMarker_Rotation.Value);
        FejdStartup.instance.m_cameraMarkerMain.rotation = Quaternion.Euler(ChangeOfSceneryPlugin.MainCameraMarker_Rotation.Value);
        FejdStartup.instance.m_cameraMarkerStart.rotation = Quaternion.Euler(ChangeOfSceneryPlugin.StartCameraMarker_Rotation.Value);
        FejdStartup.instance.m_cameraMarkerSaves.rotation = Quaternion.Euler(ChangeOfSceneryPlugin.SavesCameraMarker_Rotation.Value);
    }

    internal static void UpdatePlayerValues()
    {
        if(FejdStartup.instance == null)
            return;
        FejdStartup.instance.m_characterPreviewPoint.position = ChangeOfSceneryPlugin.MPlayerPosition.Value;
        FejdStartup.instance.m_characterPreviewPoint.rotation = Quaternion.Euler(ChangeOfSceneryPlugin.MPlayerRotation.Value);
    }


    internal static void UpdateAndSet()
    {
        if (ChangeOfSceneryPlugin.UsePreset.Value == ChangeOfSceneryPlugin.Toggle.On &&
            ChangeOfSceneryPlugin.UseVanilla.Value == ChangeOfSceneryPlugin.Toggle.Off)
        {
            SetToCameraPresets();
            SetToPlayerPresets();
        }
        else if (ChangeOfSceneryPlugin.UseVanilla.Value == ChangeOfSceneryPlugin.Toggle.On &&
                 ChangeOfSceneryPlugin.UsePreset.Value == ChangeOfSceneryPlugin.Toggle.Off)
        {
            SetToCameraDefaults();
            SetToPlayerDefaults();
        }

        UpdateCameraValues();
        UpdatePlayerValues();
    }
    
    
    
    public static float scrollSpeed = 50f;
    public static float inactivityDelay = 2f;
    public static float textElementHeight = 30f;
    public static float textElementWidth = 30f;

    private static GameObject scrollingTextElement;
    private static AutoScrollingText autoScrollingText;

    public static void CreateModListUI()
    {
        // Create the scrolling text element game object
        scrollingTextElement = new GameObject("ModList Scrolling UI");
        scrollingTextElement.transform.SetParent(FejdStartup.instance.m_mainMenu.transform, false);

        // Add a ScrollRect component
        ScrollRect scrollRect = scrollingTextElement.AddComponent<ScrollRect>();
        scrollRect.horizontal = false;
        scrollRect.vertical = true;
        scrollRect.movementType = ScrollRect.MovementType.Clamped;
        scrollRect.elasticity = 0f;
        scrollRect.inertia = true;
        scrollRect.decelerationRate = 0.135f;
        scrollRect.scrollSensitivity = 40f;

        // Add a Mask component
        //Mask mask = scrollingTextElement.AddComponent<Mask>();
        //mask.showMaskGraphic = false;

        // Add an Image component for the background
        Image image = scrollingTextElement.AddComponent<Image>();
        image.color = Color.clear;

        // Create the content game object
        GameObject content = new("Mod List");
        content.transform.SetParent(scrollRect.transform, false);
        scrollRect.content = content.GetComponent<RectTransform>();

        // Add a vertical layout group component to the content game object
        VerticalLayoutGroup verticalLayoutGroup = content.AddComponent<VerticalLayoutGroup>();
        verticalLayoutGroup.childForceExpandHeight = true;
        verticalLayoutGroup.childControlHeight = true;

        // Add a ContentSizeFitter component to the content game object
        ContentSizeFitter contentSizeFitter = content.AddComponent<ContentSizeFitter>();
        contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;

        // Add a Text component to the content game object
        Text text = content.AddComponent<Text>();
        text.text = "";
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.fontSize = 20;
        text.color = Color.white;
        foreach (string mod in GetInstalledMods())
        {
            text.text += mod + "\n";
        }

        text.alignment = TextAnchor.UpperLeft;

        // Set the size and position of the scrolling text element
        RectTransform rectTransform = scrollingTextElement.GetComponent<RectTransform>();
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, textElementWidth);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, textElementHeight);
        rectTransform.anchoredPosition = Vector2.zero;
        scrollRect.content = content.GetComponent<RectTransform>();
        //// Add an AutoScrollingText component to the scrolling text element
        autoScrollingText = scrollingTextElement.AddComponent<AutoScrollingText>();
        autoScrollingText.scrollSpeed = scrollSpeed;
        autoScrollingText.inactivityDelay = inactivityDelay;
        autoScrollingText.scrollRect = scrollRect;
        autoScrollingText.textComponent = text;
    }

    internal static string[] GetInstalledMods()
    {
        // Add all the languages to a string array
        string[] languages = Localization.instance.m_languages.ToArray();
        return languages;
    }
}

public class AutoScrollingText : MonoBehaviour
{
    public float scrollSpeed;
    public float inactivityDelay;
    public ScrollRect scrollRect;
    public Text textComponent;

    private Coroutine scrollingCoroutine;
    private bool isScrolling;

    private void OnEnable()
    {
        StartScrolling();
    }

    private void OnDisable()
    {
        StopScrolling();
    }

    public void StartScrolling()
    {
        if (scrollingCoroutine == null)
        {
            scrollingCoroutine = StartCoroutine(ScrollText());
        }
    }

    public void StopScrolling()
    {
        if (scrollingCoroutine != null)
        {
            StopCoroutine(scrollingCoroutine);
            scrollingCoroutine = null;
        }
    }

    private IEnumerator ScrollText()
    {
        while (true)
        {
            if (isScrolling)
            {
                scrollRect.verticalNormalizedPosition = Mathf.Max(0,
                    scrollRect.verticalNormalizedPosition - Time.deltaTime * scrollSpeed);
                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(inactivityDelay);
                isScrolling = true;
            }
        }
    }
}