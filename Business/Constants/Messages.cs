using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    // Magic stringlerden kurtulmak için geliştirildi.
    public static class Messages
    {
        public static string ProductAdded = "Urun basariyla eklendi.";
        public static string ProductDeleted= "Urun basariyla silindi.";
        public static string ProductUpdated = "Urun basariyla guncellendi.";

        public static string UserNotFound = "Kullanici bulunamadi.";
        public static string PasswordError = "Hatali sifre girilmistir.";
        public static string SuccessfulLogin = "Kullanici basariyla giris yapmistir.";
        public static string UserAlreadyExist = "Email kullanilmaktadir.";
        public static string UserRegistered = "Kullanici basariyla kaydedildi.";
        public static string AccessTokenCreated = "Access Token basariyla olusturuldu.";

        public static string AuthorizationDenied = "Yetkiniz yoktur.";
    }
}
