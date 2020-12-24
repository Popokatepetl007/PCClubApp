using System;
using System.Collections.Generic;
using System.Text;


namespace PCClubApp
{
    public interface IRequestDelegate
    {
        void LoginResult(bool success);

        void ShopListResult<ShopUnit>(List<ShopUnit> shopList);
    }
}
