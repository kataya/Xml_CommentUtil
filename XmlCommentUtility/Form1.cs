/***********************************************************************************
 
 [DefaultLocators.xml] での locator のXML要素をコメントアウトで無効化/有効化するツール


 参考: xpathのテストができるサイト:
  https://www.freeformatter.com/xpath-tester.html#ad-output

 1) Portal locator の xpathでの選択:
  !--Portal locator--
  <portal_locators>
    <display_name locator_name="World" display_name="World Geocode Service (ArcGIS Online)" />
  </portal_locators>

  xpathの例:
    1)xpath : "//portal_locators/display_name"
    result: <display_name locator_name="World" display_name="World Geocode Service (ArcGIS Online)" />

    2)xpath : "//portal_locators/display_name/@locator_name"
    result: locator_name="World"

    3)xpath : "//portal_locators/display_name/@display_name"
    result: display_name="World Geocode Service (ArcGIS Online)"
 
  2) Server locator の xpathでの選択:
  !--MGRS locator-- 
  <locator_ref>
    <name>MGRS</name>
    <display_name>MGRS(Military Grid Reference System)</display_name>
    <workspace_properties>
      <path>
      </path>
    </workspace_properties>
  </locator_ref>

  xpathの例:
    1)xpath : "//locator_ref/name"
    result: <name>MGRS</name>

    2)xpath : "//locator_ref/name[.='MGRS']"
    result: <name>MGRS</name>

    3)xpath : "//locator_ref/display_name[.='MGRS(Military Grid Reference System)']"
    result:  <display_name>MGRS(Military Grid Reference System)</display_name>   

***********************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Security.Permissions;

namespace XmlCommentUtility
{
    public partial class frmXmlCommetUtilityTool : Form
    {

        private const string LOCATOR_FILE = "DefaultLocators.xml";

        private string locatorfile = string.Empty;

        // 有効なロケーターと無効（Xmlでコメントアウト）されたロケーター
        private List<LocatorItem> ValidLocators = new List<LocatorItem>();
        private List<LocatorItem> InvalidLocators = new List<LocatorItem>();

        public frmXmlCommetUtilityTool()
        {
            InitializeComponent();

        }

        // タイトルバーがダブルクリックされた時に最大化されないようにする
        // https://dobon.net/vb/dotnet/form/preventmaximize.html
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            const int WM_NCLBUTTONDBLCLK = 0xA3;

            if (m.Msg == WM_NCLBUTTONDBLCLK)
            {
                //非クライアント領域がダブルクリックされた時
                m.Result = IntPtr.Zero;
                return;
            }

            base.WndProc(ref m);
        }

        /// <summary>
        /// 指定の DefaultLocators.xml ファイルから ListView に一覧表示
        /// </summary>
        private void LoadToListViews()
        {
 
            string locatorJaFile = locatorfile; //System.IO.Path.Combine(@"F:\Yourfolder\XmlCommentUtility", LOCATOR_FILE);
            if (!System.IO.File.Exists(locatorJaFile))
            {
                MessageBox.Show(this, string.Format("{0} が存在しません。", locatorJaFile));
                return;
            }


            // リストのクリア
            ValidLocators.Clear();
            InvalidLocators.Clear();
            listView1.Items.Clear();            
            //locatorfile = locatorJaFile;


            XmlDocument xmldoc = null;
            try
            {
                xmldoc = new XmlDocument();
                xmldoc.PreserveWhitespace = true;
                
                xmldoc.Load(locatorJaFile);

                // 有効なLocator定義のXML要素を読み込み
                ValidLocators = getValidLocatorsList(xmldoc);
                listView1.Items.AddRange(ValidLocators.ToArray());

                // コメントアウトされたLocator定義のXML要素を読み込み
                InvalidLocators = getCommentedLocatorsList(xmldoc);
                listView1.Items.AddRange(InvalidLocators.ToArray());

                listView1.Sorting = SortOrder.Ascending;

                //// テスト用のコード
                //// 既存の xmlノード をコメントアウトしたノードと置換
                //// World Geocode Service (ArcGIS Online) をコメントアウトへ変更
                //// 呼び出し方A:
                //bool result = changeToComment(xmldoc, "portal_locators");
                //// コメントアウトから元に戻す
                //if (!result)
                //{
                //    result = changeToElement(xmldoc, "portal_locators");
                //}
                //// MGRS(Military Grid Reference System) をコメントアウトへ変更
                //// 呼び出し方B：
                //bool result2 = changeToComment(xmldoc, "locator_ref/name[.='MGRS']",true);
                //if (!result2)
                //{
                //    result2 = changeToElement(xmldoc, "locator_ref", true, "MGRS");
                //}
                //xmldoc.Save(locatorJaFile);
 
            }
            catch (Exception ex)
            {
                string message = string.Format("{0} の読み込み中にエラーが発生しました。\n\n{1}", LOCATOR_FILE, ex.Message);
            }
            finally
            {
                if (xmldoc != null)
                    xmldoc = null;
            }
        }

        /// <summary>
        /// 指定の DefaultLocators.xml ファイルからコメントアウトされた 
        /// 'locator_ref' と 'portal_locators' の要素を カスタムListViewItem のコレクションに抽出
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private System.Collections.Generic.List<LocatorItem> getCommentedLocatorsList(XmlDocument doc)
        {
            List<LocatorItem> locators = new List<LocatorItem>();

            try
            {
                XmlNodeList locatorNodes = doc.SelectNodes("//comment()[contains(.,'<locator_ref>')]");
                foreach (XmlNode locatorNode in locatorNodes)
                {
                    XmlDocumentFragment docFrag = doc.CreateDocumentFragment();
                    docFrag.InnerXml = locatorNode.InnerText;

                    string name = docFrag.SelectSingleNode("//locator_ref/name").InnerText;
                    string dispname = docFrag.SelectSingleNode("//locator_ref/display_name").InnerText;
                    docFrag = null;

                    LocatorItem locator = createLocatorItem(name, dispname, false, LocatorItem.LocatorTypes.SERVER, false);
                    locators.Add(locator);
                }

                XmlNodeList portalNodes = doc.SelectNodes("//comment()[contains(.,'<portal_locators>')]");
                foreach (XmlNode portalNode in portalNodes)
                {
                    XmlDocumentFragment docFrag = doc.CreateDocumentFragment();
                    docFrag.InnerXml = portalNode.InnerText;

                    string name = docFrag.SelectSingleNode("//portal_locators/display_name/@locator_name").Value;
                    string dispname = docFrag.SelectSingleNode("//portal_locators/display_name/@display_name").Value;
                    docFrag = null;

                    LocatorItem locator = createLocatorItem(name, dispname, false, LocatorItem.LocatorTypes.PORTAL, false);
                    locators.Add(locator);
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return locators;
        }

        /// <summary>
        /// コメントアウトされた locator_ref ロケーター要素の name と display_name の値を返す
        /// xpath = //comment()[contains(.,'<locator_ref>')] 
        /// コメントアウトされた portal_locators ポータルロケータ name と display_name の値を返す
        /// xpath = //comment()[contains(.,'<portal_locators>')]
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private System.Collections.Generic.Dictionary<string, string> getCommentedLocators(XmlDocument doc)
        {
            Dictionary<string, string> commentedList = new Dictionary<string, string>();

            try {
                XmlNodeList locatorNodes = doc.SelectNodes("//comment()[contains(.,'<locator_ref>')]");
                foreach (XmlNode locatorNode in locatorNodes)
                {
                    XmlDocumentFragment docFrag = doc.CreateDocumentFragment();
                    docFrag.InnerXml = locatorNode.InnerText;
                    
                    string name = docFrag.SelectSingleNode("//name").InnerText;
                    string dispname = docFrag.SelectSingleNode("//display_name").InnerText;
                    commentedList.Add(name, dispname);
                    docFrag = null;

                }

                XmlNodeList portalNodes = doc.SelectNodes("//comment()[contains(.,'<portal_locators>')]");
                foreach (XmlNode portalNode in portalNodes)
                {
                    XmlDocumentFragment docFrag = doc.CreateDocumentFragment();
                    docFrag.InnerXml = portalNode.InnerText;

                    string name = docFrag.SelectSingleNode("//display_name/@locator_name").Value;
                    string dispname = docFrag.SelectSingleNode("//display_name/@display_name").Value;
                    commentedList.Add(name, dispname);
                    docFrag = null;

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return commentedList;
        }


        /// <summary>
        /// 有効な locator_ref ロケーター要素の name と display_name の値を返す
        /// xpath = //locator_ref 
        /// 有効な portal_locators ポータルロケータ name と display_name の値を返す
        /// xpath = //portal_locators
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private System.Collections.Generic.Dictionary<string, string> getValidLocators(XmlDocument doc)
        {

            Dictionary<string, string> validList = new Dictionary<string, string>();

            try {

                //<locator_ref>
                //  <name>MGRS</name>
                //  <display_name>MGRS(Military Grid Reference System)</display_name>
                //  <workspace_properties>
                //    <path>
                //    </path>
                //  </workspace_properties>
                //</locator_ref>
                XmlNodeList locatorNodes = doc.SelectNodes("//locator_ref");
                foreach (XmlNode locatorNode in locatorNodes)
                {
                    string name = locatorNode.SelectSingleNode("//name").InnerText;
                    string dispname = locatorNode.SelectSingleNode("//display_name").InnerText;
                    validList.Add(name, dispname);
                }
                
                // <portal_locators>
                //   <display_name locator_name="World" display_name="World Geocode Service (ArcGIS Online)" />
                // </portal_locators>
                XmlNodeList portalNodes = doc.SelectNodes("//portal_locators");
                foreach (XmlNode portalNode in portalNodes)
                {
                    string name = portalNode.SelectSingleNode("//display_name/@locator_name").Value;
                    string dispname = portalNode.SelectSingleNode("//display_name/@display_name").Value;
                    validList.Add(name, dispname);
                }


            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return validList;
        }

        /// <summary>
        /// カスタムのListViewItem を作成
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dispname"></param>
        /// <param name="IsChecked"></param>
        /// <param name="locType"></param>
        /// <param name="IsValid"></param>
        /// <returns></returns>
        private LocatorItem createLocatorItem(string name, string dispname, bool IsChecked, LocatorItem.LocatorTypes locType, bool IsValid) {

            LocatorItem locator = new LocatorItem();

            locator.Name = name;
            locator.Text = locator.Name;

            locator.DisplayName = dispname;
            locator.SubItems.Add(locator.DisplayName);

            locator.Checked = IsChecked;

            locator.LocatorType = locType;
            locator.SubItems.Add(locType.ToString());

            locator.IsValid = IsValid;
            if (locator.IsValid)
            {
                locator.SubItems.Add("有効");
            }
            else
            {
                locator.SubItems.Add("無効");
            }

            return locator;
        }

        /// <summary>
        /// 指定の DefaultLocators.xml ファイルから有効な 
        /// 'locator_ref' と 'portal_locators' の要素を カスタムListViewItem のコレクションに抽出
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private System.Collections.Generic.List<LocatorItem> getValidLocatorsList(XmlDocument doc)
        {
            List<LocatorItem> locators = new List<LocatorItem>();

            try
            {

                //<locator_ref>
                //  <name>MGRS</name>
                //  <display_name>MGRS(Military Grid Reference System)</display_name>
                //  <workspace_properties>
                //    <path>
                //    </path>
                //  </workspace_properties>
                //</locator_ref>
                XmlNodeList locatorNodes = doc.SelectNodes("//locator_ref");
                foreach (XmlNode locatorNode in locatorNodes)
                {
                    string name = locatorNode.SelectSingleNode("//locator_ref/name").InnerText;
                    string dispname = locatorNode.SelectSingleNode("//locator_ref/display_name").InnerText;


                    LocatorItem locator = createLocatorItem(name, dispname, true, LocatorItem.LocatorTypes.SERVER, true);
                    locators.Add(locator);
                }

                // <portal_locators>
                //   <display_name locator_name="World" display_name="World Geocode Service (ArcGIS Online)" />
                // </portal_locators>
                XmlNodeList portalNodes = doc.SelectNodes("//portal_locators");
                foreach (XmlNode portalNode in portalNodes)
                {
                    string name = portalNode.SelectSingleNode("//portal_locators/display_name/@locator_name").Value;
                    string dispname = portalNode.SelectSingleNode("//portal_locators/display_name/@display_name").Value;

                    LocatorItem locator = createLocatorItem(name, dispname, true, LocatorItem.LocatorTypes.PORTAL, true);
                    locators.Add(locator);
                }


            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }


            return locators;
        }


        /// <summary>
        /// XmlElementをコメントアウトに変更
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        private bool changeToComment(XmlDocument doc, string nodeName, bool nodeParent = false) {

                try
                {

                    // Xmlのノードを選択
                    string xpath = "//" + nodeName;
                    XmlNode selNode = doc.SelectSingleNode(xpath);

                    if (nodeParent) {
                        selNode = selNode.ParentNode;
                    }

                    // コメントノードを作成
                    string nodeContents = selNode.OuterXml;
                    XmlComment commentNode = doc.CreateComment(nodeContents);

                    // 選択したノードとコメントノードを置換
                    XmlNode parentNode = selNode.ParentNode;
                    parentNode.ReplaceChild(commentNode, selNode);

                    return true;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    return false;
                }

            }

        /// <summary>
        /// コメントアウトからXmlElementに変換
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        private bool changeToElement(XmlDocument doc, string nodeName, bool useLocatorName = false, string locatorName = "")
        {
            try
            {
                // コメントノードを選択
                string xpath = "//comment()[contains(.,'<" + nodeName + ">')]";
                if (useLocatorName)
                {
                    // //comment()[contains(.,'<name>MGRS')]
                    xpath = "//comment()[contains(.,'<name>" + locatorName + "')]"; 
                }
                XmlNode commentNode = doc.SelectSingleNode(xpath);

                // オリジナルのXmlのノードを作成
                string nodeContents = commentNode.InnerText;
                XmlDocumentFragment node = doc.CreateDocumentFragment();
                node.InnerXml = nodeContents;

                // コメントノードとオリジナルのノードを置換
                XmlNode parentNode = commentNode.ParentNode;
                parentNode.ReplaceChild(node, commentNode);

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
        }


        /// <summary>
        /// form の終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// ListView の状態で XMLのコメントアウトと、非コメントアウトを切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSettingLocatorsXml_Click(object sender, EventArgs e)
        {


            //// 既存の xmlノード をコメントアウトしたノードと置換
            //// World Geocode Service (ArcGIS Online) をコメントアウトへ変更
            //// 呼び出し方A:
            ////bool result = changeToComment(xmldoc, "portal_locators");

            ////// コメントアウトから元に戻す
            ////if (!result)
            ////{
            ////    result = changeToElement(xmldoc, "portal_locators");
            ////}

            //// MGRS(Military Grid Reference System) をコメントアウトへ変更
            //// 呼び出し方B：
            //bool result2 = changeToComment(xmldoc, "locator_ref/name[.='MGRS']", true);
            //if (!result2)
            //{
            //    result2 = changeToElement(xmldoc, "locator_ref", true, "MGRS");
            //}

            if (locatorfile == string.Empty) {
                MessageBox.Show(this, string.Format("{0} が指定されていません。", LOCATOR_FILE));
                return;
            }

            string locatorJaFile = locatorfile; 
            if (!System.IO.File.Exists(locatorJaFile))
            {
                MessageBox.Show(this, string.Format("{0} が存在しません。", locatorJaFile));
                return;
            }

            XmlDocument xmldoc = null;
            StringBuilder stb = null;

            // 設定を実行
            try
            {
                xmldoc = new XmlDocument();
                stb = new StringBuilder();
                xmldoc.Load(locatorfile);


                foreach (var item in listView1.Items)
                {
                    LocatorItem locitem = (LocatorItem)item;

                    if ((!locitem.Checked) && (locitem.IsValid))
                    {
                        System.Diagnostics.Debug.WriteLine(String.Format("ChangeToComment - name:{0}  dispname:{1}  locatortype:{2}", locitem.Name, locitem.SubItems[0].Text, locitem.LocatorType));
                        // ここでコメントアウトを呼び出し
                        // Call changeToComment
                        if (locitem.LocatorType is LocatorItem.LocatorTypes.SERVER) {
                            string nodename = string.Format("locator_ref/name[.='{0}']", locitem.Name);
                            changeToComment(xmldoc, nodename, true);
                        }
                        else if (locitem.LocatorType is LocatorItem.LocatorTypes.PORTAL) {
                            changeToComment(xmldoc, "portal_locators");
                        }
                        // コメントアウトしたので無効なリストへ移動
                        locitem.IsValid = false;
                        locitem.SubItems[3].Text = "無効";
                        // MessageBox用にメッセージを作成
                        string msg = string.Format("無効化 : {0} | {1}",locitem.Name, locitem.DisplayName);
                        if (stb.Length == 0) {
                            stb.Append(string.Format("{0} に対する設定変更：",LOCATOR_FILE));
                            stb.AppendLine();
                            stb.AppendLine();
                        }
                        stb.Append(msg);
                        stb.AppendLine();
                    }


                    // 無効なロケータで、選択状態でチェックオンされているものをXMLELEMENTに変更
                    if ((locitem.Checked) && (!locitem.IsValid))
                    {
                        System.Diagnostics.Debug.WriteLine(String.Format("ChangeToElement - name:{0}  dispname:{1}  locatortype:{2}", locitem.Name, locitem.SubItems[0].Text, locitem.LocatorType));
                        // ここでコメントアウトを呼び出し
                        // Call changeToElement
                        if (locitem.LocatorType is LocatorItem.LocatorTypes.SERVER)
                        {
                            changeToElement(xmldoc, "locator_ref", true, locitem.Name);
                        }
                        else if (locitem.LocatorType is LocatorItem.LocatorTypes.PORTAL)
                        {
                            changeToElement(xmldoc, "portal_locators");
                        }
                        locitem.IsValid = true;
                        locitem.SubItems[3].Text = "有効";
                        // MessageBox用にメッセージを作成
                        string msg = string.Format("有効化 : {0} | {1}", locitem.Name, locitem.DisplayName);
                        if (stb.Length == 0)
                        {
                            stb.Append(string.Format("{0} に対する設定変更：", LOCATOR_FILE));
                            stb.AppendLine();
                            stb.AppendLine();
                        }
                        stb.Append(msg);
                        stb.AppendLine();
                    }
                }

                xmldoc.Save(locatorfile);

                //MessageBox.Show(this, string.Format("{0} を設定しました", System.IO.Path.GetFileName(locatorfile)));
                if (stb.Length > 0)
                {
                    MessageBox.Show(this, stb.ToString());
                }
                else
                {
                    MessageBox.Show(this, "設定変更したものはありません");
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                if (xmldoc != null)
                {
                    xmldoc = null;
                }
                if (stb != null)
                {
                    stb = null;
                }
            }
        }

        /// <summary>
        /// ja フォルダ下の、DefaultLocators.xml の選択ダイアログ表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenLocatorsXml_Click(object sender, EventArgs e)
        {

            // Desktopのインストールディレクトリを取得
            string installDir = RegistryUtil.GetInstallDir(RegistryUtil.AppTypes.DESKTOP);
            string locatorDir = System.IO.Path.Combine(installDir, @"Locators\ja");
            //string locatorJaFile = System.IO.Path.Combine(locatorDir, LOCATOR_FILE);
            //MessageBox.Show(this, locatorJaFile);


            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "XML Document|*.xml";
            dlg.FileName = LOCATOR_FILE;
            dlg.InitialDirectory = locatorDir ;
            dlg.Title = string.Format("設定を変更する {0} ファイルを指定してください",LOCATOR_FILE);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                txtLocatorsXml.Text = dlg.FileName;
                locatorfile = txtLocatorsXml.Text;
                LoadToListViews();
            }

        }
    }
}
