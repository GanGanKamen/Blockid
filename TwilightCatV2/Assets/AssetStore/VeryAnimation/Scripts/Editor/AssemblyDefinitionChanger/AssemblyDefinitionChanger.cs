#if UNITY_2017_3_OR_NEWER
using UnityEngine;
using UnityEditor.Experimental.AssetImporters;
using UnityEditor;
using System;
using System.IO;
using System.Collections;

namespace VeryAnimation
{
    [ScriptedImporter(0, "asmdefChanger")]
    class AssemblyDefinitionChanger : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            var path = ctx.assetPath.Remove(ctx.assetPath.Length - Path.GetExtension(ctx.assetPath).Length);
            var name = Path.GetFileNameWithoutExtension(path);
            if (!name.StartsWith("AloneSoft."))
                return;

            var version = Path.GetExtension(path);
#if UNITY_2019_1_OR_NEWER
            if (version != ".2019_1") return;
#else
            if (version != ".2017_3") return;
#endif
            var nameExt = name + ".asmdef";
            foreach (var guid in AssetDatabase.FindAssets(name))
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                if (Path.GetFileName(assetPath) != nameExt)
                    continue;

                FileUtil.DeleteFileOrDirectory(assetPath);
                FileUtil.CopyFileOrDirectory(ctx.assetPath, assetPath);

                EditorApplication.delayCall += () =>
                {
                    AssetDatabase.Refresh();
                };
                break;
            }
        }
    }
}
#endif
