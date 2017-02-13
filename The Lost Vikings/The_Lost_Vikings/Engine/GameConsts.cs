using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The_Lost_Vikings
{
    /// <summary>
    /// All used Constants for the Game
    /// </summary>
    class GameConsts
    { 
        public const int ScreenWidth = 1024, ScreenHeight = 768;
        public const float SplashScreenShowTime = 0, CatShowTime = 8, CreditsTime = 8;
        public const int TileSize = 32;
        public const float GravitationVelocity = 0.4f;
        public const float BaleogSpeed =2f, EricSpeed = 5f, SnailSpeed = 0.5f, MrScissorsSpeed = 2f, MovingPlatformSpeed = 1f;
        public const int MrScissorsDamage = 50, SpikesDamage = 100, SnailDamage = 25, BaleogDamage = 50;
        public const float BaleogJumpForce = -3.5f, BaleogJumpOffset = 7.5f, EricJumpForce = -7f, EricJumpOffset = 15f;
        public const int PlayerHealth = 100, SnailHealth = 100;
        public const int HealPotionHpAmount = 50;
    }
}
