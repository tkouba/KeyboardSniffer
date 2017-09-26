using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace KeyboardSniffer
{
    partial class DialogAbout : Form
    {
        public DialogAbout()
        {
            InitializeComponent();
            this.Text = String.Format("About {0}", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
            this.textBoxDescription.Text = AssemblyDescription;
        }

        #region Assembly Attribute Accessors

        private T GetAssemblyAttribute<T>() where T : Attribute
        {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(T), false);
            if (attributes.Length == 0)
                return default(T);

            return (T)attributes[0];
        }

        public string AssemblyTitle
        {
            get
            {
                return GetAssemblyAttribute<AssemblyTitleAttribute>()?.Title ?? System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return String.Format("{0} {1}",
                    Assembly.GetExecutingAssembly().GetName().Version,
                    GetAssemblyAttribute<AssemblyConfigurationAttribute>()?.Configuration);
            }
        }

        public string AssemblyDescription
        {
            get
            {
                return GetAssemblyAttribute<AssemblyDescriptionAttribute>()?.Description ?? String.Empty;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                return GetAssemblyAttribute<AssemblyProductAttribute>()?.Product ?? String.Empty;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                return GetAssemblyAttribute<AssemblyCopyrightAttribute>()?.Copyright ?? String.Empty;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                return GetAssemblyAttribute<AssemblyCompanyAttribute>()?.Company ?? String.Empty;
            }
        }
        #endregion
    }
}
