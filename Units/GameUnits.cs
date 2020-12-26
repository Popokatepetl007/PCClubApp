using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Media.Imaging;

namespace PCClubApp
{
    public enum GameUnits
    {
        csgo,
        dota2,
        gtav
    }

    class GameEnumConversion
    {

        public static GameUnit AppUnitFromEnum(GameUnits app)
        {
            switch (app)
            {
                case GameUnits.csgo:
                    return new GameUnit("CsGo", "C:\\Program Files (x86)\\Steam\\Steam.exe", "https://i.pinimg.com/originals/db/72/21/db7221672fc6b447f7af2b1f61b140ef.png");
                case GameUnits.dota2:
                    return new GameUnit("Gota2", "Dota2", "https://w7.pngwing.com/pngs/361/42/png-transparent-dota-2-dota-2-league-of-legends-the-international-video-game-axe-logo-game-logo-international.png");
                default:
                    return new GameUnit("GtaV", "GtaV", "https://img.gta5-mods.com/q95/images/modded-cooler-gta-5-loading-screen-logo/3df0c9-2fe3a8abf83fdb39b09765ec00485e0c.png");
            }
        }

        public static string App_path(GameUnits app)
        {
            switch (app)
            {
                case GameUnits.csgo:
                    return "C:\\Program Files (x86)\\Steam\\Steam.exe";
                case GameUnits.dota2:
                    return "Dota2";
                default:
                    return "another app";
            }
        }

    }


    public class GameUnit
    {
        private string name;
        private string path;
        private string img;

        public GameUnit(string n, string p, string i)
        {
            name = n;
            path = p;
            img = i;
        }

        public string Name { get => name; }
        public string Path { get => path; }
        public string Img { get => img; }

        public BitmapImage ImgS { get => UIManager.BitmapImageFromUrl(img); }
    }

}
