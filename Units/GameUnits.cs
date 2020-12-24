using System;
using System.Collections.Generic;
using System.Text;

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

        public static string App_path(GameUnits app)
        {
            switch (app)
            {
                case GameUnits.csgo:
                    return "csgo";
                case GameUnits.dota2:
                    return "Dota2";
                default:
                    return "another app";
            }
        }

    }

}
