using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;
namespace TF2.Assets
{
    public class Sounds
    {
        static private readonly string Path = $"{nameof(TF2)}/Assets/Sounds/";
        static public float volume = .25f;
        //Shoot - Ranged
        public static readonly SoundStyle shotgun_shoot = new SoundStyle(Path + "shotgun_shoot") { Volume = volume };
        public static readonly SoundStyle pistol_shoot = new SoundStyle(Path + "pistol_shoot") { Volume = volume };
        public static readonly SoundStyle rocket_launcher_shoot = new SoundStyle(Path + "rocket_shoot") { Volume = volume };
        public static readonly SoundStyle sniper_shoot = new SoundStyle(Path + "sniper_shoot") { Volume = 1f };
        public static readonly SoundStyle minigun_start = new SoundStyle(Path + "minigun_wind_up") { Volume = volume };
        public static readonly SoundStyle minigun_shoot = new SoundStyle(Path + "minigun_shoot") { Volume = volume };
        public static readonly SoundStyle minigun_spin = new SoundStyle(Path + "minigun_spin") { Volume = volume };
        public static readonly SoundStyle minigun_end = new SoundStyle(Path + "minigun_wind_down") { Volume = volume };
        public static readonly SoundStyle revolver_shoot = new SoundStyle(Path + "revolver_shoot") { Volume = volume };
        public static readonly SoundStyle stickybomblaunher_shoot = new SoundStyle(Path + "stickybomblauncher_shoot") { Volume = volume };
        public static readonly SoundStyle syringegun_shoot = new SoundStyle(Path + "syringegun_shoot") { Volume = volume };
        public static readonly SoundStyle grenadelauncher_shoot = new SoundStyle(Path + "grenade_launcher_shoot") { Volume = volume};
        public static readonly SoundStyle medigun_heal = new SoundStyle(Path + "medigun_heal") { Volume = volume };
        public static readonly SoundStyle medigun_no_target = new SoundStyle(Path + "medigun_no_target") { Volume = volume};
        public static readonly SoundStyle smg_shoot = new SoundStyle(Path + "smg_shoot") { Volume = volume };
        public static readonly SoundStyle flamethrower_shoot = new SoundStyle(Path + "flame_thrower_loop") { Volume = volume };
        //Shoot - Melee 
        public static readonly SoundStyle bat_hit = new SoundStyle(Path + "bat_hit") { Volume = volume };
        //Relaod
        public static readonly SoundStyle smg_clip_in = new SoundStyle(Path + "smg_clip_in") { Volume=volume};
        public static readonly SoundStyle smg_clip_out = new SoundStyle(Path + "smg_clip_out") { Volume=volume};

        //Draw Weapon

        //Projectiles Hit
        public static readonly SoundStyle rocket_explode = new SoundStyle(Path + "rocket_explode") { Volume = volume };


    }
}
