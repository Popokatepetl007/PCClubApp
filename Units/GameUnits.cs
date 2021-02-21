using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Windows.UI.Xaml.Media.Imaging;

namespace PCClubApp
{
    public enum GameUnits
    {
        csgo,
        dota2,
        gtav,
        chrome,
        opera,
        mazilla
    }

    public enum ContentCategory
    {
        GAME,
        APPLICATION
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
                case GameUnits.chrome:
                    return new GameUnit("Google Chrome", "steam://rungameid/730", "https://i.pinimg.com/originals/25/cb/12/25cb125f913f7af06ef4560d65fa75c7.png");
                case GameUnits.opera:
                    return new GameUnit("Opera", "steam://rungameid/730", "https://aux2.iconspalace.com/uploads/opera-ios7-icon-256.png");
                case GameUnits.mazilla:
                    return new GameUnit("FireFox", "", "https://d33wubrfki0l68.cloudfront.net/06185f059f69055733688518b798a0feb4c7f160/9f07a/images/product-identity-assets/firefox.png");
                default:
                    return new GameUnit("GtaV", "GtaV", "https://img.gta5-mods.com/q95/images/modded-cooler-gta-5-loading-screen-logo/3df0c9-2fe3a8abf83fdb39b09765ec00485e0c.png");
            }
        }

        public static string App_path(GameUnits app)
        {

            return GameEnumConversion.AppUnitFromEnum(app).Path;

            /*switch (app)
            {
                case GameUnits.csgo:
                    return "C:\\Program Files (x86)\\Steam\\Steam.exe";
                case GameUnits.dota2:
                    return "Dota2";
                default:
                    return "another app";
            }*/
        }

    }


    public class GameUnit
    {
        private string name;
        private string path;
        private string img;
        private int id;
        private ContentCategory category;

        public GameUnit(string n, string p, string i)
        {
            name = n;
            path = p;
            img = i;
        }

        public GameUnit(dynamic jItem)
        {
            name = jItem.name;
            path = jItem.path;
            id = jItem.id;
            img = String.Format("/desktop/content/picture/{0}", id);
            category = jItem.category == "GAME" ? ContentCategory.GAME : ContentCategory.APPLICATION;
            Trace.WriteLine(img);
            _ = UIManager.BitmapImageFromUrl(img);
        }

        public string Name { get => name; }
        public string Path { get => path; }
        public string Img { get => img; }

        public int Id { get => id; }
        public ContentCategory Category { get => category; }

        public BitmapImage ImgS { get => UIManager.BitmapImageFromUrl(img); }
    }

}
