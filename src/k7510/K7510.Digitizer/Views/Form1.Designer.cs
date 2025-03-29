namespace cc.isr.Tcp.Tsp.K7510.Digitizer.Views;

partial class Form1
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing )
    {
        if ( disposing && (components != null) )
        {
            components.Dispose();
        }
        base.Dispose( disposing );
    }

    #region " windows form designer generated code "

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.keithleyPictureBox = new System.Windows.Forms.PictureBox();
        this.tekPictureBox = new System.Windows.Forms.PictureBox();
        this.durationNumeric = new System.Windows.Forms.NumericUpDown();
        this.durationNumericLabel = new System.Windows.Forms.Label();
        this.sampleRateNumeric = new System.Windows.Forms.NumericUpDown();
        this.sampleRateNumericLabel = new System.Windows.Forms.Label();
        this.dmmGroupBox = new System.Windows.Forms.GroupBox();
        this.unitsLabel = new System.Windows.Forms.Label();
        this.rangeLabel = new System.Windows.Forms.Label();
        this.rangeComboBox = new System.Windows.Forms.ComboBox();
        this.digitizeCurrentOption = new System.Windows.Forms.RadioButton();
        this.digitizeVoltageOption = new System.Windows.Forms.RadioButton();
        this.ipAddressTextBoxLabel = new System.Windows.Forms.Label();
        this.ipAddressTextBox = new System.Windows.Forms.TextBox();
        this.folderNameTextBox = new System.Windows.Forms.TextBox();
        this.selectFolderButton = new System.Windows.Forms.Button();
        this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
        this.startButton = new System.Windows.Forms.Button();
        this.dmmInfoGroupBox = new System.Windows.Forms.GroupBox();
        this.identityLabelLabel = new System.Windows.Forms.Label();
        this.identityLabel = new System.Windows.Forms.Label();
        this.readingsRateLabelLabel = new System.Windows.Forms.Label();
        this.readingsRateLabel = new System.Windows.Forms.Label();
        this.runtimeLabelLabel = new System.Windows.Forms.Label();
        this.runtimeLabel = new System.Windows.Forms.Label();
        this.runningStateLabel = new System.Windows.Forms.Label();
        this.progressBar = new System.Windows.Forms.ProgressBar();
        this.folderNameTextBoxLabel = new System.Windows.Forms.Label();
        this.durationUnitsLabel = new System.Windows.Forms.Label();
        this.sampleRateUnitsLabel = new System.Windows.Forms.Label();
        (( System.ComponentModel.ISupportInitialize ) (this.keithleyPictureBox)).BeginInit();
        (( System.ComponentModel.ISupportInitialize ) (this.tekPictureBox)).BeginInit();
        (( System.ComponentModel.ISupportInitialize ) (this.durationNumeric)).BeginInit();
        (( System.ComponentModel.ISupportInitialize ) (this.sampleRateNumeric)).BeginInit();
        this.dmmGroupBox.SuspendLayout();
        this.dmmInfoGroupBox.SuspendLayout();
        this.SuspendLayout();
        //
        // keithleyPictureBox
        //
        this.keithleyPictureBox.Image = global::cc.isr.Tcp.Tsp.K7510.Resources.KeithleyLogo;
        this.keithleyPictureBox.Location = new System.Drawing.Point( 322, 9 );
        this.keithleyPictureBox.Margin = new System.Windows.Forms.Padding( 4, 3, 4, 3 );
        this.keithleyPictureBox.Name = "keithleyPictureBox";
        this.keithleyPictureBox.Size = new System.Drawing.Size( 104, 24 );
        this.keithleyPictureBox.TabIndex = 0;
        this.keithleyPictureBox.TabStop = false;
        //
        // tekPictureBox
        //
        this.tekPictureBox.Image = global::cc.isr.Tcp.Tsp.K7510.Resources.TektronixLogo;
        this.tekPictureBox.Location = new System.Drawing.Point( 14, 8 );
        this.tekPictureBox.Margin = new System.Windows.Forms.Padding( 4, 3, 4, 3 );
        this.tekPictureBox.Name = "tekPictureBox";
        this.tekPictureBox.Size = new System.Drawing.Size( 122, 30 );
        this.tekPictureBox.TabIndex = 1;
        this.tekPictureBox.TabStop = false;
        //
        // durationNumeric
        //
        this.durationNumeric.Font = new System.Drawing.Font( "Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point );
        this.durationNumeric.Location = new System.Drawing.Point( 63, 47 );
        this.durationNumeric.Margin = new System.Windows.Forms.Padding( 4, 3, 4, 3 );
        this.durationNumeric.Maximum = new decimal( new int[] {
            86400,
            0,
            0,
            0} );
        this.durationNumeric.Minimum = new decimal( new int[] {
            4,
            0,
            0,
            0} );
        this.durationNumeric.Name = "durationNumeric";
        this.durationNumeric.Size = new System.Drawing.Size( 86, 23 );
        this.durationNumeric.TabIndex = 2;
        this.durationNumeric.Value = new decimal( new int[] {
            1,
            0,
            0,
            0} );
        this.durationNumeric.ValueChanged += new System.EventHandler( this.DurationNumeric_ValueChanged );
        //
        // durationNumericLabel
        //
        this.durationNumericLabel.AutoSize = true;
        this.durationNumericLabel.Font = new System.Drawing.Font( "Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point );
        this.durationNumericLabel.ForeColor = System.Drawing.Color.Chartreuse;
        this.durationNumericLabel.Location = new System.Drawing.Point( 5, 51 );
        this.durationNumericLabel.Margin = new System.Windows.Forms.Padding( 4, 0, 4, 0 );
        this.durationNumericLabel.Name = "durationNumericLabel";
        this.durationNumericLabel.Size = new System.Drawing.Size( 56, 15 );
        this.durationNumericLabel.TabIndex = 3;
        this.durationNumericLabel.Text = "Duration:";
        //
        // sampleRateNumeric
        //
        this.sampleRateNumeric.Font = new System.Drawing.Font( "Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point );
        this.sampleRateNumeric.Increment = new decimal( new int[] {
            1000,
            0,
            0,
            0} );
        this.sampleRateNumeric.Location = new System.Drawing.Point( 63, 77 );
        this.sampleRateNumeric.Margin = new System.Windows.Forms.Padding( 4, 3, 4, 3 );
        this.sampleRateNumeric.Maximum = new decimal( new int[] {
            60000,
            0,
            0,
            0} );
        this.sampleRateNumeric.Minimum = new decimal( new int[] {
            10,
            0,
            0,
            0} );
        this.sampleRateNumeric.Name = "sampleRateNumeric";
        this.sampleRateNumeric.Size = new System.Drawing.Size( 86, 23 );
        this.sampleRateNumeric.TabIndex = 4;
        this.sampleRateNumeric.ThousandsSeparator = true;
        this.sampleRateNumeric.Value = new decimal( new int[] {
            1000,
            0,
            0,
            0} );
        //
        // sampleRateNumericLabel
        //
        this.sampleRateNumericLabel.AutoSize = true;
        this.sampleRateNumericLabel.Font = new System.Drawing.Font( "Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point );
        this.sampleRateNumericLabel.ForeColor = System.Drawing.Color.Chartreuse;
        this.sampleRateNumericLabel.Location = new System.Drawing.Point( 28, 81 );
        this.sampleRateNumericLabel.Margin = new System.Windows.Forms.Padding( 4, 0, 4, 0 );
        this.sampleRateNumericLabel.Name = "sampleRateNumericLabel";
        this.sampleRateNumericLabel.Size = new System.Drawing.Size( 33, 15 );
        this.sampleRateNumericLabel.TabIndex = 5;
        this.sampleRateNumericLabel.Text = "Rate:";
        //
        // dmmGroupBox
        //
        this.dmmGroupBox.Controls.Add( this.unitsLabel );
        this.dmmGroupBox.Controls.Add( this.rangeLabel );
        this.dmmGroupBox.Controls.Add( this.rangeComboBox );
        this.dmmGroupBox.Controls.Add( this.digitizeCurrentOption );
        this.dmmGroupBox.Controls.Add( this.digitizeVoltageOption );
        this.dmmGroupBox.Controls.Add( this.ipAddressTextBoxLabel );
        this.dmmGroupBox.Controls.Add( this.ipAddressTextBox );
        this.dmmGroupBox.Font = new System.Drawing.Font( "Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point );
        this.dmmGroupBox.ForeColor = System.Drawing.Color.Chartreuse;
        this.dmmGroupBox.Location = new System.Drawing.Point( 18, 140 );
        this.dmmGroupBox.Margin = new System.Windows.Forms.Padding( 4, 3, 4, 3 );
        this.dmmGroupBox.Name = "dmmGroupBox";
        this.dmmGroupBox.Padding = new System.Windows.Forms.Padding( 4, 3, 4, 3 );
        this.dmmGroupBox.Size = new System.Drawing.Size( 408, 115 );
        this.dmmGroupBox.TabIndex = 6;
        this.dmmGroupBox.TabStop = false;
        this.dmmGroupBox.Text = "DMM Settings";
        //
        // unitsLabel
        //
        this.unitsLabel.AutoSize = true;
        this.unitsLabel.Location = new System.Drawing.Point( 174, 82 );
        this.unitsLabel.Margin = new System.Windows.Forms.Padding( 4, 0, 4, 0 );
        this.unitsLabel.Name = "unitsLabel";
        this.unitsLabel.Size = new System.Drawing.Size( 14, 15 );
        this.unitsLabel.TabIndex = 10;
        this.unitsLabel.Text = "V";
        //
        // rangeLabel
        //
        this.rangeLabel.AutoSize = true;
        this.rangeLabel.Location = new System.Drawing.Point( 7, 82 );
        this.rangeLabel.Margin = new System.Windows.Forms.Padding( 4, 0, 4, 0 );
        this.rangeLabel.Name = "rangeLabel";
        this.rangeLabel.Size = new System.Drawing.Size( 40, 15 );
        this.rangeLabel.TabIndex = 9;
        this.rangeLabel.Text = "Range";
        //
        // rangeComboBox
        //
        this.rangeComboBox.FormattingEnabled = true;
        this.rangeComboBox.Location = new System.Drawing.Point( 84, 78 );
        this.rangeComboBox.Margin = new System.Windows.Forms.Padding( 4, 3, 4, 3 );
        this.rangeComboBox.Name = "rangeComboBox";
        this.rangeComboBox.Size = new System.Drawing.Size( 81, 23 );
        this.rangeComboBox.TabIndex = 7;
        //
        // digitizeCurrentOption
        //
        this.digitizeCurrentOption.AutoSize = true;
        this.digitizeCurrentOption.Location = new System.Drawing.Point( 150, 52 );
        this.digitizeCurrentOption.Margin = new System.Windows.Forms.Padding( 4, 3, 4, 3 );
        this.digitizeCurrentOption.Name = "digitizeCurrentOption";
        this.digitizeCurrentOption.Size = new System.Drawing.Size( 107, 19 );
        this.digitizeCurrentOption.TabIndex = 8;
        this.digitizeCurrentOption.Text = "Digitize Current";
        this.digitizeCurrentOption.UseVisualStyleBackColor = true;
        this.digitizeCurrentOption.CheckedChanged += new System.EventHandler( this.DigitizeCurrentOption_CheckedChanged );
        //
        // digitizeVoltageOption
        //
        this.digitizeVoltageOption.AutoSize = true;
        this.digitizeVoltageOption.Location = new System.Drawing.Point( 10, 52 );
        this.digitizeVoltageOption.Margin = new System.Windows.Forms.Padding( 4, 3, 4, 3 );
        this.digitizeVoltageOption.Name = "digitizeVoltageOption";
        this.digitizeVoltageOption.Size = new System.Drawing.Size( 106, 19 );
        this.digitizeVoltageOption.TabIndex = 7;
        this.digitizeVoltageOption.Text = "Digitize Voltage";
        this.digitizeVoltageOption.UseVisualStyleBackColor = true;
        this.digitizeVoltageOption.CheckedChanged += new System.EventHandler( this.DigitizeVoltageOption_CheckedChanged );
        //
        // ipAddressTextBoxLabel
        //
        this.ipAddressTextBoxLabel.AutoSize = true;
        this.ipAddressTextBoxLabel.Location = new System.Drawing.Point( 7, 25 );
        this.ipAddressTextBoxLabel.Margin = new System.Windows.Forms.Padding( 4, 0, 4, 0 );
        this.ipAddressTextBoxLabel.Name = "ipAddressTextBoxLabel";
        this.ipAddressTextBoxLabel.Size = new System.Drawing.Size( 62, 15 );
        this.ipAddressTextBoxLabel.TabIndex = 6;
        this.ipAddressTextBoxLabel.Text = "IP Address";
        //
        // ipAddressTextBox
        //
        this.ipAddressTextBox.Location = new System.Drawing.Point( 150, 22 );
        this.ipAddressTextBox.Margin = new System.Windows.Forms.Padding( 4, 3, 4, 3 );
        this.ipAddressTextBox.Name = "ipAddressTextBox";
        this.ipAddressTextBox.Size = new System.Drawing.Size( 116, 23 );
        this.ipAddressTextBox.TabIndex = 0;
        this.ipAddressTextBox.Text = "192.168.0.144";
        //
        // folderNameTextBox
        //
        this.folderNameTextBox.Font = new System.Drawing.Font( "Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point );
        this.folderNameTextBox.Location = new System.Drawing.Point( 63, 108 );
        this.folderNameTextBox.Margin = new System.Windows.Forms.Padding( 4, 3, 4, 3 );
        this.folderNameTextBox.Name = "folderNameTextBox";
        this.folderNameTextBox.Size = new System.Drawing.Size( 362, 23 );
        this.folderNameTextBox.TabIndex = 13;
        this.folderNameTextBox.Text = "r:\\lxi\\k7510";
        //
        // selectFolderButton
        //
        this.selectFolderButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
        this.selectFolderButton.Location = new System.Drawing.Point( 390, 75 );
        this.selectFolderButton.Margin = new System.Windows.Forms.Padding( 4, 3, 4, 3 );
        this.selectFolderButton.Name = "selectFolderButton";
        this.selectFolderButton.Size = new System.Drawing.Size( 36, 27 );
        this.selectFolderButton.TabIndex = 12;
        this.selectFolderButton.Text = "...";
        this.selectFolderButton.UseVisualStyleBackColor = true;
        this.selectFolderButton.Click += new System.EventHandler( this.SelectFolderButton_Click );
        //
        // startButton
        //
        this.startButton.Font = new System.Drawing.Font( "Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point );
        this.startButton.Location = new System.Drawing.Point( 18, 260 );
        this.startButton.Margin = new System.Windows.Forms.Padding( 4, 3, 4, 3 );
        this.startButton.Name = "startButton";
        this.startButton.Size = new System.Drawing.Size( 103, 36 );
        this.startButton.TabIndex = 14;
        this.startButton.Text = "START";
        this.startButton.UseVisualStyleBackColor = true;
        this.startButton.Click += new System.EventHandler( this.StartButton_Click );
        //
        // dmmInfoGroupBox
        //
        this.dmmInfoGroupBox.Controls.Add( this.identityLabelLabel );
        this.dmmInfoGroupBox.Controls.Add( this.identityLabel );
        this.dmmInfoGroupBox.Controls.Add( this.readingsRateLabelLabel );
        this.dmmInfoGroupBox.Controls.Add( this.readingsRateLabel );
        this.dmmInfoGroupBox.Controls.Add( this.runtimeLabelLabel );
        this.dmmInfoGroupBox.Controls.Add( this.runtimeLabel );
        this.dmmInfoGroupBox.Font = new System.Drawing.Font( "Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point );
        this.dmmInfoGroupBox.ForeColor = System.Drawing.Color.Chartreuse;
        this.dmmInfoGroupBox.Location = new System.Drawing.Point( 18, 300 );
        this.dmmInfoGroupBox.Margin = new System.Windows.Forms.Padding( 4, 3, 4, 3 );
        this.dmmInfoGroupBox.Name = "dmmInfoGroupBox";
        this.dmmInfoGroupBox.Padding = new System.Windows.Forms.Padding( 4, 3, 4, 3 );
        this.dmmInfoGroupBox.Size = new System.Drawing.Size( 408, 125 );
        this.dmmInfoGroupBox.TabIndex = 11;
        this.dmmInfoGroupBox.TabStop = false;
        this.dmmInfoGroupBox.Text = "DMM Info";
        //
        // identityLabelLabel
        //
        this.identityLabelLabel.AutoSize = true;
        this.identityLabelLabel.Location = new System.Drawing.Point( 7, 29 );
        this.identityLabelLabel.Margin = new System.Windows.Forms.Padding( 4, 0, 4, 0 );
        this.identityLabelLabel.Name = "identityLabelLabel";
        this.identityLabelLabel.Size = new System.Drawing.Size( 55, 13 );
        this.identityLabelLabel.TabIndex = 10;
        this.identityLabelLabel.Text = "ID String:";
        //
        // identityLabel
        //
        this.identityLabel.Location = new System.Drawing.Point( 65, 29 );
        this.identityLabel.Margin = new System.Windows.Forms.Padding( 4, 3, 4, 3 );
        this.identityLabel.Name = "identityLabel";
        this.identityLabel.Size = new System.Drawing.Size( 333, 13 );
        this.identityLabel.TabIndex = 9;
        //
        // readingsRateLabelLabel
        //
        this.readingsRateLabelLabel.AutoSize = true;
        this.readingsRateLabelLabel.Location = new System.Drawing.Point( 29, 89 );
        this.readingsRateLabelLabel.Margin = new System.Windows.Forms.Padding( 4, 0, 4, 0 );
        this.readingsRateLabelLabel.Name = "readingsRateLabelLabel";
        this.readingsRateLabelLabel.Size = new System.Drawing.Size( 33, 13 );
        this.readingsRateLabelLabel.TabIndex = 8;
        this.readingsRateLabelLabel.Text = "Rate:";
        //
        // readingsRateLabel
        //
        this.readingsRateLabel.AutoSize = true;
        this.readingsRateLabel.Location = new System.Drawing.Point( 65, 89 );
        this.readingsRateLabel.Margin = new System.Windows.Forms.Padding( 4, 3, 4, 3 );
        this.readingsRateLabel.Name = "readingsRateLabel";
        this.readingsRateLabel.Size = new System.Drawing.Size( 116, 13 );
        this.readingsRateLabel.TabIndex = 7;
        //
        // runtimeLabelLabel
        //
        this.runtimeLabelLabel.AutoSize = true;
        this.runtimeLabelLabel.Location = new System.Drawing.Point( 9, 59 );
        this.runtimeLabelLabel.Margin = new System.Windows.Forms.Padding( 4, 0, 4, 0 );
        this.runtimeLabelLabel.Name = "runtimeLabelLabel";
        this.runtimeLabelLabel.Size = new System.Drawing.Size( 53, 13 );
        this.runtimeLabelLabel.TabIndex = 6;
        this.runtimeLabelLabel.Text = "Runtime:";
        //
        // runtimeLabel
        //
        this.runtimeLabel.Location = new System.Drawing.Point( 65, 59 );
        this.runtimeLabel.Margin = new System.Windows.Forms.Padding( 4, 3, 4, 3 );
        this.runtimeLabel.Name = "runtimeLabel";
        this.runtimeLabel.Size = new System.Drawing.Size( 116, 13 );
        this.runtimeLabel.TabIndex = 0;
        //
        // runningStateLabel
        //
        this.runningStateLabel.AutoSize = true;
        this.runningStateLabel.Font = new System.Drawing.Font( "Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point );
        this.runningStateLabel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
        this.runningStateLabel.Location = new System.Drawing.Point( 127, 265 );
        this.runningStateLabel.Margin = new System.Windows.Forms.Padding( 4, 0, 4, 0 );
        this.runningStateLabel.Name = "runningStateLabel";
        this.runningStateLabel.Size = new System.Drawing.Size( 111, 26 );
        this.runningStateLabel.TabIndex = 15;
        this.runningStateLabel.Text = "Running...";
        //
        // progressBar
        //
        this.progressBar.Location = new System.Drawing.Point( 246, 264 );
        this.progressBar.Margin = new System.Windows.Forms.Padding( 4, 3, 4, 3 );
        this.progressBar.Name = "progressBar";
        this.progressBar.Size = new System.Drawing.Size( 180, 27 );
        this.progressBar.TabIndex = 16;
        //
        // folderNameTextBoxLabel
        //
        this.folderNameTextBoxLabel.AutoSize = true;
        this.folderNameTextBoxLabel.Font = new System.Drawing.Font( "Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point );
        this.folderNameTextBoxLabel.ForeColor = System.Drawing.Color.Chartreuse;
        this.folderNameTextBoxLabel.Location = new System.Drawing.Point( 18, 112 );
        this.folderNameTextBoxLabel.Margin = new System.Windows.Forms.Padding( 4, 0, 4, 0 );
        this.folderNameTextBoxLabel.Name = "folderNameTextBoxLabel";
        this.folderNameTextBoxLabel.Size = new System.Drawing.Size( 43, 15 );
        this.folderNameTextBoxLabel.TabIndex = 5;
        this.folderNameTextBoxLabel.Text = "Folder:";
        //
        // durationUnitsLabel
        //
        this.durationUnitsLabel.AutoSize = true;
        this.durationUnitsLabel.Font = new System.Drawing.Font( "Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point );
        this.durationUnitsLabel.ForeColor = System.Drawing.Color.Chartreuse;
        this.durationUnitsLabel.Location = new System.Drawing.Point( 153, 51 );
        this.durationUnitsLabel.Margin = new System.Windows.Forms.Padding( 4, 0, 4, 0 );
        this.durationUnitsLabel.Name = "durationUnitsLabel";
        this.durationUnitsLabel.Size = new System.Drawing.Size( 51, 15 );
        this.durationUnitsLabel.TabIndex = 3;
        this.durationUnitsLabel.Text = "Seconds";
        //
        // sampleRateUnitsLabel
        //
        this.sampleRateUnitsLabel.AutoSize = true;
        this.sampleRateUnitsLabel.Font = new System.Drawing.Font( "Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point );
        this.sampleRateUnitsLabel.ForeColor = System.Drawing.Color.Chartreuse;
        this.sampleRateUnitsLabel.Location = new System.Drawing.Point( 153, 81 );
        this.sampleRateUnitsLabel.Margin = new System.Windows.Forms.Padding( 4, 0, 4, 0 );
        this.sampleRateUnitsLabel.Name = "sampleRateUnitsLabel";
        this.sampleRateUnitsLabel.Size = new System.Drawing.Size( 112, 15 );
        this.sampleRateUnitsLabel.TabIndex = 5;
        this.sampleRateUnitsLabel.Text = "Samples per second";
        //
        // Form1
        //
        this.AutoScaleDimensions = new System.Drawing.SizeF( 7F, 15F );
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb( (( int ) ((( byte ) (64)))), (( int ) ((( byte ) (64)))), (( int ) ((( byte ) (64)))) );
        this.ClientSize = new System.Drawing.Size( 441, 433 );
        this.Controls.Add( this.progressBar );
        this.Controls.Add( this.runningStateLabel );
        this.Controls.Add( this.dmmInfoGroupBox );
        this.Controls.Add( this.startButton );
        this.Controls.Add( this.folderNameTextBox );
        this.Controls.Add( this.selectFolderButton );
        this.Controls.Add( this.dmmGroupBox );
        this.Controls.Add( this.folderNameTextBoxLabel );
        this.Controls.Add( this.sampleRateUnitsLabel );
        this.Controls.Add( this.sampleRateNumericLabel );
        this.Controls.Add( this.sampleRateNumeric );
        this.Controls.Add( this.durationUnitsLabel );
        this.Controls.Add( this.durationNumericLabel );
        this.Controls.Add( this.durationNumeric );
        this.Controls.Add( this.tekPictureBox );
        this.Controls.Add( this.keithleyPictureBox );
        this.Margin = new System.Windows.Forms.Padding( 4, 3, 4, 3 );
        this.Name = "Form1";
        this.Text = "DMM7510 Digitizer Control Tool";
        this.Load += new System.EventHandler( this.Form1_Load );
        (( System.ComponentModel.ISupportInitialize ) (this.keithleyPictureBox)).EndInit();
        (( System.ComponentModel.ISupportInitialize ) (this.tekPictureBox)).EndInit();
        (( System.ComponentModel.ISupportInitialize ) (this.durationNumeric)).EndInit();
        (( System.ComponentModel.ISupportInitialize ) (this.sampleRateNumeric)).EndInit();
        this.dmmGroupBox.ResumeLayout( false );
        this.dmmGroupBox.PerformLayout();
        this.dmmInfoGroupBox.ResumeLayout( false );
        this.dmmInfoGroupBox.PerformLayout();
        this.ResumeLayout( false );
        this.PerformLayout();
    }

    #endregion

    private System.Windows.Forms.PictureBox keithleyPictureBox;
    private System.Windows.Forms.PictureBox tekPictureBox;
    private System.Windows.Forms.NumericUpDown durationNumeric;
    private System.Windows.Forms.Label durationNumericLabel;
    private System.Windows.Forms.NumericUpDown sampleRateNumeric;
    private System.Windows.Forms.Label sampleRateNumericLabel;
    private System.Windows.Forms.GroupBox dmmGroupBox;
    private System.Windows.Forms.RadioButton digitizeCurrentOption;
    private System.Windows.Forms.RadioButton digitizeVoltageOption;
    private System.Windows.Forms.Label ipAddressTextBoxLabel;
    private System.Windows.Forms.TextBox ipAddressTextBox;
    private System.Windows.Forms.Label rangeLabel;
    private System.Windows.Forms.ComboBox rangeComboBox;
    private System.Windows.Forms.Label unitsLabel;
    private System.Windows.Forms.TextBox folderNameTextBox;
    private System.Windows.Forms.Button selectFolderButton;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    private System.Windows.Forms.Button startButton;
    private System.Windows.Forms.GroupBox dmmInfoGroupBox;
    private System.Windows.Forms.Label runtimeLabelLabel;
    private System.Windows.Forms.Label runtimeLabel;
    private System.Windows.Forms.Label readingsRateLabelLabel;
    private System.Windows.Forms.Label readingsRateLabel;
    private System.Windows.Forms.Label identityLabelLabel;
    private System.Windows.Forms.Label identityLabel;
    private System.Windows.Forms.Label runningStateLabel;
    private System.Windows.Forms.ProgressBar progressBar;
    private Label folderNameTextBoxLabel;
    private Label durationUnitsLabel;
    private Label sampleRateUnitsLabel;
}

