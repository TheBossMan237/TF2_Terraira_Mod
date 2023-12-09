using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using TF2.Assets;
using Terraria.Audio;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace TF2.Proj
{
    
    public class Rocket : ModProjectile
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Projectiles/" + Name;
        private float TriggerDistance = 78.1225f;

        public override void SetDefaults()
        {
            Projectile.width = 10; 
            Projectile.height = 10; 
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.penetrate = 100;
            Projectile.timeLeft = 300; 
            Projectile.light = 0f;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            

        }
        private bool RocketJump(Entity Target, float Threshold) {
            if (Target == null) { return false; }
            else {

                float distance = Projectile.position.Distance(Target.position);
                if (distance < Threshold) {
                    distance = Threshold - distance;
                    Target.velocity += Projectile.oldVelocity * (distance / 15.6245f);
                    return true;
                }
                return false;

            }
            
        }
        public override void OnKill(int timeLeft){
            Projectile.NewProjectile(
                Projectile.GetSource_FromThis(),
                Projectile.position,
                Vector2.Zero,
                ModContent.ProjectileType<Explosion>(),
                100,
                100

            );
            Projectile.oldVelocity = Projectile.oldVelocity * -1;
            for (int i = 0; i < Main.npc.Length; i++) {
                RocketJump(Main.npc[i], TriggerDistance);
            }
            for (int i = 0; i < Main.player.Length; i++) {
                Player player = Main.player[i];
                
                RocketJump(player, TriggerDistance);

            }

            SoundEngine.PlaySound(Sounds.rocket_explode);
            base.OnKill(timeLeft);

        }
    }
}
