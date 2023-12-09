using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using TF2.Proj;
using TF2.Utills;
using TF2.Assets;

namespace TF2.ClassItems
    
{
    public class MarketGardener : TF2Weapon {
        public override string Texture => Mod.Name + "/Assets/Textures/Soldier/" + Name;
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.CopperBroadsword);
            MeleeWeapon(.8f);
            base.SetDefaults();
        }
    }
    internal class Shovel : TF2Weapon
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Soldier/" + Name;
        public override void SetDefaults(){
            Item.CloneDefaults(ItemID.CopperBroadsword);
            MeleeWeapon(.8f);
            
        }
    }
    internal class SoldierClassBag : ModItem
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Soldier/" + Name;
        public override void SetDefaults()
        {
            Item.useStyle = 4;
            Item.consumable = true;
            Item.width = 32;
            Item.useTime = 1;
            Item.useAnimation = 1;
            Item.height = 32;
        }
        public override bool? UseItem(Player player)
        {
            Helper.Loadout<RocketLauncher, Shotgun, Shovel, SoldierIdentifier>(player);
            return base.UseItem(player);
        }
    }
    internal class SoldierIdentifier : ModItem
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Soldier/" + Name;
        public override void SetDefaults()
        {
            Item.accessory = true;
        }
    }
    internal class RocketLauncher : TF2Weapon
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Soldier/" + Name;
        public override void SetDefaults()
        {
            Item.shoot = ModContent.ProjectileType<Rocket>();
            Item.useStyle = ItemUseStyleID.Shoot;
            WeaponData(4, 20, .8f, .92f, Sounds.rocket_launcher_shoot);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if(!CanShoot(player)) return false;

            return base.Shoot(player, source, position, velocity, type, 1, 10);
        }
    }
}
