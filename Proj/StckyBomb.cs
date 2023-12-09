using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using TF2.Utills;
namespace TF2.Proj {
   public class StickyBomb : ModProjectile {
        public static int TotalBombs = 0;
        public bool CollidedWithTile = false;
        public override string Texture => Mod.Name + "/Assets/Textures/Projectiles/" + Name;
        public override void SetDefaults(){
            Projectile.CloneDefaults(Terraria.ID.ProjectileID.Grenade);
        }
        public override bool OnTileCollide(Vector2 v) {
            if (TotalBombs > 100) {
                return true;
            }
            CollidedWithTile = true;
            TotalBombs++;
            return false;
        }
        public override void AI() {
            if (!CollidedWithTile) base.AI();
            else { Projectile.velocity = Vector2.Zero; };
        }
        public override void OnKill(int timeLeft){
            Projectile.NewProjectile(
                Projectile.GetSource_FromThis(),
                Projectile.position,
                Vector2.Zero,
                ModContent.ProjectileType<Explosion>(),
                0,
                0);
            base.OnKill(timeLeft);
        }
    }
}
