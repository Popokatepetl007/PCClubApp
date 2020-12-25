using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;


namespace PCClubApp
{
    public interface IRequestDelegateLogin
    {
        void LoginResult(bool success);
    }

    public interface IRequestDelegateShop
    {
        void ShopListResult<ShopUnit>(List<ShopUnit> shopList);
    }


    
}
