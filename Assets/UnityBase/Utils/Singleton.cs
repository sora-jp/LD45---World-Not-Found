using UnityBase.Animations;
using UnityEngine;

namespace UnityBase.Utils
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null) _instance = FindObjectOfType<T>();
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogError($"More than one {GetType().Name} exist. This is not allowed");
                Destroy(this);
                return;
            }
            _instance = (T)this;
        }
    }
}
