using System;
using Common.DataTypes.Runtime.Reactive;
using Internal.Scopes.Abstract.Lifetimes;
using UnityEngine;

namespace Global.UI.Design.Runtime.DataBindings
{
    [DisallowMultipleComponent]
    public class DesignListView : MonoBehaviour
    {
       // [SerializeField] private ListView _list;

        public void Construct<TValue, TEntry>(
            IReadOnlyLifetime lifetime,
            IViewableList<TValue> collection,
            Action<TEntry> constructListener,
            Action<TEntry> disposeListener)
        {
            // _list.AddDataBinder<TValue, TEntry>(BindView);
            // collection.View(lifetime, OnElementAdd);
            //
            // return;
            //
            // void BindView(Data.OnBind<TValue> data, TEntry target, int index)
            // {
            // }
        }

        private void OnElementAdd<T>(IReadOnlyLifetime lifetime, T value)
        {
            lifetime.ListenTerminate(OnElementRemoved);

            return;

            void OnElementRemoved()
            {
            }
        }


        private void OnValidate()
        {
            // if (_list == null)
            //     _list = GetComponent<ListView>();
        }
    }
}