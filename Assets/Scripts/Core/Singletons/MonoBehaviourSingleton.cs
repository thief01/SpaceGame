using UnityEngine;

namespace Core.Singletons
{
    public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = GetComponent<T>();
            }

            if (Instance.gameObject != gameObject)
            {
                Destroy(gameObject);
            }
        }
    }
}
