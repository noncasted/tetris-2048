using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace Tools.EditorTools.Assemblies
{
    public class InvalidAssemblyReferencesRemover
    {
        private const string _sourcesFolder = "/";

        [MenuItem("Tools/Remove invalid asmdef references")]
        private static void RemoveInvalidAssemblyReferences()
        {
            var directory = Application.dataPath + _sourcesFolder;

            var asmdefFiles = Directory.GetFiles(directory, "*.asmdef", SearchOption.AllDirectories);
            
            foreach (var filePath in asmdefFiles)
            {
                var jsonContent = File.ReadAllText(filePath);
                var asmdef = JsonConvert.DeserializeObject<AsmdefFile>(jsonContent);
                
                if (asmdef.references.Count == 0)
                    continue;
                
                var toRemove = new List<string>();

                foreach (var referenceGUID in asmdef.references)
                {
                    var reference = referenceGUID.Replace("GUID:", "");
                    
                    var assetPath = AssetDatabase.GUIDToAssetPath(reference);
                    
                    if (assetPath != string.Empty)
                        continue;

                    toRemove.Add(reference);
                }
                
                if (toRemove.Count == 0)
                    continue;

                foreach (var remove in toRemove)
                {
                    Debug.Log($"Remove invalid assembly reference: {remove}");
                    
                    var removeString = $"\"GUID:{remove}\"";
                    
                    if (jsonContent.Contains($"{removeString},") == true)
                    {
                        jsonContent = jsonContent.Replace($"{removeString},", "");
                    }
                    else if (jsonContent.Contains(removeString) == true)
                    {
                        var index = jsonContent.IndexOf(removeString, StringComparison.Ordinal);
                        jsonContent = jsonContent.Replace(removeString, "");
                        
                        var maxSteps = 10;
                        var step = 0;

                        while (step < maxSteps)
                        {
                            step++;
                            index--;

                            var symbol = jsonContent[index];

                            if (symbol == ',')
                            {
                                jsonContent.Remove(index);
                                break;
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError($"Invalid trigger in: {filePath} for {remove}");
                    }
                }

                File.WriteAllText(filePath, jsonContent);
            }
        }

        private static IEnumerable<string> GetDllAssemblyNames(string directory)
        {
            return Directory.EnumerateFiles(directory, "*.asmdef", SearchOption.AllDirectories)
                .Distinct()
                .Select(Path.GetFileNameWithoutExtension);
        }
    }
}