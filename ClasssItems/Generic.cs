using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System.Diagnostics;
using Terraria.Net;
using TF2.Utills;
using TF2.Assets;
namespace TF2.ClassItems {
    public class Pistol : TF2Weapon{
        public override string Texture => Mod.Name + "/Assets/Textures/Generic/" + Name;
        public override void SetDefaults(){
            Item.scale = .5f;
            Item.shoot = ProjectileID.Bullet;
            WeaponData(12, 36, .12f, 1.025f, Sounds.pistol_shoot);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback){
            if (!CanShoot(player)) return false;
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }



    }

    public class Shotgun : TF2Weapon{
        public override string Texture => Mod.Name + "/Assets/Textures/Generic/" + Name;
        public override void SetDefaults(){
            Item.scale = .75f;
            Item.shoot = ProjectileID.Bullet;
            WeaponData(6, 32, .825f, .51f, Sounds.shotgun_shoot);
            
            
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback){
            if (!CanShoot(player)) return false;
            const int NumBullets = 8;
            for (int i = 0; i < NumBullets; i++){
                Vector2 vel = velocity.RotatedByRandom(MathHelper.ToRadians(15));
                vel *= 1f - Main.rand.NextFloat(.3f);
                Projectile.NewProjectileDirect(source, position, vel, type, Main.rand.Next(0, 101) < 5 ? damage * 2 : damage, knockback);
            }
            return false;
        }
    }

}
