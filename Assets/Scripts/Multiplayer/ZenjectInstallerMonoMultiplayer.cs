using Core.Pooling;
using Game.Classes;
using Game.Interfaces;
using Game.ZenjectInstallers;
using UnityEngine;
using Zenject;

namespace Multiplayer
{
    public class ZenjectInstallerMonoMultiplayer : ZenjectInstallerMono
    {

        protected override void BindPools()
        {
            bullets = new MultiplayerPool(bullet, 10);
            ships = new MultiplayerPool(ship, 1);
            asteroids = new MultiplayerPool(asteroid, 10);
            explosions = new MultiplayerPool(explosion, 10);
            
            BindNewPool(bullets, "Bullets");
            BindNewPool(ships, "Ships");
            BindNewPool(asteroids, "Asteroids");
            BindNewPool(explosions, "Explosions");
        }
    }
}