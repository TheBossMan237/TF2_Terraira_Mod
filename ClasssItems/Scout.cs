using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using TF2.Utills;
using TF2.Assets;

namespace TF2.ClassItems
{
    internal class Bat : TF2Weapon
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Scout/" + Name;
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.CopperBroadsword);
            Item.useStyle = ItemUseStyleID.Swing;
            MeleeWeapon(.5f);

            
            Item.autoReuse = true;
        }

    }
    internal class ScoutClassBag : ModItem
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Scout/" + Name;
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
            Helper.Loadout<ScatterShotgun, Pistol, Bat, ScoutIdentifier>(
                player,
                ItemID.GreenCap,
                ItemID.TreasureHunterShirt
            );
            return base.UseItem(player);
        }

    }
    internal class ScoutIdentifier : ModItem
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Scout/" + Name;
        public override void SetDefaults() {
            Item.accessory = true;
        }
    }

    internal class ScatterShotgun : TF2Weapon
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Scout/" + Name;
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 16;
            Item.shootSpeed = 5f;
            Item.shoot = ProjectileID.Bullet;
            WeaponData(6, 32, .625f, .7f, Sounds.shotgun_shoot);
            


        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (!CanShoot(player)) return false;
            const int NumBullets = 8;
            for (int i = 0; i < NumBullets; i++)
            {
                Vector2 vel = velocity.RotatedByRandom(MathHelper.ToRadians(15));
                vel *= 1f - Main.rand.NextFloat(.3f);
                Projectile.NewProjectileDirect(source, position, vel, type, damage, knockback);
            }

            return true;
        }
    }
}
