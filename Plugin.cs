using System;
using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace ChangeOfScenery
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class ChangeOfSceneryPlugin : BaseUnityPlugin
    {
        internal const string ModName = "ChangeOfScenery";
        internal const string ModVersion = "1.0.2";
        internal const string Author = "Azumatt";
        private const string ModGUID = Author + "." + ModName;
        private static string ConfigFileName = ModGUID + ".cfg";
        private static string ConfigFileFullPath = Paths.ConfigPath + Path.DirectorySeparatorChar + ConfigFileName;

        internal static string ConnectionError = "";

        private readonly Harmony _harmony = new(ModGUID);

        public static readonly ManualLogSource ChangeOfSceneryLogger =
            BepInEx.Logging.Logger.CreateLogSource(ModName);
        
        public static ChangeOfSceneryPlugin instance;

        public enum Toggle
        {
            On = 1,
            Off = 0
        }
        
        // Preset variables
        public static Vector3 m_cameraMarkerCharacterPositionPreset1 = new(-206.2f, 39.34f, 162.26f);
        public static Vector3 m_cameraMarkerCharacterRotationPreset1 = new(19.6f, -211f, 0);
        
        public static Vector3 m_cameraMarkerCreditsPositionPreset1 = new(-205.3f,34.29f,132.06f);
        public static Vector3 m_cameraMarkerCreditsRotationPreset1 = new(19.6f, -211f, 0f);
        
        public static Vector3 m_cameraMarkerStartPositionPreset1 = new(-208.54f, 32.2f, 131.38f);
        public static Vector3 m_cameraMarkerStartRotationPreset1 = new(0.72f, -211f, 0);
        
        //public static Vector3 m_cameraMarkerGamePositionPreset1 = new Vector3(-202.1578f, 39.96023f, 162.05922f);
        public static Vector3 m_cameraMarkerGamePositionPreset1 = new(-204.55f, 38.0f, 158.8f);
        public static Vector3 m_cameraMarkerGameRotationPreset1 = new(-181.447f, -48.532f, -180f);
        
        
        
        public static Vector3 m_cameraMarkerSavesPositionPreset1 = new(-208.45f, 45.76f, 131.23f);
        public static Vector3 m_cameraMarkerSavesRotationPreset1 = new(0.72f, -211f, 131.23f);
        
        public static Vector3 m_characterPreviewPositionPreset1 = new(-204.51f,36.50f,157.53f);
        public static Vector3 m_characterPreviewRotationPreset1 = new(-180f,-75.30915f,180f);


        // Default variables
        internal static Vector3 defaultCharacterCameraMarker_Position = new();
        internal static Vector3 defaultCreditsCameraMarker_Position = new();
        internal static Vector3 defaultGameCameraMarker_Position = new();
        internal static Vector3 defaultMainCameraMarker_Position = new();
        internal static Vector3 defaultStartCameraMarker_Position = new();
        internal static Vector3 defaultSavesCameraMarker_Position = new();

        internal static Vector3 defaultCharacterCameraMarker_Rotation = new();
        internal static Vector3 defaultCreditsCameraMarker_Rotation = new();
        internal static Vector3 defaultGameCameraMarker_Rotation = new();
        internal static Vector3 defaultMainCameraMarker_Rotation = new();
        internal static Vector3 defaultStartCameraMarker_Rotation = new();
        internal static Vector3 defaultSavesCameraMarker_Rotation = new();
        
        internal static Transform _defaultPlayerPreviewPoint = null!;
        internal static Vector3 _defaultPlayerPosition = new();
        internal static Vector3 _defaultPlayerRotation = new();
        
        

        public void Awake()
        {
            instance = this;
            
            UsePreset = config("1 - Presets", "Use Preset", Toggle.On, "Use the preset camera positions that I have come up with.");
            UseVanilla = config("1 - Presets", "Use Vanilla", Toggle.Off, "Use the preset camera positions the Valheim devs made.");
            UsePreset.SettingChanged += (_, _) =>
            {
                if(UsePreset.Value == Toggle.On)
                    UseVanilla.Value = Toggle.Off;
                Functions.UpdateAndSet();
            };
            UseVanilla.SettingChanged += (_, _) =>
            {
                if(UseVanilla.Value == Toggle.On)
                    UsePreset.Value = Toggle.Off;
                Functions.UpdateAndSet();
            };

            CharacterCameraMarker_Position = config("2 - Camera Markers", "Character", new Vector3(-161.89f, 55.97f, 230.13f),
                "The camera marker to use when in the character screen.");
            
            CharacterCameraMarker_Rotation = config("2 - Camera Markers", "Character Rotation", new Vector3(13.79f, -84.748f, 0),
                "The camera marker to use when in the character screen.");

            CreditsCameraMarker_Position = config("2 - Camera Markers", "Credits", new Vector3(-156.8638f, 78.89107f, 225.6782f),
                "The camera marker to use when in the credits screen.");
            
            CreditsCameraMarker_Rotation = config("2 - Camera Markers", "Credits Rotation", new Vector3(-2.197f, -48.48f, 0),
                "The camera marker to use when in the credits screen.");

            GameCameraMarker_Position = config("2 - Camera Markers", "Game", new Vector3(-157.8478f, 56.59023f, 229.9292f),
                "The camera marker to use when in the game screen.");
            
            GameCameraMarker_Rotation = config("2 - Camera Markers", "Game Rotation", new Vector3(-187.257f, 77.72f, 180),
                "The camera marker to use when in the game screen.");

            MainCameraMarker_Position = config("2 - Camera Markers", "Main", new Vector3(-142.532f, 59, 227.7245f),
                "The camera marker to use when in the main menu screen.");
            
            MainCameraMarker_Rotation = config("2 - Camera Markers", "Main Rotation", new Vector3(1.241f, -84.232f, 0),
                "The camera marker to use when in the main menu screen.");

            StartCameraMarker_Position = config("2 - Camera Markers", "Start", new Vector3(-137.63f, 54.78f, 227.44f),
                "The camera marker to use when in the start screen.");
            
            StartCameraMarker_Rotation = config("2 - Camera Markers", "Start Rotation", new Vector3(41.028f, -84.232f, 0),
                "The camera marker to use when in the start screen.");

            SavesCameraMarker_Position = config("2 - Camera Markers", "Saves", new Vector3(-163.02f, 60.34f, 228.74f),
                "The camera marker to use when in the saves screen.");
            
            SavesCameraMarker_Rotation = config("2 - Camera Markers", "Saves Rotation", new Vector3(185.79f, 81.743f, -180),
                "The camera marker to use when in the saves screen.");


            CharacterCameraMarker_Position.SettingChanged += (_, _) => { UpdateCameraPositions(); };
            CreditsCameraMarker_Position.SettingChanged += (_, _) => { UpdateCameraPositions(); };
            GameCameraMarker_Position.SettingChanged += (_, _) => { UpdateCameraPositions(); };
            MainCameraMarker_Position.SettingChanged += (_, _) => { UpdateCameraPositions(); };
            StartCameraMarker_Position.SettingChanged += (_, _) => { UpdateCameraPositions(); };
            SavesCameraMarker_Position.SettingChanged += (_, _) => { UpdateCameraPositions(); };


            CharacterCameraMarker_Rotation.SettingChanged += (_, _) => { UpdateCameraRotations(); };
            CreditsCameraMarker_Rotation.SettingChanged += (_, _) => { UpdateCameraRotations(); };
            GameCameraMarker_Rotation.SettingChanged += (_, _) => { UpdateCameraRotations(); };
            MainCameraMarker_Rotation.SettingChanged += (_, _) => { UpdateCameraRotations(); };
            StartCameraMarker_Rotation.SettingChanged += (_, _) => { UpdateCameraRotations(); };
            SavesCameraMarker_Rotation.SettingChanged += (_, _) => { UpdateCameraRotations(); };
            

            MPlayerPosition = config("3 - Player Position", "Player Position", new Vector3(-204.51f,36.50f,157.53f),
                "The position to spawn the player at.");
            MPlayerRotation = config("3 - Player Position", "Player Rotation", new Vector3(13.79f, -84.748f, 0),
                "The rotation to spawn the player at.");
            

            MPlayerPosition.SettingChanged += (_, _) => { UpdatePlayerPosition(); };
            MPlayerRotation.SettingChanged += (_, _) => { UpdatePlayerPosition(); };
            

            MFirePosition = config("3 - Fire Position", "Fire Position", new Vector3(-11.264f, 36.5f, -68.486f),
                "The position to spawn the Fire at.");
            
            MFirePosition.SettingChanged += (_, _) => { UpdatePlayerPosition(); };
            
            customsong = config("4 - Custom Song", "Custom Song",
                "https://cdn.pixabay.com/download/audio/2022/08/17/audio_a52363b467.mp3?filename=the-gift-pagan-norse-background-music-117479.mp3",
                "The song that will play at the main menu. Must be a direct link to the file, the bigger the file the more likely there is a delay in the song starting.");

            customsong.SettingChanged += (_, _) => SongChanged();

            Assembly assembly = Assembly.GetExecutingAssembly();
            _harmony.PatchAll(assembly);
            SetupWatcher();
        }

        private void OnDestroy()
        {
            Config.Save();
            
            // Unsubscribe from all delegates
            customsong.SettingChanged -= (_, _) =>  SongChanged();
            
            CharacterCameraMarker_Position.SettingChanged -= (_, _) => UpdateCameraPositions();
            CreditsCameraMarker_Position.SettingChanged -= (_, _) =>  UpdateCameraPositions(); 
            GameCameraMarker_Position.SettingChanged -= (_, _) =>  UpdateCameraPositions(); 
            MainCameraMarker_Position.SettingChanged -= (_, _) =>  UpdateCameraPositions(); 
            StartCameraMarker_Position.SettingChanged -= (_, _) =>  UpdateCameraPositions(); 
            SavesCameraMarker_Position.SettingChanged -= (_, _) =>  UpdateCameraPositions(); 
            
            CharacterCameraMarker_Rotation.SettingChanged -= (_, _) =>  UpdateCameraRotations(); 
            CreditsCameraMarker_Rotation.SettingChanged -= (_, _) =>  UpdateCameraRotations(); 
            GameCameraMarker_Rotation.SettingChanged -= (_, _) =>  UpdateCameraRotations(); 
            MainCameraMarker_Rotation.SettingChanged -= (_, _) =>  UpdateCameraRotations(); 
            StartCameraMarker_Rotation.SettingChanged -= (_, _) =>  UpdateCameraRotations(); 
            SavesCameraMarker_Rotation.SettingChanged -= (_, _) =>  UpdateCameraRotations(); 
            
            MPlayerPosition.SettingChanged -= (_, _) =>  UpdatePlayerPosition(); 
            MPlayerRotation.SettingChanged -= (_, _) =>  UpdatePlayerPosition(); 
            
            MFirePosition.SettingChanged -= (_, _) =>  UpdateFirePosition(); 
            
            
        }

        private void UpdateCameraPositions()
        {
            if (FejdStartup.instance == null) return;
            FejdStartup.instance.m_cameraMarkerCharacter.position = CharacterCameraMarker_Position.Value;
            FejdStartup.instance.m_cameraMarkerCredits.position = CreditsCameraMarker_Position.Value;
            FejdStartup.instance.m_cameraMarkerGame.position = GameCameraMarker_Position.Value;
            FejdStartup.instance.m_cameraMarkerMain.position = MainCameraMarker_Position.Value;
            FejdStartup.instance.m_cameraMarkerStart.position = StartCameraMarker_Position.Value;
            FejdStartup.instance.m_cameraMarkerSaves.position = SavesCameraMarker_Position.Value;
        }        
        private void UpdatePlayerPosition()
        {
            if (FejdStartup.instance == null) return;
            GameObject? backgroundScene;
            try
            {
                backgroundScene = GameObject.Find("Backgroundscene");
            } catch (Exception e)
            {
                return;
            }
            if(backgroundScene == null) return;
            Functions.UpdatePlayerValues();
            FejdStartup.instance.UpdateCharacterList();
        }

        private void UpdateFirePosition()
        {
            // TODO: Implement this
        }

        private void UpdateCameraRotations()
        {
            if (FejdStartup.instance == null) return;
            FejdStartup.instance.m_cameraMarkerCharacter.rotation = Quaternion.Euler(CharacterCameraMarker_Rotation.Value);

            FejdStartup.instance.m_cameraMarkerCredits.rotation = Quaternion.Euler(CreditsCameraMarker_Rotation.Value);

            FejdStartup.instance.m_cameraMarkerGame.rotation = Quaternion.Euler(GameCameraMarker_Rotation.Value);

            FejdStartup.instance.m_cameraMarkerMain.rotation = Quaternion.Euler(MainCameraMarker_Rotation.Value);

            FejdStartup.instance.m_cameraMarkerStart.rotation = Quaternion.Euler(StartCameraMarker_Rotation.Value);

            FejdStartup.instance.m_cameraMarkerSaves.rotation = Quaternion.Euler(SavesCameraMarker_Rotation.Value);
        }
        
        internal void SongChanged()
        {
            if (FejdStartup.instance == null) return;
            if(MusicMan.instance == null) return;
            MusicMan.instance.Reset();
            MusicManTriggerMusicPatch.StreamAudio(customsong.Value);
        }

        private void SetupWatcher()
        {
            FileSystemWatcher watcher = new(Paths.ConfigPath, ConfigFileName);
            watcher.Changed += ReadConfigValues;
            watcher.Created += ReadConfigValues;
            watcher.Renamed += ReadConfigValues;
            watcher.IncludeSubdirectories = true;
            watcher.SynchronizingObject = ThreadingHelper.SynchronizingObject;
            watcher.EnableRaisingEvents = true;
        }

        private void ReadConfigValues(object sender, FileSystemEventArgs e)
        {
            if (!File.Exists(ConfigFileFullPath)) return;
            try
            {
                ChangeOfSceneryLogger.LogDebug("ReadConfigValues called");
                Config.Reload();
            }
            catch
            {
                ChangeOfSceneryLogger.LogError($"There was an issue loading your {ConfigFileName}");
                ChangeOfSceneryLogger.LogError("Please check your config entries for spelling and format!");
            }
        }


        #region ConfigOptions

        internal static ConfigEntry<Toggle> UsePreset = null!;
        internal static ConfigEntry<Toggle> UseVanilla = null!;
        public static ConfigEntry<Vector3> CharacterCameraMarker_Position = null!;
        public static ConfigEntry<Vector3> CreditsCameraMarker_Position = null!;
        public static ConfigEntry<Vector3> GameCameraMarker_Position = null!;
        public static ConfigEntry<Vector3> MainCameraMarker_Position = null!;
        public static ConfigEntry<Vector3> StartCameraMarker_Position = null!;
        public static ConfigEntry<Vector3> SavesCameraMarker_Position = null!;
        public static ConfigEntry<Vector3> MPlayerPosition = null!;
        public static ConfigEntry<Vector3> MFirePosition = null!;

        // Rotation
        public static ConfigEntry<Vector3> CharacterCameraMarker_Rotation = null!;
        public static ConfigEntry<Vector3> CreditsCameraMarker_Rotation  = null!;
        public static ConfigEntry<Vector3> GameCameraMarker_Rotation  = null!;
        public static ConfigEntry<Vector3> MainCameraMarker_Rotation  = null!;
        public static ConfigEntry<Vector3> StartCameraMarker_Rotation  = null!;
        public static ConfigEntry<Vector3> SavesCameraMarker_Rotation  = null!;
        public static ConfigEntry<Vector3> MPlayerRotation  = null!;
        
        public static ConfigEntry<string> customsong = null!;


        private ConfigEntry<T> config<T>(string group, string name, T value, ConfigDescription description)
        {
            ConfigEntry<T> configEntry = Config.Bind(group, name, value, description);

            return configEntry;
        }

        private ConfigEntry<T> config<T>(string group, string name, T value, string description)
        {
            return config(group, name, value, new ConfigDescription(description));
        }

        private class ConfigurationManagerAttributes
        {
            [UsedImplicitly] public bool? Browsable = false;
        }

        class AcceptableShortcuts : AcceptableValueBase
        {
            public AcceptableShortcuts() : base(typeof(KeyboardShortcut))
            {
            }

            public override object Clamp(object value) => value;
            public override bool IsValid(object value) => true;

            public override string ToDescriptionString() =>
                "# Acceptable values: " + string.Join(", ", KeyboardShortcut.AllKeyCodes);
        }

        #endregion
    }
}