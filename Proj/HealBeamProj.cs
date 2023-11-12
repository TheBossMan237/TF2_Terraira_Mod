using Microsoft.Xna.Framework.Input;
using Mono.Cecil.Rocks;
using System;
using System.Drawing;
using System.Numerics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace TF2.Proj
{
    internal class HealBeamProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10; // The width of projectile hitbox
            Projectile.height = 10; // The height of projectile hitbox
            Projectile.aiStyle = ProjAIStyleID.Beam;
            Projectile.friendly = true;
            Projectile.hostile = true;
            Projectile.penetrate = 100;
            Projectile.timeLeft = 300;
            Projectile.light = 1f;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Main.NewText("AWD");
            base.OnHitNPC(target, hit, damageDone);
        }
    }
}
