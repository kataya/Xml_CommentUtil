using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace XmlCommentUtility
{
    class LocatorItem : System.Windows.Forms.ListViewItem
    {
        public string DisplayName { get; set; }

        // XMLファイルでコメントアウトされているかどうか
        public bool IsValid { get; set; }

        // Portal用のXML設定か、Server用のXML設定か
        public object LocatorType { get; internal set; }

        internal enum LocatorTypes {
            [Description("ArcGIS Server locator")]
            SERVER,
            [Description("Portal locator")]
            PORTAL
        }

    }
}
