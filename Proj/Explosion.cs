using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
namespace TF2.Proj {
    public class Explosion : ModProjectile {
        public override string Texture => Mod.Name + "/Assets/Textures/Projectiles/" + Name;
		public bool Reverse = false;
        public override void SetStaticDefaults(){
            Main.projFrames[Type] = 7;
        }
        public override void SetDefaults() {
			Projectile.width = 90;
			Projectile.height = 90;
			Projectile.frame = 1;
            Projectile.alpha = 255;
            Projectile.friendly = true;
            Projectile.hostile = true;
			Projectile.ownerHitCheck = true;
			Projectile.penetrate = 300;      
		}
        public override void AI() {	
			Projectile.ai[0] = ++Projectile.ai[0] % 60;
			if (Projectile.frame == 0) Projectile.Kill();
			if (Projectile.ai[0] % 8 == 0) {
				if (Projectile.frame < 6){ Projectile.frame++;
				} else{ Projectile.Kill();	 }
			} 
            
        }
        public override bool PreDraw(ref Color lightColor){
			// Getting texture of projectile
			Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);

			// Calculating frameHeight and current Y pos dependence of frame
			// If texture without animation frameHeight is always texture.Height and startY is always 0
			int frameHeight = texture.Height / Main.projFrames[Projectile.type];
			Rectangle sourceRectangle = new Rectangle(0, 90*Projectile.frame, 90, 90 );
			Vector2 origin = sourceRectangle.Size() / 2f;
			Color drawColor = Color.White;
			Main.EntitySpriteDraw(texture,
				Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
				sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None, 0);

			// It's important to return false, otherwise we also draw the original texture.
			return false;

		}
    }
}