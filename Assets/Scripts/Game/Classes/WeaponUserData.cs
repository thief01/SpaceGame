using Game.Character;
using UnityEngine;

namespace Game.Classes
{
    public class WeaponUserData
    {
        public Transform Muzzle { get; set; }
        public Transform Owner { get; set; }
        public CooldownControler CooldownControler { get; set; }
        public Vector3 Target { get; set; }

        public Vector2 Direction => Target - Muzzle.position;
    }
}
