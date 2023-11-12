using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TF2.Proj
{
    internal class FlamethrowerProj : ModProjectile
    {
        public float Scale = 3;
        public override void SetDefaults()
        {
            Projectile.width = 10; // The width of projectile hitbox
            Projectile.height = 10; // The height of projectile hitbox
            Projectile.aiStyle = ProjAIStyleID.FlamingScythe;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 100;
            Projectile.timeLeft = 30;
            Projectile.light = .1f;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
        }
        public override void AI()
        {
            Scale -= .1f;
            Projectile.scale = Scale;

        }
    }
}
