using System.Collections;
using UnityEngine;

namespace BaekRyang
{
    public static class SharedFunction
    {
        public static IEnumerator DestroyFunc(GameObject gameObject, float destroyTime)
        {
            float time = 0f;

            //GetComponent는 속도가 느리므로 캐싱해서 사용
            SpriteRenderer _sr = gameObject.GetComponent<SpriteRenderer>();

            Color _cachedColor = _sr.color;
            do
            {
                _cachedColor.a -= Time.deltaTime / destroyTime;
                _sr.color      =  _cachedColor;
                time           += Time.deltaTime;

                yield return null;
            } while (time <= destroyTime);

            GameObject.Destroy(gameObject);
        }
    }
}