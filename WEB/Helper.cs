using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.ComponentModel;
using System.Net;
using System.Configuration;

namespace Helper
{
    public class Helper
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<string> ObtenerValoresTime(int valorInicial, int valorFinal)
        {
            List<string> retorno = new List<string>();
            for (int i = valorInicial; i <= valorFinal; i++)
                retorno.Add(i.ToString().Length == 1 ? "0" + i : i.ToString());
            return retorno;
        }

        public static bool ValidarFile(string extension)
        {
            string valid = System.Configuration.ConfigurationManager.AppSettings["ValidFileTypes"];

            return valid.Split(';').Contains(extension);
        }
    }

    public class Permisos
    {

    }

    public enum PerfilSuspension
    {
        PERFIL_BASIC = 1,
        PERFIL_REGISTER = 5,
        PERFIL_ADMIN = 10
    }

    /// <summary>
    /// Finds all controls of type T stores them in FoundControls
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ControlFinder<T> where T : Control
    {
        private readonly List<T> _foundControls = new List<T>();
        public IEnumerable<T> FoundControls
        {
            get { return _foundControls; }
        }

        public void FindChildControlsRecursive(Control control)
        {
            foreach (Control childControl in control.Controls)
            {
                if (childControl.GetType() == typeof(T))
                {
                    _foundControls.Add((T)childControl);
                }
                else
                {
                    FindChildControlsRecursive(childControl);
                }
            }
        }
    }

    public class ReportAuthHelper : Microsoft.Reporting.WebForms.IReportServerCredentials
    {
        private string _UserName;
        private string _PassWord;
        private string _DomainName;

        public ReportAuthHelper(string UserName, string PassWord, string DomainName)
        {
            _UserName = UserName;
            _PassWord = PassWord;
            _DomainName = DomainName;
        }

        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {
            get { return null; }
        }

        public ICredentials NetworkCredentials
        {
            get { return new NetworkCredential(_UserName, _PassWord, _DomainName); }
        }

        public bool GetFormsCredentials(out Cookie authCookie, out string user,
         out string password, out string authority)
        {
            authCookie = null;
            user = password = authority = null;
            return false;
        }
    }
}