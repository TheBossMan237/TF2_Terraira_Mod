using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TF2.Proj;
using TF2.Utills;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using TF2.Buffs;


namespace TF2.ClassItems
{
    public class SyringeGun : TF2Weapon
    {
        public override string Texture => $"{nameof(TF2)}/Assets/Textures/Medic/SyringeGun";

        public override void SetDefaults(){
            Item.scale = .75f;
            Item.shoot = ModContent.ProjectileType<SyringeProj>();
            WeaponData(40, 150, .105f, 1.305f);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (!CanShoot()) return false;
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

    }
    internal class MediGun : TF2Weapon{
        public override string Texture => $"{nameof(TF2)}/Assets/Textures/Medic/MediGun";
        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 20;
            Item.shoot = ModContent.ProjectileType<BlankBullet>();
            WeaponData(-1, -1, 0, -1);


        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (!CanShoot()) { return false; }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {

            base.OnHitNPC(player, target, hit, damageDone);

        }


    }
    internal class MedicClassBag : ModItem
    {
        public override string Texture => $"{nameof(TF2)}/Assets/Textures/Medic/MedicClassBag";
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
            p.GiveItem<MediGun>(0);
            player.hair = 115;
            p.GiveItem<SyringeGun>(1);
            p.GiveItem<Bonesaw>(2);
            player.hair = 115;
            p.GiveEquipment(new Item(ItemID.DrManFlyLabCoat), 1);
            p.GiveEquipment(new Item(ItemID.FamiliarPants), 2);

            p.GiveEquipment<MedicIdentifier>();

            return base.UseItem(player);
        }
    }
    internal class MedicIdentifier : ModItem
    {
        public override string Texture => $"{nameof(TF2)}/Assets/Textures/Medic/MedicIdentifier";
        public override void SetDefaults()
        {
            Item.accessory = true;
        }
    }
    public class Bonesaw : ModItem
    {
        public override string Texture => $"{nameof(TF2)}/Assets/Textures/Medic/Bonesaw";
        // The Display Name and Tooltip of this item can be edited in the Localization/en-US_Mods.TF2.hjson file.
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
}
