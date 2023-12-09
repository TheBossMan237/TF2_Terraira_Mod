using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TF2.Proj;
using TF2.Utills;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using TF2.Buffs;
using TF2.Assets;

namespace TF2.ClassItems
{
    public class SyringeGun : TF2Weapon
    {
        public override string Texture => $"{nameof(TF2)}/Assets/Textures/Medic/SyringeGun";

        public override void SetDefaults(){
            Item.scale = .75f;
            Item.shoot = ModContent.ProjectileType<SyringeProj>();
            WeaponData(40, 150, .105f, 1.305f, Sounds.syringegun_shoot);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (!CanShoot(player)) return false;
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

    }
    internal class MediGun : TF2Weapon{
        public NPC target;
        public bool isShooting = false;
        public float distance = 0f;
        public override string Texture => $"{nameof(TF2)}/Assets/Textures/Medic/MediGun";
        public override void SetDefaults(){
            Item.width = 54;
            Item.height = 20;
            Item.shoot = ModContent.ProjectileType<BlankBullet>();
            WeaponData(-1, -1, 0, -1, Sounds.medigun_heal);
        }
        public override void UpdateInventory(Player player){
            isShooting = player.ItemAnimationActive;
            if (!isShooting || distance > 500f) {
                target = null;
                distance = 0f;
            }
            base.UpdateInventory(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback){
            if (!CanShoot(player)) { return false; }
            //Main.mouseText - If the mouse is over some interactable entity/tile
            if (target == null && Main.mouseText) {
                for (int i = 0; i < Main.npc.Length; i++) {
                    if (Main.npc[i].getRect().Contains((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y)) {
                        target = Main.npc[i];
                        break;
                    }
                }
                
            } else if(target != null){

                Vector2 pos = (Vector2)player.HandPosition + velocity * 10;
                float spacing = .5f;
                distance = pos.Distance(target.position);
                for (float i = 0; i < distance; i+=spacing) {
                    Dust d = Main.dust[Dust.NewDust(pos, 1, 1, DustID.Stone)];
                    d.scale = .5f;
                    d.noGravity = true;
                    d.alpha = 100;
                    d.color = player.team == 1 ? Color.Blue : Color.Red;
                    pos = pos.MoveTowards(target.position, spacing);
                }


            }


            isShooting = true;
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone){

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
        public override bool? UseItem(Player player){


            Helper.Loadout<MediGun, SyringeGun, Bonesaw, MedicIdentifier>(
                player,
                Chesplate:ItemID.DrManFlyLabCoat
            );
            player.hair = 115;
            return base.UseItem(player);
        }
    }
    internal class MedicIdentifier : ModItem{
        public override string Texture => $"{nameof(TF2)}/Assets/Textures/Medic/MedicIdentifier";
        public override void SetDefaults(){
            

            Item.accessory = true;
        }
    }
    public class Bonesaw : TF2Weapon{
        public override string Texture => $"{nameof(TF2)}/Assets/Textures/Medic/Bonesaw";
        public override void SetDefaults(){

            MeleeWeapon(.8f);
        }
    }
}
