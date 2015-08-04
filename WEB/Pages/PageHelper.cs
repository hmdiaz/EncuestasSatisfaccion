using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Configuration;
using System.Net.NetworkInformation;
using System.Net;
//using Tickets.BLL;
//using MailSend;
using System.Net.Mail;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Diagnostics;
using System.Text.RegularExpressions;
using AztecaClientV2;


namespace Tickets.Layouts
{
    public class PageHelper : System.Web.UI.Page
    {

        #region Propiedades


        /// <summary>
        /// Formato de la fecha personalizado
        /// </summary>
        public string FormatDate
        {
            get
            {
                return "yyyy/MM/dd HH:mm:ss";
            }
        }

        /// <summary>
        /// Define el tamaño de los archivos configurado desde el web.config
        /// </summary>
        public int LenghtArchivo
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["LenghtArchivo"]);
            }
        }

        public int LenghtArchivo1
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["LenghtArchivo1"]);
            }
        }

        /// <summary>
        /// Define ruta de acceso cuando un usuario no tiene permisos. Configurado en el web.Config
        /// </summary>
        private string LoginFailed
        {
            get
            {
                return ConfigurationManager.AppSettings["LoginFailed"];
            }
        }

        /// <summary>
        /// Define el ID del Usuario Logeado.
        /// </summary>
        public int UsuarioAuditado
        {
            get
            {
                if (Session["SessionSecurity"] != null)
                {
                    return ((ResponseLogin)Session["SessionSecurity"]).UserID;
                }
                else
                {
                    Response.Redirect(LoginFailed);
                    return 0;
                }
            }
        }

        /// <summary>
        /// Captura la Ip del usuario que realiza la petición.
        /// </summary>
        public string UsuarioIp
        {
            get
            {
                return Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily.ToString().Equals("InterNetwork")).ToString();
            }
        }

        /// <summary>
        /// Define el login del usuario que ingreso a la aplicación.
        /// </summary>
        public string UsuarioLogin
        {
            get
            {
                if (Session["SessionSecurity"] != null)
                {
                    return ((ResponseLogin)Session["SessionSecurity"]).UserLogin;
                }
                else
                {
                    return "Login Failed";
                }
            }
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Metodo que verifica si el usuario tiene permisos para ingresar a una pagina.
        /// </summary>
        public void Seguridad()
        {
            if (Session["SessionSecurity"] == null)
            {
                string cad = Request["cadSeria"];
                if (cad != "" && cad != null)
                {
                    string vRet = Desencriptar(cad);
                    ResponseLogin Definicion = new LoginBLL().ResponseJson(vRet);
                    Session.Add("SessionSecurity", Definicion);
                }
            }

            if (Session["SessionSecurity"] != null)
            {
                string Sistema = ConfigurationManager.AppSettings["Sistema"];
                string cadena = HttpContext.Current.Request.Url.LocalPath;
                ResponseLogin Accesos = (ResponseLogin)Session["SessionSecurity"];
                string[] partes = cadena.Split('/');
                string Pantalla = partes[partes.Length - 1];

                if (Accesos.Systems.FirstOrDefault(s => s.sistema.Equals(Sistema)).pantallas.Count(m => m.pantalla.ToUpper() + ".ASPX" == Pantalla.ToUpper()) == 0)
                {
                    Response.Redirect(LoginFailed);
                }
                else
                {
                    ValidaControles(Accesos, Pantalla);
                }
            }
            else
            {
                Response.Redirect(LoginFailed);
            }
        }

        public static string CleanInput(string strIn)
        {
            return Regex.Replace(strIn, @"[^\w\.@-]", " ");
        }


        /// <summary>
        /// Valida las extenciones de los archivos
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public bool ValidaExtencion(string nombre)
        {
            string[] partes = nombre.ToLower().Split('.');
            string extencion = partes[partes.Length - 1];
            string TiposExtencion = ConfigurationManager.AppSettings["ValidFileTypes"];
            string[] GrupoExtenciones = TiposExtencion.Split(';');
            return GrupoExtenciones.Contains(extencion);
        }

        /// <summary>
        /// Valida los controles que tienen acceso en la aplicación
        /// </summary>
        public void ValidaControles(ResponseLogin Accesos, string Pantalla)
        {
            string NombreForm = String.Empty;
            string Content = ConfigurationManager.AppSettings["ContentPlaceHolder"];
            string Sistema = ConfigurationManager.AppSettings["Sistema"];

            List<Control> controles = this.Page.Controls.Cast<Control>().Flatten(c => c.Controls.Cast<Control>()).ToList();
            Pantalla pantalla = Accesos.Systems.FirstOrDefault(s => s.sistema.Equals(Sistema)).pantallas.FirstOrDefault(p => p.pantalla + ".ASPX" == Pantalla.ToUpper());

            if (pantalla != null)
            {
                foreach (string item in pantalla.recursos)
                {
                    Control control = controles.FirstOrDefault(c => c.ID != null && c.ID.ToString().ToUpper().Equals(item));
                    if (control is WebControl) (control as WebControl).Enabled = false;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="compressedText"></param>
        /// <returns></returns>
        public static string Desencriptar(string compressedText)
        {
            byte[] gzBuffer = Convert.FromBase64String(compressedText);
            using (MemoryStream ms = new MemoryStream())
            {
                int msgLength = BitConverter.ToInt32(gzBuffer, 0);
                int tam = gzBuffer.Length;
                tam -= 4;
                ms.Write(gzBuffer, 4, tam);

                byte[] buffer = new byte[msgLength];

                ms.Position = 0;
                using (GZipStream zip = new GZipStream(ms, CompressionMode.Decompress))
                {
                    zip.Read(buffer, 0, buffer.Length);
                }
                return Encoding.UTF8.GetString(buffer);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Encripta(string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            MemoryStream ms = new MemoryStream();
            using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true))
            {
                zip.Write(buffer, 0, buffer.Length);
            }
            ms.Position = 0;
            MemoryStream outStream = new MemoryStream();

            byte[] compressed = new byte[ms.Length];
            ms.Read(compressed, 0, compressed.Length);

            byte[] gzBuffer = new byte[compressed.Length + 4];
            System.Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
            System.Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);
            return Convert.ToBase64String(gzBuffer);
        }

        /// <summary>
        /// Metodo para envio de correos masivos.
        /// </summary>
        /// <param name="mails">Colección de correos</param>
        /// <param name="Asunto">Asucnto del Mail</param>
        /// <param name="Cuerpo">Cuerpo o mensaje del mensaje</param>
        //public void SendMailMasivos(List<MailAddress> MailPara, List<MailAddress> MailCopia, List<MailAddress> MailCopiaOculta, string Asunto, string Cuerpo)
        //{
        //    var smtp = ConfigurationManager.AppSettings["smtp"];
        //    if (String.IsNullOrEmpty(smtp))
        //    {
        //        throw new ArgumentNullException("ConnexionString",
        //                                        "Doesn't exist or it has an empty value in the configuration file");
        //    }

        //    var frommail = ConfigurationManager.AppSettings["frommail"];
        //    if (String.IsNullOrEmpty(frommail))
        //    {
        //        throw new ArgumentNullException("frommail",
        //                                        "Doesn't exist or it has an empty value in the configuration file");
        //    }

        //    var frommailpassword = ConfigurationManager.AppSettings["frommailpassword"];
        //    if (String.IsNullOrEmpty(frommailpassword))
        //    {
        //        throw new ArgumentNullException("frommailpassword",
        //                                        "Doesn't exist or it has an empty value in the configuration file");
        //    }

        //    var frommailalias = ConfigurationManager.AppSettings["frommailalias"];
        //    if (String.IsNullOrEmpty(frommailalias))
        //    {
        //        throw new ArgumentNullException("frommailalias",
        //                                        "Doesn't exist or it has an empty value in the configuration file");
        //    }

        //    bool bSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["ssl"]);
        //    var timemin = Convert.ToInt32(ConfigurationManager.AppSettings["timeoutmin"]);
        //    var timemax = Convert.ToInt32(ConfigurationManager.AppSettings["timeoutmax"]);
        //    MailManage mail = new MailManage(smtp, frommail, frommailpassword, frommailalias, bSSL);
        //    List<Attachment> attach = new List<Attachment>();
        //    try
        //    {
        //        mail.Send(MailPara, MailCopia, MailCopiaOculta, Asunto, Cuerpo, attach, 60000);
        //    }
        //    catch (Exception ex)
        //    {
        //        EventLog.WriteEntry(ConfigurationManager.AppSettings["Sistema"], string.Format("Error: {0}. StackTrace: {1}", ex.Message, ex.StackTrace), EventLogEntryType.Error);
        //    }
        //}

        /// <summary>
        /// Método para asignar de propiedades a un control Label.
        /// </summary>
        /// <param name="ControlText"></param>
        /// <returns></returns>
        public Label LoadLabel(string ControlText)
        {
            Label dynamiclabel = new Label();
            dynamiclabel.Text = ControlText;
            dynamiclabel.CssClass = "";
            return dynamiclabel;
        }

        public Label LoadLabelDescription(string ControlText)
        {
            Label dynamiclabel = new Label();
            dynamiclabel.Text = "<div class='AjustaDescripcionAccion'>" + ControlText + "</div>";
            dynamiclabel.CssClass = "";
            return dynamiclabel;
        }

        /// <summary>
        /// Método encargado de asignar propiedades a un control Texbox.
        /// </summary>
        /// <param name="ControlID"></param>
        /// <returns></returns>
        public TextBox LoadTexBox(string ControlID, string Text = "", bool Enabled = true)
        {
            TextBox dynamictextbox = new TextBox();
            dynamictextbox.Text = "";
            dynamictextbox.ID = ControlID;
            dynamictextbox.Width = 200;
            dynamictextbox.CssClass = "";
            dynamictextbox.Enabled = Enabled;
            if (Text != "")
            {
                dynamictextbox.Text = Text;
            }
            return dynamictextbox;
        }

        #endregion
    }

    public class LoginBLL
    {
        public ResponseLogin ResponseJson(string Cadena)
        {
            return JsonHelper.FromJson<ResponseLogin>(Cadena);
        }
    }

    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Flattens an object hierarchy.
        /// </summary>
        /// <param name="rootLevel">The root level in the hierarchy.</param>
        /// <param name="nextLevel">A function that returns the next level below a given item.</param>
        /// <returns><![CDATA[An IEnumerable<T> containing every item from every level in the hierarchy.]]></returns>
        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> rootLevel, Func<T, IEnumerable<T>> nextLevel)
        {
            List<T> accumulation = new List<T>();
            accumulation.AddRange(rootLevel);
            flattenLevel<T>(accumulation, rootLevel, nextLevel);
            return accumulation;
        }

        /// <summary>
        /// Recursive helper method that traverses a hierarchy, accumulating items along the way.
        /// </summary>
        /// <param name="accumulation">A collection in which to accumulate items.</param>
        /// <param name="currentLevel">The current level we are traversing.</param>
        /// <param name="nextLevel">A function that returns the next level below a given item.</param>
        private static void flattenLevel<T>(List<T> accumulation, IEnumerable<T> currentLevel, Func<T, IEnumerable<T>> nextLevel)
        {
            foreach (T item in currentLevel)
            {
                accumulation.AddRange(currentLevel);
                flattenLevel<T>(accumulation, nextLevel(item), nextLevel);
            }
        }
    }

    public class PageControl : System.Web.UI.UserControl
    {

        #region Propiedades

        /// <summary>
        /// Define ruta de acceso cuando un usuario no tiene permisos. Configurado en el web.Config
        /// </summary>
        private string LoginFailed
        {
            get
            {
                return ConfigurationManager.AppSettings["LoginFailed"];
            }
        }

        /// <summary>
        /// Define el ID del Usuario Logeado.
        /// </summary>
        public int UsuarioAuditado
        {
            get
            {
                if (Session["SessionSecurity"] != null)
                {
                    return ((ResponseLogin)Session["SessionSecurity"]).UserID;
                }
                else
                {
                    Response.Redirect(LoginFailed);
                    return 0;
                }
            }
        }

        /// <summary>
        /// Captura la Ip del usuario que realiza la petición.
        /// </summary>
        public string UsuarioIp
        {
            get
            {
                IPHostEntry host;
                string localIP = "";
                host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily.ToString() == "InterNetwork")
                    {
                        localIP = ip.ToString();
                    }
                }
                return localIP;
            }
        }


        /// <summary>
        /// Define el login del usuario que ingreso a la aplicación.
        /// </summary>
        public string UsuarioLogin
        {
            get
            {
                if (Session["SessionSecurity"] != null)
                {
                    return ((ResponseLogin)Session["SessionSecurity"]).UserLogin;
                }
                else
                {
                    Response.Redirect(LoginFailed);
                    return "Login Failed";
                }
            }
        }

        #endregion

    }
}