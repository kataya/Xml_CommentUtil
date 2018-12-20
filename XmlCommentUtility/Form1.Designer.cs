namespace XmlCommentUtility
{
    partial class frmXmlCommetUtilityTool
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmXmlCommetUtilityTool));
            this.btnOpenLocatorsXml = new System.Windows.Forms.Button();
            this.lblLocatorsXml = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSettingLocatorsXml = new System.Windows.Forms.Button();
            this.txtLocatorsXml = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblSettingInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOpenLocatorsXml
            // 
            this.btnOpenLocatorsXml.Location = new System.Drawing.Point(708, 22);
            this.btnOpenLocatorsXml.Name = "btnOpenLocatorsXml";
            this.btnOpenLocatorsXml.Size = new System.Drawing.Size(23, 23);
            this.btnOpenLocatorsXml.TabIndex = 3;
            this.btnOpenLocatorsXml.Text = "..";
            this.btnOpenLocatorsXml.UseVisualStyleBackColor = true;
            this.btnOpenLocatorsXml.Click += new System.EventHandler(this.btnOpenLocatorsXml_Click);
            // 
            // lblLocatorsXml
            // 
            this.lblLocatorsXml.AutoSize = true;
            this.lblLocatorsXml.Location = new System.Drawing.Point(10, 9);
            this.lblLocatorsXml.Name = "lblLocatorsXml";
            this.lblLocatorsXml.Size = new System.Drawing.Size(162, 12);
            this.lblLocatorsXml.TabIndex = 1;
            this.lblLocatorsXml.Text = "「DefualtLocators.xml」 ファイル：";
            // 
            // listView1
            // 
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listView1.Location = new System.Drawing.Point(8, 63);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(724, 170);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "名前";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "表示名";
            this.columnHeader2.Width = 300;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "種類";
            this.columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "有効/無効";
            this.columnHeader4.Width = 100;
            // 
            // btnSettingLocatorsXml
            // 
            this.btnSettingLocatorsXml.Location = new System.Drawing.Point(576, 239);
            this.btnSettingLocatorsXml.Name = "btnSettingLocatorsXml";
            this.btnSettingLocatorsXml.Size = new System.Drawing.Size(75, 23);
            this.btnSettingLocatorsXml.TabIndex = 6;
            this.btnSettingLocatorsXml.Text = "設定実行";
            this.btnSettingLocatorsXml.UseVisualStyleBackColor = true;
            this.btnSettingLocatorsXml.Click += new System.EventHandler(this.btnSettingLocatorsXml_Click);
            // 
            // txtLocatorsXml
            // 
            this.txtLocatorsXml.Location = new System.Drawing.Point(10, 24);
            this.txtLocatorsXml.Name = "txtLocatorsXml";
            this.txtLocatorsXml.ReadOnly = true;
            this.txtLocatorsXml.Size = new System.Drawing.Size(692, 19);
            this.txtLocatorsXml.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(657, 239);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblSettingInfo
            // 
            this.lblSettingInfo.AutoSize = true;
            this.lblSettingInfo.Location = new System.Drawing.Point(10, 239);
            this.lblSettingInfo.Name = "lblSettingInfo";
            this.lblSettingInfo.Size = new System.Drawing.Size(213, 12);
            this.lblSettingInfo.TabIndex = 5;
            this.lblSettingInfo.Text = "有効化する場合 ☑   無効化する場合 □：";
            // 
            // frmXmlCommetUtilityTool
            // 
            this.AcceptButton = this.btnSettingLocatorsXml;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(744, 270);
            this.Controls.Add(this.lblSettingInfo);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtLocatorsXml);
            this.Controls.Add(this.btnSettingLocatorsXml);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.lblLocatorsXml);
            this.Controls.Add(this.btnOpenLocatorsXml);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmXmlCommetUtilityTool";
            this.Text = "デフォルトロケーターの設定変更ツール";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenLocatorsXml;
        private System.Windows.Forms.Label lblLocatorsXml;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btnSettingLocatorsXml;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.TextBox txtLocatorsXml;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblSettingInfo;
    }
}

