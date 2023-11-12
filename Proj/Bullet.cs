using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TF2.Proj
{
    internal class Bullet : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.damage = 10;
            
            Projectile.friendly = true;
            Projectile.CritChance = 0;
        }
        public override void AI()
        {
            base.AI();
        }
        public override void OnSpawn(IEntitySource source)
        {
            Player p = Main.player[Projectile.owner];
            Projectile.rotation = p.itemRotation;
            base.OnSpawn(source);
        }
    }
}
