using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sagromin.Helpers
{
    public class ConstantHelpers
    {
        public static readonly int DEFAULT_PAGE_SIZE = 10;
        public static readonly int DEFAULT_PAGE_SIZE_MODAL = 10;
        public const String TEXTO_SELECCIONAR = "[-Seleccionar-]";


        public static class EXTENSION_REPORTE
        {
            public static readonly String PDF = "PDF";
            public static readonly String EXCEL = "XLS";
        }
        
        public static class AREAS
        {
            public const string ADMIN = "Admin";
            public const string CLIENT = "Client";
            public const string DRIVER = "Driver";
        }

        public static class LAYOUT
        {
            public static readonly String MODAL_LAYOUT_PATH = "~/Views/Shared/_ModalLayout.cshtml";
            public static readonly String MODAL_EMAIL_PATH = "~/Views/Shared/_MailLayout.cshtml";
            public static readonly String DEFAULT_LAYOUT_PATH = "~/Views/Shared/_Layout.cshtml";
            public static readonly String LOGIN_LAYOUT_PATH = "~/Views/Shared/_LoginLayout.cshtml";
        }

        public static class ROL
        {
            public const string ADMINISTRADOR = "ADM";
            public const string PRODUCT_MARKER = "ANL";
            public const string RESPONSABLE_COMERCIAL = "RCO";
            public const string VISOR_FIJA = "VIF";
            public const string MOBILE = "MOB";
            public const string EMPLEADO = "EMP";

            public static string GetNombreRol(string Estado)
            {
                switch (Estado)
                {
                    case ADMINISTRADOR: return "Administrador";
                    case PRODUCT_MARKER: return "Product Marker";
                    case RESPONSABLE_COMERCIAL: return "Responsable Comercial";
                    case VISOR_FIJA: return "Visor Fija";
                    case MOBILE: return "Visor Mobile";
                    case EMPLEADO: return "EMPLEADO";
                    default: return String.Empty;
                }
            }
        }

        public static class TIPORESERVA
        {
            public const string INDIVIDUAL = "IND";
            public const string MULTIPLAYER = "MUL";
            public const string EVENTO = "EVE";
            public const string MANTENIMIENTO = "MAN";            
        }

        public static class CODIGOFORMULARIO
        {
            public const string INDIVIDUAL = "REGUL";
            public const string EVENTO = "CUMPL";
            public const string MULTIPLAYER = "MULTI";
            public const string INACTIVO = "INACT";
        }

        public static class ESTADO
        {
            public const string ACTIVO = "ACT";
            public const string INACTIVO = "INA";

            public const string PREVENTA = "PRV";
            public const string CANCELADO = "CAN";

            public const string ENTREGADO = "NTR";
            public const string NOENTREGADO = "NEN";

            public const string ENTRADA = "ENT";
            public const string SALIDA = "EXI";


            public static string GetNameEstado(string Estado)
            {
                switch (Estado)
                {
                    case ACTIVO:
                        return "ACTIVO";
                    case INACTIVO:
                        return "INACTIVO";
                    case PREVENTA:
                        return "PRE-VENTA";
                    case CANCELADO:
                        return "CANCELADO";
                    case ENTREGADO:
                        return "ENTREGADO";
                    case NOENTREGADO:
                        return "NOENTREGADO";
                    case ENTRADA:
                        return "ENTRADA";
                    case SALIDA:
                        return "SALIDA";
                }

                return string.Empty;
            }
            public static string GetLabelEstado(string Estado)
            {
                switch (Estado)
                {
                    case ACTIVO:
                        return "<label class='badge badge-success'>ACTIVO</label>";
                    case INACTIVO:
                        return "<label class='badge badge-danger'>INACTIVO</label>";
                    case PREVENTA:
                        return "<label class='badge badge-warning'>PRE-VENTA</label>";
                    case CANCELADO:
                        return "<label class='badge badge-success'>CANCELADO</label>";
                    case ENTREGADO:
                        return "<label class='badge badge-success'>ENTREGADO</label>";
                    case NOENTREGADO:
                        return "<label class='badge badge-warning'>NO ENTREGADO</label>";
                    case ENTRADA:
                        return "<label class='badge badge-blue'>ENTRADA</label>";
                    case SALIDA:
                        return "<label class='badge badge-danger'>SALIDA</label>";
                }
                return string.Empty;
            }
        }
        
        public static PagedListRenderOptions Bootstrap3Pager
        {
            get
            {
                return new PagedListRenderOptions
                {
                    DisplayLinkToFirstPage = PagedListDisplayMode.IfNeeded,
                    DisplayLinkToLastPage = PagedListDisplayMode.IfNeeded,
                    DisplayLinkToPreviousPage = PagedListDisplayMode.IfNeeded,
                    DisplayLinkToNextPage = PagedListDisplayMode.IfNeeded,
                    DisplayLinkToIndividualPages = true,
                    DisplayPageCountAndCurrentLocation = false,
                    MaximumPageNumbersToDisplay = 10,
                    DisplayEllipsesWhenNotShowingAllPageNumbers = true,
                    EllipsesFormat = "&#8230;",
                    LinkToFirstPageFormat = "««",
                    LinkToPreviousPageFormat = "«",
                    LinkToIndividualPageFormat = "{0}",
                    LinkToNextPageFormat = "»",
                    LinkToLastPageFormat = "»»",
                    PageCountAndCurrentLocationFormat = "Page {0} of {1}.",
                    ItemSliceAndTotalFormat = "Showing items {0} through {1} of {2}.",
                    FunctionToDisplayEachPageNumber = null,
                    ClassToApplyToFirstListItemInPager = null,
                    ClassToApplyToLastListItemInPager = null,
                    ContainerDivClasses = new[] { "pagination-container" },
                    UlElementClasses = new[] { "pagination" },
                    LiElementClasses = new[] { "page-item" },
                };
            }
        }

        public static PagedListRenderOptions Bootstrap4Pager
        {
            get
            {
                PagedListRenderOptions PagedListOptions = new PagedListRenderOptions();
                PagedListOptions.LiElementClasses = new List<string> { "page-item" };
                PagedListOptions.UlElementClasses = new List<string> { "pagination", "pagination sm" };
                PagedListOptions.ContainerDivClasses = new List<string> { "nav text-xs-center" };
                PagedListOptions.FunctionToTransformEachPageLink = (liTag, aTag) => { aTag.Attributes.Add("class", "page-link"); liTag.InnerHtml = aTag.ToString(); return liTag; };
                PagedListOptions.LinkToPreviousPageFormat = "Anterior";
                PagedListOptions.LinkToNextPageFormat = "Siguiente";
                PagedListOptions.DisplayLinkToPreviousPage = PagedListDisplayMode.Always;
                PagedListOptions.DisplayLinkToNextPage = PagedListDisplayMode.Always;
                return PagedListOptions;
            }
            set
            {
                PagedListRenderOptions PagedListOptions = value;
            }
        }

        public static Decimal RedondeoBCR(Decimal Importe)
        {
            var stringMontoRedondeado = Convert.ToInt32((Importe) * 100).ToSafeString();
            var importeTotalLength = stringMontoRedondeado.Length;
            if (stringMontoRedondeado.LastOrDefault().ToInteger() < 5)
            {
                stringMontoRedondeado = stringMontoRedondeado.Substring(0, importeTotalLength - 1) + "0";
            }
            else
            {
                stringMontoRedondeado = stringMontoRedondeado.Substring(0, importeTotalLength - 1) + "5";
            }

            return (stringMontoRedondeado.ToDecimal() / 100);
        }
    }
}
