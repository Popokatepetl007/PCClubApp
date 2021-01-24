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
        void ShopResultBuyProduct(bool succes);
    }

    public interface IRequestDelegateProfile
    {
        void ProfileResult(ProfileData profile);
    }

    public interface IRequestDelegateSuccessResult
    {
        void SuccessResult();
        void ErrorResult(string message);
    }

    public interface IRequestDelegateContentList
    {
        void ContentResult(List<GameUnit> listResult);
    }


    public interface ISocketChat
    {
        void MessageInput(int id);
    }

    public interface IRequestChat
    {
        void MessgateResult(ChatMessage chatMessage);
    }

    
}
