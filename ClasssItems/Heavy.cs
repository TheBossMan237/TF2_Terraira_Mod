using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using TF2.Utills;

namespace TF2.ClassItems
{
    public class HeavyIdentifier : ModItem
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Heavy/" + Name;
        public override void SetDefaults()
        {
            Item.accessory = true;
        }
    }
    internal class HeavyClassBag : ModItem
    {
        public override string Texture => $"{nameof(TF2)}/Assets/Textures/Heavy/HeavyClassBag";
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
            p.GiveItem<Minigun>(0);
            p.GiveItem<Shotgun>(1);
            p.GiveItem<Fists>(2);
            player.hair = 15;
            p.GiveEquipment(new Item(ItemID.FamiliarShirt), 1);
            p.GiveEquipment(new Item(ItemID.FamiliarPants), 2);
            p.GiveEquipment<HeavyIdentifier>();

            return base.UseItem(player);
        }
    }
    internal class Minigun : TF2Weapon
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Heavy/" + Name;
        public override void SetDefaults(){

            Item.shoot = ProjectileID.Bullet;
            Item.useStyle = ItemUseStyleID.Shoot;
            WeaponData(200, -1, 1.575f, -1);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if(!CanShoot()) return false;

            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
    internal class Fists : TF2Weapon
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Heavy/" + Name;
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Spear);
            WeaponData(-1, -1, 48, -1, true);
        }
    }
}
