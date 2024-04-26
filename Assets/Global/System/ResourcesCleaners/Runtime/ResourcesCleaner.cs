using System;
using Cysharp.Threading.Tasks;
using Global.System.ResourcesCleaners.Abstract;
using UnityEngine;

namespace Global.System.ResourcesCleaners.Runtime
{
    public class ResourcesCleaner : IResourcesCleaner
    {
        public async UniTask CleanUp()
        {
            GC.Collect();

            await Resources.UnloadUnusedAssets().ToUniTask();
        }
    }
}