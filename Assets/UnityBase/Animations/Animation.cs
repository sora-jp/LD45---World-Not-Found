using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace UnityBase.Animations
{
    public static partial class Animation
    {
        public static void Animate<TComponent, T>(this TComponent target, string propName, T end, float time, Func<T, T, float, T> lerper, EaseMode mode = EaseMode.Linear) where TComponent : Component
        {
            Animate(target.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance), target, end, time, new Lerper<T>(lerper), mode);
        }

        static void Animate(PropertyInfo info, Component target, object end, float time, Lerper lerper, EaseMode mode)
        {
            if (!target.TryGetComponent(out AnimationCoroutineContainer container)) container = target.gameObject.AddComponent<AnimationCoroutineContainer>();
            container.StartCoroutine(_Animate(info, target, end, time, lerper, mode));
        }

        static IEnumerator _Animate(PropertyInfo info, object target, object end, float time, Lerper lerper, EaseMode mode)
        {
            object start = info.GetValue(target);
            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime / time;
                info.SetValue(target, lerper.Lerp(start, end, Ease(t, mode)));
                yield return null;
            }
            info.SetValue(target, end);
        }

        static float Ease(float t, EaseMode mode)
        {
            const float a = 3;
            switch (mode)
            {
                case EaseMode.Linear:
                    return t;
                case EaseMode.EaseIn:
                    return Mathf.Pow(t, a);
                case EaseMode.EaseOut:
                    return 1 - Mathf.Pow(1 - t, a);
                case EaseMode.EaseInOut:
                    return Mathf.Pow(t, a) / (Mathf.Pow(t, a) + Mathf.Pow(1 - t, a));
                default:
                    return -1;
            }
        }
    }

    internal class Lerper<T> : Lerper
    {
        readonly Func<T, T, float, T> m_lerpFunc;

        public Lerper(Func<T, T, float, T> lerpFunc) => m_lerpFunc = lerpFunc;

        public override object Lerp(object a, object b, float t) => m_lerpFunc((T)a, (T)b, t);
    }
}