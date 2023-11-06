using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.ID;
namespace TF2.ClassItems {
    internal class Kukri : ModItem
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Sniper/" + Name;
        public override void SetDefaults()
        {
            Item.damage = 65;
            Item.DamageType = DamageClass.Melee;
            Item.width = 60;
            Item.height = 26;
            Item.useTime = 1;
            Item.useAnimation = 1;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = 10000;
            Item.rare = 2;
            Item.UseSound = SoundID.Item7;
            Item.autoReuse = true;
        }
    }
    internal class SniperClassBag : ModItem
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Sniper/" + Name;
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
            TF2Player p = player.GetModPlayer<TF2Player>();
            p.ClearHotbar();
            p.GiveItem<SniperRifle>(0);
            p.GiveItem<SMG>(1);
            p.GiveItem<Kukri>(2);
            p.GiveEquipment<SniperIdentifier>();

            return base.UseItem(player);
        }
    }
    internal class SniperIdentifier : ModItem
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Sniper/" + Name;
        public override void SetDefaults()
        {
            Item.accessory = true;
        }
    }
    public class SMG : ModItem
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Sniper/" + Name;


        public override void SetDefaults()
        {

            Item.scale = .5f;
            Item.damage = 14;
            Item.useTime = 1;
            Item.useAnimation = 1;
            Item.width = 54;
            Item.height = 20;
            Item.shoot = AmmoID.Bullet;
            Item.holdStyle = 0;
            Item.useStyle = ItemUseStyleID.Shoot;

            Item.shootSpeed = 5f;
            Item.noMelee = true;
            Item.autoReuse = true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            return base.Shoot(player, source, position, velocity, type, damage, knockback);

        }
    }
    internal class SniperRifle : ModItem
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Sniper/" + Name;
        public override void SetDefaults()
        {
            Item.width = 62;
            Item.height = 24;
            Item.shoot = ProjectileID.Bullet;
            Item.useAnimation = 1;
            Item.useTime = 1;
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shootSpeed = 5f;

        }
    }
}
