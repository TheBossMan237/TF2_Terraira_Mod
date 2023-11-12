using Terraria;
using Terraria.ModLoader;
namespace TF2.Buffs
{
    internal class HealEffect :  ModBuff
    {
        public override void SetStaticDefaults()
        {
            
            base.SetStaticDefaults();
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.Heal(1);
            base.Update(player, ref buffIndex);
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.life < npc.lifeMax) npc.life++;
            npc.HealEffect(1);
            base.Update(npc, ref buffIndex);
        }
    }
}
