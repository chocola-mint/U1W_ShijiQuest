using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Freeheart.Audio
{
    /// <summary>
    /// A data container holding audio settings.
    /// </summary>
    [CreateAssetMenu(fileName = "AudioSettings", menuName = "Freeheart/Audio/AudioSettings", order = 1)]
    public class AudioSettings : ScriptableObject
    {
        [SerializeField, HideInInspector]
        private VolumeSettings BGMVolumeSettings;
        private VolumeSettings SFXVolumeSettings;
        public VolumeSettings GetBGMVolumeSettings() => BGMVolumeSettings;
        public VolumeSettings GetSFXVolumeSettings() => SFXVolumeSettings;
        
// #if UNITY_EDITOR
//         public static T AddSubAsset<T>(UnityEngine.Object addTo, string sSubAssetName) where T : ScriptableObject
//         {
//             T subAsset = null;
//             subAsset = ScriptableObject.CreateInstance<T>();
//             AssetDatabase.AddObjectToAsset(subAsset, addTo);
//             subAsset.name = sSubAssetName;
//             AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(subAsset));
//             AssetDatabase.SaveAssets();
//             return subAsset;
//         }
//         [Button("Initialize")]
//         void EditorInit()
//         {
//             // first try to find existing child within all assets contained in this asset
//             var assets = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(this));
//             foreach(var asset in assets)
//                 AssetDatabase.RemoveObjectFromAsset(asset);
//             BGMVolumeSettings = AddSubAsset<VolumeSettings>(this, nameof(BGMVolumeSettings));
//             SFXVolumeSettings = AddSubAsset<VolumeSettings>(this, nameof(SFXVolumeSettings));
//             // BGMVolumeSettings = CreateInstance<VolumeSettings>();
//             // BGMVolumeSettings.name = nameof(BGMVolumeSettings);
//             // AssetDatabase.CreateAsset(BGMVolumeSettings, $"Assets/TEMP_A_0001.asset");
//             // AssetDatabase.AddObjectToAsset(BGMVolumeSettings, this);
//             // SFXVolumeSettings = CreateInstance<VolumeSettings>();
//             // SFXVolumeSettings.name = nameof(SFXVolumeSettings);
//             // AssetDatabase.CreateAsset(SFXVolumeSettings, $"Assets/TEMP_B_1212.asset");
//             // AssetDatabase.AddObjectToAsset(SFXVolumeSettings, this);
//             // Mark this asset as dirty so it is correctly saved in case we just changed the "child" field
//             // without using the "AddObjectToAsset" (which afaik does this automatically)
//             EditorUtility.SetDirty(this);

//             // Save all changes
//             AssetDatabase.SaveAssets();
            
//         }
// #endif
        
    }
    
}
