using Core.Pooling;
using UnityEngine;
using Zenject;

namespace Game.Character
{
    public class DeadFxControler : MonoBehaviour
    {
        [SerializeField] private float fxAliveTime;

        [Inject(Id = "Explosions")] public BasePool fxPool;

        public void OnDie()
        {
            BasePoolObject g = fxPool.GetNewObject();
            if (g != null)
            {
                g.transform.position = transform.position;
                g.KillWithDelay(fxAliveTime);
            }
        }

    }
}
