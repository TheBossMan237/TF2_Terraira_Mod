using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TF2.Assets;
using TF2.Utills;
using TF2.Proj;
namespace TF2.ClassItems
{
    
    public class GrenadeLauncher : TF2Weapon{
        
        public override string Texture => Mod.Name + "/Assets/Textures/Demoman/" + Name;
        public override void SetDefaults(){
            Item.shoot = ProjectileID.Grenade;
            Item.useStyle = ItemUseStyleID.Shoot;
            WeaponData(4, 16, .6f, 1.24f, Sounds.grenadelauncher_shoot);

        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if(!CanShoot(player)) return false;
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
    public class StickyBombLauncher : TF2Weapon
    {
        public Projectile[] Explosives = new Projectile[100];
        public int ArrIndex = 0;
        public int BulletID = ModContent.ProjectileType<StickyBomb>();
        public override string Texture => Mod.Name + "/Assets/Textures/Demoman/" + Name;
        public override void SetDefaults(){
            Item.shoot = ModContent.ProjectileType<BlankBullet>();
            
            Item.useStyle = ItemUseStyleID.Shoot;
            WeaponData(8, 24, .6f, 1.09f, Sounds.stickybomblaunher_shoot);
        }
        public override bool AltFunctionUse(Player player){
            for (int i = 0; i < 100; i++) {
                if (Explosives[i] == null) break;
                Explosives[i].Kill();
            }
            Explosives = new Projectile[100];
            ArrIndex = 0;
            return base.AltFunctionUse(player);
        }
        public override void HoldStyle(Player player, Rectangle heldItemFrame){

            base.HoldStyle(player, heldItemFrame);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
            if (!CanShoot(player)) return false;
            Projectile p = Main.projectile[Projectile.NewProjectile(source, position, velocity, BulletID, 0, 0)];
            Explosives[ArrIndex] = p;
            if (ArrIndex < 100) ArrIndex++;

            return true;
        }   
    }
    public  class Bottle : TF2Weapon
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Demoman/" + Name;
        public override void SetDefaults(){
            Item.CloneDefaults(ItemID.CopperBroadsword);
            MeleeWeapon(.8f);
        }
    }
    public class DemomanClassBag : ModItem
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Demoman/" + Name;
        public override void SetDefaults(){
            Item.useStyle = 4;
            Item.consumable = true;
            Item.width = 32;
            Item.useTime = 1;
            Item.useAnimation = 1;
            Item.height = 32;
        }

        public override bool? UseItem(Player player)
        {
            Helper.Loadout<GrenadeLauncher, StickyBombLauncher, Kukri, DemomanIdentifier>(player);
            

            return base.UseItem(player);
        }

    }
    public class DemomanIdentifier : ModItem
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Demoman/" + Name;
        public override void SetDefaults()
        {
            Item.accessory = true;
        }
    }
}
