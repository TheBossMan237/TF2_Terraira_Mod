using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TF2.Proj
{
    internal class SyringeProj : ModProjectile
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Projectiles/" + Name;
        public override void SetDefaults(){
            Projectile.aiStyle = ProjAIStyleID.FlamingScythe;
            
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 100;
            Projectile.timeLeft = 30;
            Projectile.light = 1f;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
        }
    }
}
