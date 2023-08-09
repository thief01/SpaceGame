using System.Collections.Generic;
using UnityEngine;

namespace Game.Character
{
    public class CooldownControler : MonoBehaviour
    {
        private class Cooldown
        {
            public float Time { get; set; }
            public string Name { get; set; }
        }

        private List<Cooldown> cooldowns = new List<Cooldown>();

        private void Update()
        {
            for (int i = 0; i < cooldowns.Count; i++)
            {
                cooldowns[i].Time -= Time.deltaTime;
                if (cooldowns[i].Time <= 0)
                {
                    cooldowns.RemoveAt(i);
                    i--;
                }
            }
        }

        public void AddNewCooldown(float time, string name)
        {
            cooldowns.Add(new Cooldown()
            {
                Time = time,
                Name = name.ToLower()
            });
        }

        public float GetCooldown(string name)
        {
            if (cooldowns == null || cooldowns.Count == 0)
            {
                return 0;
            }
            if (!cooldowns.Exists(ctg => ctg.Name == name.ToLower()))
            {
                return 0;
            }
            return cooldowns.Find(ctg => ctg.Name == name.ToLower()).Time;
        }
    }
}
