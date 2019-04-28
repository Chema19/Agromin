using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Sagromin.Models;

namespace Sagromin.Helpers
{
    public enum AppRol
    {
        Administrador
    }

    public enum SessionKey
    {
        UserId,
        FullName,
        Rol,
        RolId,
        RolCompleto,
        Codigo,
        LocalId,
        Accesos,
        AplicacionId,
        Permisos,
        _appBP,
        LstRolUsuario,
        ViewPermisos,
    }

    public static class SessionHelpers
    {
        //public static List<String> ControladoresDisponibles { get; set; }
        // public static List<String> controladoresdelusuario { get; set; }

        #region Private

        private static object Get(HttpSessionState Session, String Key)
        {
            return Session[Key];
        }

        private static void Set(HttpSessionState Session, String Key, object Value)
        {
            Session[Key] = Value;
        }

        private static bool Exists(HttpSessionState Session, String Key)
        {
            return Session[Key] != null;
        }

        private static object Get(HttpSessionStateBase Session, String Key)
        {
            return Session[Key];
        }

        private static void Set(HttpSessionStateBase Session, String Key, object Value)
        {
            Session[Key] = Value;
        }

        private static bool Exists(HttpSessionStateBase Session, String Key)
        {
            return Session[Key] != null;
        }

        #endregion

        #region Getters setters GlobalKey
        //HttpSessionState
        public static object Get(this HttpSessionState Session, SessionKey Key)
        {
            return Get(Session, Key.ToString());
        }

        public static void Set(this HttpSessionState Session, SessionKey Key, object Value)
        {
            Set(Session, Key.ToString(), Value);
        }

        public static bool Exists(this HttpSessionState Session, SessionKey Key)
        {
            return Exists(Session, Key.ToString());
        }

        //HttpSessionStateBase
        public static object Get(this HttpSessionStateBase Session, SessionKey Key)
        {
            //try
            //{
            return Get(Session, Key.ToString());
            //}
            //catch(Exception)
            //{
            //    return null;
            //}
        }

        public static void Set(this HttpSessionStateBase Session, SessionKey Key, object Value)
        {
            Set(Session, Key.ToString(), Value);
        }

        public static bool Exists(this HttpSessionStateBase Session, SessionKey Key)
        {
            return Exists(Session, Key.ToString());
        }
        #endregion

        #region IsLoggedIn
        public static Boolean IsLoggedIn(this HttpSessionState Session)
        {
            return Get(Session, SessionKey.UserId) != null;
        }

        public static Boolean IsLoggedIn(this HttpSessionStateBase Session)
        {
            return Get(Session, SessionKey.UserId) != null;
        }
        #endregion

        #region GetPermisos
        public static string[] GetPermisos(this HttpSessionState Session)
        {
            return (string[])Get(Session, SessionKey.Permisos);
        }
        public static string[] GetPermisos(this HttpSessionStateBase Session)
        {
            return (string[])Get(Session, SessionKey.Permisos);
        }
        #endregion

        


       

        #region GetRolCompleto
        public static String GetRolCompleto(this HttpSessionState Session)
        {
            return (String)Get(Session, SessionKey.RolCompleto);
        }

        public static String GetRolCompleto(this HttpSessionStateBase Session)
        {
            return (String)Get(Session, SessionKey.RolCompleto);
        }
        #endregion

        #region Controllers



        #endregion

        #region Get_Full_Name

        public static String GetFullName(this HttpSessionState Session)
        {
            return (String)Get(Session, SessionKey.FullName);
        }

        public static String GetFullName(this HttpSessionStateBase Session)
        {
            return (String)Get(Session, SessionKey.FullName);
        }
        #endregion

        #region Get_UserId
        public static Int32 GetUserId(this HttpSessionState Session)
        {
            return Get(Session, SessionKey.UserId).ToInteger();
        }

        public static Int32 GetUserId(this HttpSessionStateBase Session)
        {
            return Get(Session, SessionKey.UserId).ToInteger();
        }
        #endregion

        #region Get_LocalId
        public static Int32 GetLocalId(this HttpSessionState Session)
        {
            return Get(Session, SessionKey.LocalId).ToInteger();
        }

        public static Int32 GetLocalId(this HttpSessionStateBase Session)
        {
            return Get(Session, SessionKey.LocalId).ToInteger();
        }
        #endregion



        #region Cookies
        //public static void SetCookie(this HttpSessionStateBase Session)
        //{
        //    try
        //    {
        //        var lstSessionKey = Session.Keys;
        //        var sessionKeyNames = Enum.GetNames(typeof(SessionKey)).ToList();
        //        var dictSessionObject = sessionKeyNames.ToDictionary(x => x, v => new object());

        //        foreach (var key in dictSessionObject.Keys.ToList())
        //            dictSessionObject[key] = null;

        //        for (int i = 0; i <= Session.Count - 1; i++)
        //        {
        //            var key = Session.Keys[i];
        //            if (sessionKeyNames.Any(x => x.Equals(key)))
        //            {
        //                if (key.Equals(SessionKey.Rol.ToString()))
        //                    dictSessionObject[key] = Session[key].ToString();
        //                else
        //                    dictSessionObject[key] = Session[key];
        //            }
        //        }

        //        var result = JsonConvert.SerializeObject(dictSessionObject, new JsonSerializerSettings()
        //        {
        //            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
        //            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        //            ObjectCreationHandling = ObjectCreationHandling.Reuse
        //        });

        //        var encriptacion = new Encriptacion();
        //        var resultEncriptado = encriptacion.Encriptar(result);

        //        CookieHelpers.Set(SessionKey._appBP, resultEncriptado);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public static void RestoreSessionFromCookie(this HttpSessionState Session)
        //{
        //    try
        //    {
        //        var encriptacion = new Encriptacion();
        //        var coockieValue = encriptacion.Desencriptar(CookieHelpers.GetValue(SessionKey._appBP));

        //        var dictSessionObject = new Dictionary<string, object>();
        //        dictSessionObject = JsonConvert.DeserializeObject<Dictionary<String, object>>(coockieValue);

        //        foreach (var item in dictSessionObject)
        //        {
        //            if (item.Value != null)
        //            {
        //                var sessionKey = item.Key.ToEnum<SessionKey>();
        //                if (item.Key.Equals(SessionKey.Rol.ToString()))
        //                    Set(Session, sessionKey, item.Value.ToString().ToEnum<AppRol>());
        //                else
        //                    Set(Session, sessionKey, item.Value);
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        CookieHelpers.DeleteAll();
        //    }
        //}
        #endregion
    }
}