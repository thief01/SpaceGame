using System.Collections;
using UnityEngine;

namespace Core.Pooling
{
    public class BasePoolObject : MonoBehaviour
    {
        public BasePool BasePool { get; set; }

        public virtual void Kill()
        {
            StopAllCoroutines();
            BasePool.ReturnObject(this);
        }

        public void KillWithDelay(float delay)
        {
            StopAllCoroutines();
            StartCoroutine(KillDelay(delay));
        }

        private IEnumerator KillDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            Kill();
        }
    }
}
