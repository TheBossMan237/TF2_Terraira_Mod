using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TF2.Assets;
using TF2.Utills;
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
        public Projectile[] Explosives;
        public override string Texture => Mod.Name + "/Assets/Textures/Demoman/" + Name;
        public override void SetDefaults(){
            Item.shoot = ProjectileID.StickyGrenade;
            
            Item.useStyle = ItemUseStyleID.Shoot;
            WeaponData(8, 24, .6f, 1.09f, Sounds.stickybomblaunher_shoot);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback){
            if(!CanShoot(player)) return false;
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
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
            TF2Player p = player.GetModPlayer<TF2Player>();
            p.Loadout<GrenadeLauncher, StickyBombLauncher, Bottle, DemomanIdentifier>();


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
