using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.Win32;

namespace XmlCommentUtility
{
    static class RegistryUtil
    {

        // 対応しているアプリケーション
        internal enum AppTypes
        {
            [Description("ArcMap")]
            DESKTOP,
            [Description("ArcGIS Engine")]
            ENGINE //,
            //[Description("ArcGIS Server")]
            //SERVER,
            //[Description("ArcGIS Pro")]
            //PRO
        }

        /// <summary>
        /// enum で定義した Description をもとに AppTypes の値を取得
        /// （今回は使わない）
        /// </summary>
        /// <param name="desc"></param>
        /// <returns></returns>
        static private int getApptypeByName(string desc)
        {
            foreach (AppTypes apptype in Enum.GetValues(typeof(AppTypes)))
            {

                var gm = apptype.GetType().GetMember(apptype.ToString());
                var attributes = gm[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                var description = ((DescriptionAttribute)attributes[0]).Description;

                if (description.Equals(desc))
                {
                    return (int)apptype;
                }

            }

            return -1;
        }

        /// <summary>
        /// 指定したアプリのInstallDirをレジストリに格納された情報から返す
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        static internal string GetInstallDir(AppTypes types) {

            const string INSTALLDIR = @"InstallDir";
            const string REALVERSION = @"RealVersion";

            string installDir = string.Empty;

            RegistryKey agskey = null;
            System.Object installKey = null;

            try
            {

                System.Object tempDesk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\ESRI\ArcGIS").GetValue(REALVERSION);
                string curVer = tempDesk.ToString().Substring(0, 4); //LocalMachineレジストリ検索用に4文字を返す(10.6.x ⇒ 10.6 )

                switch (types)
                {
                    case AppTypes.DESKTOP:
                        agskey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\ESRI\Desktop" + curVer); // 32bitのレジストリ（64bitではWow6432Node）
                        break;
                    case AppTypes.ENGINE:
                        agskey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\ESRI\Engine" + curVer); // 32bitのレジストリ（64bitではWow6432Node）
                        break;
                }

                installKey = agskey.GetValue(INSTALLDIR);
                installDir = installKey.ToString();

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                //MessageBox.Show(ex.StackTrace);
                throw;
            }
            finally
            {
                if (agskey != null)
                    agskey.Close();
            }

            return installDir;
        }


    }
}
