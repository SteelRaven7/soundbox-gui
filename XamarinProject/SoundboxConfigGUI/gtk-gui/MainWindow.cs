
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.UIManager UIManager;
	private global::Gtk.Action FileAction;
	private global::Gtk.Action ConnectionAction;
	private global::Gtk.Action newAction;
	private global::Gtk.Action openAction;
	private global::Gtk.Action saveAction;
	private global::Gtk.Action saveAsAction;
	private global::Gtk.Action quitAction;
	private global::Gtk.Action connectAction;
	private global::Gtk.Action jumpToAction;
	private global::Gtk.Action disconnectAction;
	private global::Gtk.VBox vbox4;
	private global::Gtk.MenuBar menubar6;
	private global::Gtk.Notebook notebook1;
	private global::Gtk.Table table1;
	private global::Gtk.Table table3;
	private global::Gtk.ScrolledWindow GtkScrolledWindow;
	private global::Gtk.TextView log;
	private global::Gtk.Label logLabel;
	private global::Gtk.VBox vbox1;
	private global::Gtk.Table table2;
	private global::Gtk.Label label1;
	private global::Gtk.Button refreshPortsButton;
	private global::Gtk.ComboBox portList;
	private global::Gtk.Button connectButton;
	private global::Gtk.Label labelStatus;
	private global::Gtk.Label label8;
	private global::Gtk.Label labelEqualizer;
	private global::Gtk.Table table4;
	private global::Gtk.Entry echoDelay;
	private global::Gtk.Entry echoDryGain;
	private global::Gtk.Entry echoFeedback;
	private global::Gtk.Entry echoWetGain;
	private global::Gtk.Label label10;
	private global::Gtk.Label label2;
	private global::Gtk.Label label3;
	private global::Gtk.Label label4;
	private global::Gtk.Label label5;
	private global::Gtk.Label label6;
	private global::Gtk.Label label9;
	private global::Gtk.Label legokso;
	private global::Gtk.Label labelEcho;
	private global::Gtk.Statusbar statusbar2;
	private global::Gtk.ProgressBar progressbar;
	private global::Gtk.Label statusLabel;

	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.UIManager = new global::Gtk.UIManager ();
		global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup ("Default");
		this.FileAction = new global::Gtk.Action ("FileAction", global::Mono.Unix.Catalog.GetString ("File"), null, null);
		this.FileAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("File");
		w1.Add (this.FileAction, null);
		this.ConnectionAction = new global::Gtk.Action ("ConnectionAction", global::Mono.Unix.Catalog.GetString ("Connection"), null, null);
		this.ConnectionAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Connection");
		w1.Add (this.ConnectionAction, null);
		this.newAction = new global::Gtk.Action ("newAction", global::Mono.Unix.Catalog.GetString ("_New"), null, "gtk-new");
		this.newAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_New");
		w1.Add (this.newAction, null);
		this.openAction = new global::Gtk.Action ("openAction", global::Mono.Unix.Catalog.GetString ("_Open"), null, "gtk-open");
		this.openAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Open");
		w1.Add (this.openAction, null);
		this.saveAction = new global::Gtk.Action ("saveAction", global::Mono.Unix.Catalog.GetString ("_Save"), null, "gtk-save");
		this.saveAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Save");
		w1.Add (this.saveAction, null);
		this.saveAsAction = new global::Gtk.Action ("saveAsAction", global::Mono.Unix.Catalog.GetString ("Save _As"), null, "gtk-save-as");
		this.saveAsAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Save _As");
		w1.Add (this.saveAsAction, null);
		this.quitAction = new global::Gtk.Action ("quitAction", global::Mono.Unix.Catalog.GetString ("_Quit"), null, "gtk-quit");
		this.quitAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Quit");
		w1.Add (this.quitAction, null);
		this.connectAction = new global::Gtk.Action ("connectAction", global::Mono.Unix.Catalog.GetString ("C_onnect"), null, "gtk-connect");
		this.connectAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("C_onnect");
		w1.Add (this.connectAction, null);
		this.jumpToAction = new global::Gtk.Action ("jumpToAction", global::Mono.Unix.Catalog.GetString ("Program"), null, "gtk-jump-to");
		this.jumpToAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Program");
		w1.Add (this.jumpToAction, null);
		this.disconnectAction = new global::Gtk.Action ("disconnectAction", global::Mono.Unix.Catalog.GetString ("_Disconnect"), null, "gtk-disconnect");
		this.disconnectAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Disconnect");
		w1.Add (this.disconnectAction, null);
		this.UIManager.InsertActionGroup (w1, 0);
		this.AddAccelGroup (this.UIManager.AccelGroup);
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("MainWindow");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.vbox4 = new global::Gtk.VBox ();
		this.vbox4.Name = "vbox4";
		this.vbox4.Spacing = 6;
		// Container child vbox4.Gtk.Box+BoxChild
		this.UIManager.AddUiFromString (@"<ui><menubar name='menubar6'><menu name='FileAction' action='FileAction'><menuitem name='newAction' action='newAction'/><menuitem name='openAction' action='openAction'/><menuitem name='saveAction' action='saveAction'/><menuitem name='saveAsAction' action='saveAsAction'/><menuitem name='quitAction' action='quitAction'/></menu><menu name='ConnectionAction' action='ConnectionAction'><menuitem name='jumpToAction' action='jumpToAction'/></menu></menubar></ui>");
		this.menubar6 = ((global::Gtk.MenuBar)(this.UIManager.GetWidget ("/menubar6")));
		this.menubar6.Name = "menubar6";
		this.vbox4.Add (this.menubar6);
		global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.menubar6]));
		w2.Position = 0;
		w2.Expand = false;
		w2.Fill = false;
		// Container child vbox4.Gtk.Box+BoxChild
		this.notebook1 = new global::Gtk.Notebook ();
		this.notebook1.CanFocus = true;
		this.notebook1.Name = "notebook1";
		this.notebook1.CurrentPage = 2;
		// Container child notebook1.Gtk.Notebook+NotebookChild
		this.table1 = new global::Gtk.Table (((uint)(1)), ((uint)(2)), false);
		this.table1.Name = "table1";
		this.table1.RowSpacing = ((uint)(6));
		this.table1.ColumnSpacing = ((uint)(6));
		// Container child table1.Gtk.Table+TableChild
		this.table3 = new global::Gtk.Table (((uint)(2)), ((uint)(1)), false);
		this.table3.Name = "table3";
		this.table3.RowSpacing = ((uint)(6));
		this.table3.ColumnSpacing = ((uint)(6));
		// Container child table3.Gtk.Table+TableChild
		this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow.WidthRequest = 500;
		this.GtkScrolledWindow.Name = "GtkScrolledWindow";
		this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
		this.log = new global::Gtk.TextView ();
		this.log.CanFocus = true;
		this.log.Name = "log";
		this.log.Editable = false;
		this.GtkScrolledWindow.Add (this.log);
		this.table3.Add (this.GtkScrolledWindow);
		global::Gtk.Table.TableChild w4 = ((global::Gtk.Table.TableChild)(this.table3 [this.GtkScrolledWindow]));
		w4.TopAttach = ((uint)(1));
		w4.BottomAttach = ((uint)(2));
		// Container child table3.Gtk.Table+TableChild
		this.logLabel = new global::Gtk.Label ();
		this.logLabel.Name = "logLabel";
		this.logLabel.LabelProp = global::Mono.Unix.Catalog.GetString ("Log");
		this.table3.Add (this.logLabel);
		global::Gtk.Table.TableChild w5 = ((global::Gtk.Table.TableChild)(this.table3 [this.logLabel]));
		w5.YOptions = ((global::Gtk.AttachOptions)(4));
		this.table1.Add (this.table3);
		global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table1 [this.table3]));
		w6.LeftAttach = ((uint)(1));
		w6.RightAttach = ((uint)(2));
		w6.XOptions = ((global::Gtk.AttachOptions)(4));
		w6.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table1.Gtk.Table+TableChild
		this.vbox1 = new global::Gtk.VBox ();
		this.vbox1.Name = "vbox1";
		this.vbox1.Spacing = 6;
		// Container child vbox1.Gtk.Box+BoxChild
		this.table2 = new global::Gtk.Table (((uint)(1)), ((uint)(2)), false);
		this.table2.Name = "table2";
		this.table2.RowSpacing = ((uint)(6));
		this.table2.ColumnSpacing = ((uint)(6));
		// Container child table2.Gtk.Table+TableChild
		this.label1 = new global::Gtk.Label ();
		this.label1.Name = "label1";
		this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("COM Ports");
		this.table2.Add (this.label1);
		global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.table2 [this.label1]));
		w7.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table2.Gtk.Table+TableChild
		this.refreshPortsButton = new global::Gtk.Button ();
		this.refreshPortsButton.CanFocus = true;
		this.refreshPortsButton.Name = "refreshPortsButton";
		this.refreshPortsButton.UseUnderline = true;
		this.refreshPortsButton.Label = global::Mono.Unix.Catalog.GetString ("Refresh");
		this.table2.Add (this.refreshPortsButton);
		global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.table2 [this.refreshPortsButton]));
		w8.LeftAttach = ((uint)(1));
		w8.RightAttach = ((uint)(2));
		w8.XOptions = ((global::Gtk.AttachOptions)(4));
		w8.YOptions = ((global::Gtk.AttachOptions)(4));
		this.vbox1.Add (this.table2);
		global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.table2]));
		w9.Position = 0;
		w9.Expand = false;
		w9.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.portList = global::Gtk.ComboBox.NewText ();
		this.portList.Name = "portList";
		this.vbox1.Add (this.portList);
		global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.portList]));
		w10.Position = 1;
		w10.Expand = false;
		w10.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.connectButton = new global::Gtk.Button ();
		this.connectButton.WidthRequest = 50;
		this.connectButton.CanFocus = true;
		this.connectButton.Name = "connectButton";
		this.connectButton.UseUnderline = true;
		this.connectButton.Label = global::Mono.Unix.Catalog.GetString ("Program");
		this.vbox1.Add (this.connectButton);
		global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.connectButton]));
		w11.Position = 2;
		w11.Expand = false;
		w11.Fill = false;
		this.table1.Add (this.vbox1);
		this.notebook1.Add (this.table1);
		// Notebook tab
		this.labelStatus = new global::Gtk.Label ();
		this.labelStatus.Name = "labelStatus";
		this.labelStatus.LabelProp = global::Mono.Unix.Catalog.GetString ("Status");
		this.notebook1.SetTabLabel (this.table1, this.labelStatus);
		this.labelStatus.ShowAll ();
		// Container child notebook1.Gtk.Notebook+NotebookChild
		this.label8 = new global::Gtk.Label ();
		this.label8.Name = "label8";
		this.label8.LabelProp = global::Mono.Unix.Catalog.GetString ("Equalizer");
		this.notebook1.Add (this.label8);
		global::Gtk.Notebook.NotebookChild w14 = ((global::Gtk.Notebook.NotebookChild)(this.notebook1 [this.label8]));
		w14.Position = 1;
		// Notebook tab
		this.labelEqualizer = new global::Gtk.Label ();
		this.labelEqualizer.Name = "labelEqualizer";
		this.labelEqualizer.LabelProp = global::Mono.Unix.Catalog.GetString ("Equalizer");
		this.notebook1.SetTabLabel (this.label8, this.labelEqualizer);
		this.labelEqualizer.ShowAll ();
		// Container child notebook1.Gtk.Notebook+NotebookChild
		this.table4 = new global::Gtk.Table (((uint)(4)), ((uint)(3)), false);
		this.table4.Name = "table4";
		this.table4.RowSpacing = ((uint)(6));
		this.table4.ColumnSpacing = ((uint)(6));
		// Container child table4.Gtk.Table+TableChild
		this.echoDelay = new global::Gtk.Entry ();
		this.echoDelay.CanFocus = true;
		this.echoDelay.Name = "echoDelay";
		this.echoDelay.Text = global::Mono.Unix.Catalog.GetString ("1.0");
		this.echoDelay.IsEditable = true;
		this.echoDelay.InvisibleChar = '●';
		this.table4.Add (this.echoDelay);
		global::Gtk.Table.TableChild w15 = ((global::Gtk.Table.TableChild)(this.table4 [this.echoDelay]));
		w15.LeftAttach = ((uint)(1));
		w15.RightAttach = ((uint)(2));
		w15.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table4.Gtk.Table+TableChild
		this.echoDryGain = new global::Gtk.Entry ();
		this.echoDryGain.CanFocus = true;
		this.echoDryGain.Name = "echoDryGain";
		this.echoDryGain.Text = global::Mono.Unix.Catalog.GetString ("1.0");
		this.echoDryGain.IsEditable = true;
		this.echoDryGain.InvisibleChar = '●';
		this.table4.Add (this.echoDryGain);
		global::Gtk.Table.TableChild w16 = ((global::Gtk.Table.TableChild)(this.table4 [this.echoDryGain]));
		w16.TopAttach = ((uint)(1));
		w16.BottomAttach = ((uint)(2));
		w16.LeftAttach = ((uint)(1));
		w16.RightAttach = ((uint)(2));
		w16.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table4.Gtk.Table+TableChild
		this.echoFeedback = new global::Gtk.Entry ();
		this.echoFeedback.CanFocus = true;
		this.echoFeedback.Name = "echoFeedback";
		this.echoFeedback.Text = global::Mono.Unix.Catalog.GetString ("0.5");
		this.echoFeedback.IsEditable = true;
		this.echoFeedback.InvisibleChar = '●';
		this.table4.Add (this.echoFeedback);
		global::Gtk.Table.TableChild w17 = ((global::Gtk.Table.TableChild)(this.table4 [this.echoFeedback]));
		w17.TopAttach = ((uint)(3));
		w17.BottomAttach = ((uint)(4));
		w17.LeftAttach = ((uint)(1));
		w17.RightAttach = ((uint)(2));
		w17.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table4.Gtk.Table+TableChild
		this.echoWetGain = new global::Gtk.Entry ();
		this.echoWetGain.CanFocus = true;
		this.echoWetGain.Name = "echoWetGain";
		this.echoWetGain.Text = global::Mono.Unix.Catalog.GetString ("0.8");
		this.echoWetGain.IsEditable = true;
		this.echoWetGain.InvisibleChar = '●';
		this.table4.Add (this.echoWetGain);
		global::Gtk.Table.TableChild w18 = ((global::Gtk.Table.TableChild)(this.table4 [this.echoWetGain]));
		w18.TopAttach = ((uint)(2));
		w18.BottomAttach = ((uint)(3));
		w18.LeftAttach = ((uint)(1));
		w18.RightAttach = ((uint)(2));
		w18.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table4.Gtk.Table+TableChild
		this.label10 = new global::Gtk.Label ();
		this.label10.Name = "label10";
		this.label10.LabelProp = global::Mono.Unix.Catalog.GetString ("(-1 - 1)");
		this.table4.Add (this.label10);
		global::Gtk.Table.TableChild w19 = ((global::Gtk.Table.TableChild)(this.table4 [this.label10]));
		w19.TopAttach = ((uint)(3));
		w19.BottomAttach = ((uint)(4));
		w19.LeftAttach = ((uint)(2));
		w19.RightAttach = ((uint)(3));
		w19.XOptions = ((global::Gtk.AttachOptions)(4));
		w19.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table4.Gtk.Table+TableChild
		this.label2 = new global::Gtk.Label ();
		this.label2.Name = "label2";
		this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("Delay");
		this.table4.Add (this.label2);
		global::Gtk.Table.TableChild w20 = ((global::Gtk.Table.TableChild)(this.table4 [this.label2]));
		w20.XOptions = ((global::Gtk.AttachOptions)(4));
		w20.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table4.Gtk.Table+TableChild
		this.label3 = new global::Gtk.Label ();
		this.label3.Name = "label3";
		this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("(0 - 1.5) [s]");
		this.table4.Add (this.label3);
		global::Gtk.Table.TableChild w21 = ((global::Gtk.Table.TableChild)(this.table4 [this.label3]));
		w21.LeftAttach = ((uint)(2));
		w21.RightAttach = ((uint)(3));
		w21.XOptions = ((global::Gtk.AttachOptions)(4));
		w21.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table4.Gtk.Table+TableChild
		this.label4 = new global::Gtk.Label ();
		this.label4.Name = "label4";
		this.label4.LabelProp = global::Mono.Unix.Catalog.GetString ("Dry gain");
		this.table4.Add (this.label4);
		global::Gtk.Table.TableChild w22 = ((global::Gtk.Table.TableChild)(this.table4 [this.label4]));
		w22.TopAttach = ((uint)(1));
		w22.BottomAttach = ((uint)(2));
		w22.XOptions = ((global::Gtk.AttachOptions)(4));
		w22.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table4.Gtk.Table+TableChild
		this.label5 = new global::Gtk.Label ();
		this.label5.Name = "label5";
		this.label5.LabelProp = global::Mono.Unix.Catalog.GetString ("label4");
		this.table4.Add (this.label5);
		global::Gtk.Table.TableChild w23 = ((global::Gtk.Table.TableChild)(this.table4 [this.label5]));
		w23.TopAttach = ((uint)(2));
		w23.BottomAttach = ((uint)(3));
		w23.XOptions = ((global::Gtk.AttachOptions)(4));
		w23.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table4.Gtk.Table+TableChild
		this.label6 = new global::Gtk.Label ();
		this.label6.Name = "label6";
		this.label6.LabelProp = global::Mono.Unix.Catalog.GetString ("label5");
		this.table4.Add (this.label6);
		global::Gtk.Table.TableChild w24 = ((global::Gtk.Table.TableChild)(this.table4 [this.label6]));
		w24.TopAttach = ((uint)(3));
		w24.BottomAttach = ((uint)(4));
		w24.XOptions = ((global::Gtk.AttachOptions)(4));
		w24.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table4.Gtk.Table+TableChild
		this.label9 = new global::Gtk.Label ();
		this.label9.Name = "label9";
		this.label9.LabelProp = global::Mono.Unix.Catalog.GetString ("(-1 - 1)");
		this.table4.Add (this.label9);
		global::Gtk.Table.TableChild w25 = ((global::Gtk.Table.TableChild)(this.table4 [this.label9]));
		w25.TopAttach = ((uint)(2));
		w25.BottomAttach = ((uint)(3));
		w25.LeftAttach = ((uint)(2));
		w25.RightAttach = ((uint)(3));
		w25.XOptions = ((global::Gtk.AttachOptions)(4));
		w25.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table4.Gtk.Table+TableChild
		this.legokso = new global::Gtk.Label ();
		this.legokso.Name = "legokso";
		this.legokso.LabelProp = global::Mono.Unix.Catalog.GetString ("(-1 - 1)");
		this.table4.Add (this.legokso);
		global::Gtk.Table.TableChild w26 = ((global::Gtk.Table.TableChild)(this.table4 [this.legokso]));
		w26.TopAttach = ((uint)(1));
		w26.BottomAttach = ((uint)(2));
		w26.LeftAttach = ((uint)(2));
		w26.RightAttach = ((uint)(3));
		w26.XOptions = ((global::Gtk.AttachOptions)(4));
		w26.YOptions = ((global::Gtk.AttachOptions)(4));
		this.notebook1.Add (this.table4);
		global::Gtk.Notebook.NotebookChild w27 = ((global::Gtk.Notebook.NotebookChild)(this.notebook1 [this.table4]));
		w27.Position = 2;
		// Notebook tab
		this.labelEcho = new global::Gtk.Label ();
		this.labelEcho.Name = "labelEcho";
		this.labelEcho.LabelProp = global::Mono.Unix.Catalog.GetString ("Echo");
		this.notebook1.SetTabLabel (this.table4, this.labelEcho);
		this.labelEcho.ShowAll ();
		this.vbox4.Add (this.notebook1);
		global::Gtk.Box.BoxChild w28 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.notebook1]));
		w28.Position = 1;
		// Container child vbox4.Gtk.Box+BoxChild
		this.statusbar2 = new global::Gtk.Statusbar ();
		this.statusbar2.Name = "statusbar2";
		this.statusbar2.Spacing = 6;
		// Container child statusbar2.Gtk.Box+BoxChild
		this.progressbar = new global::Gtk.ProgressBar ();
		this.progressbar.Name = "progressbar";
		this.progressbar.Text = "";
		this.progressbar.PulseStep = 0.06D;
		this.statusbar2.Add (this.progressbar);
		global::Gtk.Box.BoxChild w29 = ((global::Gtk.Box.BoxChild)(this.statusbar2 [this.progressbar]));
		w29.Position = 1;
		// Container child statusbar2.Gtk.Box+BoxChild
		this.statusLabel = new global::Gtk.Label ();
		this.statusLabel.WidthRequest = 150;
		this.statusLabel.Name = "statusLabel";
		this.statusLabel.LabelProp = global::Mono.Unix.Catalog.GetString ("Not connected");
		this.statusbar2.Add (this.statusLabel);
		global::Gtk.Box.BoxChild w30 = ((global::Gtk.Box.BoxChild)(this.statusbar2 [this.statusLabel]));
		w30.Position = 2;
		w30.Expand = false;
		w30.Fill = false;
		this.vbox4.Add (this.statusbar2);
		global::Gtk.Box.BoxChild w31 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.statusbar2]));
		w31.Position = 2;
		w31.Expand = false;
		w31.Fill = false;
		w31.Padding = ((uint)(2));
		this.Add (this.vbox4);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 818;
		this.DefaultHeight = 540;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.connectAction.Activated += new global::System.EventHandler (this.Connect);
		this.jumpToAction.Activated += new global::System.EventHandler (this.Program);
		this.disconnectAction.Activated += new global::System.EventHandler (this.Disconnect);
		this.refreshPortsButton.Clicked += new global::System.EventHandler (this.RefreshPorts);
		this.portList.Changed += new global::System.EventHandler (this.PortChanged);
		this.connectButton.Clicked += new global::System.EventHandler (this.Program);
	}
}
