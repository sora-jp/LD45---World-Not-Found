using UnityBase.Animations;
using UnityEngine;

namespace UnityBase.Utils
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError($"More than one {GetType().Name} exist. This is not allowed");
                Destroy(this);
                return;
            }
            Instance = (T)this;
        }
    }
}
