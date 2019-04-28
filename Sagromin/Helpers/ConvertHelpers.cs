using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Sagromin.Helpers
{
    public static class ConvertHelpers
    {
        private static readonly DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        public static String TostringUTF(this String val)
        {
            return Encoding.UTF8.GetString(Encoding.GetEncoding(1252).GetBytes(val ?? String.Empty));
        }
        public static String ToStringJavaScript(this String val)
        {
            return HttpUtility.JavaScriptStringEncode(val ?? String.Empty);
        }
        public static class SeparaPalabras
        {
            public enum TipoSeparacion
            {
                Defecto,
                Personalizado
            }
            /// <summary>
            /// Separa palabras, por defecto el separador será un espacio.
            /// </summary>
            /// <param name="palabras">grupo de palabras a separar</param>
            /// <returns></returns>
            public static String SepararPalabras(params String[] palabras)
            {
                return SepararPalabras(TipoSeparacion.Defecto, palabras);
            }
            /// <summary>
            /// Separa palabras, por defecto el separador será un espacio.
            /// </summary>
            /// <param name="tipoSeparacion">si el tipo de separación es Personalizado, entonces la primera palabra será el separador</param>
            /// <param name="palabras">grupo de palabras a separar</param>
            /// <returns></returns>
            public static String SepararPalabras(TipoSeparacion tipoSeparacion, params String[] palabras)
            {
                int posicion = 1;
                String separador = palabras[0] ?? String.Empty;
                if (tipoSeparacion == TipoSeparacion.Defecto)
                {
                    separador = " ";
                    posicion--;
                }
                String palabraSeparada = String.Empty;
                posicion++;
                if (palabras.Length > posicion)
                {
                    palabraSeparada = palabras[posicion];
                    for (int i = posicion; i < palabras.Length; i++)
                    {
                        palabraSeparada += separador + (palabras[i] ?? String.Empty);
                    }
                }
                return palabraSeparada;
            }
        }


        public static DateTime? ToFormatDateTime(this string val, string format = "ddMMyyyy")
        {
            try
            {
                DateTime timestamp;
                if (DateTime.TryParseExact(val, format, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out timestamp))
                {
                    return timestamp;
                }
            }
            catch (Exception)
            {

            }
            return null;
        }
        public static IDictionary<String, Object> ToObjectDictionary(this object val)
        {
            try
            {
                return val.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).ToDictionary(prop => prop.Name, prop => prop.GetValue(val, null));
            }
            catch (Exception)
            {
                return null;
                throw;
            }

        }
        public static string ToJson(this object val)
        {
            return JsonConvert.SerializeObject(val, Formatting.Indented, new JsonSerializerSettings() { PreserveReferencesHandling = PreserveReferencesHandling.None, ReferenceLoopHandling = ReferenceLoopHandling.Ignore, ObjectCreationHandling = ObjectCreationHandling.Reuse });
        }
        public static MvcHtmlString ToHTMLJson(this object val)
        {
            return MvcHtmlString.Create(val.ToJson());
        }

        public static String SafeTrim(this string val)
        {
            try
            {
                var result = String.Empty;
                if (!String.IsNullOrEmpty(val))
                {
                    val = val.Trim();
                }
            }
            catch (Exception)
            {

            }
            return val;
        }



        public static String ToDurationString(this Int32 val)
        {
            var hours = val / 60;
            var minutes = val % 60;
            var text = String.Empty;
            if (hours > 0)
                text += hours.ToString() + "h ";
            text += minutes.ToString("D2") + " m";
            return text;
        }

        public static Int32 ToInteger(this object val)
        {
            return ConvertHelpers.ToInteger(val, 0);
        }
        public static Int32? ToNullInteger(this object val)
        {
            if (val == null)
            {
                return null;
            }
            else
            {
                return ConvertHelpers.ToInteger(val, 0);
            }
        }

        public static String ToSafeString(this object val)
        {
            return (val ?? String.Empty).ToString();
        }

        public static Int32 ToInteger(this object val, Int32 def)
        {
            try
            {
                Int32 reval = 0;

                if (Int32.TryParse(val.ToString(), out reval))
                    return reval;
            }
            catch
            {
            }

            return def;
        }

        public static Decimal ToDecimal(this object val)
        {
            return ConvertHelpers.ToDecimal(val, 0);
        }

        public static Decimal ToDecimal(this object val, Decimal def)
        {
            try
            {
                Decimal reval = 0;

                if (Decimal.TryParse(val.ToString(), out reval))
                    return reval;
            }
            catch
            {
            }

            return def;
        }
        public static T ToEnum<T>(this String value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static long ToJsonDateTime(this DateTime val)
        {
            return ((long)(val - unixEpoch).TotalSeconds) * 1000;
        }

        public static String ToFullCallendarDate(this DateTime val)
        {
            return val.ToString("yyyy-MM-dd hh:mm:ss");
        }

        public static DateTime ToDateTime(this object val)
        {
            return ConvertHelpers.ToDateTime(val, DateTime.MinValue);
        }

        /// <summary>
        /// Convierte una fecha UTC a una fecha "Local". Se toma la hora de Perú (GMT-5)
        /// </summary>
        /// <param name="val">Fecha, en UTC</param>
        /// <returns>Fecha, en hora de Perú</returns>
        public static DateTime ToLocalDate(this DateTime val)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(val, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));
        }

        /// <summary>
        /// Convierte una fecha UTC a una fecha "Local". Se toma la hora de Perú (GMT-5)
        /// </summary>
        /// <param name="val">Fecha, en UTC</param>
        /// <returns>Fecha, en hora de Perú</returns>
        public static DateTime? ToLocalDate(this DateTime? val)
        {
            if (val.HasValue)
                return ToLocalDate(val.Value);
            else
                return val;
        }

        /// <summary>
        /// Convierte una fecha UTC a una string de una fecha "Local" con formato. Se toma la hora de Per� (GMT-5)
        /// </summary>
        /// <param name="val">Fecha, en UTC</param>
        /// <param name="format">Formato para el string</param>
        /// <returns>Fecha, en hora de Peru</returns>
        public static String ToLocalDateFormat(this DateTime val, String format)
        {
            return val.ToLocalDate().ToString(format, System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Convierte una fecha UTC a una string de una fecha "Local" con formato. Se toma la hora de Perú (GMT-5)
        /// </summary>
        /// <param name="val">Fecha, en UTC</param>
        /// <param name="format">Formato para el string</param>
        /// <returns>Fecha, en hora de Perú</returns>
        public static String ToLocalDateString(this DateTime val, String format)
        {
            return val.ToLocalDate().ToString(format);
        }

        /// <summary>
        /// Convierte una fecha UTC a una string de una fecha "Local" con formato. Se toma la hora de Perú (GMT-5)
        /// </summary>
        /// <param name="val">Fecha, en UTC</param>
        /// <param name="format">Formato para el string</param>
        /// <returns>Fecha, en hora de Perú</returns>
        public static String ToLocalDateString(this DateTime? val, String format)
        {
            if (val.HasValue)
                return ToLocalDateString(val.Value, format);
            else
                return String.Empty;
        }

        public static DateTime ToDateTime(this object val, DateTime def)
        {
            try
            {
                DateTime reval = DateTime.MinValue;

                if (DateTime.TryParse(val.ToString(), out reval))
                    return reval;
            }
            catch 
            {
            }

            return def;
        }

        public static Boolean IsBetween(this DateTime val, DateTime Start, DateTime End)
        {
            return val >= Start && val <= End;
        }

        public static Boolean ToBoolean(this object val)
        {
            return ConvertHelpers.ToBoolean(val, false);
        }

        public static Boolean ToBoolean(this object val, Boolean def)
        {
            try
            {
                Boolean reval = false;

                if (Boolean.TryParse(val.ToString(), out reval))
                    return reval;
            }
            catch 
            {
            }

            return def;
        }
    }

    public static class PeruDateTime
    {
        public static DateTime Now
        {
            get
            {
                return (TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time")));
            }

        }
    }
}