﻿using UnityEngine;

namespace Internal.Scopes.Abstract.Instances.Services
{
    public interface IServiceScopeBinder
    {
        void MoveToModules(MonoBehaviour service);
        void MoveToModules(GameObject service);
        void MoveToModules(Transform service);
    }
}