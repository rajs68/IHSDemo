
using IHSEnergy.Enerdeq.DataTemplateManager;
using IHSEnergy.Enerdeq.ExportBuilder;
using IHSEnergy.Enerdeq.GraphBuilder;
using IHSEnergy.Enerdeq.QueryBuilder;
using IHSEnergy.Enerdeq.QueryBuilder.Service;
using IHSEnergy.Enerdeq.ReportBuilder;
using IHSEnergy.Enerdeq.Session;
using log4net;
using log4net.Config;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using WebServicesTestClient.BusinessLayer;
using WebServicesTestClient.Controls;
using WebServicesTestClient.Properties;
namespace WebServicesTestClient.UI
{
    public class WebServicesTestClientForm : Form
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly string[] prodIDs = new string[]
		{
			"24207C091108",
			"24207C130346",
			"24207C163021"
		};
        private readonly string[] wellIDs = new string[]
		{
			"42399314300000",
			"42399344420001"
		};
        private ExportBuilder exportBuilder;
        private GraphBuilder graphBuilder;
        private QueryBuilder queryBuilder;
        private ReportBuilder reportBuilder;
        private WebServicesSession session;
        private DataTemplateManager dataTemplateManager;
        private bool initializingUI = false;
        private IContainer components = null;
        private Button GetIdsQueryButton;
        private Button AttributesQueryButton;
        private Button BuildExportButton;
        private Button BuildGraphButton;
        private Button BuildReportButton;
        private Button CountQueryButton;
        private Button DeleteGraphButton;
        private Button DeleteReportButton;
        private Button DeleteExportButton;
        private RadioButton ExportOverwriteAppendButton;
        private RadioButton ExportOverwriteFalseButton;
        private RadioButton ExportOverwriteTrueButton;
        private Button RetrieveExportButton;
        private RadioButton GraphOverwriteTrueButton;
        private Button GraphStatusButton;
        private RadioButton OverwriteTypeFalseButton;
        private RadioButton ReportOverwriteFalseButton;
        private RadioButton ReportOverwriteTrueButton;
        private Button ReportStatusButton;
        private Button RetrieveGraphButton;
        private Button RetrieveReportButton;
        private Button button1;
        private Button button2;
        private Button button3;
        private CheckBox ExportCompressCheckbox;
        private TabPage exportTab;
        private CheckBox GraphCompressCheckbox;
        private GroupBox groupBox1;
        private GroupBox groupBox11;
        private GroupBox groupBox12;
        private GroupBox groupBox13;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private GroupBox groupBox7;
        private GroupBox groupBox8;
        private GroupBox groupBox9;
        private Label lblAction;
        private Label lblExportJobID;
        private Label lblExportName;
        private Label lblExportOverwriteType;
        private Label lblGraphName;
        private Label lblGraphOverwriteType;
        private Label lblPassword;
        private Label lblQSXML;
        private Label lblReportJobID;
        private Label lblReportName;
        private Label lblReportOverwrite;
        private Label lblService;
        private Label lblUser;
        private TabPage queryTab;
        private CheckBox ReportCompressCheckbox;
        private TabPage reportTab;
        private TabControl tabcontrol1;
        private TabPage tabGraph;
        private TextBox ApplicationTextBox;
        private TextBox ExportNameTextBox;
        private TextBox GraphJobIdTextBox;
        private TextBox GraphNameTextBox;
        private TextBox PasswordTextBox;
        private TextBox QueryTextBox;
        private TextBox ReportNameTextBox;
        private Button ClearButton;
        private Button SessionButton;
        private TextBox UserTextBox;
        private ComboBox BaseUrlDropDownList;
        private Label label1;
        private TextBox ReportIdsTextBox;
        private TextBox ServiceUrlTextBox;
        private ComboBox ReportDatatypeDropDownList;
        private ComboBox ReportTemplateDropDownList;
        private Label label3;
        private Label label2;
        private ComboBox QueryDropDownList;
        private ComboBox ExportDatatypeDropDownList;
        private ComboBox ExportTemplateDropDownList;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox ExportIdsTextBox;
        private ComboBox GraphDatatypeDropDownList;
        private ComboBox GraphTemplateDropDownList;
        private Label label7;
        private Label label8;
        private Label label9;
        private TextBox GraphIdsTextBox;
        private Button BuildExportFromQueryButton;
        private ComboBox SavedQueryDropDownList;
        private Label label10;
        private TabPage tabChangeDelete;
        private GroupBox groupBox6;
        private TextBox ChangeDeleteIdsTextBox;
        private Label label13;
        private GroupBox groupBox10;
        private Label PagingDetailsLabel;
        private DateTimePicker ToDatePicker;
        private Label label12;
        private DateTimePicker FromDatePicker;
        private Label label11;
        private NumericUpDown PageTextBox;
        private Label label16;
        private Button GetChangeDeleteButton;
        private Button GetChangeDeleteFromIdsButton;
        private Button BuildGraphFromQueryButton;
        private TextBox ChangeDeleteResultsTextBox;
        private ComboBox GraphFormatDropDownList;
        private Label label14;
        private ComboBox TargetTypeDropDownList;
        private Label label15;
        private Button RefreshSavedQueriesButton;
        private GroupBox groupBox14;
        private GroupBox groupBox15;
        private Button button4;
        private TextBox ExportJobIdTextBox;
        private Label label18;
        private Button RefreshCompleteExportsListButton;
        private CheckBox LaunchExportFolderCheckbox;
        private GroupBox groupBox5;
        private Button LoadQueryButton;
        private Button SaveQueryButton;
        private Button DeleteQueryButton;
        private ToolTip toolTip1;
        private GroupBox groupBox16;
        private TextBox ResultsXmlTextBox;
        private Button RefreshQueriesButton;
        public Button BuildReportFromQueryButton;
        private Button ExportStatusButton;
        private GroupBox groupBox17;
        private GroupBox groupBox18;
        private Button button5;
        private Label label17;
        private Label label19;
        private CheckBox LaunchGraphFolderCheckbox;
        private Button RefreshCompleteGraphsListButton;
        private ComboBox CompletedGraphsComboBox;
        private GroupBox groupBox19;
        private GroupBox groupBox20;
        private Button button7;
        private TextBox ReportJobIdTextBox;
        private Label label20;
        private CheckBox LaunchReportFolderCheckbox;
        private Button RefreshCompleteReportsListButton;
        private ComboBox CompletedReportsComboBox;
        private TabPage onelineExportTab;
        private GroupBox groupBox21;
        private GroupBox groupBox22;
        private Button button6;
        private TextBox OnelineJobIdTextBox;
        private Button OnelineStatusButton;
        private Label label21;
        private GroupBox groupBox23;
        private CheckBox LaunchOnelineFolderCheckbox;
        private Button RefreshCompleteOnelinesListButton;
        private ComboBox CompletedOnelinesComboBox;
        private CheckBox OnelineCompressCheckbox;
        private GroupBox groupBox24;
        private Button button10;
        private Button DeleteOnelineButton;
        private Label label22;
        private Button RetrieveOnelineButton;
        private GroupBox groupBox25;
        private Button DeleteMe;
        private ComboBox OnelineDatatypeDropDownList;
        private Label label23;
        private Label label24;
        private Label label25;
        private TextBox OnelineIdsTextBox;
        private RadioButton OnelineOverwriteFalseButton;
        private RadioButton OnelineOverwriteTrueButton;
        private Label label26;
        private TextBox OnelineNameTextBox;
        private Label label27;
        private Button BuildOnelineButton;
        private TabPage dataTemplateTab;
        private Label label28;
        private ComboBox OnelineFormatTypeDropDownList;
        private GroupBox groupBox26;
        private Label label29;
        private Button LoadDataTemplateButton;
        private Button SaveDataTemplateButton;
        private Button DeleteDataTemplateButton;
        private ComboBox DataTemplateDropdownList;
        private Button RefreshDataTemplatesButton;
        private TextBox DataTemplateTextBox;
        private TabPage spatialExportTab;
        private RadioButton CustomTemplatesRadioButton;
        private RadioButton StandardTemplatesRadioButton;
        private RadioButton AllTemplatesRadioButton;
        private ComboBox DataTemplateDataTypeComboBox;
        private Label label32;
        private Label label31;
        private Label label33;
        private Label label34;
        private ComboBox CompletedExportsComboBox;
        private CheckBox ExMissingLLCheckbox;
        private Label versionLabel;
        private ComboBoxEx OnelineTemplateDropDownList;
        private Button BuildOnelineFromQueryButton;
        private GroupBox groupBox27;
        private GroupBox groupBox28;
        private Button button8;
        private TextBox SpatialExportJobIdTextBox;
        private Button SpatialExportStatusButton;
        private Label label30;
        private GroupBox groupBox29;
        private Label label35;
        private CheckBox LaunchSpatialExportFolderCheckBox;
        private Button RefreshCompleteSpatialExportListButton;
        private ComboBox CompletedSpatialExportsComboBox;
        private CheckBox SpatialExportCompressCheckBox;
        private GroupBox groupBox30;
        private Button button12;
        private Button DeleteSpatialExportButton;
        private Label label36;
        private Button RetrieveSpatialExportButton;
        private GroupBox groupBox31;
        private CheckedListBox LayersCheckedListBox;
        private Button button16;
        private Label label39;
        private Label label40;
        private RadioButton SpatialExportOverwriteFalseButton;
        private RadioButton SpatialExportOverwriteTrueButton;
        private Label label41;
        private TextBox SpatialExportNameTextBox;
        private Label label42;
        private WaterMarkTextBox MaxLongTextBox;
        private WaterMarkTextBox MaxLatTextBox;
        private WaterMarkTextBox MinLatitudeTextBox;
        private WaterMarkTextBox MinLongitudeTextBox;
        private Button DeselectAllLayersButton;
        private Button SelectAllLayersButton;
        private Button EntitlementsButton;
        public WebServicesTestClientForm()
        {
            this.InitializeComponent();
            XmlConfigurator.Configure(new System.IO.FileInfo(ConfigurationManager.AppSettings["Log4NetConfigFile"]));
        }
        public static string[] GetVersion()
        {
            System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
            System.Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            list.Add(string.Format("{0}.{1}.{2}", version.Major, version.Minor, version.Build));
            list.Add(version.Revision.ToString());
            return list.ToArray();
        }
        private void BuildReport(object sender, System.EventArgs e)
        {
            try
            {
                using (new CursorManager(this))
                {
                    string[] ids = this.GetIds(this.ReportDatatypeDropDownList.Text, this.ReportIdsTextBox.Lines);
                    bool overwriteFlag = !this.ReportOverwriteFalseButton.Checked;
                    string text = this.reportBuilder.Build("US", this.ReportDatatypeDropDownList.Text, this.ReportTemplateDropDownList.Text, ids, this.ReportNameTextBox.Text, overwriteFlag);
                    this.ReportJobIdTextBox.Text = text;
                }
                this.FlashControl(this.ReportJobIdTextBox);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(this, ex.Message, this.Text);
            }
        }
        private string[] GetIds(string datatype, System.Collections.Generic.IEnumerable<string> rawIds)
        {
            System.Collections.Generic.List<string> list = (
                from id in rawIds
                select id.Trim()).ToList<string>();
            string[] result;
            if (datatype == "Well" || datatype == "Activity Data")
            {
                result = ((list.Count == 0) ? this.wellIDs : list.ToArray());
            }
            else
            {
                if (datatype.StartsWith("Production"))
                {
                    result = ((list.Count == 0) ? this.prodIDs : list.ToArray());
                }
                else
                {
                    result = new string[0];
                }
            }
            return result;
        }
        private void GetReportStatus(object sender, System.EventArgs e)
        {
            string text = this.ReportJobIdTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(text))
            {
                string status = new ReportBuilderHelper(this.reportBuilder).GetStatus(text);
                this.LoadReportsList();
                MessageBox.Show(this, status, this.Text);
            }
        }
        private void DeleteReport(object sender, System.EventArgs e)
        {
            string text = this.CompletedReportsComboBox.Text;
            if (!string.IsNullOrEmpty(text))
            {
                using (new CursorManager(this))
                {
                    string text2 = this.reportBuilder.Delete(text) ? "Job '{0}' deleted." : "Unable to delete job '{0}'.";
                    text2 = string.Format(text2, text);
                    MessageBox.Show(this, text2, this.Text);
                }
                this.LoadReportsList();
            }
        }
        private void RetrieveReport(object sender, System.EventArgs e)
        {
            using (new CursorManager(this))
            {
                bool @checked = this.ReportCompressCheckbox.Checked;
                string text = ConfigurationManager.AppSettings["WorkingDirectory"] ?? "c:\\";
                text = System.IO.Path.Combine(text, "reports");
                try
                {
                    string text2 = this.CompletedReportsComboBox.Text;
                    byte[] bytes = this.reportBuilder.Retrieve(text2, @checked);
                    text2 = System.IO.Path.Combine(text, text2);
                    System.IO.Directory.CreateDirectory(text);
                    System.IO.File.WriteAllBytes(text2, bytes);
                    if (this.LaunchReportFolderCheckbox.Checked)
                    {
                        Process.Start("c:\\windows\\explorer.exe", text);
                    }
                    else
                    {
                        MessageBox.Show(this, string.Format("Report saved as '{0}'", text2), this.Text);
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(this, ex.Message, this.Text);
                }
            }
        }
        private void FormLoad(object sender, System.EventArgs e)
        {
            this.versionLabel.Text = string.Format("Version: {0}   Build: {1}", WebServicesTestClientForm.GetVersion());
            this.InitializeSessionControls();
        }
        private void InitializeSessionControls()
        {
            this.UserTextBox.Text = ConfigurationManager.AppSettings["Username"];
            this.PasswordTextBox.Text = ConfigurationManager.AppSettings["Password"];
            this.ServiceUrlTextBox.Text = ConfigurationManager.AppSettings["BaseUrl"];
            this.ApplicationTextBox.Text = ConfigurationManager.AppSettings["ApplicationId"];
            System.Collections.Generic.Dictionary<string, string> dictionary = new System.Collections.Generic.Dictionary<string, string>();
            string[] allKeys = ConfigurationManager.AppSettings.AllKeys;
            for (int i = 0; i < allKeys.Length; i++)
            {
                string text = allKeys[i];
                if (text.StartsWith("BaseUrl."))
                {
                    string value = ConfigurationManager.AppSettings[text];
                    string key = text.Replace("BaseUrl.", "");
                    dictionary.Add(key, value);
                }
            }
            this.BaseUrlDropDownList.DataSource = new BindingSource(dictionary, null);
            this.BaseUrlDropDownList.DisplayMember = "Key";
            this.BaseUrlDropDownList.ValueMember = "Value";
            this.BaseUrlDropDownList.SelectedIndex = this.BaseUrlDropDownList.Items.Count - 1;
        }
        private void InitializeQueryTab()
        {
            this.LoadQueryList();
            this.LoadSavedQueryList();
            this.LoadTargetTypeList();
        }
        private void LoadQueryList()
        {
            using (new CursorManager(this))
            {
                this.QueryDropDownList.Items.Clear();
                this.QueryDropDownList.DisplayMember = "Key";
                this.QueryDropDownList.ValueMember = "Value";
                this.QueryDropDownList.Items.Add(new System.Collections.Generic.KeyValuePair<string, string>("Select a query...", ""));
                this.QueryDropDownList.SelectedIndex = 0;
                string path = ConfigurationManager.AppSettings["QueryDirectory"] ?? "Queries";
                string[] files = System.IO.Directory.GetFiles(path);
                string[] array = files;
                for (int i = 0; i < array.Length; i++)
                {
                    string path2 = array[i];
                    string fullPath = System.IO.Path.GetFullPath(path2);
                    string fileName = System.IO.Path.GetFileName(fullPath);
                    this.QueryDropDownList.Items.Add(new System.Collections.Generic.KeyValuePair<string, string>(fileName, fullPath));
                }
            }
        }
        private void LoadSavedQueryList()
        {
            using (new CursorManager(this))
            {
                string[] savedQueries = this.queryBuilder.GetSavedQueries();
                System.Array.Sort<string>(savedQueries);
                this.SavedQueryDropDownList.Items.Add("Select a saved query...");
                this.PopulateDropDownList(this.SavedQueryDropDownList, savedQueries, true);
            }
        }
        private void LoadTargetTypeList()
        {
            this.TargetTypeDropDownList.Items.Clear();
            this.TargetTypeDropDownList.DisplayMember = "Key";
            this.TargetTypeDropDownList.ValueMember = "Value";
            this.TargetTypeDropDownList.Items.Add(new System.Collections.Generic.KeyValuePair<string, string>("None", null));
            this.TargetTypeDropDownList.SelectedIndex = 0;
            this.TargetTypeDropDownList.Items.Add(new System.Collections.Generic.KeyValuePair<string, string>("Well", "Well"));
            this.TargetTypeDropDownList.Items.Add(new System.Collections.Generic.KeyValuePair<string, string>("Unallocated", "Production Unallocated"));
            this.TargetTypeDropDownList.Items.Add(new System.Collections.Generic.KeyValuePair<string, string>("Allocated", "Production Allocated"));
            this.TargetTypeDropDownList.Items.Add(new System.Collections.Generic.KeyValuePair<string, string>("Activity", "Activity Data"));
        }
        private void InitializeReportTab()
        {
            this.ReportNameTextBox.Text = "Report1";
            if (this.reportBuilder != null)
            {
                string[] datatypes = this.reportBuilder.GetDatatypes("US");
                this.PopulateDropDownList(this.ReportDatatypeDropDownList, datatypes, false);
                this.LoadReportsList();
            }
        }
        private void InitializeExportTab()
        {
            this.ExportNameTextBox.Text = "Export1";
            if (this.exportBuilder != null)
            {
                string[] datatypes = this.exportBuilder.GetDatatypes("US");
                this.PopulateDropDownList(this.ExportDatatypeDropDownList, datatypes, false);
                this.LoadExportsList();
            }
        }
        private void InitializeSpatialExportTab()
        {
            this.SpatialExportNameTextBox.Text = "SpatialExport1";
            if (this.exportBuilder != null)
            {
                string[] layers = this.exportBuilder.GetLayers();
                System.Array.Sort<string>(layers);
                this.LayersCheckedListBox.Items.Clear();
                this.LayersCheckedListBox.Items.AddRange(layers);
                this.CheckLayers(true);
            }
        }
        private void CheckLayers(bool all)
        {
            for (int i = 0; i < this.LayersCheckedListBox.Items.Count; i++)
            {
                this.LayersCheckedListBox.SetItemChecked(i, all);
            }
        }
        private void InitializeGraphTab()
        {
            this.GraphNameTextBox.Text = "Graph1";
            if (this.graphBuilder != null)
            {
                string[] datatypes = this.graphBuilder.GetDatatypes("US");
                this.PopulateDropDownList(this.GraphDatatypeDropDownList, datatypes, false);
                string[] imageFormats = this.graphBuilder.GetImageFormats();
                this.PopulateDropDownList(this.GraphFormatDropDownList, imageFormats, false);
                this.LoadGraphsList();
            }
        }
        private void InitializeChangeDeleteTab()
        {
            string[] lines = new string[]
			{
				"42317711392010",
				"42317711742010 ",
				"42317711842010",
				"42323712972010",
				"42329711732010"
			};
            this.ChangeDeleteIdsTextBox.Lines = lines;
            System.DateTime value = System.DateTime.Today.AddDays((double)(-1 * System.DateTime.Today.Day)).AddDays(1.0).AddYears(-1);
            System.DateTime value2 = System.DateTime.Today.AddDays(-1.0);
            this.FromDatePicker.Value = value;
            this.ToDatePicker.Value = value2;
            this.PageTextBox.Value = 1m;
        }
        private void InitializeOnelineExportTab()
        {
            this.OnelineNameTextBox.Text = "OnelineExport1";
            if (this.exportBuilder != null)
            {
                string[] datatypes = this.exportBuilder.GetDatatypes("US");
                this.PopulateDropDownList(this.OnelineDatatypeDropDownList, datatypes, false);
                this.LoadOnelineExportsList();
                string[] fileTypes = this.dataTemplateManager.GetFileTypes();
                this.PopulateDropDownList(this.OnelineFormatTypeDropDownList, fileTypes, false);
            }
        }
        private void InitializeDataTemplateTab()
        {
            this.DataTemplateDataTypeComboBox.SelectedIndex = 0;
            this.LoadDataTemplateList();
        }
        private void LoadDataTemplateList()
        {
            using (new CursorManager(this))
            {
                System.Collections.Generic.List<string> templates = new DataTemplateHelper(this.dataTemplateManager).GetTemplates(this.DataTemplateDataTypeComboBox.Text, this.StandardTemplatesRadioButton.Checked, this.CustomTemplatesRadioButton.Checked);
                this.PopulateDropDownList(this.DataTemplateDropdownList, templates, false);
            }
        }
        private void Form_Closing(object sender, CancelEventArgs args)
        {
            using (new CursorManager(this))
            {
                if (sender == this)
                {
                    try
                    {
                        if (this.session != null)
                        {
                            this.session.Destroy();
                        }
                    }
                    catch (System.Exception exception)
                    {
                        WebServicesTestClientForm.Log.Info("Error while destroying session.", exception);
                    }
                }
            }
        }
        private void ExecuteAttributesQuery(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.QueryTextBox.Text.Trim()) && !this.QueryTextBox.Text.Trim().StartsWith("Error loading query"))
            {
                try
                {
                    this.ResultsXmlTextBox.Text = "Result of Attribute Query is ...";
                    this.Refresh();
                    string attributes = this.queryBuilder.GetAttributes(this.QueryTextBox.Text);
                    this.ResultsXmlTextBox.Text = attributes;
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(this, ex.Message, this.Text);
                }
            }
        }
        private void GetCount(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.QueryTextBox.Text.Trim()) && !this.QueryTextBox.Text.Trim().StartsWith("Error loading query"))
            {
                try
                {
                    using (new CursorManager(this))
                    {
                        this.ResultsXmlTextBox.Text = "Result of Count Query is ...";
                        this.Refresh();
                        int num = (((System.Collections.Generic.KeyValuePair<string, string>)this.TargetTypeDropDownList.SelectedItem).Value == null) ? this.queryBuilder.GetCount(this.QueryTextBox.Text) : this.queryBuilder.GetCount(this.QueryTextBox.Text, ((System.Collections.Generic.KeyValuePair<string, string>)this.TargetTypeDropDownList.SelectedItem).Value);
                        this.ResultsXmlTextBox.Text = num.ToString();
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(this, ex.Message, this.Text);
                }
            }
        }
        private void GetIds(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.QueryTextBox.Text.Trim()) && !this.QueryTextBox.Text.Trim().StartsWith("Error loading query"))
            {
                try
                {
                    using (new CursorManager(this))
                    {
                        this.ResultsXmlTextBox.Text = "Result of Keys Query is ...";
                        this.Refresh();
                        string[] array = (((System.Collections.Generic.KeyValuePair<string, string>)this.TargetTypeDropDownList.SelectedItem).Value == null) ? this.queryBuilder.GetKeys(this.QueryTextBox.Text) : this.queryBuilder.GetKeys(this.QueryTextBox.Text, ((System.Collections.Generic.KeyValuePair<string, string>)this.TargetTypeDropDownList.SelectedItem).Value);
                        this.ResultsXmlTextBox.Text = "";
                        string[] array2 = array;
                        for (int i = 0; i < array2.Length; i++)
                        {
                            string str = array2[i];
                            TextBox expr_EF = this.ResultsXmlTextBox;
                            expr_EF.Text = expr_EF.Text + str + System.Environment.NewLine;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(this, ex.Message, this.Text);
                }
            }
        }
        private void ClearQuery(object sender, System.EventArgs e)
        {
            using (new CursorManager(this))
            {
                this.QueryTextBox.Text = "";
                this.QueryTextBox.ForeColor = this.ResultsXmlTextBox.ForeColor;
                this.ResultsXmlTextBox.Text = "";
                this.QueryDropDownList.SelectedIndex = 0;
                this.TargetTypeDropDownList.SelectedIndex = 0;
            }
        }
        private string GetSelectedTemplate()
        {
            string text = this.ExportTemplateDropDownList.Text;
            if (this.ExMissingLLCheckbox.Checked)
            {
                string text2 = text;
                if (text2 != null)
                {
                    if (!(text2 == "297 Well (comma delimited)"))
                    {
                        if (!(text2 == "297 Well (fixed field)"))
                        {
                            if (text2 == "EnerdeqML 1.0 Well")
                            {
                                text = "<EXPORT><TEXTUAL_EXPORTS><WELL_XML EXCLUDE_MISSING_LATLONGS=\"true\" /></TEXTUAL_EXPORTS></EXPORT>";
                            }
                        }
                        else
                        {
                            text = "<EXPORT><TEXTUAL_EXPORTS><WELL_297 DELIMITER='FIXED_FIELD' EX_MISSING_LOCATIONS=\"true\" /></TEXTUAL_EXPORTS></EXPORT>";
                        }
                    }
                    else
                    {
                        text = "<EXPORT><TEXTUAL_EXPORTS><WELL_297 DELIMITER='COMMA' EX_MISSING_LOCATIONS=\"true\" /></TEXTUAL_EXPORTS></EXPORT>";
                    }
                }
            }
            return text;
        }
        private void BuildExport(object sender, System.EventArgs e)
        {
            try
            {
                using (new CursorManager(this))
                {
                    Overwrite overwriteType = WebServicesTestClientForm.GetOverwriteType(this.ExportOverwriteTrueButton, this.ExportOverwriteFalseButton);
                    string[] ids = this.GetIds(this.ExportDatatypeDropDownList.Text, this.ExportIdsTextBox.Lines);
                    string text = this.exportBuilder.Build("US", this.ExportDatatypeDropDownList.Text, this.GetSelectedTemplate(), ids, this.ExportNameTextBox.Text, overwriteType);
                    this.ExportJobIdTextBox.Text = text;
                }
                this.FlashControl(this.ExportJobIdTextBox);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(this, ex.Message, this.Text);
            }
        }
        private static Overwrite GetOverwriteType(RadioButton trueButton, RadioButton falseButton)
        {
            Overwrite result;
            if (trueButton.Checked)
            {
                result = Overwrite.True;
            }
            else
            {
                result = (falseButton.Checked ? Overwrite.False : Overwrite.Append);
            }
            return result;
        }
        private void GetExportStatus(object sender, System.EventArgs e)
        {
            string text = this.ExportJobIdTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(text))
            {
                string status = new ExportBuilderHelper(this.exportBuilder).GetStatus(text);
                this.LoadExportsList();
                MessageBox.Show(this, status, this.Text);
            }
        }
        private void DeleteExport(object sender, System.EventArgs e)
        {
            string text = this.CompletedExportsComboBox.Text;
            if (!string.IsNullOrEmpty(text))
            {
                using (new CursorManager(this))
                {
                    string text2 = this.exportBuilder.Delete(text) ? "Job '{0}' deleted." : "Unable to delete job '{0}'.";
                    text2 = string.Format(text2, text);
                    MessageBox.Show(this, text2, this.Text);
                }
                this.LoadExportsList();
            }
        }
        private void RetrieveExport(object sender, System.EventArgs e)
        {
            this.RetrieveExportFile(this.ExportCompressCheckbox.Checked, this.CompletedExportsComboBox.Text, this.LaunchExportFolderCheckbox.Checked);
        }
        private void DragEnterHandler(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = (e.Data.GetDataPresent(DataFormats.FileDrop, false) ? DragDropEffects.Copy : DragDropEffects.None);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(this, ex.Message, this.Text);
            }
        }
        private void DragDropHandler(object sender, DragEventArgs e)
        {
            try
            {
                string[] array = (string[])e.Data.GetData(DataFormats.FileDrop);
                int num = 0;
                if (num < array.Length)
                {
                    string path = array[num];
                    ((Control)sender).Text = System.IO.File.ReadAllText(path);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(this, ex.Message, this.Text);
            }
        }
        private void BuildGraph(object sender, System.EventArgs e)
        {
            try
            {
                using (new CursorManager(this))
                {
                    bool overwriteFlag = !this.ExportOverwriteFalseButton.Checked;
                    string[] ids = this.GetIds(this.GraphDatatypeDropDownList.Text, this.GraphIdsTextBox.Lines);
                    string[] array = this.graphBuilder.Build("US", this.GraphDatatypeDropDownList.Text, this.GraphTemplateDropDownList.Text, ids, 400, 400, this.GraphFormatDropDownList.Text, this.GraphNameTextBox.Text, overwriteFlag);
                    this.GraphJobIdTextBox.Text = array[0];
                }
                this.FlashControl(this.GraphJobIdTextBox);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(this, ex.Message, this.Text);
            }
        }
        private void RetrieveGraph(object sender, System.EventArgs e)
        {
            using (new CursorManager(this))
            {
                bool @checked = this.GraphCompressCheckbox.Checked;
                string text = ConfigurationManager.AppSettings["WorkingDirectory"] ?? "c:\\";
                text = System.IO.Path.Combine(text, "graphs");
                try
                {
                    string text2 = this.CompletedGraphsComboBox.Text;
                    byte[] bytes = this.graphBuilder.Retrieve(text2, @checked);
                    text2 = System.IO.Path.Combine(text, text2);
                    System.IO.Directory.CreateDirectory(text);
                    System.IO.File.WriteAllBytes(text2, bytes);
                    if (this.LaunchGraphFolderCheckbox.Checked)
                    {
                        Process.Start("c:\\windows\\explorer.exe", text);
                    }
                    else
                    {
                        MessageBox.Show(this, string.Format("Graph saved as '{0}'", text2));
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(this, ex.Message, this.Text);
                }
            }
        }
        private void SessionButtonClick(object sender, System.EventArgs e)
        {
            using (new CursorManager(this))
            {
                if (this.session == null)
                {
                    try
                    {
                        this.session = WebServicesSession.Create(this.ServiceUrlTextBox.Text, this.UserTextBox.Text, this.PasswordTextBox.Text, this.ApplicationTextBox.Text);
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(this, "Unable to connect (" + ex.Message + ")", this.Text);
                        return;
                    }
                    this.tabcontrol1.Enabled = true;
                    this.SessionButton.Text = "Disconnect";
                    this.EntitlementsButton.Enabled = true;
                    this.queryBuilder = new QueryBuilder(this.session);
                    this.reportBuilder = new ReportBuilder(this.session);
                    this.exportBuilder = new ExportBuilder(this.session);
                    this.graphBuilder = new GraphBuilder(this.session);
                    this.dataTemplateManager = new DataTemplateManager(this.session);
                    this.RefreshForm();
                }
                else
                {
                    this.queryBuilder = null;
                    this.reportBuilder = null;
                    this.exportBuilder = null;
                    this.graphBuilder = null;
                    this.dataTemplateManager = null;
                    try
                    {
                        this.session.Destroy();
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(this, "Error disconnecting (" + ex.Message + ")", this.Text);
                    }
                    this.session = null;
                    this.SessionButton.Text = "Connect";
                    this.EntitlementsButton.Enabled = false;
                    this.tabcontrol1.Enabled = false;
                }
            }
        }
        private void BaseUrlDropDownListSelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.ServiceUrlTextBox.Text = ((System.Collections.Generic.KeyValuePair<string, string>)this.BaseUrlDropDownList.SelectedItem).Value;
            if (this.session != null)
            {
                this.SessionButtonClick(null, null);
            }
        }
        private void RefreshForm()
        {
            this.initializingUI = true;
            if (this.session != null)
            {
                this.InitializeQueryTab();
                this.InitializeReportTab();
                this.InitializeExportTab();
                this.InitializeGraphTab();
                this.InitializeChangeDeleteTab();
                this.InitializeSpatialExportTab();
                this.InitializeOnelineExportTab();
                this.InitializeDataTemplateTab();
                this.initializingUI = false;
            }
        }
        private void QueryDropDownListSelectedIndexChanged(object sender, System.EventArgs e)
        {
            using (new CursorManager(this))
            {
                string value = ((System.Collections.Generic.KeyValuePair<string, string>)this.QueryDropDownList.SelectedItem).Value;
                this.QueryTextBox.ForeColor = this.ResultsXmlTextBox.ForeColor;
                this.QueryTextBox.Text = string.Empty;
                if (!string.IsNullOrEmpty(value))
                {
                    try
                    {
                        this.QueryTextBox.Text = Util.FormatXml(System.IO.File.ReadAllText(value));
                    }
                    catch (System.Exception ex)
                    {
                        this.QueryTextBox.ForeColor = Color.Red;
                        this.QueryTextBox.Text = string.Format("Error loading query '{0}'\r\n{1}", this.QueryDropDownList.Text, ex.Message);
                    }
                }
            }
        }
        private void ReportDatatypeDropDownListSelectedValueChanged(object sender, System.EventArgs e)
        {
            if (this.reportBuilder != null)
            {
                this.PopulateDropDownList(this.ReportTemplateDropDownList, this.reportBuilder.GetTemplates("US", this.ReportDatatypeDropDownList.Text), false);
            }
        }
        private void ExportDatatypeDropDownListSelectedValueChanged(object sender, System.EventArgs e)
        {
            if (this.exportBuilder != null)
            {
                this.PopulateDropDownList(this.ExportTemplateDropDownList, this.exportBuilder.GetTemplates("US", this.ExportDatatypeDropDownList.Text), false);
            }
        }
        private void GraphDatatypeDropDownListSelectedValueChanged(object sender, System.EventArgs e)
        {
            if (this.graphBuilder != null)
            {
                this.PopulateDropDownList(this.GraphTemplateDropDownList, this.graphBuilder.GetTemplates("US", this.GraphDatatypeDropDownList.Text), false);
            }
        }
        private void PopulateDropDownList(ComboBox list, System.Collections.Generic.IEnumerable<string> values, bool keepTitle = false)
        {
            using (new CursorManager(this))
            {
                string text = string.Empty;
                if (keepTitle && list.Items.Count > 0)
                {
                    text = list.Items[0].ToString();
                }
                list.Items.Clear();
                list.Text = string.Empty;
                if (keepTitle && !string.IsNullOrEmpty(text))
                {
                    list.Items.Add(text);
                }
                foreach (string current in values)
                {
                    list.Items.Add(current);
                }
                if (list.Items.Count > 0)
                {
                    list.SelectedIndex = 0;
                }
            }
        }
        private void GetGraphStatus(object sender, System.EventArgs e)
        {
            string text = this.GraphJobIdTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(text))
            {
                string status = new GraphBuilderHelper(this.graphBuilder).GetStatus(text);
                this.LoadGraphsList();
                MessageBox.Show(this, status, this.Text);
            }
        }
        private void DeleteGraph(object sender, System.EventArgs e)
        {
            string text = this.CompletedGraphsComboBox.Text;
            if (!string.IsNullOrEmpty(text))
            {
                using (new CursorManager(this))
                {
                    string text2 = this.graphBuilder.Delete(text) ? "Job '{0}' deleted." : "Unable to delete job '{0}'.";
                    text2 = string.Format(text2, text);
                    MessageBox.Show(this, text2, this.Text);
                }
                this.LoadGraphsList();
            }
        }
        public void KeyPressHandler(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\u0001')
            {
                ((TextBox)sender).SelectAll();
                e.Handled = true;
            }
        }
        private void SavedQueryDropDownListSelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.SavedQueryDropDownList.SelectedIndex != 0)
            {
                using (new CursorManager(this))
                {
                    this.QueryTextBox.ForeColor = this.ResultsXmlTextBox.ForeColor;
                    this.QueryTextBox.Text = this.SavedQueryDropDownList.Text;
                    this.Refresh();
                }
            }
        }
        private void LoadSavedQuery(object sender, System.EventArgs e)
        {
            this.LoadSavedQuery();
        }
        private void LoadSavedQuery()
        {
            if (this.SavedQueryDropDownList.SelectedIndex != 0)
            {
                using (new CursorManager(this))
                {
                    this.QueryTextBox.ForeColor = this.ResultsXmlTextBox.ForeColor;
                    this.QueryTextBox.Text = string.Empty;
                    this.Refresh();
                    try
                    {
                        string text = this.SavedQueryDropDownList.Text;
                        string queryDefinition = this.queryBuilder.GetQueryDefinition(text);
                        this.QueryTextBox.Text = Util.FormatXml(queryDefinition);
                    }
                    catch (System.Exception ex)
                    {
                        this.QueryTextBox.ForeColor = Color.Red;
                        this.QueryTextBox.Text = string.Format("Error loading query '{0}'\r\n{1}", this.SavedQueryDropDownList.Text, ex.Message);
                    }
                }
            }
        }
        private void GetChangeDeleteFromIdsButtonClick(object sender, System.EventArgs e)
        {
            using (new CursorManager(this))
            {
                try
                {
                    string[] lines = this.ChangeDeleteIdsTextBox.Lines;
                    if (lines.Length != 0)
                    {
                        for (int i = 0; i < lines.Length; i++)
                        {
                            lines[i] = lines[i].Trim();
                        }
                        PagingResponse pagingResponse;
                        string changesAndDeletesFromIds = this.queryBuilder.GetChangesAndDeletesFromIds("well", lines, this.FromDatePicker.Value.ToString("yyyy/MM/dd"), this.ToDatePicker.Value.ToString("yyyy/MM/dd"), out pagingResponse);
                        this.PagingDetailsLabel.Text = string.Format("Page: {0}  PageCount: {1}  Default: {2}  Pages: {3}  Total: {4}", new object[]
						{
							pagingResponse.Page,
							pagingResponse.PageCount,
							pagingResponse.DefaultPageSize,
							pagingResponse.Pages,
							pagingResponse.TotalCount
						});
                        this.ChangeDeleteResultsTextBox.ForeColor = this.ChangeDeleteIdsTextBox.ForeColor;
                        this.ChangeDeleteResultsTextBox.Text = Util.FormatXml(changesAndDeletesFromIds);
                    }
                }
                catch (System.Exception ex)
                {
                    this.ChangeDeleteResultsTextBox.ForeColor = Color.Red;
                    this.PagingDetailsLabel.Text = "Error occurred";
                    this.ChangeDeleteResultsTextBox.Text = ex.Message;
                }
            }
        }
        private void GetChangeDeleteButtonClick(object sender, System.EventArgs e)
        {
            using (new CursorManager(this))
            {
                try
                {
                    PagingResponse pagingResponse;
                    string changesAndDeletes = this.queryBuilder.GetChangesAndDeletes("well", this.FromDatePicker.Value.ToString("yyyy/MM/dd"), this.ToDatePicker.Value.ToString("yyyy/MM/dd"), int.Parse(this.PageTextBox.Text), out pagingResponse);
                    this.PagingDetailsLabel.Text = string.Format("Page: {0}  PageCount: {1}  Default: {2}  Pages: {3}  Total: {4}", new object[]
					{
						pagingResponse.Page,
						pagingResponse.PageCount,
						pagingResponse.DefaultPageSize,
						pagingResponse.Pages,
						pagingResponse.TotalCount
					});
                    this.ChangeDeleteResultsTextBox.ForeColor = this.ChangeDeleteIdsTextBox.ForeColor;
                    this.ChangeDeleteResultsTextBox.Text = Util.FormatXml(changesAndDeletes);
                }
                catch (System.Exception ex)
                {
                    this.ChangeDeleteResultsTextBox.ForeColor = Color.Red;
                    this.PagingDetailsLabel.Text = "Error occurred";
                    this.ChangeDeleteResultsTextBox.Text = ex.Message;
                }
            }
        }
        private void FlashControl(Control control)
        {
            this.FlashControl(control, Color.Orange, 100);
        }
        private void FlashControl(Control control, Color color, int delay)
        {
            Color backColor = control.BackColor;
            for (int i = 0; i < 5; i++)
            {
                control.BackColor = color;
                this.Refresh();
                System.Threading.Thread.Sleep(delay);
                control.BackColor = backColor;
                this.Refresh();
                System.Threading.Thread.Sleep(delay);
            }
        }
        private void BuildReportFromQuery(object sender, System.EventArgs e)
        {
            try
            {
                using (new CursorManager(this))
                {
                    bool overwriteType = !this.ReportOverwriteFalseButton.Checked;
                    string text = this.QueryTextBox.Text;
                    string text2 = this.reportBuilder.BuildFromQuery("US", this.ReportDatatypeDropDownList.Text, this.ReportTemplateDropDownList.Text, text, this.ReportNameTextBox.Text, overwriteType);
                    this.ReportJobIdTextBox.Text = text2;
                }
                this.FlashControl(this.ReportJobIdTextBox);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(this, ex.Message, this.Text);
            }
        }
        private void BuildGraphFromQuery(object sender, System.EventArgs e)
        {
            try
            {
                using (new CursorManager(this))
                {
                    bool overwriteFlag = !this.ExportOverwriteFalseButton.Checked;
                    string text = this.QueryTextBox.Text;
                    string[] array = this.graphBuilder.BuildFromQuery("US", this.GraphDatatypeDropDownList.Text, this.GraphTemplateDropDownList.Text, text, 400, 400, this.GraphFormatDropDownList.Text, this.GraphNameTextBox.Text, overwriteFlag);
                    this.GraphJobIdTextBox.Text = array[0];
                }
                this.FlashControl(this.GraphJobIdTextBox);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(this, ex.Message, this.Text);
            }
        }
        private void DeleteQuery(object sender, System.EventArgs e)
        {
            if (this.SavedQueryDropDownList.SelectedIndex != 0)
            {
                string text = this.SavedQueryDropDownList.Text;
                string text2 = string.Format("Are you sure you want to delete query '{0}'?", text);
                DialogResult dialogResult = MessageBox.Show(this, text2, "Delete Saved Query?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult != DialogResult.No)
                {
                    try
                    {
                        this.queryBuilder.DeleteQuery(text);
                        this.LoadSavedQueryList();
                    }
                    catch (System.Exception message)
                    {
                        WebServicesTestClientForm.Log.Error(message);
                        throw;
                    }
                }
            }
        }
        private void RefreshSavedQueries(object sender, System.EventArgs e)
        {
            this.LoadSavedQueryList();
        }
        private void RefreshExportsList(object sender, System.EventArgs e)
        {
            this.LoadExportsList();
        }
        private void RefreshGraphsList(object sender, System.EventArgs e)
        {
            this.LoadGraphsList();
        }
        private void RefreshReportsList(object sender, System.EventArgs e)
        {
            this.LoadReportsList();
        }
        private void LoadExportsList()
        {
            using (new CursorManager(this))
            {
                string[] array = this.exportBuilder.List();
                System.Array.Sort<string>(array);
                this.PopulateDropDownList(this.CompletedExportsComboBox, array, false);
            }
        }
        private void LoadOnelineExportsList()
        {
            using (new CursorManager(this))
            {
                string[] array = this.exportBuilder.List();
                System.Array.Sort<string>(array);
                this.PopulateDropDownList(this.CompletedOnelinesComboBox, array, false);
            }
        }
        private void LoadSpatialExportsList()
        {
            using (new CursorManager(this))
            {
                System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>(this.exportBuilder.List());
                System.Collections.Generic.List<string> list2 = list.FindAll((string s) => s.EndsWith(".zip"));
                list2.Sort();
                string[] values = list2.ToArray();
                this.PopulateDropDownList(this.CompletedSpatialExportsComboBox, values, false);
            }
        }
        private void LoadGraphsList()
        {
            using (new CursorManager(this))
            {
                string[] array = this.graphBuilder.List();
                System.Array.Sort<string>(array);
                this.PopulateDropDownList(this.CompletedGraphsComboBox, array, false);
            }
        }
        private void LoadReportsList()
        {
            using (new CursorManager(this))
            {
                string[] array = this.reportBuilder.List();
                System.Array.Sort<string>(array);
                this.PopulateDropDownList(this.CompletedReportsComboBox, array, false);
            }
        }
        private void SaveQuery(object sender, System.EventArgs e)
        {
            string text = Interaction.InputBox("Enter a query name.", this.Text, "MyQuery", -1, -1);
            text = text.Trim();
            string text2 = this.QueryTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(text) && text2.Contains("<criterias>"))
            {
                try
                {
                    string value = this.queryBuilder.SaveQuery(text, text2);
                    if (bool.Parse(value))
                    {
                        string text3 = string.Format("Query '{0}' successfully saved.", text);
                        MessageBox.Show(this, text3, this.Text);
                        this.LoadSavedQueryList();
                    }
                }
                catch (System.Exception message)
                {
                    WebServicesTestClientForm.Log.Error(message);
                    throw;
                }
            }
        }
        private void RefreshQueries(object sender, System.EventArgs e)
        {
            this.LoadQueryList();
        }
        private void DataTemplateParametersChanged(object sender, System.EventArgs e)
        {
            if (this.dataTemplateManager != null)
            {
                this.LoadDataTemplateList();
            }
        }
        private void LoadDataTemplate(object sender, System.EventArgs e)
        {
            if (this.dataTemplateManager != null)
            {
                try
                {
                    using (new CursorManager(this))
                    {
                        string templateDefinition = this.dataTemplateManager.GetTemplateDefinition(this.DataTemplateDropdownList.Text);
                        this.DataTemplateTextBox.Text = Util.FormatXml(templateDefinition);
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(this, ex.Message, this.Text);
                }
            }
        }
        private void SaveDataTemplate(object sender, System.EventArgs e)
        {
            if (this.dataTemplateManager != null)
            {
                if (!string.IsNullOrEmpty(this.DataTemplateTextBox.Text.Trim()))
                {
                    try
                    {
                        using (new CursorManager(this))
                        {
                            string value = this.dataTemplateManager.SaveTemplate(this.DataTemplateTextBox.Text);
                            if (bool.Parse(value))
                            {
                                string text = string.Format("Data Template successfully saved.", new object[0]);
                                MessageBox.Show(this, text, this.Text);
                                this.LoadDataTemplateList();
                            }
                        }
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(this, ex.Message, this.Text);
                    }
                }
            }
        }
        private void DeleteDataTemplate(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.DataTemplateDropdownList.Text))
            {
                string text = this.DataTemplateDropdownList.Text;
                string text2 = string.Format("Are you sure you want to delete data template '{0}'?", text);
                DialogResult dialogResult = MessageBox.Show(this, text2, "Delete Data Template?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult != DialogResult.No)
                {
                    try
                    {
                        this.dataTemplateManager.DeleteTemplate(text);
                        this.LoadDataTemplateList();
                    }
                    catch (System.Exception message)
                    {
                        WebServicesTestClientForm.Log.Error(message);
                        throw;
                    }
                }
            }
        }
        private void BuildOneline(object sender, System.EventArgs e)
        {
            try
            {
                using (new CursorManager(this))
                {
                    Overwrite overwriteType = WebServicesTestClientForm.GetOverwriteType(this.OnelineOverwriteTrueButton, this.OnelineOverwriteFalseButton);
                    string[] ids = this.GetIds(this.OnelineDatatypeDropDownList.Text, this.OnelineIdsTextBox.Lines);
                    string text = this.OnelineTemplateDropDownList.Text;
                    string text2 = this.exportBuilder.BuildOneline("US", this.OnelineDatatypeDropDownList.Text, ids, text, this.OnelineFormatTypeDropDownList.Text, this.OnelineNameTextBox.Text, overwriteType);
                    this.OnelineJobIdTextBox.Text = text2;
                }
                this.FlashControl(this.OnelineJobIdTextBox);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(this, ex.Message, this.Text);
            }
        }
        private void OnelineDatatypeDropDownListSelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.dataTemplateManager != null)
            {
                System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>
				{
					"-- Well Oneline Templates --"
				};
                list.AddRange(this.dataTemplateManager.GetTemplates("US", "Well"));
                list.Add("-- Production Oneline Templates --");
                list.AddRange(this.dataTemplateManager.GetTemplates("US", "Production"));
                this.PopulateDropDownList(this.OnelineTemplateDropDownList, list, false);
            }
        }
        private static string GetTemplateDatatype(string datatype)
        {
            string result;
            if (datatype.Contains("Production"))
            {
                result = "Production";
            }
            else
            {
                result = "Well";
            }
            return result;
        }
        private void GetOnelineStatus(object sender, System.EventArgs e)
        {
            string text = this.OnelineJobIdTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(text))
            {
                string status = new ExportBuilderHelper(this.exportBuilder).GetStatus(text);
                this.LoadOnelineExportsList();
                MessageBox.Show(this, status, this.Text);
            }
        }
        private void RefreshOnelinesList(object sender, System.EventArgs e)
        {
            this.LoadOnelineExportsList();
        }
        private void DeleteOneline(object sender, System.EventArgs e)
        {
            string text = this.CompletedOnelinesComboBox.Text;
            if (!string.IsNullOrEmpty(text))
            {
                using (new CursorManager(this))
                {
                    string text2 = this.exportBuilder.Delete(text) ? "Job '{0}' deleted." : "Unable to delete job '{0}'.";
                    text2 = string.Format(text2, text);
                    MessageBox.Show(this, text2, this.Text);
                }
                this.LoadOnelineExportsList();
            }
        }
        private void RetrieveOneline(object sender, System.EventArgs e)
        {
            this.RetrieveExportFile(this.OnelineCompressCheckbox.Checked, this.CompletedOnelinesComboBox.Text, this.LaunchOnelineFolderCheckbox.Checked);
        }
        private void RetrieveExportFile(bool compress, string filename, bool launch)
        {
            using (new CursorManager(this))
            {
                string text = ConfigurationManager.AppSettings["WorkingDirectory"] ?? "c:\\";
                text = System.IO.Path.Combine(text, "exports");
                try
                {
                    byte[] bytes = this.exportBuilder.Retrieve(filename, compress);
                    filename = System.IO.Path.Combine(text, filename);
                    System.IO.Directory.CreateDirectory(text);
                    System.IO.File.WriteAllBytes(filename, bytes);
                    if (launch)
                    {
                        Process.Start("c:\\windows\\explorer.exe", text);
                    }
                    else
                    {
                        MessageBox.Show(this, string.Format("Export saved as '{0}'", filename), this.Text);
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void ExportTemplateDropDownListSelectedIndexChanged(object sender, System.EventArgs e)
        {
            string text = this.ExportTemplateDropDownList.Text;
            if (text == "297 Well (fixed field)" || text == "297 Well (comma delimited)" || text == "EnerdeqML 1.0 Well")
            {
                this.ExMissingLLCheckbox.Enabled = true;
            }
            else
            {
                this.ExMissingLLCheckbox.Checked = false;
                this.ExMissingLLCheckbox.Enabled = false;
            }
        }
        private void BuildOnelineFromQuery(object sender, System.EventArgs e)
        {
            try
            {
                using (new CursorManager(this))
                {
                    Overwrite overwriteType = WebServicesTestClientForm.GetOverwriteType(this.OnelineOverwriteTrueButton, this.OnelineOverwriteFalseButton);
                    string text = this.OnelineTemplateDropDownList.Text;
                    string text2 = this.QueryTextBox.Text;
                    string text3 = this.exportBuilder.BuildOnelineFromQuery(text2, text, this.OnelineFormatTypeDropDownList.Text, this.OnelineNameTextBox.Text, overwriteType);
                    this.OnelineJobIdTextBox.Text = text3;
                }
                this.FlashControl(this.OnelineJobIdTextBox);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(this, ex.Message, this.Text);
            }
        }
        private void OnelineTemplateDropDownListSelectedIndexChanging(object sender, CancelEventArgs e)
        {
            e.Cancel = (((ComboBoxEx)sender).Text.StartsWith("--") && !this.initializingUI);
        }
        private void DeleteSpatialExport(object sender, System.EventArgs e)
        {
            string text = this.CompletedSpatialExportsComboBox.Text;
            if (!string.IsNullOrEmpty(text))
            {
                using (new CursorManager(this))
                {
                    string text2 = this.exportBuilder.Delete(text) ? "Job '{0}' deleted." : "Unable to delete job '{0}'.";
                    text2 = string.Format(text2, text);
                    MessageBox.Show(this, text2, this.Text);
                }
                this.LoadSpatialExportsList();
            }
        }
        private void RetrieveSpatialExport(object sender, System.EventArgs e)
        {
            this.RetrieveExportFile(this.SpatialExportCompressCheckBox.Checked, this.CompletedSpatialExportsComboBox.Text, this.LaunchSpatialExportFolderCheckBox.Checked);
        }
        private void RefreshSpatialExportList(object sender, System.EventArgs e)
        {
            this.LoadSpatialExportsList();
        }
        private void GetSpatialExportStatus(object sender, System.EventArgs e)
        {
            string text = this.SpatialExportJobIdTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(text))
            {
                string status = new ExportBuilderHelper(this.exportBuilder).GetStatus(text);
                this.LoadSpatialExportsList();
                MessageBox.Show(this, status, this.Text);
            }
        }
        private void BuildSpatialExport(object sender, System.EventArgs e)
        {
            try
            {
                using (new CursorManager(this))
                {
                    System.Collections.Generic.List<string> list = (
                        from object item in this.LayersCheckedListBox.CheckedItems
                        select item.ToString()).ToList<string>();
                    string text = this.exportBuilder.BuildSpatial(this.GetDouble(this.MinLatitudeTextBox.Text, -90.0), this.GetDouble(this.MaxLatTextBox.Text, 90.0), this.GetDouble(this.MinLongitudeTextBox.Text, -180.0), this.GetDouble(this.MaxLongTextBox.Text, 180.0), list.ToArray(), this.SpatialExportNameTextBox.Text, this.SpatialExportOverwriteTrueButton.Checked);
                    this.SpatialExportJobIdTextBox.Text = text;
                }
                this.FlashControl(this.SpatialExportJobIdTextBox);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(this, ex.Message, this.Text);
            }
        }
        private double GetDouble(string text, double defaultValue)
        {
            double num;
            return double.TryParse(text, out num) ? num : defaultValue;
        }
        private void SelectAllLayers(object sender, System.EventArgs e)
        {
            this.CheckLayers(true);
        }
        private void DeselectAllLayers(object sender, System.EventArgs e)
        {
            this.CheckLayers(false);
        }
        private void versionLabel_Click(object sender, System.EventArgs e)
        {
        }
        private void EntitlementsButton_Click(object sender, System.EventArgs e)
        {
            string entitlements = this.session.GetEntitlements();
            MessageBox.Show(entitlements);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(WebServicesTestClientForm));
            this.tabcontrol1 = new TabControl();
            this.queryTab = new TabPage();
            this.groupBox5 = new GroupBox();
            this.RefreshQueriesButton = new Button();
            this.LoadQueryButton = new Button();
            this.label15 = new Label();
            this.ClearButton = new Button();
            this.SaveQueryButton = new Button();
            this.AttributesQueryButton = new Button();
            this.GetIdsQueryButton = new Button();
            this.DeleteQueryButton = new Button();
            this.CountQueryButton = new Button();
            this.TargetTypeDropDownList = new ComboBox();
            this.QueryDropDownList = new ComboBox();
            this.lblQSXML = new Label();
            this.label10 = new Label();
            this.SavedQueryDropDownList = new ComboBox();
            this.RefreshSavedQueriesButton = new Button();
            this.QueryTextBox = new TextBox();
            this.groupBox16 = new GroupBox();
            this.ResultsXmlTextBox = new TextBox();
            this.tabChangeDelete = new TabPage();
            this.groupBox10 = new GroupBox();
            this.ChangeDeleteResultsTextBox = new TextBox();
            this.PagingDetailsLabel = new Label();
            this.groupBox6 = new GroupBox();
            this.GetChangeDeleteFromIdsButton = new Button();
            this.PageTextBox = new NumericUpDown();
            this.ToDatePicker = new DateTimePicker();
            this.label16 = new Label();
            this.label12 = new Label();
            this.GetChangeDeleteButton = new Button();
            this.FromDatePicker = new DateTimePicker();
            this.label11 = new Label();
            this.ChangeDeleteIdsTextBox = new TextBox();
            this.label13 = new Label();
            this.exportTab = new TabPage();
            this.groupBox14 = new GroupBox();
            this.groupBox15 = new GroupBox();
            this.button4 = new Button();
            this.ExportJobIdTextBox = new TextBox();
            this.ExportStatusButton = new Button();
            this.label18 = new Label();
            this.groupBox8 = new GroupBox();
            this.CompletedExportsComboBox = new ComboBox();
            this.label33 = new Label();
            this.LaunchExportFolderCheckbox = new CheckBox();
            this.RefreshCompleteExportsListButton = new Button();
            this.ExportCompressCheckbox = new CheckBox();
            this.groupBox9 = new GroupBox();
            this.button3 = new Button();
            this.DeleteExportButton = new Button();
            this.lblExportJobID = new Label();
            this.RetrieveExportButton = new Button();
            this.groupBox7 = new GroupBox();
            this.ExMissingLLCheckbox = new CheckBox();
            this.BuildExportFromQueryButton = new Button();
            this.ExportDatatypeDropDownList = new ComboBox();
            this.ExportTemplateDropDownList = new ComboBox();
            this.label4 = new Label();
            this.label5 = new Label();
            this.label6 = new Label();
            this.ExportIdsTextBox = new TextBox();
            this.ExportOverwriteAppendButton = new RadioButton();
            this.ExportOverwriteFalseButton = new RadioButton();
            this.ExportOverwriteTrueButton = new RadioButton();
            this.lblExportOverwriteType = new Label();
            this.ExportNameTextBox = new TextBox();
            this.lblExportName = new Label();
            this.BuildExportButton = new Button();
            this.spatialExportTab = new TabPage();
            this.groupBox27 = new GroupBox();
            this.groupBox28 = new GroupBox();
            this.button8 = new Button();
            this.SpatialExportJobIdTextBox = new TextBox();
            this.SpatialExportStatusButton = new Button();
            this.label30 = new Label();
            this.groupBox29 = new GroupBox();
            this.label35 = new Label();
            this.LaunchSpatialExportFolderCheckBox = new CheckBox();
            this.RefreshCompleteSpatialExportListButton = new Button();
            this.CompletedSpatialExportsComboBox = new ComboBox();
            this.SpatialExportCompressCheckBox = new CheckBox();
            this.groupBox30 = new GroupBox();
            this.button12 = new Button();
            this.DeleteSpatialExportButton = new Button();
            this.label36 = new Label();
            this.RetrieveSpatialExportButton = new Button();
            this.groupBox31 = new GroupBox();
            this.DeselectAllLayersButton = new Button();
            this.SelectAllLayersButton = new Button();
            this.MinLatitudeTextBox = new WaterMarkTextBox();
            this.MinLongitudeTextBox = new WaterMarkTextBox();
            this.MaxLongTextBox = new WaterMarkTextBox();
            this.MaxLatTextBox = new WaterMarkTextBox();
            this.LayersCheckedListBox = new CheckedListBox();
            this.button16 = new Button();
            this.label39 = new Label();
            this.label40 = new Label();
            this.SpatialExportOverwriteFalseButton = new RadioButton();
            this.SpatialExportOverwriteTrueButton = new RadioButton();
            this.label41 = new Label();
            this.SpatialExportNameTextBox = new TextBox();
            this.label42 = new Label();
            this.dataTemplateTab = new TabPage();
            this.groupBox26 = new GroupBox();
            this.CustomTemplatesRadioButton = new RadioButton();
            this.StandardTemplatesRadioButton = new RadioButton();
            this.AllTemplatesRadioButton = new RadioButton();
            this.DataTemplateDataTypeComboBox = new ComboBox();
            this.label32 = new Label();
            this.label31 = new Label();
            this.label29 = new Label();
            this.LoadDataTemplateButton = new Button();
            this.SaveDataTemplateButton = new Button();
            this.DeleteDataTemplateButton = new Button();
            this.DataTemplateDropdownList = new ComboBox();
            this.RefreshDataTemplatesButton = new Button();
            this.DataTemplateTextBox = new TextBox();
            this.onelineExportTab = new TabPage();
            this.groupBox21 = new GroupBox();
            this.groupBox22 = new GroupBox();
            this.button6 = new Button();
            this.OnelineJobIdTextBox = new TextBox();
            this.OnelineStatusButton = new Button();
            this.label21 = new Label();
            this.groupBox23 = new GroupBox();
            this.label34 = new Label();
            this.LaunchOnelineFolderCheckbox = new CheckBox();
            this.RefreshCompleteOnelinesListButton = new Button();
            this.CompletedOnelinesComboBox = new ComboBox();
            this.OnelineCompressCheckbox = new CheckBox();
            this.groupBox24 = new GroupBox();
            this.button10 = new Button();
            this.DeleteOnelineButton = new Button();
            this.label22 = new Label();
            this.RetrieveOnelineButton = new Button();
            this.groupBox25 = new GroupBox();
            this.BuildOnelineFromQueryButton = new Button();
            this.BuildOnelineButton = new Button();
            this.OnelineTemplateDropDownList = new ComboBoxEx();
            this.label28 = new Label();
            this.OnelineFormatTypeDropDownList = new ComboBox();
            this.OnelineDatatypeDropDownList = new ComboBox();
            this.label23 = new Label();
            this.label24 = new Label();
            this.label25 = new Label();
            this.OnelineIdsTextBox = new TextBox();
            this.OnelineOverwriteFalseButton = new RadioButton();
            this.OnelineOverwriteTrueButton = new RadioButton();
            this.label26 = new Label();
            this.OnelineNameTextBox = new TextBox();
            this.label27 = new Label();
            this.tabGraph = new TabPage();
            this.groupBox17 = new GroupBox();
            this.groupBox18 = new GroupBox();
            this.button5 = new Button();
            this.label17 = new Label();
            this.GraphJobIdTextBox = new TextBox();
            this.GraphStatusButton = new Button();
            this.groupBox12 = new GroupBox();
            this.label19 = new Label();
            this.LaunchGraphFolderCheckbox = new CheckBox();
            this.RefreshCompleteGraphsListButton = new Button();
            this.CompletedGraphsComboBox = new ComboBox();
            this.GraphCompressCheckbox = new CheckBox();
            this.groupBox13 = new GroupBox();
            this.button2 = new Button();
            this.DeleteGraphButton = new Button();
            this.RetrieveGraphButton = new Button();
            this.groupBox11 = new GroupBox();
            this.label14 = new Label();
            this.GraphFormatDropDownList = new ComboBox();
            this.BuildGraphFromQueryButton = new Button();
            this.GraphDatatypeDropDownList = new ComboBox();
            this.GraphTemplateDropDownList = new ComboBox();
            this.label7 = new Label();
            this.label8 = new Label();
            this.label9 = new Label();
            this.GraphIdsTextBox = new TextBox();
            this.OverwriteTypeFalseButton = new RadioButton();
            this.GraphOverwriteTrueButton = new RadioButton();
            this.lblGraphOverwriteType = new Label();
            this.GraphNameTextBox = new TextBox();
            this.lblGraphName = new Label();
            this.BuildGraphButton = new Button();
            this.reportTab = new TabPage();
            this.groupBox19 = new GroupBox();
            this.groupBox20 = new GroupBox();
            this.button7 = new Button();
            this.ReportJobIdTextBox = new TextBox();
            this.ReportStatusButton = new Button();
            this.label20 = new Label();
            this.groupBox2 = new GroupBox();
            this.LaunchReportFolderCheckbox = new CheckBox();
            this.RefreshCompleteReportsListButton = new Button();
            this.CompletedReportsComboBox = new ComboBox();
            this.ReportCompressCheckbox = new CheckBox();
            this.groupBox4 = new GroupBox();
            this.button1 = new Button();
            this.DeleteReportButton = new Button();
            this.lblReportJobID = new Label();
            this.RetrieveReportButton = new Button();
            this.groupBox1 = new GroupBox();
            this.BuildReportFromQueryButton = new Button();
            this.ReportDatatypeDropDownList = new ComboBox();
            this.ReportTemplateDropDownList = new ComboBox();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.ReportIdsTextBox = new TextBox();
            this.ReportOverwriteFalseButton = new RadioButton();
            this.ReportOverwriteTrueButton = new RadioButton();
            this.lblReportOverwrite = new Label();
            this.ReportNameTextBox = new TextBox();
            this.lblReportName = new Label();
            this.BuildReportButton = new Button();
            this.DeleteMe = new Button();
            this.groupBox3 = new GroupBox();
            this.EntitlementsButton = new Button();
            this.ServiceUrlTextBox = new TextBox();
            this.BaseUrlDropDownList = new ComboBox();
            this.SessionButton = new Button();
            this.lblService = new Label();
            this.ApplicationTextBox = new TextBox();
            this.PasswordTextBox = new TextBox();
            this.UserTextBox = new TextBox();
            this.lblAction = new Label();
            this.lblPassword = new Label();
            this.lblUser = new Label();
            this.toolTip1 = new ToolTip(this.components);
            this.versionLabel = new Label();
            this.tabcontrol1.SuspendLayout();
            this.queryTab.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.tabChangeDelete.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((ISupportInitialize)this.PageTextBox).BeginInit();
            this.exportTab.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.spatialExportTab.SuspendLayout();
            this.groupBox27.SuspendLayout();
            this.groupBox28.SuspendLayout();
            this.groupBox29.SuspendLayout();
            this.groupBox30.SuspendLayout();
            this.groupBox31.SuspendLayout();
            this.dataTemplateTab.SuspendLayout();
            this.groupBox26.SuspendLayout();
            this.onelineExportTab.SuspendLayout();
            this.groupBox21.SuspendLayout();
            this.groupBox22.SuspendLayout();
            this.groupBox23.SuspendLayout();
            this.groupBox24.SuspendLayout();
            this.groupBox25.SuspendLayout();
            this.tabGraph.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.groupBox18.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.reportTab.SuspendLayout();
            this.groupBox19.SuspendLayout();
            this.groupBox20.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            base.SuspendLayout();
            this.tabcontrol1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.tabcontrol1.Controls.Add(this.queryTab);
            this.tabcontrol1.Controls.Add(this.tabChangeDelete);
            this.tabcontrol1.Controls.Add(this.exportTab);
            this.tabcontrol1.Controls.Add(this.spatialExportTab);
            this.tabcontrol1.Controls.Add(this.dataTemplateTab);
            this.tabcontrol1.Controls.Add(this.onelineExportTab);
            this.tabcontrol1.Controls.Add(this.tabGraph);
            this.tabcontrol1.Controls.Add(this.reportTab);
            this.tabcontrol1.Enabled = false;
            this.tabcontrol1.Location = new Point(8, 172);
            this.tabcontrol1.Name = "tabcontrol1";
            this.tabcontrol1.SelectedIndex = 0;
            this.tabcontrol1.Size = new Size(641, 429);
            this.tabcontrol1.TabIndex = 1;
            this.toolTip1.SetToolTip(this.tabcontrol1, "Build Graph(s) from list of Ids");
            this.queryTab.Controls.Add(this.groupBox5);
            this.queryTab.Controls.Add(this.groupBox16);
            this.queryTab.Location = new Point(4, 22);
            this.queryTab.Name = "queryTab";
            this.queryTab.Size = new Size(633, 403);
            this.queryTab.TabIndex = 2;
            this.queryTab.Text = "Query";
            this.queryTab.UseVisualStyleBackColor = true;
            this.queryTab.Visible = false;
            this.groupBox5.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.groupBox5.Controls.Add(this.RefreshQueriesButton);
            this.groupBox5.Controls.Add(this.LoadQueryButton);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.ClearButton);
            this.groupBox5.Controls.Add(this.SaveQueryButton);
            this.groupBox5.Controls.Add(this.GetIdsQueryButton);
            this.groupBox5.Controls.Add(this.DeleteQueryButton);
            this.groupBox5.Controls.Add(this.CountQueryButton);
            this.groupBox5.Controls.Add(this.TargetTypeDropDownList);
            this.groupBox5.Controls.Add(this.QueryDropDownList);
            this.groupBox5.Controls.Add(this.lblQSXML);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.SavedQueryDropDownList);
            this.groupBox5.Controls.Add(this.RefreshSavedQueriesButton);
            this.groupBox5.Controls.Add(this.QueryTextBox);
            this.groupBox5.Controls.Add(this.AttributesQueryButton);
            this.groupBox5.Location = new Point(10, 10);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new Size(613, 273);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            this.RefreshQueriesButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.RefreshQueriesButton.BackgroundImage = Resources.Refresh;
            this.RefreshQueriesButton.BackgroundImageLayout = ImageLayout.Stretch;
            this.RefreshQueriesButton.ImageAlign = ContentAlignment.TopLeft;
            this.RefreshQueriesButton.Location = new Point(454, 32);
            this.RefreshQueriesButton.Name = "RefreshQueriesButton";
            this.RefreshQueriesButton.Size = new Size(26, 24);
            this.RefreshQueriesButton.TabIndex = 15;
            this.toolTip1.SetToolTip(this.RefreshQueriesButton, "Refresh query list");
            this.RefreshQueriesButton.Click += new System.EventHandler(this.RefreshQueries);
            this.LoadQueryButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.LoadQueryButton.AutoSize = true;
            this.LoadQueryButton.BackgroundImage = Resources.Load;
            this.LoadQueryButton.BackgroundImageLayout = ImageLayout.Center;
            this.LoadQueryButton.ImageAlign = ContentAlignment.TopLeft;
            this.LoadQueryButton.Location = new Point(492, 72);
            this.LoadQueryButton.Name = "LoadQueryButton";
            this.LoadQueryButton.Size = new Size(35, 24);
            this.LoadQueryButton.TabIndex = 14;
            this.toolTip1.SetToolTip(this.LoadQueryButton, "Get the Saved Query definition");
            this.LoadQueryButton.UseVisualStyleBackColor = false;
            this.LoadQueryButton.Click += new System.EventHandler(this.LoadSavedQuery);
            this.label15.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.label15.AutoSize = true;
            this.label15.Location = new Point(520, 193);
            this.label15.Name = "label15";
            this.label15.Size = new Size(84, 13);
            this.label15.TabIndex = 5;
            this.label15.Text = "Target Datatype";
            this.ClearButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.ClearButton.Location = new Point(520, 166);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new Size(87, 24);
            this.ClearButton.TabIndex = 3;
            this.ClearButton.Text = "Clear";
            this.ClearButton.Click += new System.EventHandler(this.ClearQuery);
            this.SaveQueryButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.SaveQueryButton.BackgroundImage = Resources.Save;
            this.SaveQueryButton.BackgroundImageLayout = ImageLayout.Center;
            this.SaveQueryButton.ImageAlign = ContentAlignment.TopLeft;
            this.SaveQueryButton.Location = new Point(531, 72);
            this.SaveQueryButton.Name = "SaveQueryButton";
            this.SaveQueryButton.Size = new Size(35, 24);
            this.SaveQueryButton.TabIndex = 13;
            this.toolTip1.SetToolTip(this.SaveQueryButton, "Save Criteria Xml");
            this.SaveQueryButton.Click += new System.EventHandler(this.SaveQuery);
            this.AttributesQueryButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.AttributesQueryButton.Location = new Point(518, 242);
            this.AttributesQueryButton.Name = "AttributesQueryButton";
            this.AttributesQueryButton.Size = new Size(87, 24);
            this.AttributesQueryButton.TabIndex = 0;
            this.AttributesQueryButton.Text = "Attributes";
            this.toolTip1.SetToolTip(this.AttributesQueryButton, "Get attributes (results xml) for query");
            this.AttributesQueryButton.Visible = false;
            this.AttributesQueryButton.Click += new System.EventHandler(this.ExecuteAttributesQuery);
            this.GetIdsQueryButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.GetIdsQueryButton.Location = new Point(520, 135);
            this.GetIdsQueryButton.Name = "GetIdsQueryButton";
            this.GetIdsQueryButton.Size = new Size(87, 24);
            this.GetIdsQueryButton.TabIndex = 2;
            this.GetIdsQueryButton.Text = "IDs";
            this.toolTip1.SetToolTip(this.GetIdsQueryButton, "Get Ids from query");
            this.GetIdsQueryButton.Click += new System.EventHandler(this.GetIds);
            this.DeleteQueryButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.DeleteQueryButton.BackgroundImage = Resources.Delete;
            this.DeleteQueryButton.BackgroundImageLayout = ImageLayout.Center;
            this.DeleteQueryButton.ImageAlign = ContentAlignment.TopLeft;
            this.DeleteQueryButton.Location = new Point(570, 72);
            this.DeleteQueryButton.Name = "DeleteQueryButton";
            this.DeleteQueryButton.Size = new Size(35, 24);
            this.DeleteQueryButton.TabIndex = 12;
            this.toolTip1.SetToolTip(this.DeleteQueryButton, "Delete selected Saved Query");
            this.DeleteQueryButton.Click += new System.EventHandler(this.DeleteQuery);
            this.CountQueryButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.CountQueryButton.Location = new Point(520, 105);
            this.CountQueryButton.Name = "CountQueryButton";
            this.CountQueryButton.Size = new Size(87, 24);
            this.CountQueryButton.TabIndex = 1;
            this.CountQueryButton.Text = "Count";
            this.toolTip1.SetToolTip(this.CountQueryButton, "Get query count");
            this.CountQueryButton.Click += new System.EventHandler(this.GetCount);
            this.TargetTypeDropDownList.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.TargetTypeDropDownList.DropDownStyle = ComboBoxStyle.DropDownList;
            this.TargetTypeDropDownList.FormattingEnabled = true;
            this.TargetTypeDropDownList.Location = new Point(520, 209);
            this.TargetTypeDropDownList.Name = "TargetTypeDropDownList";
            this.TargetTypeDropDownList.Size = new Size(87, 21);
            this.TargetTypeDropDownList.TabIndex = 4;
            this.toolTip1.SetToolTip(this.TargetTypeDropDownList, "Optional target datatype for count & ids");
            this.QueryDropDownList.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            this.QueryDropDownList.DropDownStyle = ComboBoxStyle.DropDownList;
            this.QueryDropDownList.FormattingEnabled = true;
            this.QueryDropDownList.Location = new Point(10, 32);
            this.QueryDropDownList.Name = "QueryDropDownList";
            this.QueryDropDownList.Size = new Size(438, 21);
            this.QueryDropDownList.TabIndex = 1;
            this.QueryDropDownList.SelectedIndexChanged += new System.EventHandler(this.QueryDropDownListSelectedIndexChanged);
            this.lblQSXML.AccessibleDescription = "";
            this.lblQSXML.AutoSize = true;
            this.lblQSXML.Location = new Point(7, 16);
            this.lblQSXML.Name = "lblQSXML";
            this.lblQSXML.Size = new Size(265, 13);
            this.lblQSXML.TabIndex = 0;
            this.lblQSXML.Text = "Select an existing query or paste (drag/drop) your own.";
            this.label10.AccessibleDescription = "";
            this.label10.AutoSize = true;
            this.label10.Location = new Point(7, 56);
            this.label10.Name = "label10";
            this.label10.Size = new Size(133, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "OR Select a Saved Query.";
            this.SavedQueryDropDownList.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            this.SavedQueryDropDownList.DropDownStyle = ComboBoxStyle.DropDownList;
            this.SavedQueryDropDownList.FormattingEnabled = true;
            this.SavedQueryDropDownList.Location = new Point(10, 72);
            this.SavedQueryDropDownList.Name = "SavedQueryDropDownList";
            this.SavedQueryDropDownList.Size = new Size(438, 21);
            this.SavedQueryDropDownList.TabIndex = 3;
            this.SavedQueryDropDownList.SelectedIndexChanged += new System.EventHandler(this.SavedQueryDropDownListSelectedIndexChanged);
            this.RefreshSavedQueriesButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.RefreshSavedQueriesButton.BackgroundImage = Resources.Refresh;
            this.RefreshSavedQueriesButton.BackgroundImageLayout = ImageLayout.Stretch;
            this.RefreshSavedQueriesButton.ImageAlign = ContentAlignment.TopLeft;
            this.RefreshSavedQueriesButton.Location = new Point(454, 72);
            this.RefreshSavedQueriesButton.Name = "RefreshSavedQueriesButton";
            this.RefreshSavedQueriesButton.Size = new Size(26, 24);
            this.RefreshSavedQueriesButton.TabIndex = 11;
            this.toolTip1.SetToolTip(this.RefreshSavedQueriesButton, "Refresh saved query list");
            this.RefreshSavedQueriesButton.Click += new System.EventHandler(this.RefreshSavedQueries);
            this.QueryTextBox.AllowDrop = true;
            this.QueryTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.QueryTextBox.Location = new Point(6, 105);
            this.QueryTextBox.Multiline = true;
            this.QueryTextBox.Name = "QueryTextBox";
            this.QueryTextBox.ScrollBars = ScrollBars.Vertical;
            this.QueryTextBox.Size = new Size(496, 162);
            this.QueryTextBox.TabIndex = 9;
            this.QueryTextBox.DragDrop += new DragEventHandler(this.DragDropHandler);
            this.QueryTextBox.DragEnter += new DragEventHandler(this.DragEnterHandler);
            this.QueryTextBox.KeyPress += new KeyPressEventHandler(this.KeyPressHandler);
            this.groupBox16.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.groupBox16.Controls.Add(this.ResultsXmlTextBox);
            this.groupBox16.Location = new Point(10, 289);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new Size(627, 106);
            this.groupBox16.TabIndex = 14;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "Query Results";
            this.ResultsXmlTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.ResultsXmlTextBox.BackColor = SystemColors.Window;
            this.ResultsXmlTextBox.Location = new Point(6, 19);
            this.ResultsXmlTextBox.Multiline = true;
            this.ResultsXmlTextBox.Name = "ResultsXmlTextBox";
            this.ResultsXmlTextBox.ReadOnly = true;
            this.ResultsXmlTextBox.ScrollBars = ScrollBars.Vertical;
            this.ResultsXmlTextBox.Size = new Size(615, 81);
            this.ResultsXmlTextBox.TabIndex = 10;
            this.ResultsXmlTextBox.KeyPress += new KeyPressEventHandler(this.KeyPressHandler);
            this.tabChangeDelete.Controls.Add(this.groupBox10);
            this.tabChangeDelete.Controls.Add(this.groupBox6);
            this.tabChangeDelete.Location = new Point(4, 22);
            this.tabChangeDelete.Name = "tabChangeDelete";
            this.tabChangeDelete.Padding = new Padding(3);
            this.tabChangeDelete.Size = new Size(633, 403);
            this.tabChangeDelete.TabIndex = 4;
            this.tabChangeDelete.Text = "Change/Delete";
            this.tabChangeDelete.UseVisualStyleBackColor = true;
            this.groupBox10.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.groupBox10.Controls.Add(this.ChangeDeleteResultsTextBox);
            this.groupBox10.Controls.Add(this.PagingDetailsLabel);
            this.groupBox10.Location = new Point(10, 145);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new Size(613, 252);
            this.groupBox10.TabIndex = 32;
            this.groupBox10.TabStop = false;
            this.ChangeDeleteResultsTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.ChangeDeleteResultsTextBox.BackColor = SystemColors.Window;
            this.ChangeDeleteResultsTextBox.Location = new Point(9, 33);
            this.ChangeDeleteResultsTextBox.Multiline = true;
            this.ChangeDeleteResultsTextBox.Name = "ChangeDeleteResultsTextBox";
            this.ChangeDeleteResultsTextBox.ReadOnly = true;
            this.ChangeDeleteResultsTextBox.ScrollBars = ScrollBars.Vertical;
            this.ChangeDeleteResultsTextBox.Size = new Size(594, 213);
            this.ChangeDeleteResultsTextBox.TabIndex = 31;
            this.ChangeDeleteResultsTextBox.KeyPress += new KeyPressEventHandler(this.KeyPressHandler);
            this.PagingDetailsLabel.AutoSize = true;
            this.PagingDetailsLabel.Location = new Point(11, 17);
            this.PagingDetailsLabel.Name = "PagingDetailsLabel";
            this.PagingDetailsLabel.Size = new Size(0, 13);
            this.PagingDetailsLabel.TabIndex = 30;
            this.groupBox6.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.groupBox6.Controls.Add(this.GetChangeDeleteFromIdsButton);
            this.groupBox6.Controls.Add(this.PageTextBox);
            this.groupBox6.Controls.Add(this.ToDatePicker);
            this.groupBox6.Controls.Add(this.label16);
            this.groupBox6.Controls.Add(this.label12);
            this.groupBox6.Controls.Add(this.GetChangeDeleteButton);
            this.groupBox6.Controls.Add(this.FromDatePicker);
            this.groupBox6.Controls.Add(this.label11);
            this.groupBox6.Controls.Add(this.ChangeDeleteIdsTextBox);
            this.groupBox6.Controls.Add(this.label13);
            this.groupBox6.Location = new Point(10, 10);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new Size(613, 129);
            this.groupBox6.TabIndex = 27;
            this.groupBox6.TabStop = false;
            this.GetChangeDeleteFromIdsButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.GetChangeDeleteFromIdsButton.Location = new Point(504, 19);
            this.GetChangeDeleteFromIdsButton.Name = "GetChangeDeleteFromIdsButton";
            this.GetChangeDeleteFromIdsButton.Size = new Size(101, 24);
            this.GetChangeDeleteFromIdsButton.TabIndex = 7;
            this.GetChangeDeleteFromIdsButton.Text = "Targeted";
            this.GetChangeDeleteFromIdsButton.Click += new System.EventHandler(this.GetChangeDeleteFromIdsButtonClick);
            this.PageTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.PageTextBox.Location = new Point(537, 86);
            this.PageTextBox.Name = "PageTextBox";
            this.PageTextBox.Size = new Size(54, 20);
            this.PageTextBox.TabIndex = 6;
            NumericUpDown arg_1F74_0 = this.PageTextBox;
            int[] array = new int[4];
            array[0] = 1;
            arg_1F74_0.Value = new decimal(array);
            this.ToDatePicker.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.ToDatePicker.CustomFormat = "MMM dd, yyyy";
            this.ToDatePicker.Format = DateTimePickerFormat.Custom;
            this.ToDatePicker.Location = new Point(393, 49);
            this.ToDatePicker.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.ToDatePicker.Name = "ToDatePicker";
            this.ToDatePicker.Size = new Size(102, 20);
            this.ToDatePicker.TabIndex = 4;
            this.ToDatePicker.Value = new System.DateTime(2010, 8, 7, 0, 0, 0, 0);
            this.label16.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.label16.AutoSize = true;
            this.label16.Location = new Point(501, 88);
            this.label16.Name = "label16";
            this.label16.Size = new Size(32, 13);
            this.label16.TabIndex = 5;
            this.label16.Text = "Page";
            this.label16.TextAlign = ContentAlignment.MiddleRight;
            this.label12.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.label12.AutoSize = true;
            this.label12.Location = new Point(367, 45);
            this.label12.Name = "label12";
            this.label12.Size = new Size(20, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "To";
            this.label12.TextAlign = ContentAlignment.MiddleRight;
            this.GetChangeDeleteButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.GetChangeDeleteButton.Location = new Point(504, 49);
            this.GetChangeDeleteButton.Name = "GetChangeDeleteButton";
            this.GetChangeDeleteButton.Size = new Size(101, 24);
            this.GetChangeDeleteButton.TabIndex = 8;
            this.GetChangeDeleteButton.Text = "Firehose";
            this.GetChangeDeleteButton.Click += new System.EventHandler(this.GetChangeDeleteButtonClick);
            this.FromDatePicker.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.FromDatePicker.CustomFormat = "MMM dd, yyyy";
            this.FromDatePicker.Format = DateTimePickerFormat.Custom;
            this.FromDatePicker.Location = new Point(393, 23);
            this.FromDatePicker.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.FromDatePicker.Name = "FromDatePicker";
            this.FromDatePicker.Size = new Size(102, 20);
            this.FromDatePicker.TabIndex = 2;
            this.FromDatePicker.Value = new System.DateTime(2010, 8, 7, 0, 0, 0, 0);
            this.label11.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.label11.AutoSize = true;
            this.label11.Location = new Point(360, 23);
            this.label11.Name = "label11";
            this.label11.Size = new Size(30, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Date";
            this.label11.TextAlign = ContentAlignment.MiddleRight;
            this.ChangeDeleteIdsTextBox.AllowDrop = true;
            this.ChangeDeleteIdsTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.ChangeDeleteIdsTextBox.Location = new Point(36, 19);
            this.ChangeDeleteIdsTextBox.Multiline = true;
            this.ChangeDeleteIdsTextBox.Name = "ChangeDeleteIdsTextBox";
            this.ChangeDeleteIdsTextBox.Size = new Size(314, 93);
            this.ChangeDeleteIdsTextBox.TabIndex = 0;
            this.ChangeDeleteIdsTextBox.DragDrop += new DragEventHandler(this.DragDropHandler);
            this.ChangeDeleteIdsTextBox.DragEnter += new DragEventHandler(this.DragEnterHandler);
            this.ChangeDeleteIdsTextBox.KeyPress += new KeyPressEventHandler(this.KeyPressHandler);
            this.label13.AutoSize = true;
            this.label13.Location = new Point(9, 19);
            this.label13.Name = "label13";
            this.label13.Size = new Size(21, 13);
            this.label13.TabIndex = 31;
            this.label13.Text = "Ids";
            this.label13.TextAlign = ContentAlignment.MiddleRight;
            this.exportTab.Controls.Add(this.groupBox14);
            this.exportTab.Controls.Add(this.groupBox8);
            this.exportTab.Controls.Add(this.groupBox7);
            this.exportTab.Location = new Point(4, 22);
            this.exportTab.Name = "exportTab";
            this.exportTab.Size = new Size(633, 403);
            this.exportTab.TabIndex = 1;
            this.exportTab.Text = "Export";
            this.exportTab.UseVisualStyleBackColor = true;
            this.exportTab.Visible = false;
            this.groupBox14.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.groupBox14.Controls.Add(this.groupBox15);
            this.groupBox14.Controls.Add(this.ExportJobIdTextBox);
            this.groupBox14.Controls.Add(this.ExportStatusButton);
            this.groupBox14.Controls.Add(this.label18);
            this.groupBox14.Location = new Point(10, 232);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new Size(613, 52);
            this.groupBox14.TabIndex = 31;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Check Export Status";
            this.groupBox15.Controls.Add(this.button4);
            this.groupBox15.Location = new Point(0, -70);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new Size(410, 70);
            this.groupBox15.TabIndex = 42;
            this.groupBox15.TabStop = false;
            this.button4.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.button4.Location = new Point(310, 15);
            this.button4.Name = "button4";
            this.button4.Size = new Size(85, 30);
            this.button4.TabIndex = 23;
            this.button4.Text = "Build";
            this.ExportJobIdTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            this.ExportJobIdTextBox.Location = new Point(80, 17);
            this.ExportJobIdTextBox.Name = "ExportJobIdTextBox";
            this.ExportJobIdTextBox.Size = new Size(418, 20);
            this.ExportJobIdTextBox.TabIndex = 33;
            this.ExportStatusButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.ExportStatusButton.ImageAlign = ContentAlignment.MiddleLeft;
            this.ExportStatusButton.Location = new Point(504, 14);
            this.ExportStatusButton.Name = "ExportStatusButton";
            this.ExportStatusButton.Size = new Size(101, 24);
            this.ExportStatusButton.TabIndex = 41;
            this.ExportStatusButton.Text = "&Status";
            this.toolTip1.SetToolTip(this.ExportStatusButton, "Get the status of the specified job id.");
            this.ExportStatusButton.Click += new System.EventHandler(this.GetExportStatus);
            this.label18.AutoSize = true;
            this.label18.Location = new Point(9, 20);
            this.label18.Name = "label18";
            this.label18.Size = new Size(38, 13);
            this.label18.TabIndex = 39;
            this.label18.Text = "Job ID";
            this.groupBox8.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.groupBox8.Controls.Add(this.CompletedExportsComboBox);
            this.groupBox8.Controls.Add(this.label33);
            this.groupBox8.Controls.Add(this.LaunchExportFolderCheckbox);
            this.groupBox8.Controls.Add(this.RefreshCompleteExportsListButton);
            this.groupBox8.Controls.Add(this.ExportCompressCheckbox);
            this.groupBox8.Controls.Add(this.groupBox9);
            this.groupBox8.Controls.Add(this.DeleteExportButton);
            this.groupBox8.Controls.Add(this.lblExportJobID);
            this.groupBox8.Controls.Add(this.RetrieveExportButton);
            this.groupBox8.Location = new Point(10, 290);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new Size(613, 102);
            this.groupBox8.TabIndex = 30;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Retrieve / Delete Exports";
            this.CompletedExportsComboBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            this.CompletedExportsComboBox.FormattingEnabled = true;
            this.CompletedExportsComboBox.Location = new Point(78, 16);
            this.CompletedExportsComboBox.Name = "CompletedExportsComboBox";
            this.CompletedExportsComboBox.Size = new Size(420, 21);
            this.CompletedExportsComboBox.TabIndex = 48;
            this.label33.AutoSize = true;
            this.label33.Location = new Point(12, 82);
            this.label33.Name = "label33";
            this.label33.Size = new Size(286, 13);
            this.label33.TabIndex = 47;
            this.label33.Text = "* Exports and Oneline Exports are stored in the same folder.";
            this.LaunchExportFolderCheckbox.Checked = true;
            this.LaunchExportFolderCheckbox.CheckState = CheckState.Checked;
            this.LaunchExportFolderCheckbox.Location = new Point(178, 43);
            this.LaunchExportFolderCheckbox.Name = "LaunchExportFolderCheckbox";
            this.LaunchExportFolderCheckbox.Size = new Size(104, 24);
            this.LaunchExportFolderCheckbox.TabIndex = 46;
            this.LaunchExportFolderCheckbox.Text = "Launch Explorer";
            this.RefreshCompleteExportsListButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.RefreshCompleteExportsListButton.BackgroundImage = Resources.Refresh;
            this.RefreshCompleteExportsListButton.BackgroundImageLayout = ImageLayout.Stretch;
            this.RefreshCompleteExportsListButton.ImageAlign = ContentAlignment.TopLeft;
            this.RefreshCompleteExportsListButton.Location = new Point(504, 14);
            this.RefreshCompleteExportsListButton.Name = "RefreshCompleteExportsListButton";
            this.RefreshCompleteExportsListButton.Size = new Size(26, 24);
            this.RefreshCompleteExportsListButton.TabIndex = 45;
            this.RefreshCompleteExportsListButton.Click += new System.EventHandler(this.RefreshExportsList);
            this.ExportCompressCheckbox.Checked = true;
            this.ExportCompressCheckbox.CheckState = CheckState.Checked;
            this.ExportCompressCheckbox.Location = new Point(78, 43);
            this.ExportCompressCheckbox.Name = "ExportCompressCheckbox";
            this.ExportCompressCheckbox.Size = new Size(104, 24);
            this.ExportCompressCheckbox.TabIndex = 43;
            this.ExportCompressCheckbox.Text = "Compress";
            this.groupBox9.Controls.Add(this.button3);
            this.groupBox9.Location = new Point(0, -70);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new Size(410, 70);
            this.groupBox9.TabIndex = 42;
            this.groupBox9.TabStop = false;
            this.button3.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.button3.Location = new Point(310, 15);
            this.button3.Name = "button3";
            this.button3.Size = new Size(85, 30);
            this.button3.TabIndex = 23;
            this.button3.Text = "Build";
            this.DeleteExportButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.DeleteExportButton.Location = new Point(504, 72);
            this.DeleteExportButton.Name = "DeleteExportButton";
            this.DeleteExportButton.Size = new Size(101, 24);
            this.DeleteExportButton.TabIndex = 40;
            this.DeleteExportButton.Text = "&Delete";
            this.toolTip1.SetToolTip(this.DeleteExportButton, "Delete selected job on server.");
            this.DeleteExportButton.Click += new System.EventHandler(this.DeleteExport);
            this.lblExportJobID.AutoSize = true;
            this.lblExportJobID.Location = new Point(9, 20);
            this.lblExportJobID.Name = "lblExportJobID";
            this.lblExportJobID.Size = new Size(29, 13);
            this.lblExportJobID.TabIndex = 39;
            this.lblExportJobID.Text = "Jobs";
            this.RetrieveExportButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.RetrieveExportButton.Location = new Point(504, 42);
            this.RetrieveExportButton.Name = "RetrieveExportButton";
            this.RetrieveExportButton.Size = new Size(101, 24);
            this.RetrieveExportButton.TabIndex = 38;
            this.RetrieveExportButton.Text = "&Retrieve";
            this.toolTip1.SetToolTip(this.RetrieveExportButton, "Retrieve the specified export from server.");
            this.RetrieveExportButton.Click += new System.EventHandler(this.RetrieveExport);
            this.groupBox7.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.groupBox7.Controls.Add(this.ExMissingLLCheckbox);
            this.groupBox7.Controls.Add(this.BuildExportFromQueryButton);
            this.groupBox7.Controls.Add(this.ExportDatatypeDropDownList);
            this.groupBox7.Controls.Add(this.ExportTemplateDropDownList);
            this.groupBox7.Controls.Add(this.label4);
            this.groupBox7.Controls.Add(this.label5);
            this.groupBox7.Controls.Add(this.label6);
            this.groupBox7.Controls.Add(this.ExportIdsTextBox);
            this.groupBox7.Controls.Add(this.ExportOverwriteAppendButton);
            this.groupBox7.Controls.Add(this.ExportOverwriteFalseButton);
            this.groupBox7.Controls.Add(this.ExportOverwriteTrueButton);
            this.groupBox7.Controls.Add(this.lblExportOverwriteType);
            this.groupBox7.Controls.Add(this.ExportNameTextBox);
            this.groupBox7.Controls.Add(this.lblExportName);
            this.groupBox7.Controls.Add(this.BuildExportButton);
            this.groupBox7.Location = new Point(10, 10);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new Size(613, 213);
            this.groupBox7.TabIndex = 27;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Build Export";
            this.ExMissingLLCheckbox.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.ExMissingLLCheckbox.Enabled = false;
            this.ExMissingLLCheckbox.Location = new Point(504, 113);
            this.ExMissingLLCheckbox.Name = "ExMissingLLCheckbox";
            this.ExMissingLLCheckbox.Size = new Size(103, 46);
            this.ExMissingLLCheckbox.TabIndex = 44;
            this.ExMissingLLCheckbox.Text = "Exclude Missing Lat/Long";
            this.toolTip1.SetToolTip(this.ExMissingLLCheckbox, "Exclude wells missing Lat/Longs from export.");
            this.BuildExportFromQueryButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.BuildExportFromQueryButton.Location = new Point(504, 49);
            this.BuildExportFromQueryButton.Name = "BuildExportFromQueryButton";
            this.BuildExportFromQueryButton.Size = new Size(101, 27);
            this.BuildExportFromQueryButton.TabIndex = 41;
            this.BuildExportFromQueryButton.Text = "Build From Query";
            this.toolTip1.SetToolTip(this.BuildExportFromQueryButton, "Build Export from Query / CriteriaXml on Query tab");
            this.BuildExportFromQueryButton.Click += new System.EventHandler(this.BuildExportFromQueryButton_Click);
            this.ExportDatatypeDropDownList.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.ExportDatatypeDropDownList.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ExportDatatypeDropDownList.FormattingEnabled = true;
            this.ExportDatatypeDropDownList.Location = new Point(78, 102);
            this.ExportDatatypeDropDownList.Name = "ExportDatatypeDropDownList";
            this.ExportDatatypeDropDownList.Size = new Size(420, 21);
            this.ExportDatatypeDropDownList.TabIndex = 40;
            this.ExportDatatypeDropDownList.SelectedValueChanged += new System.EventHandler(this.ExportDatatypeDropDownListSelectedValueChanged);
            this.ExportTemplateDropDownList.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.ExportTemplateDropDownList.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ExportTemplateDropDownList.FormattingEnabled = true;
            this.ExportTemplateDropDownList.Location = new Point(78, 129);
            this.ExportTemplateDropDownList.Name = "ExportTemplateDropDownList";
            this.ExportTemplateDropDownList.Size = new Size(420, 21);
            this.ExportTemplateDropDownList.TabIndex = 35;
            this.ExportTemplateDropDownList.SelectedIndexChanged += new System.EventHandler(this.ExportTemplateDropDownListSelectedIndexChanged);
            this.label4.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(9, 131);
            this.label4.Name = "label4";
            this.label4.Size = new Size(51, 13);
            this.label4.TabIndex = 39;
            this.label4.Text = "Template";
            this.label4.TextAlign = ContentAlignment.MiddleRight;
            this.label5.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(9, 104);
            this.label5.Name = "label5";
            this.label5.Size = new Size(50, 13);
            this.label5.TabIndex = 38;
            this.label5.Text = "Datatype";
            this.label5.TextAlign = ContentAlignment.MiddleRight;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(9, 19);
            this.label6.Name = "label6";
            this.label6.Size = new Size(21, 13);
            this.label6.TabIndex = 37;
            this.label6.Text = "Ids";
            this.label6.TextAlign = ContentAlignment.MiddleRight;
            this.ExportIdsTextBox.AllowDrop = true;
            this.ExportIdsTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.ExportIdsTextBox.Location = new Point(78, 19);
            this.ExportIdsTextBox.MaxLength = 5000000;
            this.ExportIdsTextBox.Multiline = true;
            this.ExportIdsTextBox.Name = "ExportIdsTextBox";
            this.ExportIdsTextBox.Size = new Size(420, 76);
            this.ExportIdsTextBox.TabIndex = 36;
            this.ExportIdsTextBox.DragDrop += new DragEventHandler(this.DragDropHandler);
            this.ExportIdsTextBox.DragEnter += new DragEventHandler(this.DragEnterHandler);
            this.ExportIdsTextBox.KeyPress += new KeyPressEventHandler(this.KeyPressHandler);
            this.ExportOverwriteAppendButton.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            this.ExportOverwriteAppendButton.Location = new Point(179, 187);
            this.ExportOverwriteAppendButton.Name = "ExportOverwriteAppendButton";
            this.ExportOverwriteAppendButton.Size = new Size(70, 20);
            this.ExportOverwriteAppendButton.TabIndex = 29;
            this.ExportOverwriteAppendButton.Text = "Append";
            this.ExportOverwriteFalseButton.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            this.ExportOverwriteFalseButton.Location = new Point(129, 187);
            this.ExportOverwriteFalseButton.Name = "ExportOverwriteFalseButton";
            this.ExportOverwriteFalseButton.Size = new Size(50, 20);
            this.ExportOverwriteFalseButton.TabIndex = 28;
            this.ExportOverwriteFalseButton.Text = "False";
            this.ExportOverwriteTrueButton.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            this.ExportOverwriteTrueButton.Checked = true;
            this.ExportOverwriteTrueButton.Location = new Point(80, 187);
            this.ExportOverwriteTrueButton.Name = "ExportOverwriteTrueButton";
            this.ExportOverwriteTrueButton.Size = new Size(50, 20);
            this.ExportOverwriteTrueButton.TabIndex = 27;
            this.ExportOverwriteTrueButton.TabStop = true;
            this.ExportOverwriteTrueButton.Text = "True";
            this.lblExportOverwriteType.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            this.lblExportOverwriteType.AutoSize = true;
            this.lblExportOverwriteType.Location = new Point(9, 187);
            this.lblExportOverwriteType.Name = "lblExportOverwriteType";
            this.lblExportOverwriteType.Size = new Size(52, 13);
            this.lblExportOverwriteType.TabIndex = 26;
            this.lblExportOverwriteType.Text = "Overwrite";
            this.lblExportOverwriteType.TextAlign = ContentAlignment.MiddleRight;
            this.ExportNameTextBox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.ExportNameTextBox.Location = new Point(78, 157);
            this.ExportNameTextBox.Name = "ExportNameTextBox";
            this.ExportNameTextBox.Size = new Size(420, 20);
            this.ExportNameTextBox.TabIndex = 24;
            this.ExportNameTextBox.KeyPress += new KeyPressEventHandler(this.KeyPressHandler);
            this.lblExportName.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            this.lblExportName.AutoSize = true;
            this.lblExportName.Location = new Point(9, 160);
            this.lblExportName.Name = "lblExportName";
            this.lblExportName.Size = new Size(49, 13);
            this.lblExportName.TabIndex = 25;
            this.lblExportName.Text = "Filename";
            this.lblExportName.TextAlign = ContentAlignment.MiddleRight;
            this.BuildExportButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.BuildExportButton.Location = new Point(504, 19);
            this.BuildExportButton.Name = "BuildExportButton";
            this.BuildExportButton.Size = new Size(101, 24);
            this.BuildExportButton.TabIndex = 23;
            this.BuildExportButton.Text = "Build";
            this.toolTip1.SetToolTip(this.BuildExportButton, "Build Export from list of Ids");
            this.BuildExportButton.Click += new System.EventHandler(this.BuildExport);
            this.spatialExportTab.Controls.Add(this.groupBox27);
            this.spatialExportTab.Controls.Add(this.groupBox29);
            this.spatialExportTab.Controls.Add(this.groupBox31);
            this.spatialExportTab.Location = new Point(4, 22);
            this.spatialExportTab.Name = "spatialExportTab";
            this.spatialExportTab.Padding = new Padding(3);
            this.spatialExportTab.Size = new Size(633, 403);
            this.spatialExportTab.TabIndex = 7;
            this.spatialExportTab.Text = "Spatial Export";
            this.spatialExportTab.UseVisualStyleBackColor = true;
            this.groupBox27.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.groupBox27.Controls.Add(this.groupBox28);
            this.groupBox27.Controls.Add(this.SpatialExportJobIdTextBox);
            this.groupBox27.Controls.Add(this.SpatialExportStatusButton);
            this.groupBox27.Controls.Add(this.label30);
            this.groupBox27.Location = new Point(10, 232);
            this.groupBox27.Name = "groupBox27";
            this.groupBox27.Size = new Size(613, 52);
            this.groupBox27.TabIndex = 0;
            this.groupBox27.TabStop = false;
            this.groupBox27.Text = "Check Spatial Export Status";
            this.groupBox28.Controls.Add(this.button8);
            this.groupBox28.Location = new Point(0, -70);
            this.groupBox28.Name = "groupBox28";
            this.groupBox28.Size = new Size(410, 70);
            this.groupBox28.TabIndex = 0;
            this.groupBox28.TabStop = false;
            this.button8.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.button8.Location = new Point(310, 15);
            this.button8.Name = "button8";
            this.button8.Size = new Size(85, 30);
            this.button8.TabIndex = 23;
            this.button8.Text = "Build";
            this.SpatialExportJobIdTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            this.SpatialExportJobIdTextBox.Location = new Point(78, 17);
            this.SpatialExportJobIdTextBox.Name = "SpatialExportJobIdTextBox";
            this.SpatialExportJobIdTextBox.Size = new Size(418, 20);
            this.SpatialExportJobIdTextBox.TabIndex = 33;
            this.SpatialExportStatusButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.SpatialExportStatusButton.ImageAlign = ContentAlignment.MiddleLeft;
            this.SpatialExportStatusButton.Location = new Point(505, 14);
            this.SpatialExportStatusButton.Name = "SpatialExportStatusButton";
            this.SpatialExportStatusButton.Size = new Size(101, 24);
            this.SpatialExportStatusButton.TabIndex = 41;
            this.SpatialExportStatusButton.Text = "&Status";
            this.toolTip1.SetToolTip(this.SpatialExportStatusButton, "Get the status of the specified job id.");
            this.SpatialExportStatusButton.Click += new System.EventHandler(this.GetSpatialExportStatus);
            this.label30.AutoSize = true;
            this.label30.Location = new Point(9, 20);
            this.label30.Name = "label30";
            this.label30.Size = new Size(38, 13);
            this.label30.TabIndex = 39;
            this.label30.Text = "Job ID";
            this.groupBox29.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.groupBox29.Controls.Add(this.label35);
            this.groupBox29.Controls.Add(this.LaunchSpatialExportFolderCheckBox);
            this.groupBox29.Controls.Add(this.RefreshCompleteSpatialExportListButton);
            this.groupBox29.Controls.Add(this.CompletedSpatialExportsComboBox);
            this.groupBox29.Controls.Add(this.SpatialExportCompressCheckBox);
            this.groupBox29.Controls.Add(this.groupBox30);
            this.groupBox29.Controls.Add(this.DeleteSpatialExportButton);
            this.groupBox29.Controls.Add(this.label36);
            this.groupBox29.Controls.Add(this.RetrieveSpatialExportButton);
            this.groupBox29.Location = new Point(10, 290);
            this.groupBox29.Name = "groupBox29";
            this.groupBox29.Size = new Size(613, 102);
            this.groupBox29.TabIndex = 1;
            this.groupBox29.TabStop = false;
            this.groupBox29.Text = "Retrieve / Delete Spatial Exports";
            this.label35.AutoSize = true;
            this.label35.Location = new Point(9, 83);
            this.label35.Name = "label35";
            this.label35.Size = new Size(286, 13);
            this.label35.TabIndex = 48;
            this.label35.Text = "* Exports and Oneline Exports are stored in the same folder.";
            this.LaunchSpatialExportFolderCheckBox.Checked = true;
            this.LaunchSpatialExportFolderCheckBox.CheckState = CheckState.Checked;
            this.LaunchSpatialExportFolderCheckBox.Location = new Point(178, 43);
            this.LaunchSpatialExportFolderCheckBox.Name = "LaunchSpatialExportFolderCheckBox";
            this.LaunchSpatialExportFolderCheckBox.Size = new Size(104, 24);
            this.LaunchSpatialExportFolderCheckBox.TabIndex = 46;
            this.LaunchSpatialExportFolderCheckBox.Text = "Launch Explorer";
            this.RefreshCompleteSpatialExportListButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.RefreshCompleteSpatialExportListButton.BackgroundImage = Resources.Refresh;
            this.RefreshCompleteSpatialExportListButton.BackgroundImageLayout = ImageLayout.Stretch;
            this.RefreshCompleteSpatialExportListButton.ImageAlign = ContentAlignment.TopLeft;
            this.RefreshCompleteSpatialExportListButton.Location = new Point(504, 14);
            this.RefreshCompleteSpatialExportListButton.Name = "RefreshCompleteSpatialExportListButton";
            this.RefreshCompleteSpatialExportListButton.Size = new Size(26, 24);
            this.RefreshCompleteSpatialExportListButton.TabIndex = 45;
            this.RefreshCompleteSpatialExportListButton.Click += new System.EventHandler(this.RefreshSpatialExportList);
            this.CompletedSpatialExportsComboBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            this.CompletedSpatialExportsComboBox.FormattingEnabled = true;
            this.CompletedSpatialExportsComboBox.Location = new Point(78, 16);
            this.CompletedSpatialExportsComboBox.Name = "CompletedSpatialExportsComboBox";
            this.CompletedSpatialExportsComboBox.Size = new Size(420, 21);
            this.CompletedSpatialExportsComboBox.TabIndex = 44;
            this.SpatialExportCompressCheckBox.Checked = true;
            this.SpatialExportCompressCheckBox.CheckState = CheckState.Checked;
            this.SpatialExportCompressCheckBox.Location = new Point(78, 43);
            this.SpatialExportCompressCheckBox.Name = "SpatialExportCompressCheckBox";
            this.SpatialExportCompressCheckBox.Size = new Size(104, 24);
            this.SpatialExportCompressCheckBox.TabIndex = 43;
            this.SpatialExportCompressCheckBox.Text = "Compress";
            this.groupBox30.Controls.Add(this.button12);
            this.groupBox30.Location = new Point(0, -70);
            this.groupBox30.Name = "groupBox30";
            this.groupBox30.Size = new Size(410, 70);
            this.groupBox30.TabIndex = 1;
            this.groupBox30.TabStop = false;
            this.button12.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.button12.Location = new Point(310, 15);
            this.button12.Name = "button12";
            this.button12.Size = new Size(85, 30);
            this.button12.TabIndex = 23;
            this.button12.Text = "Build";
            this.DeleteSpatialExportButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.DeleteSpatialExportButton.Location = new Point(504, 72);
            this.DeleteSpatialExportButton.Name = "DeleteSpatialExportButton";
            this.DeleteSpatialExportButton.Size = new Size(101, 24);
            this.DeleteSpatialExportButton.TabIndex = 40;
            this.DeleteSpatialExportButton.Text = "&Delete";
            this.toolTip1.SetToolTip(this.DeleteSpatialExportButton, "Delete selected job on server.");
            this.DeleteSpatialExportButton.Click += new System.EventHandler(this.DeleteSpatialExport);
            this.label36.AutoSize = true;
            this.label36.Location = new Point(9, 20);
            this.label36.Name = "label36";
            this.label36.Size = new Size(29, 13);
            this.label36.TabIndex = 39;
            this.label36.Text = "Jobs";
            this.RetrieveSpatialExportButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.RetrieveSpatialExportButton.Location = new Point(504, 42);
            this.RetrieveSpatialExportButton.Name = "RetrieveSpatialExportButton";
            this.RetrieveSpatialExportButton.Size = new Size(101, 24);
            this.RetrieveSpatialExportButton.TabIndex = 38;
            this.RetrieveSpatialExportButton.Text = "&Retrieve";
            this.toolTip1.SetToolTip(this.RetrieveSpatialExportButton, "Retrieve the specified export from server.");
            this.RetrieveSpatialExportButton.Click += new System.EventHandler(this.RetrieveSpatialExport);
            this.groupBox31.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.groupBox31.Controls.Add(this.DeselectAllLayersButton);
            this.groupBox31.Controls.Add(this.SelectAllLayersButton);
            this.groupBox31.Controls.Add(this.MinLatitudeTextBox);
            this.groupBox31.Controls.Add(this.MinLongitudeTextBox);
            this.groupBox31.Controls.Add(this.MaxLongTextBox);
            this.groupBox31.Controls.Add(this.MaxLatTextBox);
            this.groupBox31.Controls.Add(this.LayersCheckedListBox);
            this.groupBox31.Controls.Add(this.button16);
            this.groupBox31.Controls.Add(this.label39);
            this.groupBox31.Controls.Add(this.label40);
            this.groupBox31.Controls.Add(this.SpatialExportOverwriteFalseButton);
            this.groupBox31.Controls.Add(this.SpatialExportOverwriteTrueButton);
            this.groupBox31.Controls.Add(this.label41);
            this.groupBox31.Controls.Add(this.SpatialExportNameTextBox);
            this.groupBox31.Controls.Add(this.label42);
            this.groupBox31.Location = new Point(10, 10);
            this.groupBox31.Name = "groupBox31";
            this.groupBox31.Size = new Size(613, 213);
            this.groupBox31.TabIndex = 1;
            this.groupBox31.TabStop = false;
            this.groupBox31.Text = "Build Spatial Export";
            this.DeselectAllLayersButton.Location = new Point(28, 73);
            this.DeselectAllLayersButton.Name = "DeselectAllLayersButton";
            this.DeselectAllLayersButton.Size = new Size(44, 23);
            this.DeselectAllLayersButton.TabIndex = 30;
            this.DeselectAllLayersButton.Text = "None";
            this.DeselectAllLayersButton.UseVisualStyleBackColor = true;
            this.DeselectAllLayersButton.Click += new System.EventHandler(this.DeselectAllLayers);
            this.SelectAllLayersButton.Location = new Point(28, 44);
            this.SelectAllLayersButton.Name = "SelectAllLayersButton";
            this.SelectAllLayersButton.Size = new Size(44, 23);
            this.SelectAllLayersButton.TabIndex = 29;
            this.SelectAllLayersButton.Text = "All";
            this.SelectAllLayersButton.UseVisualStyleBackColor = true;
            this.SelectAllLayersButton.Click += new System.EventHandler(this.SelectAllLayers);
            this.MinLatitudeTextBox.Anchor = AnchorStyles.Bottom;
            this.MinLatitudeTextBox.Font = new Font("Microsoft Sans Serif", 8.25f);
            this.MinLatitudeTextBox.Location = new Point(260, 162);
            this.MinLatitudeTextBox.Name = "MinLatitudeTextBox";
            this.MinLatitudeTextBox.Size = new Size(74, 20);
            this.MinLatitudeTextBox.TabIndex = 4;
            this.MinLatitudeTextBox.WaterMarkColor = Color.Gray;
            this.MinLatitudeTextBox.WaterMarkText = "Min Latitude";
            this.MinLatitudeTextBox.WordWrap = false;
            this.MinLongitudeTextBox.Anchor = AnchorStyles.Bottom;
            this.MinLongitudeTextBox.Font = new Font("Microsoft Sans Serif", 8.25f);
            this.MinLongitudeTextBox.Location = new Point(355, 138);
            this.MinLongitudeTextBox.Name = "MinLongitudeTextBox";
            this.MinLongitudeTextBox.Size = new Size(79, 20);
            this.MinLongitudeTextBox.TabIndex = 6;
            this.MinLongitudeTextBox.WaterMarkColor = Color.Gray;
            this.MinLongitudeTextBox.WaterMarkText = "Min Longitude";
            this.MaxLongTextBox.Anchor = AnchorStyles.Bottom;
            this.MaxLongTextBox.Font = new Font("Microsoft Sans Serif", 8.25f);
            this.MaxLongTextBox.Location = new Point(146, 138);
            this.MaxLongTextBox.Name = "MaxLongTextBox";
            this.MaxLongTextBox.Size = new Size(79, 20);
            this.MaxLongTextBox.TabIndex = 5;
            this.MaxLongTextBox.WaterMarkColor = Color.Gray;
            this.MaxLongTextBox.WaterMarkText = "Max Longitude";
            this.MaxLatTextBox.Anchor = AnchorStyles.Bottom;
            this.MaxLatTextBox.Font = new Font("Microsoft Sans Serif", 8.25f);
            this.MaxLatTextBox.Location = new Point(260, 122);
            this.MaxLatTextBox.Name = "MaxLatTextBox";
            this.MaxLatTextBox.Size = new Size(74, 20);
            this.MaxLatTextBox.TabIndex = 3;
            this.MaxLatTextBox.WaterMarkColor = Color.Gray;
            this.MaxLatTextBox.WaterMarkText = "Max Latitude";
            this.LayersCheckedListBox.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.LayersCheckedListBox.CheckOnClick = true;
            this.LayersCheckedListBox.FormattingEnabled = true;
            this.LayersCheckedListBox.Location = new Point(78, 19);
            this.LayersCheckedListBox.Name = "LayersCheckedListBox";
            this.LayersCheckedListBox.Size = new Size(420, 79);
            this.LayersCheckedListBox.TabIndex = 1;
            this.button16.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.button16.Location = new Point(504, 19);
            this.button16.Name = "button16";
            this.button16.Size = new Size(101, 24);
            this.button16.TabIndex = 23;
            this.button16.Text = "Build";
            this.toolTip1.SetToolTip(this.button16, "Build Export from list of Ids");
            this.button16.Click += new System.EventHandler(this.BuildSpatialExport);
            this.label39.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            this.label39.AutoSize = true;
            this.label39.Location = new Point(11, 138);
            this.label39.Name = "label39";
            this.label39.Size = new Size(37, 13);
            this.label39.TabIndex = 2;
            this.label39.Text = "Extent";
            this.label39.TextAlign = ContentAlignment.MiddleRight;
            this.label40.AutoSize = true;
            this.label40.Location = new Point(9, 19);
            this.label40.Name = "label40";
            this.label40.Size = new Size(38, 13);
            this.label40.TabIndex = 0;
            this.label40.Text = "Layers";
            this.label40.TextAlign = ContentAlignment.MiddleRight;
            this.SpatialExportOverwriteFalseButton.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            this.SpatialExportOverwriteFalseButton.Location = new Point(550, 187);
            this.SpatialExportOverwriteFalseButton.Name = "SpatialExportOverwriteFalseButton";
            this.SpatialExportOverwriteFalseButton.Size = new Size(50, 20);
            this.SpatialExportOverwriteFalseButton.TabIndex = 28;
            this.SpatialExportOverwriteFalseButton.Text = "False";
            this.SpatialExportOverwriteTrueButton.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            this.SpatialExportOverwriteTrueButton.Checked = true;
            this.SpatialExportOverwriteTrueButton.Location = new Point(501, 187);
            this.SpatialExportOverwriteTrueButton.Name = "SpatialExportOverwriteTrueButton";
            this.SpatialExportOverwriteTrueButton.Size = new Size(50, 20);
            this.SpatialExportOverwriteTrueButton.TabIndex = 27;
            this.SpatialExportOverwriteTrueButton.TabStop = true;
            this.SpatialExportOverwriteTrueButton.Text = "True";
            this.label41.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            this.label41.AutoSize = true;
            this.label41.Location = new Point(519, 171);
            this.label41.Name = "label41";
            this.label41.Size = new Size(52, 13);
            this.label41.TabIndex = 26;
            this.label41.Text = "Overwrite";
            this.label41.TextAlign = ContentAlignment.MiddleRight;
            this.SpatialExportNameTextBox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.SpatialExportNameTextBox.Location = new Point(78, 187);
            this.SpatialExportNameTextBox.Name = "SpatialExportNameTextBox";
            this.SpatialExportNameTextBox.Size = new Size(417, 20);
            this.SpatialExportNameTextBox.TabIndex = 2;
            this.label42.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            this.label42.AutoSize = true;
            this.label42.Location = new Point(9, 190);
            this.label42.Name = "label42";
            this.label42.Size = new Size(49, 13);
            this.label42.TabIndex = 12;
            this.label42.Text = "Filename";
            this.label42.TextAlign = ContentAlignment.MiddleRight;
            this.dataTemplateTab.Controls.Add(this.groupBox26);
            this.dataTemplateTab.Location = new Point(4, 22);
            this.dataTemplateTab.Name = "dataTemplateTab";
            this.dataTemplateTab.Padding = new Padding(3);
            this.dataTemplateTab.Size = new Size(633, 403);
            this.dataTemplateTab.TabIndex = 5;
            this.dataTemplateTab.Text = "Data Template";
            this.dataTemplateTab.UseVisualStyleBackColor = true;
            this.groupBox26.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.groupBox26.Controls.Add(this.CustomTemplatesRadioButton);
            this.groupBox26.Controls.Add(this.StandardTemplatesRadioButton);
            this.groupBox26.Controls.Add(this.AllTemplatesRadioButton);
            this.groupBox26.Controls.Add(this.DataTemplateDataTypeComboBox);
            this.groupBox26.Controls.Add(this.label32);
            this.groupBox26.Controls.Add(this.label31);
            this.groupBox26.Controls.Add(this.label29);
            this.groupBox26.Controls.Add(this.LoadDataTemplateButton);
            this.groupBox26.Controls.Add(this.SaveDataTemplateButton);
            this.groupBox26.Controls.Add(this.DeleteDataTemplateButton);
            this.groupBox26.Controls.Add(this.DataTemplateDropdownList);
            this.groupBox26.Controls.Add(this.RefreshDataTemplatesButton);
            this.groupBox26.Controls.Add(this.DataTemplateTextBox);
            this.groupBox26.Location = new Point(10, 10);
            this.groupBox26.Name = "groupBox26";
            this.groupBox26.Size = new Size(613, 387);
            this.groupBox26.TabIndex = 21;
            this.groupBox26.TabStop = false;
            this.CustomTemplatesRadioButton.AutoSize = true;
            this.CustomTemplatesRadioButton.Location = new Point(233, 14);
            this.CustomTemplatesRadioButton.Name = "CustomTemplatesRadioButton";
            this.CustomTemplatesRadioButton.Size = new Size(60, 17);
            this.CustomTemplatesRadioButton.TabIndex = 33;
            this.CustomTemplatesRadioButton.Text = "Custom";
            this.CustomTemplatesRadioButton.UseVisualStyleBackColor = true;
            this.CustomTemplatesRadioButton.CheckedChanged += new System.EventHandler(this.DataTemplateParametersChanged);
            this.StandardTemplatesRadioButton.AutoSize = true;
            this.StandardTemplatesRadioButton.Location = new Point(145, 14);
            this.StandardTemplatesRadioButton.Name = "StandardTemplatesRadioButton";
            this.StandardTemplatesRadioButton.Size = new Size(68, 17);
            this.StandardTemplatesRadioButton.TabIndex = 32;
            this.StandardTemplatesRadioButton.Text = "Standard";
            this.StandardTemplatesRadioButton.UseVisualStyleBackColor = true;
            this.StandardTemplatesRadioButton.CheckedChanged += new System.EventHandler(this.DataTemplateParametersChanged);
            this.AllTemplatesRadioButton.AutoSize = true;
            this.AllTemplatesRadioButton.Checked = true;
            this.AllTemplatesRadioButton.Location = new Point(91, 14);
            this.AllTemplatesRadioButton.Name = "AllTemplatesRadioButton";
            this.AllTemplatesRadioButton.Size = new Size(36, 17);
            this.AllTemplatesRadioButton.TabIndex = 31;
            this.AllTemplatesRadioButton.TabStop = true;
            this.AllTemplatesRadioButton.Text = "All";
            this.AllTemplatesRadioButton.UseVisualStyleBackColor = true;
            this.AllTemplatesRadioButton.CheckedChanged += new System.EventHandler(this.DataTemplateParametersChanged);
            this.DataTemplateDataTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.DataTemplateDataTypeComboBox.FormattingEnabled = true;
            this.DataTemplateDataTypeComboBox.Items.AddRange(new object[]
			{
				"Well",
				"Production"
			});
            this.DataTemplateDataTypeComboBox.Location = new Point(91, 37);
            this.DataTemplateDataTypeComboBox.Name = "DataTemplateDataTypeComboBox";
            this.DataTemplateDataTypeComboBox.Size = new Size(121, 21);
            this.DataTemplateDataTypeComboBox.TabIndex = 30;
            this.DataTemplateDataTypeComboBox.SelectedValueChanged += new System.EventHandler(this.DataTemplateParametersChanged);
            this.label32.AccessibleDescription = "";
            this.label32.AutoSize = true;
            this.label32.Location = new Point(3, 16);
            this.label32.Name = "label32";
            this.label32.Size = new Size(78, 13);
            this.label32.TabIndex = 29;
            this.label32.Text = "Template Type";
            this.label32.TextAlign = ContentAlignment.MiddleRight;
            this.label31.AccessibleDescription = "";
            this.label31.AutoSize = true;
            this.label31.Location = new Point(3, 42);
            this.label31.Name = "label31";
            this.label31.Size = new Size(50, 13);
            this.label31.TabIndex = 28;
            this.label31.Text = "Datatype";
            this.label31.TextAlign = ContentAlignment.MiddleRight;
            this.label29.AccessibleDescription = "";
            this.label29.AutoSize = true;
            this.label29.Location = new Point(3, 68);
            this.label29.Name = "label29";
            this.label29.Size = new Size(77, 13);
            this.label29.TabIndex = 27;
            this.label29.Text = "Data Template";
            this.label29.TextAlign = ContentAlignment.MiddleRight;
            this.LoadDataTemplateButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.LoadDataTemplateButton.AutoSize = true;
            this.LoadDataTemplateButton.BackgroundImage = Resources.Load;
            this.LoadDataTemplateButton.BackgroundImageLayout = ImageLayout.Center;
            this.LoadDataTemplateButton.ImageAlign = ContentAlignment.TopLeft;
            this.LoadDataTemplateButton.Location = new Point(495, 61);
            this.LoadDataTemplateButton.Name = "LoadDataTemplateButton";
            this.LoadDataTemplateButton.Size = new Size(35, 24);
            this.LoadDataTemplateButton.TabIndex = 26;
            this.toolTip1.SetToolTip(this.LoadDataTemplateButton, "Get the Saved Query definition");
            this.LoadDataTemplateButton.UseVisualStyleBackColor = false;
            this.LoadDataTemplateButton.Click += new System.EventHandler(this.LoadDataTemplate);
            this.SaveDataTemplateButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.SaveDataTemplateButton.BackgroundImage = Resources.Save;
            this.SaveDataTemplateButton.BackgroundImageLayout = ImageLayout.Center;
            this.SaveDataTemplateButton.ImageAlign = ContentAlignment.TopLeft;
            this.SaveDataTemplateButton.Location = new Point(534, 61);
            this.SaveDataTemplateButton.Name = "SaveDataTemplateButton";
            this.SaveDataTemplateButton.Size = new Size(35, 24);
            this.SaveDataTemplateButton.TabIndex = 25;
            this.toolTip1.SetToolTip(this.SaveDataTemplateButton, "Save Criteria Xml");
            this.SaveDataTemplateButton.Click += new System.EventHandler(this.SaveDataTemplate);
            this.DeleteDataTemplateButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.DeleteDataTemplateButton.BackgroundImage = Resources.Delete;
            this.DeleteDataTemplateButton.BackgroundImageLayout = ImageLayout.Center;
            this.DeleteDataTemplateButton.ImageAlign = ContentAlignment.TopLeft;
            this.DeleteDataTemplateButton.Location = new Point(573, 61);
            this.DeleteDataTemplateButton.Name = "DeleteDataTemplateButton";
            this.DeleteDataTemplateButton.Size = new Size(35, 24);
            this.DeleteDataTemplateButton.TabIndex = 24;
            this.toolTip1.SetToolTip(this.DeleteDataTemplateButton, "Delete selected Saved Query");
            this.DeleteDataTemplateButton.Click += new System.EventHandler(this.DeleteDataTemplate);
            this.DataTemplateDropdownList.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            this.DataTemplateDropdownList.DropDownStyle = ComboBoxStyle.DropDownList;
            this.DataTemplateDropdownList.FormattingEnabled = true;
            this.DataTemplateDropdownList.Location = new Point(91, 64);
            this.DataTemplateDropdownList.Name = "DataTemplateDropdownList";
            this.DataTemplateDropdownList.Size = new Size(347, 21);
            this.DataTemplateDropdownList.TabIndex = 21;
            this.RefreshDataTemplatesButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.RefreshDataTemplatesButton.BackgroundImage = Resources.Refresh;
            this.RefreshDataTemplatesButton.BackgroundImageLayout = ImageLayout.Stretch;
            this.RefreshDataTemplatesButton.ImageAlign = ContentAlignment.TopLeft;
            this.RefreshDataTemplatesButton.Location = new Point(454, 61);
            this.RefreshDataTemplatesButton.Name = "RefreshDataTemplatesButton";
            this.RefreshDataTemplatesButton.Size = new Size(26, 24);
            this.RefreshDataTemplatesButton.TabIndex = 23;
            this.toolTip1.SetToolTip(this.RefreshDataTemplatesButton, "Refresh saved query list");
            this.RefreshDataTemplatesButton.Click += new System.EventHandler(this.DataTemplateParametersChanged);
            this.DataTemplateTextBox.AllowDrop = true;
            this.DataTemplateTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.DataTemplateTextBox.Location = new Point(6, 91);
            this.DataTemplateTextBox.Multiline = true;
            this.DataTemplateTextBox.Name = "DataTemplateTextBox";
            this.DataTemplateTextBox.ScrollBars = ScrollBars.Vertical;
            this.DataTemplateTextBox.Size = new Size(601, 290);
            this.DataTemplateTextBox.TabIndex = 22;
            this.DataTemplateTextBox.DragDrop += new DragEventHandler(this.DragDropHandler);
            this.DataTemplateTextBox.DragEnter += new DragEventHandler(this.DragEnterHandler);
            this.DataTemplateTextBox.KeyPress += new KeyPressEventHandler(this.KeyPressHandler);
            this.onelineExportTab.Controls.Add(this.groupBox21);
            this.onelineExportTab.Controls.Add(this.groupBox23);
            this.onelineExportTab.Controls.Add(this.groupBox25);
            this.onelineExportTab.Location = new Point(4, 22);
            this.onelineExportTab.Name = "onelineExportTab";
            this.onelineExportTab.Padding = new Padding(3);
            this.onelineExportTab.Size = new Size(633, 403);
            this.onelineExportTab.TabIndex = 6;
            this.onelineExportTab.Text = "Oneline Export";
            this.onelineExportTab.UseVisualStyleBackColor = true;
            this.groupBox21.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.groupBox21.Controls.Add(this.groupBox22);
            this.groupBox21.Controls.Add(this.OnelineJobIdTextBox);
            this.groupBox21.Controls.Add(this.OnelineStatusButton);
            this.groupBox21.Controls.Add(this.label21);
            this.groupBox21.Location = new Point(10, 232);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new Size(613, 52);
            this.groupBox21.TabIndex = 34;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "Check Oneline Export Status";
            this.groupBox22.Controls.Add(this.button6);
            this.groupBox22.Location = new Point(0, -70);
            this.groupBox22.Name = "groupBox22";
            this.groupBox22.Size = new Size(410, 70);
            this.groupBox22.TabIndex = 42;
            this.groupBox22.TabStop = false;
            this.button6.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.button6.Location = new Point(310, 15);
            this.button6.Name = "button6";
            this.button6.Size = new Size(85, 30);
            this.button6.TabIndex = 23;
            this.button6.Text = "Build";
            this.OnelineJobIdTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            this.OnelineJobIdTextBox.Location = new Point(80, 17);
            this.OnelineJobIdTextBox.Name = "OnelineJobIdTextBox";
            this.OnelineJobIdTextBox.Size = new Size(418, 20);
            this.OnelineJobIdTextBox.TabIndex = 33;
            this.OnelineStatusButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.OnelineStatusButton.ImageAlign = ContentAlignment.MiddleLeft;
            this.OnelineStatusButton.Location = new Point(504, 14);
            this.OnelineStatusButton.Name = "OnelineStatusButton";
            this.OnelineStatusButton.Size = new Size(101, 24);
            this.OnelineStatusButton.TabIndex = 41;
            this.OnelineStatusButton.Text = "&Status";
            this.toolTip1.SetToolTip(this.OnelineStatusButton, "Get the status of the specified job id.");
            this.OnelineStatusButton.Click += new System.EventHandler(this.GetOnelineStatus);
            this.label21.AutoSize = true;
            this.label21.Location = new Point(9, 20);
            this.label21.Name = "label21";
            this.label21.Size = new Size(38, 13);
            this.label21.TabIndex = 39;
            this.label21.Text = "Job ID";
            this.groupBox23.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.groupBox23.Controls.Add(this.label34);
            this.groupBox23.Controls.Add(this.LaunchOnelineFolderCheckbox);
            this.groupBox23.Controls.Add(this.RefreshCompleteOnelinesListButton);
            this.groupBox23.Controls.Add(this.CompletedOnelinesComboBox);
            this.groupBox23.Controls.Add(this.OnelineCompressCheckbox);
            this.groupBox23.Controls.Add(this.groupBox24);
            this.groupBox23.Controls.Add(this.DeleteOnelineButton);
            this.groupBox23.Controls.Add(this.label22);
            this.groupBox23.Controls.Add(this.RetrieveOnelineButton);
            this.groupBox23.Location = new Point(10, 290);
            this.groupBox23.Name = "groupBox23";
            this.groupBox23.Size = new Size(613, 102);
            this.groupBox23.TabIndex = 33;
            this.groupBox23.TabStop = false;
            this.groupBox23.Text = "Retrieve / Delete Oneline Exports";
            this.label34.AutoSize = true;
            this.label34.Location = new Point(9, 83);
            this.label34.Name = "label34";
            this.label34.Size = new Size(286, 13);
            this.label34.TabIndex = 48;
            this.label34.Text = "* Exports and Oneline Exports are stored in the same folder.";
            this.LaunchOnelineFolderCheckbox.Checked = true;
            this.LaunchOnelineFolderCheckbox.CheckState = CheckState.Checked;
            this.LaunchOnelineFolderCheckbox.Location = new Point(178, 43);
            this.LaunchOnelineFolderCheckbox.Name = "LaunchOnelineFolderCheckbox";
            this.LaunchOnelineFolderCheckbox.Size = new Size(104, 24);
            this.LaunchOnelineFolderCheckbox.TabIndex = 46;
            this.LaunchOnelineFolderCheckbox.Text = "Launch Explorer";
            this.RefreshCompleteOnelinesListButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.RefreshCompleteOnelinesListButton.BackgroundImage = Resources.Refresh;
            this.RefreshCompleteOnelinesListButton.BackgroundImageLayout = ImageLayout.Stretch;
            this.RefreshCompleteOnelinesListButton.ImageAlign = ContentAlignment.TopLeft;
            this.RefreshCompleteOnelinesListButton.Location = new Point(504, 14);
            this.RefreshCompleteOnelinesListButton.Name = "RefreshCompleteOnelinesListButton";
            this.RefreshCompleteOnelinesListButton.Size = new Size(26, 24);
            this.RefreshCompleteOnelinesListButton.TabIndex = 45;
            this.RefreshCompleteOnelinesListButton.Click += new System.EventHandler(this.RefreshOnelinesList);
            this.CompletedOnelinesComboBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            this.CompletedOnelinesComboBox.FormattingEnabled = true;
            this.CompletedOnelinesComboBox.Location = new Point(78, 16);
            this.CompletedOnelinesComboBox.Name = "CompletedOnelinesComboBox";
            this.CompletedOnelinesComboBox.Size = new Size(420, 21);
            this.CompletedOnelinesComboBox.TabIndex = 44;
            this.OnelineCompressCheckbox.Checked = true;
            this.OnelineCompressCheckbox.CheckState = CheckState.Checked;
            this.OnelineCompressCheckbox.Location = new Point(78, 43);
            this.OnelineCompressCheckbox.Name = "OnelineCompressCheckbox";
            this.OnelineCompressCheckbox.Size = new Size(104, 24);
            this.OnelineCompressCheckbox.TabIndex = 43;
            this.OnelineCompressCheckbox.Text = "Compress";
            this.groupBox24.Controls.Add(this.button10);
            this.groupBox24.Location = new Point(0, -70);
            this.groupBox24.Name = "groupBox24";
            this.groupBox24.Size = new Size(410, 70);
            this.groupBox24.TabIndex = 42;
            this.groupBox24.TabStop = false;
            this.button10.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.button10.Location = new Point(310, 15);
            this.button10.Name = "button10";
            this.button10.Size = new Size(85, 30);
            this.button10.TabIndex = 23;
            this.button10.Text = "Build";
            this.DeleteOnelineButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.DeleteOnelineButton.Location = new Point(504, 72);
            this.DeleteOnelineButton.Name = "DeleteOnelineButton";
            this.DeleteOnelineButton.Size = new Size(101, 24);
            this.DeleteOnelineButton.TabIndex = 40;
            this.DeleteOnelineButton.Text = "&Delete";
            this.toolTip1.SetToolTip(this.DeleteOnelineButton, "Delete selected job on server.");
            this.DeleteOnelineButton.Click += new System.EventHandler(this.DeleteOneline);
            this.label22.AutoSize = true;
            this.label22.Location = new Point(9, 20);
            this.label22.Name = "label22";
            this.label22.Size = new Size(29, 13);
            this.label22.TabIndex = 39;
            this.label22.Text = "Jobs";
            this.RetrieveOnelineButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.RetrieveOnelineButton.Location = new Point(504, 42);
            this.RetrieveOnelineButton.Name = "RetrieveOnelineButton";
            this.RetrieveOnelineButton.Size = new Size(101, 24);
            this.RetrieveOnelineButton.TabIndex = 38;
            this.RetrieveOnelineButton.Text = "&Retrieve";
            this.toolTip1.SetToolTip(this.RetrieveOnelineButton, "Retrieve the specified export from server.");
            this.RetrieveOnelineButton.Click += new System.EventHandler(this.RetrieveOneline);
            this.groupBox25.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.groupBox25.Controls.Add(this.BuildOnelineFromQueryButton);
            this.groupBox25.Controls.Add(this.BuildOnelineButton);
            this.groupBox25.Controls.Add(this.OnelineTemplateDropDownList);
            this.groupBox25.Controls.Add(this.label28);
            this.groupBox25.Controls.Add(this.OnelineFormatTypeDropDownList);
            this.groupBox25.Controls.Add(this.OnelineDatatypeDropDownList);
            this.groupBox25.Controls.Add(this.label23);
            this.groupBox25.Controls.Add(this.label24);
            this.groupBox25.Controls.Add(this.label25);
            this.groupBox25.Controls.Add(this.OnelineIdsTextBox);
            this.groupBox25.Controls.Add(this.OnelineOverwriteFalseButton);
            this.groupBox25.Controls.Add(this.OnelineOverwriteTrueButton);
            this.groupBox25.Controls.Add(this.label26);
            this.groupBox25.Controls.Add(this.OnelineNameTextBox);
            this.groupBox25.Controls.Add(this.label27);
            this.groupBox25.Location = new Point(10, 10);
            this.groupBox25.Name = "groupBox25";
            this.groupBox25.Size = new Size(613, 213);
            this.groupBox25.TabIndex = 32;
            this.groupBox25.TabStop = false;
            this.groupBox25.Text = "Build Oneline Export";
            this.BuildOnelineFromQueryButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.BuildOnelineFromQueryButton.Location = new Point(504, 49);
            this.BuildOnelineFromQueryButton.Name = "BuildOnelineFromQueryButton";
            this.BuildOnelineFromQueryButton.Size = new Size(101, 27);
            this.BuildOnelineFromQueryButton.TabIndex = 53;
            this.BuildOnelineFromQueryButton.Text = "Build From Query";
            this.toolTip1.SetToolTip(this.BuildOnelineFromQueryButton, "Build Export from Query / CriteriaXml on Query tab");
            this.BuildOnelineFromQueryButton.Click += new System.EventHandler(this.BuildOnelineFromQuery);
            this.BuildOnelineButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.BuildOnelineButton.Location = new Point(504, 19);
            this.BuildOnelineButton.Name = "BuildOnelineButton";
            this.BuildOnelineButton.Size = new Size(101, 24);
            this.BuildOnelineButton.TabIndex = 23;
            this.BuildOnelineButton.Text = "Build";
            this.toolTip1.SetToolTip(this.BuildOnelineButton, "Build Export from list of Ids");
            this.BuildOnelineButton.Click += new System.EventHandler(this.BuildOneline);
            this.OnelineTemplateDropDownList.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.OnelineTemplateDropDownList.DropDownStyle = ComboBoxStyle.DropDownList;
            this.OnelineTemplateDropDownList.FormattingEnabled = true;
            this.OnelineTemplateDropDownList.Location = new Point(78, 129);
            this.OnelineTemplateDropDownList.Name = "OnelineTemplateDropDownList";
            this.OnelineTemplateDropDownList.Size = new Size(420, 21);
            this.OnelineTemplateDropDownList.TabIndex = 52;
            this.OnelineTemplateDropDownList.SelectedIndexChanging += new CancelEventHandler(this.OnelineTemplateDropDownListSelectedIndexChanging);
            this.label28.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            this.label28.AutoSize = true;
            this.label28.Location = new Point(342, 190);
            this.label28.Name = "label28";
            this.label28.Size = new Size(50, 13);
            this.label28.TabIndex = 51;
            this.label28.Text = "File Type";
            this.label28.TextAlign = ContentAlignment.MiddleRight;
            this.OnelineFormatTypeDropDownList.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            this.OnelineFormatTypeDropDownList.DropDownStyle = ComboBoxStyle.DropDownList;
            this.OnelineFormatTypeDropDownList.FormattingEnabled = true;
            this.OnelineFormatTypeDropDownList.Location = new Point(398, 187);
            this.OnelineFormatTypeDropDownList.Name = "OnelineFormatTypeDropDownList";
            this.OnelineFormatTypeDropDownList.Size = new Size(100, 21);
            this.OnelineFormatTypeDropDownList.TabIndex = 50;
            this.OnelineDatatypeDropDownList.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.OnelineDatatypeDropDownList.DropDownStyle = ComboBoxStyle.DropDownList;
            this.OnelineDatatypeDropDownList.FormattingEnabled = true;
            this.OnelineDatatypeDropDownList.Location = new Point(78, 102);
            this.OnelineDatatypeDropDownList.Name = "OnelineDatatypeDropDownList";
            this.OnelineDatatypeDropDownList.Size = new Size(420, 21);
            this.OnelineDatatypeDropDownList.TabIndex = 40;
            this.OnelineDatatypeDropDownList.SelectedIndexChanged += new System.EventHandler(this.OnelineDatatypeDropDownListSelectedIndexChanged);
            this.label23.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            this.label23.AutoSize = true;
            this.label23.Location = new Point(9, 131);
            this.label23.Name = "label23";
            this.label23.Size = new Size(51, 13);
            this.label23.TabIndex = 39;
            this.label23.Text = "Template";
            this.label23.TextAlign = ContentAlignment.MiddleRight;
            this.label24.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            this.label24.AutoSize = true;
            this.label24.Location = new Point(9, 104);
            this.label24.Name = "label24";
            this.label24.Size = new Size(50, 13);
            this.label24.TabIndex = 38;
            this.label24.Text = "Datatype";
            this.label24.TextAlign = ContentAlignment.MiddleRight;
            this.label25.AutoSize = true;
            this.label25.Location = new Point(9, 19);
            this.label25.Name = "label25";
            this.label25.Size = new Size(21, 13);
            this.label25.TabIndex = 37;
            this.label25.Text = "Ids";
            this.label25.TextAlign = ContentAlignment.MiddleRight;
            this.OnelineIdsTextBox.AllowDrop = true;
            this.OnelineIdsTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.OnelineIdsTextBox.Location = new Point(78, 19);
            this.OnelineIdsTextBox.MaxLength = 5000000;
            this.OnelineIdsTextBox.Multiline = true;
            this.OnelineIdsTextBox.Name = "OnelineIdsTextBox";
            this.OnelineIdsTextBox.Size = new Size(420, 76);
            this.OnelineIdsTextBox.TabIndex = 36;
            this.OnelineIdsTextBox.KeyPress += new KeyPressEventHandler(this.KeyPressHandler);
            this.OnelineOverwriteFalseButton.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            this.OnelineOverwriteFalseButton.Location = new Point(129, 187);
            this.OnelineOverwriteFalseButton.Name = "OnelineOverwriteFalseButton";
            this.OnelineOverwriteFalseButton.Size = new Size(50, 20);
            this.OnelineOverwriteFalseButton.TabIndex = 28;
            this.OnelineOverwriteFalseButton.Text = "False";
            this.OnelineOverwriteTrueButton.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            this.OnelineOverwriteTrueButton.Checked = true;
            this.OnelineOverwriteTrueButton.Location = new Point(80, 187);
            this.OnelineOverwriteTrueButton.Name = "OnelineOverwriteTrueButton";
            this.OnelineOverwriteTrueButton.Size = new Size(50, 20);
            this.OnelineOverwriteTrueButton.TabIndex = 27;
            this.OnelineOverwriteTrueButton.TabStop = true;
            this.OnelineOverwriteTrueButton.Text = "True";
            this.label26.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            this.label26.AutoSize = true;
            this.label26.Location = new Point(9, 187);
            this.label26.Name = "label26";
            this.label26.Size = new Size(52, 13);
            this.label26.TabIndex = 26;
            this.label26.Text = "Overwrite";
            this.label26.TextAlign = ContentAlignment.MiddleRight;
            this.OnelineNameTextBox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.OnelineNameTextBox.Location = new Point(78, 157);
            this.OnelineNameTextBox.Name = "OnelineNameTextBox";
            this.OnelineNameTextBox.Size = new Size(420, 20);
            this.OnelineNameTextBox.TabIndex = 24;
            this.label27.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            this.label27.AutoSize = true;
            this.label27.Location = new Point(9, 160);
            this.label27.Name = "label27";
            this.label27.Size = new Size(49, 13);
            this.label27.TabIndex = 25;
            this.label27.Text = "Filename";
            this.label27.TextAlign = ContentAlignment.MiddleRight;
            this.tabGraph.Controls.Add(this.groupBox17);
            this.tabGraph.Controls.Add(this.groupBox12);
            this.tabGraph.Controls.Add(this.groupBox11);
            this.tabGraph.Location = new Point(4, 22);
            this.tabGraph.Name = "tabGraph";
            this.tabGraph.Size = new Size(633, 403);
            this.tabGraph.TabIndex = 3;
            this.tabGraph.Text = "Graph";
            this.tabGraph.UseVisualStyleBackColor = true;
            this.groupBox17.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.groupBox17.Controls.Add(this.groupBox18);
            this.groupBox17.Controls.Add(this.label17);
            this.groupBox17.Controls.Add(this.GraphJobIdTextBox);
            this.groupBox17.Controls.Add(this.GraphStatusButton);
            this.groupBox17.Location = new Point(10, 232);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new Size(613, 52);
            this.groupBox17.TabIndex = 32;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "Check Graph Status";
            this.groupBox18.Controls.Add(this.button5);
            this.groupBox18.Location = new Point(0, -70);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new Size(410, 70);
            this.groupBox18.TabIndex = 42;
            this.groupBox18.TabStop = false;
            this.button5.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.button5.Location = new Point(310, 15);
            this.button5.Name = "button5";
            this.button5.Size = new Size(85, 30);
            this.button5.TabIndex = 23;
            this.button5.Text = "Build";
            this.label17.AutoSize = true;
            this.label17.Location = new Point(9, 20);
            this.label17.Name = "label17";
            this.label17.Size = new Size(38, 13);
            this.label17.TabIndex = 39;
            this.label17.Text = "Job ID";
            this.GraphJobIdTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            this.GraphJobIdTextBox.Location = new Point(80, 17);
            this.GraphJobIdTextBox.Name = "GraphJobIdTextBox";
            this.GraphJobIdTextBox.Size = new Size(418, 20);
            this.GraphJobIdTextBox.TabIndex = 33;
            this.GraphJobIdTextBox.KeyPress += new KeyPressEventHandler(this.KeyPressHandler);
            this.GraphStatusButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.GraphStatusButton.Location = new Point(504, 14);
            this.GraphStatusButton.Name = "GraphStatusButton";
            this.GraphStatusButton.Size = new Size(101, 24);
            this.GraphStatusButton.TabIndex = 41;
            this.GraphStatusButton.Text = "&Status";
            this.GraphStatusButton.Click += new System.EventHandler(this.GetGraphStatus);
            this.groupBox12.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.groupBox12.Controls.Add(this.label19);
            this.groupBox12.Controls.Add(this.LaunchGraphFolderCheckbox);
            this.groupBox12.Controls.Add(this.RefreshCompleteGraphsListButton);
            this.groupBox12.Controls.Add(this.CompletedGraphsComboBox);
            this.groupBox12.Controls.Add(this.GraphCompressCheckbox);
            this.groupBox12.Controls.Add(this.groupBox13);
            this.groupBox12.Controls.Add(this.DeleteGraphButton);
            this.groupBox12.Controls.Add(this.RetrieveGraphButton);
            this.groupBox12.Location = new Point(10, 290);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new Size(613, 102);
            this.groupBox12.TabIndex = 30;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Retrieve / Delete Graphs";
            this.label19.AutoSize = true;
            this.label19.Location = new Point(9, 20);
            this.label19.Name = "label19";
            this.label19.Size = new Size(29, 13);
            this.label19.TabIndex = 50;
            this.label19.Text = "Jobs";
            this.LaunchGraphFolderCheckbox.Checked = true;
            this.LaunchGraphFolderCheckbox.CheckState = CheckState.Checked;
            this.LaunchGraphFolderCheckbox.Location = new Point(178, 43);
            this.LaunchGraphFolderCheckbox.Name = "LaunchGraphFolderCheckbox";
            this.LaunchGraphFolderCheckbox.Size = new Size(104, 24);
            this.LaunchGraphFolderCheckbox.TabIndex = 49;
            this.LaunchGraphFolderCheckbox.Text = "Launch Explorer";
            this.RefreshCompleteGraphsListButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.RefreshCompleteGraphsListButton.BackgroundImage = Resources.Refresh;
            this.RefreshCompleteGraphsListButton.BackgroundImageLayout = ImageLayout.Stretch;
            this.RefreshCompleteGraphsListButton.ImageAlign = ContentAlignment.TopLeft;
            this.RefreshCompleteGraphsListButton.Location = new Point(504, 14);
            this.RefreshCompleteGraphsListButton.Name = "RefreshCompleteGraphsListButton";
            this.RefreshCompleteGraphsListButton.Size = new Size(26, 24);
            this.RefreshCompleteGraphsListButton.TabIndex = 48;
            this.RefreshCompleteGraphsListButton.Click += new System.EventHandler(this.RefreshGraphsList);
            this.CompletedGraphsComboBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            this.CompletedGraphsComboBox.FormattingEnabled = true;
            this.CompletedGraphsComboBox.Location = new Point(78, 16);
            this.CompletedGraphsComboBox.Name = "CompletedGraphsComboBox";
            this.CompletedGraphsComboBox.Size = new Size(420, 21);
            this.CompletedGraphsComboBox.TabIndex = 47;
            this.GraphCompressCheckbox.Checked = true;
            this.GraphCompressCheckbox.CheckState = CheckState.Checked;
            this.GraphCompressCheckbox.Location = new Point(78, 43);
            this.GraphCompressCheckbox.Name = "GraphCompressCheckbox";
            this.GraphCompressCheckbox.Size = new Size(80, 24);
            this.GraphCompressCheckbox.TabIndex = 43;
            this.GraphCompressCheckbox.Text = "Compress";
            this.groupBox13.Controls.Add(this.button2);
            this.groupBox13.Location = new Point(0, -70);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new Size(410, 70);
            this.groupBox13.TabIndex = 42;
            this.groupBox13.TabStop = false;
            this.button2.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.button2.Location = new Point(310, 15);
            this.button2.Name = "button2";
            this.button2.Size = new Size(85, 30);
            this.button2.TabIndex = 23;
            this.button2.Text = "Build";
            this.DeleteGraphButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.DeleteGraphButton.Location = new Point(504, 72);
            this.DeleteGraphButton.Name = "DeleteGraphButton";
            this.DeleteGraphButton.Size = new Size(101, 24);
            this.DeleteGraphButton.TabIndex = 40;
            this.DeleteGraphButton.Text = "&Delete";
            this.DeleteGraphButton.Click += new System.EventHandler(this.DeleteGraph);
            this.RetrieveGraphButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.RetrieveGraphButton.Location = new Point(504, 42);
            this.RetrieveGraphButton.Name = "RetrieveGraphButton";
            this.RetrieveGraphButton.Size = new Size(101, 24);
            this.RetrieveGraphButton.TabIndex = 38;
            this.RetrieveGraphButton.Text = "&Retrieve";
            this.RetrieveGraphButton.Click += new System.EventHandler(this.RetrieveGraph);
            this.groupBox11.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.groupBox11.Controls.Add(this.label14);
            this.groupBox11.Controls.Add(this.GraphFormatDropDownList);
            this.groupBox11.Controls.Add(this.BuildGraphFromQueryButton);
            this.groupBox11.Controls.Add(this.GraphDatatypeDropDownList);
            this.groupBox11.Controls.Add(this.GraphTemplateDropDownList);
            this.groupBox11.Controls.Add(this.label7);
            this.groupBox11.Controls.Add(this.label8);
            this.groupBox11.Controls.Add(this.label9);
            this.groupBox11.Controls.Add(this.GraphIdsTextBox);
            this.groupBox11.Controls.Add(this.OverwriteTypeFalseButton);
            this.groupBox11.Controls.Add(this.GraphOverwriteTrueButton);
            this.groupBox11.Controls.Add(this.lblGraphOverwriteType);
            this.groupBox11.Controls.Add(this.GraphNameTextBox);
            this.groupBox11.Controls.Add(this.lblGraphName);
            this.groupBox11.Controls.Add(this.BuildGraphButton);
            this.groupBox11.Location = new Point(10, 10);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new Size(613, 213);
            this.groupBox11.TabIndex = 28;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Build Graph";
            this.label14.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            this.label14.AutoSize = true;
            this.label14.Location = new Point(408, 190);
            this.label14.Name = "label14";
            this.label14.Size = new Size(39, 13);
            this.label14.TabIndex = 49;
            this.label14.Text = "Format";
            this.label14.TextAlign = ContentAlignment.MiddleRight;
            this.GraphFormatDropDownList.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            this.GraphFormatDropDownList.DropDownStyle = ComboBoxStyle.DropDownList;
            this.GraphFormatDropDownList.FormattingEnabled = true;
            this.GraphFormatDropDownList.Location = new Point(447, 187);
            this.GraphFormatDropDownList.Name = "GraphFormatDropDownList";
            this.GraphFormatDropDownList.Size = new Size(51, 21);
            this.GraphFormatDropDownList.TabIndex = 48;
            this.BuildGraphFromQueryButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.BuildGraphFromQueryButton.Location = new Point(504, 49);
            this.BuildGraphFromQueryButton.Name = "BuildGraphFromQueryButton";
            this.BuildGraphFromQueryButton.Size = new Size(101, 27);
            this.BuildGraphFromQueryButton.TabIndex = 47;
            this.BuildGraphFromQueryButton.Text = "Build From Query";
            this.toolTip1.SetToolTip(this.BuildGraphFromQueryButton, "Build Graph(s) from Query / CriteriaXml on Query tab");
            this.BuildGraphFromQueryButton.Click += new System.EventHandler(this.BuildGraphFromQuery);
            this.GraphDatatypeDropDownList.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.GraphDatatypeDropDownList.DropDownStyle = ComboBoxStyle.DropDownList;
            this.GraphDatatypeDropDownList.FormattingEnabled = true;
            this.GraphDatatypeDropDownList.Location = new Point(78, 102);
            this.GraphDatatypeDropDownList.Name = "GraphDatatypeDropDownList";
            this.GraphDatatypeDropDownList.Size = new Size(420, 21);
            this.GraphDatatypeDropDownList.TabIndex = 46;
            this.GraphDatatypeDropDownList.SelectedValueChanged += new System.EventHandler(this.GraphDatatypeDropDownListSelectedValueChanged);
            this.GraphTemplateDropDownList.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.GraphTemplateDropDownList.DropDownStyle = ComboBoxStyle.DropDownList;
            this.GraphTemplateDropDownList.FormattingEnabled = true;
            this.GraphTemplateDropDownList.Location = new Point(78, 129);
            this.GraphTemplateDropDownList.Name = "GraphTemplateDropDownList";
            this.GraphTemplateDropDownList.Size = new Size(420, 21);
            this.GraphTemplateDropDownList.TabIndex = 41;
            this.label7.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            this.label7.AutoSize = true;
            this.label7.Location = new Point(9, 131);
            this.label7.Name = "label7";
            this.label7.Size = new Size(51, 13);
            this.label7.TabIndex = 45;
            this.label7.Text = "Template";
            this.label7.TextAlign = ContentAlignment.MiddleRight;
            this.label8.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            this.label8.AutoSize = true;
            this.label8.Location = new Point(9, 104);
            this.label8.Name = "label8";
            this.label8.Size = new Size(50, 13);
            this.label8.TabIndex = 44;
            this.label8.Text = "Datatype";
            this.label8.TextAlign = ContentAlignment.MiddleRight;
            this.label9.AutoSize = true;
            this.label9.Location = new Point(9, 19);
            this.label9.Name = "label9";
            this.label9.Size = new Size(21, 13);
            this.label9.TabIndex = 43;
            this.label9.Text = "Ids";
            this.label9.TextAlign = ContentAlignment.MiddleRight;
            this.GraphIdsTextBox.AllowDrop = true;
            this.GraphIdsTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.GraphIdsTextBox.Location = new Point(78, 19);
            this.GraphIdsTextBox.Multiline = true;
            this.GraphIdsTextBox.Name = "GraphIdsTextBox";
            this.GraphIdsTextBox.Size = new Size(420, 74);
            this.GraphIdsTextBox.TabIndex = 42;
            this.GraphIdsTextBox.DragDrop += new DragEventHandler(this.DragDropHandler);
            this.GraphIdsTextBox.DragEnter += new DragEventHandler(this.DragEnterHandler);
            this.GraphIdsTextBox.KeyPress += new KeyPressEventHandler(this.KeyPressHandler);
            this.OverwriteTypeFalseButton.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            this.OverwriteTypeFalseButton.Location = new Point(129, 187);
            this.OverwriteTypeFalseButton.Name = "OverwriteTypeFalseButton";
            this.OverwriteTypeFalseButton.Size = new Size(50, 20);
            this.OverwriteTypeFalseButton.TabIndex = 28;
            this.OverwriteTypeFalseButton.Text = "False";
            this.GraphOverwriteTrueButton.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            this.GraphOverwriteTrueButton.Checked = true;
            this.GraphOverwriteTrueButton.Location = new Point(80, 187);
            this.GraphOverwriteTrueButton.Name = "GraphOverwriteTrueButton";
            this.GraphOverwriteTrueButton.Size = new Size(50, 20);
            this.GraphOverwriteTrueButton.TabIndex = 27;
            this.GraphOverwriteTrueButton.TabStop = true;
            this.GraphOverwriteTrueButton.Text = "True";
            this.lblGraphOverwriteType.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            this.lblGraphOverwriteType.AutoSize = true;
            this.lblGraphOverwriteType.Location = new Point(9, 187);
            this.lblGraphOverwriteType.Name = "lblGraphOverwriteType";
            this.lblGraphOverwriteType.Size = new Size(52, 13);
            this.lblGraphOverwriteType.TabIndex = 26;
            this.lblGraphOverwriteType.Text = "Overwrite";
            this.lblGraphOverwriteType.TextAlign = ContentAlignment.MiddleRight;
            this.GraphNameTextBox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.GraphNameTextBox.Location = new Point(78, 157);
            this.GraphNameTextBox.Name = "GraphNameTextBox";
            this.GraphNameTextBox.Size = new Size(420, 20);
            this.GraphNameTextBox.TabIndex = 24;
            this.GraphNameTextBox.KeyPress += new KeyPressEventHandler(this.KeyPressHandler);
            this.lblGraphName.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            this.lblGraphName.AutoSize = true;
            this.lblGraphName.Location = new Point(9, 160);
            this.lblGraphName.Name = "lblGraphName";
            this.lblGraphName.Size = new Size(49, 13);
            this.lblGraphName.TabIndex = 25;
            this.lblGraphName.Text = "Filename";
            this.lblGraphName.TextAlign = ContentAlignment.MiddleRight;
            this.BuildGraphButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.BuildGraphButton.Location = new Point(504, 19);
            this.BuildGraphButton.Name = "BuildGraphButton";
            this.BuildGraphButton.Size = new Size(101, 24);
            this.BuildGraphButton.TabIndex = 23;
            this.BuildGraphButton.Text = "Build";
            this.BuildGraphButton.Click += new System.EventHandler(this.BuildGraph);
            this.reportTab.Controls.Add(this.groupBox19);
            this.reportTab.Controls.Add(this.groupBox2);
            this.reportTab.Controls.Add(this.groupBox1);
            this.reportTab.Location = new Point(4, 22);
            this.reportTab.Name = "reportTab";
            this.reportTab.Size = new Size(633, 403);
            this.reportTab.TabIndex = 0;
            this.reportTab.Text = "Report";
            this.reportTab.UseVisualStyleBackColor = true;
            this.groupBox19.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.groupBox19.Controls.Add(this.groupBox20);
            this.groupBox19.Controls.Add(this.ReportJobIdTextBox);
            this.groupBox19.Controls.Add(this.ReportStatusButton);
            this.groupBox19.Controls.Add(this.label20);
            this.groupBox19.Location = new Point(10, 232);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new Size(613, 52);
            this.groupBox19.TabIndex = 32;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "Check Report Status";
            this.groupBox20.Controls.Add(this.button7);
            this.groupBox20.Location = new Point(0, -70);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.Size = new Size(410, 70);
            this.groupBox20.TabIndex = 42;
            this.groupBox20.TabStop = false;
            this.button7.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.button7.Location = new Point(310, 15);
            this.button7.Name = "button7";
            this.button7.Size = new Size(85, 30);
            this.button7.TabIndex = 23;
            this.button7.Text = "Build";
            this.ReportJobIdTextBox.Location = new Point(80, 17);
            this.ReportJobIdTextBox.Name = "ReportJobIdTextBox";
            this.ReportJobIdTextBox.Size = new Size(213, 20);
            this.ReportJobIdTextBox.TabIndex = 33;
            this.ReportJobIdTextBox.KeyPress += new KeyPressEventHandler(this.KeyPressHandler);
            this.ReportStatusButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.ReportStatusButton.Location = new Point(504, 14);
            this.ReportStatusButton.Name = "ReportStatusButton";
            this.ReportStatusButton.Size = new Size(101, 24);
            this.ReportStatusButton.TabIndex = 41;
            this.ReportStatusButton.Text = "&Status";
            this.ReportStatusButton.Click += new System.EventHandler(this.GetReportStatus);
            this.label20.AutoSize = true;
            this.label20.Location = new Point(9, 20);
            this.label20.Name = "label20";
            this.label20.Size = new Size(38, 13);
            this.label20.TabIndex = 39;
            this.label20.Text = "Job ID";
            this.groupBox2.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.groupBox2.Controls.Add(this.LaunchReportFolderCheckbox);
            this.groupBox2.Controls.Add(this.RefreshCompleteReportsListButton);
            this.groupBox2.Controls.Add(this.CompletedReportsComboBox);
            this.groupBox2.Controls.Add(this.ReportCompressCheckbox);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.DeleteReportButton);
            this.groupBox2.Controls.Add(this.lblReportJobID);
            this.groupBox2.Controls.Add(this.RetrieveReportButton);
            this.groupBox2.Location = new Point(10, 290);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(613, 102);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Retrieve / Delete Reports";
            this.LaunchReportFolderCheckbox.Checked = true;
            this.LaunchReportFolderCheckbox.CheckState = CheckState.Checked;
            this.LaunchReportFolderCheckbox.Location = new Point(178, 43);
            this.LaunchReportFolderCheckbox.Name = "LaunchReportFolderCheckbox";
            this.LaunchReportFolderCheckbox.Size = new Size(104, 24);
            this.LaunchReportFolderCheckbox.TabIndex = 52;
            this.LaunchReportFolderCheckbox.Text = "Launch Explorer";
            this.RefreshCompleteReportsListButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.RefreshCompleteReportsListButton.BackgroundImage = Resources.Refresh;
            this.RefreshCompleteReportsListButton.BackgroundImageLayout = ImageLayout.Stretch;
            this.RefreshCompleteReportsListButton.ImageAlign = ContentAlignment.TopLeft;
            this.RefreshCompleteReportsListButton.Location = new Point(504, 14);
            this.RefreshCompleteReportsListButton.Name = "RefreshCompleteReportsListButton";
            this.RefreshCompleteReportsListButton.Size = new Size(26, 24);
            this.RefreshCompleteReportsListButton.TabIndex = 51;
            this.RefreshCompleteReportsListButton.Click += new System.EventHandler(this.RefreshReportsList);
            this.CompletedReportsComboBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            this.CompletedReportsComboBox.FormattingEnabled = true;
            this.CompletedReportsComboBox.Location = new Point(78, 16);
            this.CompletedReportsComboBox.Name = "CompletedReportsComboBox";
            this.CompletedReportsComboBox.Size = new Size(420, 21);
            this.CompletedReportsComboBox.TabIndex = 50;
            this.ReportCompressCheckbox.Checked = true;
            this.ReportCompressCheckbox.CheckState = CheckState.Checked;
            this.ReportCompressCheckbox.Location = new Point(78, 43);
            this.ReportCompressCheckbox.Name = "ReportCompressCheckbox";
            this.ReportCompressCheckbox.Size = new Size(80, 24);
            this.ReportCompressCheckbox.TabIndex = 43;
            this.ReportCompressCheckbox.Text = "Compress";
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Location = new Point(0, -70);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(410, 70);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.button1.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.button1.Location = new Point(310, 15);
            this.button1.Name = "button1";
            this.button1.Size = new Size(85, 30);
            this.button1.TabIndex = 23;
            this.button1.Text = "Build";
            this.DeleteReportButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.DeleteReportButton.Location = new Point(504, 72);
            this.DeleteReportButton.Name = "DeleteReportButton";
            this.DeleteReportButton.Size = new Size(101, 24);
            this.DeleteReportButton.TabIndex = 40;
            this.DeleteReportButton.Text = "&Delete";
            this.DeleteReportButton.Click += new System.EventHandler(this.DeleteReport);
            this.lblReportJobID.AutoSize = true;
            this.lblReportJobID.Location = new Point(9, 20);
            this.lblReportJobID.Name = "lblReportJobID";
            this.lblReportJobID.Size = new Size(29, 13);
            this.lblReportJobID.TabIndex = 39;
            this.lblReportJobID.Text = "Jobs";
            this.RetrieveReportButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.RetrieveReportButton.Location = new Point(504, 42);
            this.RetrieveReportButton.Name = "RetrieveReportButton";
            this.RetrieveReportButton.Size = new Size(101, 24);
            this.RetrieveReportButton.TabIndex = 38;
            this.RetrieveReportButton.Text = "&Retrieve";
            this.RetrieveReportButton.Click += new System.EventHandler(this.RetrieveReport);
            this.groupBox1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.groupBox1.Controls.Add(this.BuildReportFromQueryButton);
            this.groupBox1.Controls.Add(this.ReportDatatypeDropDownList);
            this.groupBox1.Controls.Add(this.ReportTemplateDropDownList);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ReportIdsTextBox);
            this.groupBox1.Controls.Add(this.ReportOverwriteFalseButton);
            this.groupBox1.Controls.Add(this.ReportOverwriteTrueButton);
            this.groupBox1.Controls.Add(this.lblReportOverwrite);
            this.groupBox1.Controls.Add(this.ReportNameTextBox);
            this.groupBox1.Controls.Add(this.lblReportName);
            this.groupBox1.Controls.Add(this.BuildReportButton);
            this.groupBox1.Location = new Point(10, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(613, 213);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Build Report";
            this.BuildReportFromQueryButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.BuildReportFromQueryButton.Location = new Point(504, 49);
            this.BuildReportFromQueryButton.Name = "BuildReportFromQueryButton";
            this.BuildReportFromQueryButton.Size = new Size(101, 27);
            this.BuildReportFromQueryButton.TabIndex = 42;
            this.BuildReportFromQueryButton.Text = "Build From Query";
            this.toolTip1.SetToolTip(this.BuildReportFromQueryButton, "Build Report from Query / CriteriaXml on Query tab");
            this.BuildReportFromQueryButton.Click += new System.EventHandler(this.BuildReportFromQuery);
            this.ReportDatatypeDropDownList.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.ReportDatatypeDropDownList.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ReportDatatypeDropDownList.FormattingEnabled = true;
            this.ReportDatatypeDropDownList.Location = new Point(78, 102);
            this.ReportDatatypeDropDownList.Name = "ReportDatatypeDropDownList";
            this.ReportDatatypeDropDownList.Size = new Size(420, 21);
            this.ReportDatatypeDropDownList.TabIndex = 3;
            this.ReportDatatypeDropDownList.SelectedValueChanged += new System.EventHandler(this.ReportDatatypeDropDownListSelectedValueChanged);
            this.ReportTemplateDropDownList.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.ReportTemplateDropDownList.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ReportTemplateDropDownList.FormattingEnabled = true;
            this.ReportTemplateDropDownList.Location = new Point(78, 129);
            this.ReportTemplateDropDownList.Name = "ReportTemplateDropDownList";
            this.ReportTemplateDropDownList.Size = new Size(420, 21);
            this.ReportTemplateDropDownList.TabIndex = 5;
            this.label3.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(9, 131);
            this.label3.Name = "label3";
            this.label3.Size = new Size(51, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Template";
            this.label3.TextAlign = ContentAlignment.MiddleRight;
            this.label2.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(9, 104);
            this.label2.Name = "label2";
            this.label2.Size = new Size(50, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Datatype";
            this.label2.TextAlign = ContentAlignment.MiddleRight;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(9, 19);
            this.label1.Name = "label1";
            this.label1.Size = new Size(21, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ids";
            this.label1.TextAlign = ContentAlignment.MiddleRight;
            this.ReportIdsTextBox.AllowDrop = true;
            this.ReportIdsTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.ReportIdsTextBox.Location = new Point(78, 19);
            this.ReportIdsTextBox.Multiline = true;
            this.ReportIdsTextBox.Name = "ReportIdsTextBox";
            this.ReportIdsTextBox.Size = new Size(420, 76);
            this.ReportIdsTextBox.TabIndex = 1;
            this.ReportIdsTextBox.DragDrop += new DragEventHandler(this.DragDropHandler);
            this.ReportIdsTextBox.DragEnter += new DragEventHandler(this.DragEnterHandler);
            this.ReportIdsTextBox.KeyPress += new KeyPressEventHandler(this.KeyPressHandler);
            this.ReportOverwriteFalseButton.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            this.ReportOverwriteFalseButton.Location = new Point(129, 187);
            this.ReportOverwriteFalseButton.Name = "ReportOverwriteFalseButton";
            this.ReportOverwriteFalseButton.Size = new Size(50, 20);
            this.ReportOverwriteFalseButton.TabIndex = 28;
            this.ReportOverwriteFalseButton.Text = "False";
            this.ReportOverwriteTrueButton.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            this.ReportOverwriteTrueButton.Checked = true;
            this.ReportOverwriteTrueButton.Location = new Point(80, 187);
            this.ReportOverwriteTrueButton.Name = "ReportOverwriteTrueButton";
            this.ReportOverwriteTrueButton.Size = new Size(50, 20);
            this.ReportOverwriteTrueButton.TabIndex = 27;
            this.ReportOverwriteTrueButton.TabStop = true;
            this.ReportOverwriteTrueButton.Text = "True";
            this.lblReportOverwrite.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            this.lblReportOverwrite.AutoSize = true;
            this.lblReportOverwrite.Location = new Point(9, 187);
            this.lblReportOverwrite.Name = "lblReportOverwrite";
            this.lblReportOverwrite.Size = new Size(52, 13);
            this.lblReportOverwrite.TabIndex = 26;
            this.lblReportOverwrite.Text = "Overwrite";
            this.lblReportOverwrite.TextAlign = ContentAlignment.MiddleRight;
            this.ReportNameTextBox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.ReportNameTextBox.Location = new Point(78, 157);
            this.ReportNameTextBox.Name = "ReportNameTextBox";
            this.ReportNameTextBox.Size = new Size(420, 20);
            this.ReportNameTextBox.TabIndex = 24;
            this.ReportNameTextBox.KeyPress += new KeyPressEventHandler(this.KeyPressHandler);
            this.lblReportName.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            this.lblReportName.AutoSize = true;
            this.lblReportName.Location = new Point(9, 160);
            this.lblReportName.Name = "lblReportName";
            this.lblReportName.Size = new Size(49, 13);
            this.lblReportName.TabIndex = 6;
            this.lblReportName.Text = "Filename";
            this.lblReportName.TextAlign = ContentAlignment.MiddleRight;
            this.BuildReportButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.BuildReportButton.Location = new Point(504, 19);
            this.BuildReportButton.Name = "BuildReportButton";
            this.BuildReportButton.Size = new Size(101, 24);
            this.BuildReportButton.TabIndex = 23;
            this.BuildReportButton.Text = "Build";
            this.toolTip1.SetToolTip(this.BuildReportButton, "Build Report from list of Ids");
            this.BuildReportButton.Click += new System.EventHandler(this.BuildReport);
            this.DeleteMe.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.DeleteMe.BackColor = Color.Maroon;
            this.DeleteMe.Location = new Point(0, 0);
            this.DeleteMe.Name = "DeleteMe";
            this.DeleteMe.Size = new Size(1000, 1000);
            this.DeleteMe.TabIndex = 1;
            this.DeleteMe.Text = "Build From Query";
            this.toolTip1.SetToolTip(this.DeleteMe, "Build Export from Query / CriteriaXml on Query tab");
            this.DeleteMe.UseVisualStyleBackColor = false;
            this.DeleteMe.Click += new System.EventHandler(this.BuildOnelineFromQuery);
            this.groupBox3.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            this.groupBox3.Controls.Add(this.EntitlementsButton);
            this.groupBox3.Controls.Add(this.ServiceUrlTextBox);
            this.groupBox3.Controls.Add(this.BaseUrlDropDownList);
            this.groupBox3.Controls.Add(this.SessionButton);
            this.groupBox3.Controls.Add(this.lblService);
            this.groupBox3.Controls.Add(this.ApplicationTextBox);
            this.groupBox3.Controls.Add(this.PasswordTextBox);
            this.groupBox3.Controls.Add(this.UserTextBox);
            this.groupBox3.Controls.Add(this.lblAction);
            this.groupBox3.Controls.Add(this.lblPassword);
            this.groupBox3.Controls.Add(this.lblUser);
            this.groupBox3.Location = new Point(8, 8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(638, 158);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Credentials";
            this.EntitlementsButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.EntitlementsButton.Enabled = false;
            this.EntitlementsButton.Location = new Point(536, 24);
            this.EntitlementsButton.Name = "EntitlementsButton";
            this.EntitlementsButton.Size = new Size(85, 24);
            this.EntitlementsButton.TabIndex = 28;
            this.EntitlementsButton.Text = "Entitlements";
            this.EntitlementsButton.UseVisualStyleBackColor = true;
            this.EntitlementsButton.Click += new System.EventHandler(this.EntitlementsButton_Click);
            this.ServiceUrlTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            this.ServiceUrlTextBox.Location = new Point(105, 132);
            this.ServiceUrlTextBox.Name = "ServiceUrlTextBox";
            this.ServiceUrlTextBox.Size = new Size(516, 20);
            this.ServiceUrlTextBox.TabIndex = 27;
            this.BaseUrlDropDownList.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            this.BaseUrlDropDownList.DropDownStyle = ComboBoxStyle.DropDownList;
            this.BaseUrlDropDownList.FormattingEnabled = true;
            this.BaseUrlDropDownList.Location = new Point(105, 102);
            this.BaseUrlDropDownList.Name = "BaseUrlDropDownList";
            this.BaseUrlDropDownList.Size = new Size(425, 21);
            this.BaseUrlDropDownList.TabIndex = 26;
            this.BaseUrlDropDownList.SelectedIndexChanged += new System.EventHandler(this.BaseUrlDropDownListSelectedIndexChanged);
            this.SessionButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.SessionButton.Location = new Point(536, 101);
            this.SessionButton.Name = "SessionButton";
            this.SessionButton.Size = new Size(85, 24);
            this.SessionButton.TabIndex = 25;
            this.SessionButton.Text = "Connect";
            this.SessionButton.Click += new System.EventHandler(this.SessionButtonClick);
            this.lblService.AutoSize = true;
            this.lblService.Location = new Point(6, 105);
            this.lblService.Name = "lblService";
            this.lblService.Size = new Size(95, 13);
            this.lblService.TabIndex = 18;
            this.lblService.Text = "Base &Service URL";
            this.lblService.TextAlign = ContentAlignment.MiddleRight;
            this.ApplicationTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            this.ApplicationTextBox.Location = new Point(105, 75);
            this.ApplicationTextBox.Name = "ApplicationTextBox";
            this.ApplicationTextBox.Size = new Size(516, 20);
            this.ApplicationTextBox.TabIndex = 13;
            this.PasswordTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            this.PasswordTextBox.Location = new Point(105, 50);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.PasswordChar = '*';
            this.PasswordTextBox.Size = new Size(516, 20);
            this.PasswordTextBox.TabIndex = 12;
            this.PasswordTextBox.UseSystemPasswordChar = true;
            this.UserTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            this.UserTextBox.Location = new Point(105, 24);
            this.UserTextBox.Name = "UserTextBox";
            this.UserTextBox.Size = new Size(425, 20);
            this.UserTextBox.TabIndex = 1;
            this.lblAction.AutoSize = true;
            this.lblAction.Location = new Point(40, 77);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new Size(59, 13);
            this.lblAction.TabIndex = 16;
            this.lblAction.Text = "&Application";
            this.lblAction.TextAlign = ContentAlignment.MiddleRight;
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new Point(45, 52);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new Size(53, 13);
            this.lblPassword.TabIndex = 15;
            this.lblPassword.Text = "&Password";
            this.lblPassword.TextAlign = ContentAlignment.MiddleRight;
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new Point(70, 26);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new Size(29, 13);
            this.lblUser.TabIndex = 0;
            this.lblUser.Text = "&User";
            this.lblUser.TextAlign = ContentAlignment.MiddleRight;
            this.versionLabel.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            this.versionLabel.ForeColor = SystemColors.ControlDarkDark;
            this.versionLabel.Location = new Point(400, 600);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new Size(249, 20);
            this.versionLabel.TabIndex = 2;
            this.versionLabel.Text = "Version";
            this.versionLabel.TextAlign = ContentAlignment.MiddleRight;
            this.versionLabel.Click += new System.EventHandler(this.versionLabel_Click);
            this.AutoScaleBaseSize = new Size(5, 13);
            this.AutoValidate = AutoValidate.EnablePreventFocusChange;
            base.ClientSize = new Size(662, 619);
            base.Controls.Add(this.versionLabel);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.tabcontrol1);
            this.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
            this.MinimumSize = new Size(678, 657);
            base.Name = "WebServicesTestClientForm";
            this.Text = "IHS Energy Web Services US Demo Tool";
            base.Closing += new CancelEventHandler(this.Form_Closing);
            base.Load += new System.EventHandler(this.FormLoad);
            this.tabcontrol1.ResumeLayout(false);
            this.queryTab.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.tabChangeDelete.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((ISupportInitialize)this.PageTextBox).EndInit();
            this.exportTab.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.spatialExportTab.ResumeLayout(false);
            this.groupBox27.ResumeLayout(false);
            this.groupBox27.PerformLayout();
            this.groupBox28.ResumeLayout(false);
            this.groupBox29.ResumeLayout(false);
            this.groupBox29.PerformLayout();
            this.groupBox30.ResumeLayout(false);
            this.groupBox31.ResumeLayout(false);
            this.groupBox31.PerformLayout();
            this.dataTemplateTab.ResumeLayout(false);
            this.groupBox26.ResumeLayout(false);
            this.groupBox26.PerformLayout();
            this.onelineExportTab.ResumeLayout(false);
            this.groupBox21.ResumeLayout(false);
            this.groupBox21.PerformLayout();
            this.groupBox22.ResumeLayout(false);
            this.groupBox23.ResumeLayout(false);
            this.groupBox23.PerformLayout();
            this.groupBox24.ResumeLayout(false);
            this.groupBox25.ResumeLayout(false);
            this.groupBox25.PerformLayout();
            this.tabGraph.ResumeLayout(false);
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            this.groupBox18.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.reportTab.ResumeLayout(false);
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            this.groupBox20.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            base.ResumeLayout(false);
        }
        private void BuildExportFromQueryButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                using (new CursorManager(this))
                {
                    Overwrite overwriteType = WebServicesTestClientForm.GetOverwriteType(this.ExportOverwriteTrueButton, this.ExportOverwriteFalseButton);
                    string text = this.QueryTextBox.Text;
                    string text2 = this.exportBuilder.BuildFromQuery("US", this.ExportDatatypeDropDownList.Text, this.GetSelectedTemplate(), text, this.ExportNameTextBox.Text, overwriteType);
                    this.ExportJobIdTextBox.Text = text2;
                }
                this.FlashControl(this.ExportJobIdTextBox);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(this, ex.Message, this.Text);
            }
        }
    }
}
