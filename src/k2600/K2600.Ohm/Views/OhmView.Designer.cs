namespace cc.isr.Tcp.Tsp.K2600.Ohm.Views;

partial class OhmView
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    #region " windows form designer generated code "

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OhmView));
        this.VoltageLevelNumeric = new NumericUpDown();
        this.VoltageLevelNumericLabel = new Label();
        this.ApertureNumeric = new NumericUpDown();
        this.ApertureNumericLabel = new Label();
        this.OhmGroupBox = new GroupBox();
        this.ConfigureButton = new Button();
        this.AutoRangeCheckBox = new CheckBox();
        this.CurrentLevelNumeric = new NumericUpDown();
        this.CurrentLevelUnitsLabel = new Label();
        this.CurrentLevelNumericLabel = new Label();
        this.CurrentSourceOption = new RadioButton();
        this.VoltageSourceOption = new RadioButton();
        this.ApertureUnitsLabel = new Label();
        this.VoltageLevelUnitsLabel = new Label();
        this.IPAddressTextBoxLabel = new Label();
        this.IPAddressTextBox = new TextBox();
        this.MeasureButton = new Button();
        this.MeasurementGroupBox = new GroupBox();
        this.RunningStateLabel = new Label();
        this.ResistanceReadingLabelLabel = new Label();
        this.CurrentReadingLabelLabel = new Label();
        this.VoltageReadingLabelLabel = new Label();
        this.DurationLabelLabel = new Label();
        this.ResistanceReadingLabel = new Label();
        this.CurrentReadingLabel = new Label();
        this.VoltageReadingLabel = new Label();
        this.DurationLabel = new Label();
        this.IdentityLabelLabel = new Label();
        this.IdentityLabel = new Label();
        this.InstrumentGroupBox = new GroupBox();
        this.ToggleConnectionButton = new Button();
        this.UserPromptLabel = new Label();
        ((System.ComponentModel.ISupportInitialize)this.VoltageLevelNumeric).BeginInit();
        ((System.ComponentModel.ISupportInitialize)this.ApertureNumeric).BeginInit();
        this.OhmGroupBox.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)this.CurrentLevelNumeric).BeginInit();
        this.MeasurementGroupBox.SuspendLayout();
        this.InstrumentGroupBox.SuspendLayout();
        this.SuspendLayout();
        // 
        // VoltageLevelNumeric
        // 
        this.VoltageLevelNumeric.DecimalPlaces = 2;
        this.VoltageLevelNumeric.Font = new Font("Segoe UI", 9F);
        this.VoltageLevelNumeric.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
        this.VoltageLevelNumeric.Location = new Point(65, 53);
        this.VoltageLevelNumeric.Margin = new Padding(4, 3, 4, 3);
        this.VoltageLevelNumeric.Maximum = new decimal(new int[] { 2, 0, 0, 65536 });
        this.VoltageLevelNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
        this.VoltageLevelNumeric.Name = "VoltageLevelNumeric";
        this.VoltageLevelNumeric.Size = new Size(56, 23);
        this.VoltageLevelNumeric.TabIndex = 2;
        this.VoltageLevelNumeric.Value = new decimal(new int[] { 1, 0, 0, 65536 });
        // 
        // VoltageLevelNumericLabel
        // 
        this.VoltageLevelNumericLabel.AutoSize = true;
        this.VoltageLevelNumericLabel.Font = new Font("Segoe UI", 9F);
        this.VoltageLevelNumericLabel.ForeColor = Color.Chartreuse;
        this.VoltageLevelNumericLabel.Location = new Point(10, 55);
        this.VoltageLevelNumericLabel.Margin = new Padding(4, 0, 4, 0);
        this.VoltageLevelNumericLabel.Name = "VoltageLevelNumericLabel";
        this.VoltageLevelNumericLabel.Size = new Size(49, 15);
        this.VoltageLevelNumericLabel.TabIndex = 3;
        this.VoltageLevelNumericLabel.Text = "Voltage:";
        this.VoltageLevelNumericLabel.TextAlign = ContentAlignment.TopRight;
        // 
        // ApertureNumeric
        // 
        this.ApertureNumeric.DecimalPlaces = 2;
        this.ApertureNumeric.Font = new Font("Segoe UI", 9F);
        this.ApertureNumeric.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
        this.ApertureNumeric.Location = new Point(240, 57);
        this.ApertureNumeric.Margin = new Padding(4, 3, 4, 3);
        this.ApertureNumeric.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
        this.ApertureNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
        this.ApertureNumeric.Name = "ApertureNumeric";
        this.ApertureNumeric.Size = new Size(52, 23);
        this.ApertureNumeric.TabIndex = 4;
        this.ApertureNumeric.TabStop = false;
        this.ApertureNumeric.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // ApertureNumericLabel
        // 
        this.ApertureNumericLabel.AutoSize = true;
        this.ApertureNumericLabel.Font = new Font("Segoe UI", 9F);
        this.ApertureNumericLabel.ForeColor = Color.Chartreuse;
        this.ApertureNumericLabel.Location = new Point(182, 59);
        this.ApertureNumericLabel.Margin = new Padding(4, 0, 4, 0);
        this.ApertureNumericLabel.Name = "ApertureNumericLabel";
        this.ApertureNumericLabel.Size = new Size(56, 15);
        this.ApertureNumericLabel.TabIndex = 5;
        this.ApertureNumericLabel.Text = "Aperture:";
        this.ApertureNumericLabel.TextAlign = ContentAlignment.TopRight;
        // 
        // OhmGroupBox
        // 
        this.OhmGroupBox.Controls.Add(this.ConfigureButton);
        this.OhmGroupBox.Controls.Add(this.AutoRangeCheckBox);
        this.OhmGroupBox.Controls.Add(this.CurrentLevelNumeric);
        this.OhmGroupBox.Controls.Add(this.CurrentLevelUnitsLabel);
        this.OhmGroupBox.Controls.Add(this.CurrentLevelNumericLabel);
        this.OhmGroupBox.Controls.Add(this.CurrentSourceOption);
        this.OhmGroupBox.Controls.Add(this.VoltageSourceOption);
        this.OhmGroupBox.Controls.Add(this.VoltageLevelNumericLabel);
        this.OhmGroupBox.Controls.Add(this.VoltageLevelNumeric);
        this.OhmGroupBox.Controls.Add(this.ApertureUnitsLabel);
        this.OhmGroupBox.Controls.Add(this.VoltageLevelUnitsLabel);
        this.OhmGroupBox.Controls.Add(this.ApertureNumeric);
        this.OhmGroupBox.Controls.Add(this.ApertureNumericLabel);
        this.OhmGroupBox.Font = new Font("Segoe UI", 9F);
        this.OhmGroupBox.ForeColor = Color.Chartreuse;
        this.OhmGroupBox.Location = new Point(18, 115);
        this.OhmGroupBox.Margin = new Padding(4, 3, 4, 3);
        this.OhmGroupBox.Name = "OhmGroupBox";
        this.OhmGroupBox.Padding = new Padding(4, 3, 4, 3);
        this.OhmGroupBox.Size = new Size(408, 125);
        this.OhmGroupBox.TabIndex = 6;
        this.OhmGroupBox.TabStop = false;
        this.OhmGroupBox.Text = "Measurement Settings";
        // 
        // ConfigureButton
        // 
        this.ConfigureButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
        this.ConfigureButton.ForeColor = SystemColors.ControlText;
        this.ConfigureButton.Location = new Point(297, 16);
        this.ConfigureButton.Name = "ConfigureButton";
        this.ConfigureButton.Size = new Size(103, 36);
        this.ConfigureButton.TabIndex = 13;
        this.ConfigureButton.Text = "Apply";
        this.ConfigureButton.UseVisualStyleBackColor = true;
        // 
        // AutoRangeCheckBox
        // 
        this.AutoRangeCheckBox.CheckAlign = ContentAlignment.MiddleRight;
        this.AutoRangeCheckBox.Checked = true;
        this.AutoRangeCheckBox.CheckState = CheckState.Checked;
        this.AutoRangeCheckBox.Location = new Point(180, 86);
        this.AutoRangeCheckBox.Name = "AutoRangeCheckBox";
        this.AutoRangeCheckBox.Size = new Size(89, 24);
        this.AutoRangeCheckBox.TabIndex = 12;
        this.AutoRangeCheckBox.Text = "Auto Range";
        this.AutoRangeCheckBox.UseVisualStyleBackColor = true;
        // 
        // CurrentLevelNumeric
        // 
        this.CurrentLevelNumeric.DecimalPlaces = 3;
        this.CurrentLevelNumeric.Increment = new decimal(new int[] { 5, 0, 0, 196608 });
        this.CurrentLevelNumeric.Location = new Point(65, 86);
        this.CurrentLevelNumeric.Maximum = new decimal(new int[] { 1, 0, 0, 65536 });
        this.CurrentLevelNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
        this.CurrentLevelNumeric.Name = "CurrentLevelNumeric";
        this.CurrentLevelNumeric.Size = new Size(56, 23);
        this.CurrentLevelNumeric.TabIndex = 11;
        this.CurrentLevelNumeric.Value = new decimal(new int[] { 1, 0, 0, 131072 });
        // 
        // CurrentLevelUnitsLabel
        // 
        this.CurrentLevelUnitsLabel.AutoSize = true;
        this.CurrentLevelUnitsLabel.Location = new Point(120, 89);
        this.CurrentLevelUnitsLabel.Margin = new Padding(4, 0, 4, 0);
        this.CurrentLevelUnitsLabel.Name = "CurrentLevelUnitsLabel";
        this.CurrentLevelUnitsLabel.Size = new Size(49, 15);
        this.CurrentLevelUnitsLabel.TabIndex = 10;
        this.CurrentLevelUnitsLabel.Text = "Ampere";
        // 
        // CurrentLevelNumericLabel
        // 
        this.CurrentLevelNumericLabel.AutoSize = true;
        this.CurrentLevelNumericLabel.Location = new Point(9, 88);
        this.CurrentLevelNumericLabel.Margin = new Padding(4, 0, 4, 0);
        this.CurrentLevelNumericLabel.Name = "CurrentLevelNumericLabel";
        this.CurrentLevelNumericLabel.Size = new Size(50, 15);
        this.CurrentLevelNumericLabel.TabIndex = 9;
        this.CurrentLevelNumericLabel.Text = "Current:";
        this.CurrentLevelNumericLabel.TextAlign = ContentAlignment.TopRight;
        // 
        // CurrentSourceOption
        // 
        this.CurrentSourceOption.AutoSize = true;
        this.CurrentSourceOption.CheckAlign = ContentAlignment.MiddleRight;
        this.CurrentSourceOption.Location = new Point(143, 23);
        this.CurrentSourceOption.Margin = new Padding(4, 3, 4, 3);
        this.CurrentSourceOption.Name = "CurrentSourceOption";
        this.CurrentSourceOption.Size = new Size(104, 19);
        this.CurrentSourceOption.TabIndex = 8;
        this.CurrentSourceOption.Text = "Current Source";
        this.CurrentSourceOption.UseVisualStyleBackColor = true;
        // 
        // VoltageSourceOption
        // 
        this.VoltageSourceOption.AutoSize = true;
        this.VoltageSourceOption.CheckAlign = ContentAlignment.MiddleRight;
        this.VoltageSourceOption.Checked = true;
        this.VoltageSourceOption.Location = new Point(8, 23);
        this.VoltageSourceOption.Margin = new Padding(4, 3, 4, 3);
        this.VoltageSourceOption.Name = "VoltageSourceOption";
        this.VoltageSourceOption.Size = new Size(103, 19);
        this.VoltageSourceOption.TabIndex = 7;
        this.VoltageSourceOption.TabStop = true;
        this.VoltageSourceOption.Text = "Voltage Source";
        this.VoltageSourceOption.UseVisualStyleBackColor = true;
        // 
        // ApertureUnitsLabel
        // 
        this.ApertureUnitsLabel.AutoSize = true;
        this.ApertureUnitsLabel.Font = new Font("Segoe UI", 9F);
        this.ApertureUnitsLabel.ForeColor = Color.Chartreuse;
        this.ApertureUnitsLabel.Location = new Point(295, 62);
        this.ApertureUnitsLabel.Margin = new Padding(4, 0, 4, 0);
        this.ApertureUnitsLabel.Name = "ApertureUnitsLabel";
        this.ApertureUnitsLabel.Size = new Size(102, 15);
        this.ApertureUnitsLabel.TabIndex = 5;
        this.ApertureUnitsLabel.Text = "Power Line Cycles";
        // 
        // VoltageLevelUnitsLabel
        // 
        this.VoltageLevelUnitsLabel.AutoSize = true;
        this.VoltageLevelUnitsLabel.Font = new Font("Segoe UI", 9F);
        this.VoltageLevelUnitsLabel.ForeColor = Color.Chartreuse;
        this.VoltageLevelUnitsLabel.Location = new Point(121, 59);
        this.VoltageLevelUnitsLabel.Margin = new Padding(4, 0, 4, 0);
        this.VoltageLevelUnitsLabel.Name = "VoltageLevelUnitsLabel";
        this.VoltageLevelUnitsLabel.Size = new Size(27, 15);
        this.VoltageLevelUnitsLabel.TabIndex = 3;
        this.VoltageLevelUnitsLabel.Text = "Volt";
        // 
        // IPAddressTextBoxLabel
        // 
        this.IPAddressTextBoxLabel.AutoSize = true;
        this.IPAddressTextBoxLabel.Location = new Point(4, 26);
        this.IPAddressTextBoxLabel.Margin = new Padding(4, 0, 4, 0);
        this.IPAddressTextBoxLabel.Name = "IPAddressTextBoxLabel";
        this.IPAddressTextBoxLabel.Size = new Size(65, 15);
        this.IPAddressTextBoxLabel.TabIndex = 6;
        this.IPAddressTextBoxLabel.Text = "IP Address:";
        // 
        // IPAddressTextBox
        // 
        this.IPAddressTextBox.Location = new Point(72, 22);
        this.IPAddressTextBox.Margin = new Padding(4, 3, 4, 3);
        this.IPAddressTextBox.Name = "IPAddressTextBox";
        this.IPAddressTextBox.Size = new Size(116, 23);
        this.IPAddressTextBox.TabIndex = 0;
        this.IPAddressTextBox.Text = "192.168.0.150";
        // 
        // MeasureButton
        // 
        this.MeasureButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
        this.MeasureButton.ForeColor = SystemColors.ControlText;
        this.MeasureButton.Location = new Point(297, 16);
        this.MeasureButton.Margin = new Padding(4, 3, 4, 3);
        this.MeasureButton.Name = "MeasureButton";
        this.MeasureButton.Size = new Size(103, 36);
        this.MeasureButton.TabIndex = 14;
        this.MeasureButton.Text = "Measure";
        this.MeasureButton.UseVisualStyleBackColor = true;
        // 
        // MeasurementGroupBox
        // 
        this.MeasurementGroupBox.Controls.Add(this.RunningStateLabel);
        this.MeasurementGroupBox.Controls.Add(this.ResistanceReadingLabelLabel);
        this.MeasurementGroupBox.Controls.Add(this.CurrentReadingLabelLabel);
        this.MeasurementGroupBox.Controls.Add(this.VoltageReadingLabelLabel);
        this.MeasurementGroupBox.Controls.Add(this.DurationLabelLabel);
        this.MeasurementGroupBox.Controls.Add(this.MeasureButton);
        this.MeasurementGroupBox.Controls.Add(this.ResistanceReadingLabel);
        this.MeasurementGroupBox.Controls.Add(this.CurrentReadingLabel);
        this.MeasurementGroupBox.Controls.Add(this.VoltageReadingLabel);
        this.MeasurementGroupBox.Controls.Add(this.DurationLabel);
        this.MeasurementGroupBox.Font = new Font("Segoe UI", 8.25F);
        this.MeasurementGroupBox.ForeColor = Color.Chartreuse;
        this.MeasurementGroupBox.Location = new Point(18, 252);
        this.MeasurementGroupBox.Margin = new Padding(4, 3, 4, 3);
        this.MeasurementGroupBox.Name = "MeasurementGroupBox";
        this.MeasurementGroupBox.Padding = new Padding(4, 3, 4, 3);
        this.MeasurementGroupBox.Size = new Size(408, 125);
        this.MeasurementGroupBox.TabIndex = 11;
        this.MeasurementGroupBox.TabStop = false;
        this.MeasurementGroupBox.Text = "Measurement";
        // 
        // RunningStateLabel
        // 
        this.RunningStateLabel.AutoSize = true;
        this.RunningStateLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
        this.RunningStateLabel.ForeColor = SystemColors.ActiveCaption;
        this.RunningStateLabel.Location = new Point(297, 55);
        this.RunningStateLabel.Margin = new Padding(4, 0, 4, 0);
        this.RunningStateLabel.Name = "RunningStateLabel";
        this.RunningStateLabel.Size = new Size(104, 30);
        this.RunningStateLabel.TabIndex = 15;
        this.RunningStateLabel.Text = "Running...";
        // 
        // ResistanceReadingLabelLabel
        // 
        this.ResistanceReadingLabelLabel.Location = new Point(9, 90);
        this.ResistanceReadingLabelLabel.Margin = new Padding(4, 0, 4, 0);
        this.ResistanceReadingLabelLabel.Name = "ResistanceReadingLabelLabel";
        this.ResistanceReadingLabelLabel.Size = new Size(68, 13);
        this.ResistanceReadingLabelLabel.TabIndex = 6;
        this.ResistanceReadingLabelLabel.Text = "Resistance:";
        this.ResistanceReadingLabelLabel.TextAlign = ContentAlignment.TopRight;
        // 
        // CurrentReadingLabelLabel
        // 
        this.CurrentReadingLabelLabel.Location = new Point(9, 68);
        this.CurrentReadingLabelLabel.Margin = new Padding(4, 0, 4, 0);
        this.CurrentReadingLabelLabel.Name = "CurrentReadingLabelLabel";
        this.CurrentReadingLabelLabel.Size = new Size(68, 13);
        this.CurrentReadingLabelLabel.TabIndex = 6;
        this.CurrentReadingLabelLabel.Text = "Current:";
        this.CurrentReadingLabelLabel.TextAlign = ContentAlignment.TopRight;
        // 
        // VoltageReadingLabelLabel
        // 
        this.VoltageReadingLabelLabel.Location = new Point(9, 46);
        this.VoltageReadingLabelLabel.Margin = new Padding(4, 0, 4, 0);
        this.VoltageReadingLabelLabel.Name = "VoltageReadingLabelLabel";
        this.VoltageReadingLabelLabel.Size = new Size(68, 13);
        this.VoltageReadingLabelLabel.TabIndex = 6;
        this.VoltageReadingLabelLabel.Text = "Voltage:";
        this.VoltageReadingLabelLabel.TextAlign = ContentAlignment.TopRight;
        // 
        // DurationLabelLabel
        // 
        this.DurationLabelLabel.Location = new Point(12, 24);
        this.DurationLabelLabel.Margin = new Padding(4, 0, 4, 0);
        this.DurationLabelLabel.Name = "DurationLabelLabel";
        this.DurationLabelLabel.Size = new Size(65, 13);
        this.DurationLabelLabel.TabIndex = 6;
        this.DurationLabelLabel.Text = "Duration:";
        this.DurationLabelLabel.TextAlign = ContentAlignment.TopRight;
        // 
        // ResistanceReadingLabel
        // 
        this.ResistanceReadingLabel.Location = new Point(80, 90);
        this.ResistanceReadingLabel.Margin = new Padding(4, 3, 4, 3);
        this.ResistanceReadingLabel.Name = "ResistanceReadingLabel";
        this.ResistanceReadingLabel.Size = new Size(116, 13);
        this.ResistanceReadingLabel.TabIndex = 0;
        // 
        // CurrentReadingLabel
        // 
        this.CurrentReadingLabel.Location = new Point(80, 68);
        this.CurrentReadingLabel.Margin = new Padding(4, 3, 4, 3);
        this.CurrentReadingLabel.Name = "CurrentReadingLabel";
        this.CurrentReadingLabel.Size = new Size(116, 13);
        this.CurrentReadingLabel.TabIndex = 0;
        // 
        // VoltageReadingLabel
        // 
        this.VoltageReadingLabel.Location = new Point(80, 46);
        this.VoltageReadingLabel.Margin = new Padding(4, 3, 4, 3);
        this.VoltageReadingLabel.Name = "VoltageReadingLabel";
        this.VoltageReadingLabel.Size = new Size(116, 13);
        this.VoltageReadingLabel.TabIndex = 0;
        // 
        // DurationLabel
        // 
        this.DurationLabel.Location = new Point(80, 24);
        this.DurationLabel.Margin = new Padding(4, 3, 4, 3);
        this.DurationLabel.Name = "DurationLabel";
        this.DurationLabel.Size = new Size(116, 13);
        this.DurationLabel.TabIndex = 0;
        // 
        // IdentityLabelLabel
        // 
        this.IdentityLabelLabel.AutoSize = true;
        this.IdentityLabelLabel.Location = new Point(7, 53);
        this.IdentityLabelLabel.Margin = new Padding(4, 0, 4, 0);
        this.IdentityLabelLabel.Name = "IdentityLabelLabel";
        this.IdentityLabelLabel.Size = new Size(55, 15);
        this.IdentityLabelLabel.TabIndex = 10;
        this.IdentityLabelLabel.Text = "ID String:";
        // 
        // IdentityLabel
        // 
        this.IdentityLabel.Location = new Point(70, 55);
        this.IdentityLabel.Margin = new Padding(4, 3, 4, 3);
        this.IdentityLabel.Name = "IdentityLabel";
        this.IdentityLabel.Size = new Size(328, 33);
        this.IdentityLabel.TabIndex = 9;
        // 
        // InstrumentGroupBox
        // 
        this.InstrumentGroupBox.Controls.Add(this.ToggleConnectionButton);
        this.InstrumentGroupBox.Controls.Add(this.IdentityLabel);
        this.InstrumentGroupBox.Controls.Add(this.IdentityLabelLabel);
        this.InstrumentGroupBox.Controls.Add(this.IPAddressTextBoxLabel);
        this.InstrumentGroupBox.Controls.Add(this.IPAddressTextBox);
        this.InstrumentGroupBox.ForeColor = Color.Chartreuse;
        this.InstrumentGroupBox.Location = new Point(18, 3);
        this.InstrumentGroupBox.Name = "InstrumentGroupBox";
        this.InstrumentGroupBox.Size = new Size(408, 100);
        this.InstrumentGroupBox.TabIndex = 17;
        this.InstrumentGroupBox.TabStop = false;
        this.InstrumentGroupBox.Text = "Instrument Settings";
        // 
        // ToggleConnectionButton
        // 
        this.ToggleConnectionButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
        this.ToggleConnectionButton.ForeColor = SystemColors.ControlText;
        this.ToggleConnectionButton.Location = new Point(205, 16);
        this.ToggleConnectionButton.Name = "ToggleConnectionButton";
        this.ToggleConnectionButton.Size = new Size(193, 33);
        this.ToggleConnectionButton.TabIndex = 11;
        this.ToggleConnectionButton.Text = "Open Connection";
        this.ToggleConnectionButton.UseVisualStyleBackColor = true;
        // 
        // UserPromptLabel
        // 
        this.UserPromptLabel.ForeColor = SystemColors.Info;
        this.UserPromptLabel.Location = new Point(18, 390);
        this.UserPromptLabel.Name = "UserPromptLabel";
        this.UserPromptLabel.Size = new Size(408, 78);
        this.UserPromptLabel.TabIndex = 18;
        this.UserPromptLabel.Text = "Press 'Open Connection' to connect to the instrument over LAN.";
        // 
        // OhmView
        // 
        this.AutoScaleDimensions = new SizeF(7F, 15F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.BackColor = Color.FromArgb(64, 64, 64);
        this.ClientSize = new Size(441, 477);
        this.Controls.Add(this.UserPromptLabel);
        this.Controls.Add(this.InstrumentGroupBox);
        this.Controls.Add(this.MeasurementGroupBox);
        this.Controls.Add(this.OhmGroupBox);
        this.Icon = (Icon)resources.GetObject("$this.Icon");
        this.Margin = new Padding(4, 3, 4, 3);
        this.Name = "OhmView";
        this.Text = "2600 Cold Resistance Meter";
        this.Load += this.OhmView_Load;
        ((System.ComponentModel.ISupportInitialize)this.VoltageLevelNumeric).EndInit();
        ((System.ComponentModel.ISupportInitialize)this.ApertureNumeric).EndInit();
        this.OhmGroupBox.ResumeLayout(false);
        this.OhmGroupBox.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)this.CurrentLevelNumeric).EndInit();
        this.MeasurementGroupBox.ResumeLayout(false);
        this.MeasurementGroupBox.PerformLayout();
        this.InstrumentGroupBox.ResumeLayout(false);
        this.InstrumentGroupBox.PerformLayout();
        this.ResumeLayout(false);
    }

    #endregion
    private System.Windows.Forms.NumericUpDown VoltageLevelNumeric;
    private System.Windows.Forms.Label VoltageLevelNumericLabel;
    private System.Windows.Forms.NumericUpDown ApertureNumeric;
    private System.Windows.Forms.Label ApertureNumericLabel;
    private System.Windows.Forms.GroupBox OhmGroupBox;
    private System.Windows.Forms.RadioButton CurrentSourceOption;
    private System.Windows.Forms.RadioButton VoltageSourceOption;
    private System.Windows.Forms.Label IPAddressTextBoxLabel;
    private System.Windows.Forms.TextBox IPAddressTextBox;
    private System.Windows.Forms.Label CurrentLevelNumericLabel;
    private System.Windows.Forms.Label CurrentLevelUnitsLabel;
    private System.Windows.Forms.Button MeasureButton;
    private System.Windows.Forms.GroupBox MeasurementGroupBox;
    private System.Windows.Forms.Label DurationLabelLabel;
    private System.Windows.Forms.Label DurationLabel;
    private System.Windows.Forms.Label IdentityLabelLabel;
    private System.Windows.Forms.Label IdentityLabel;
    private System.Windows.Forms.Label RunningStateLabel;
    private System.Windows.Forms.Label VoltageLevelUnitsLabel;
    private System.Windows.Forms.Label ApertureUnitsLabel;
    private System.Windows.Forms.GroupBox InstrumentGroupBox;
    private System.Windows.Forms.NumericUpDown CurrentLevelNumeric;
    private System.Windows.Forms.CheckBox AutoRangeCheckBox;
    private System.Windows.Forms.Button ToggleConnectionButton;
    private System.Windows.Forms.Label ResistanceReadingLabelLabel;
    private System.Windows.Forms.Label CurrentReadingLabelLabel;
    private System.Windows.Forms.Label VoltageReadingLabelLabel;
    private System.Windows.Forms.Label ResistanceReadingLabel;
    private System.Windows.Forms.Label CurrentReadingLabel;
    private System.Windows.Forms.Label VoltageReadingLabel;
    private System.Windows.Forms.Button ConfigureButton;
    private System.Windows.Forms.Label UserPromptLabel;
}

