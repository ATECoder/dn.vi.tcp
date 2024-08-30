using cc.isr.Tcp.Tsp.Device;

namespace cc.isr.Tcp.Tsp.K2600.Ohm.Views;

/// <summary>   An ohm view. </summary>
/// <remarks>   2024-02-06. </remarks>
public partial class OhmView : Form
{
    public OhmView()
    {
        this.InitializeComponent();
        this.ToggleConnectionButton.Click += this.ToggleConnectionButton_Click;
        this.ConfigureButton.Click += this.ConfigureButton_Click;
        this.MeasureButton.Click += this.MeasureButton_Click;

    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing )
    {
        if ( disposing )
        {
            this.components?.Dispose();
            this.TspDevice?.Dispose();
        }
        base.Dispose( disposing );
    }

    /// <summary>   Gets or sets the tsp device. </summary>
    /// <value> The tsp device. </value>
    private TspDevice? TspDevice { get; set; }

    /// <summary>   Event handler. Called by OhmView for load events. </summary>
    /// <remarks>   2024-02-06. </remarks>
    /// <param name="sender">   Source of the event. </param>
    /// <param name="e">        Event information. </param>
    private void OhmView_Load( object sender, EventArgs e )
    {
        this.VoltageSourceOption.Checked = true;
        this.RunningStateLabel.Text = string.Empty;
        this.ConfigureButton.Enabled = false;
        this.MeasureButton.Enabled = false;
        this.ToggleConnectionButton.Enabled = true;
        this.UserPromptLabel.Text = $"Press '{this.ConfigureButton.Text}' to connect to the instrument at the {this.IPAddressTextBox.Text} address.";
    }

    /// <summary>   Toggle connection. </summary>
    /// <remarks>   2024-02-06. </remarks>
    private void ToggleConnection()
    {
        try
        {
            this.ConfigureButton.Enabled = false;
            this.ToggleConnectionButton.Enabled = false;
            this.MeasureButton.Enabled = false;
            this.UserPromptLabel.Text = "";

            if ( this.TspDevice == null || !this.TspDevice.Session.Connected )
            {
                this.TspDevice = new( this.IPAddressTextBox.Text );
            }

            if ( this.TspDevice.Session.Connected )
            {
                this.UserPromptLabel.Text = "Disconnecting...";
                this.TspDevice.Disconnect();
                this.IdentityLabel.Text = string.Empty;
                this.UserPromptLabel.Text = $"Disconnected; press '{this.ConfigureButton.Text}' to connect.";
                Application.DoEvents();
            }
            else
            {
                this.UserPromptLabel.Text = "Connecting...";
                string identity = string.Empty;
                this.TspDevice.Connect( true, ref identity );
                this.IdentityLabel.Text = identity;
                this.ConfigureButton.Enabled = true;
                this.UserPromptLabel.Text = $"Connected; press '{this.ConfigureButton.Text}' to configure the measurement.";
                Application.DoEvents();
            }
            this.ToggleConnectionButton.Text = this.TspDevice.Session.Connected ? "Close connection" : "Open connection";

        }
        catch ( Exception ex )
        {
            this.UserPromptLabel.Text = $"Connection failed: {ex.Message}";
        }
        finally
        {
            this.ToggleConnectionButton.Enabled = true;
            Application.DoEvents();
        }
    }

    /// <summary>   Event handler. Called by ToggleConnectionButton for click events. </summary>
    /// <remarks>   2024-02-06. </remarks>
    /// <param name="sender">   Source of the event. </param>
    /// <param name="e">        Event information. </param>
    private void ToggleConnectionButton_Click( object? sender, EventArgs e )
    {
        this.ToggleConnection();
    }

    /// <summary>   Configure constant source ohm. </summary>
    /// <remarks>   2024-02-06. </remarks>
    private void ConfigureConstantSourceOhm()
    {
        try
        {
            this.ConfigureButton.Enabled = false;
            this.ToggleConnectionButton.Enabled = false;
            this.MeasureButton.Enabled = false;
            this.UserPromptLabel.Text = "";

            if ( this.TspDevice == null || !this.TspDevice.Session.Connected )
                this.ToggleConnection();

            if ( this.TspDevice != null && this.TspDevice.Session.Connected )
            {
                this.UserPromptLabel.Text = "Configuring...";
                this.TspDevice.CurrentLevel = ( double ) this.CurrentLevelNumeric.Value;
                this.TspDevice.VoltageLevel = ( double ) this.VoltageLevelNumeric.Value;
                this.TspDevice.AutoRange = this.AutoRangeCheckBox.Checked;
                this.TspDevice.Aperture = ( double ) this.ApertureNumeric.Value;
                this.TspDevice.SourceFunction = this.VoltageSourceOption.Checked
                        ? TspDevice.DCVoltageSourceFunction
                        : TspDevice.DCCurrentSourceFunction;
                this.TspDevice.ConfigureConstantSource();
                this.UserPromptLabel.Text = $"Configured; Press '{this.MeasureButton.Text}'.";
                Application.DoEvents();
                this.MeasureButton.Enabled = true;
            }
        }
        catch ( Exception ex )
        {
            this.UserPromptLabel.Text = $"Configuration failed: {ex.Message}";
        }
        finally
        {
            this.ConfigureButton.Enabled = true;
            this.ToggleConnectionButton.Enabled = true;
            Application.DoEvents();
        }
    }

    /// <summary>   Event handler. Called by ConfigureButton for click events. </summary>
    /// <remarks>   2024-02-06. </remarks>
    /// <param name="sender">   Source of the event. </param>
    /// <param name="e">        Event information. </param>
    private void ConfigureButton_Click( object? sender, EventArgs e )
    {
        this.ConfigureConstantSourceOhm();
    }

    /// <summary>   Measures this object. </summary>
    /// <remarks>   2024-02-06. </remarks>
    private void Measure()
    {
        if ( this.TspDevice == null || !this.TspDevice.Session.Connected ) return;

        try
        {
            this.ConfigureButton.Enabled = false;
            this.ToggleConnectionButton.Enabled = false;
            this.MeasureButton.Enabled = false;
            this.UserPromptLabel.Text = "";
            Application.DoEvents();

            this.RunningStateLabel.Text = "Measuring...";
            Application.DoEvents();

            _ = (this.TspDevice?.MeasureResistance());

            this.DurationLabel.Text = $"{this.TspDevice!.ReadingDuration.GetValueOrDefault().Milliseconds} ms";
            this.VoltageReadingLabel.Text = $"{this.TspDevice!.VoltageReading} Volt";
            this.CurrentReadingLabel.Text = $"{this.TspDevice!.CurrentReading} Ampere";
            this.ResistanceReadingLabel.Text = $"{this.TspDevice.Resistance:0.000} Ohm";
            this.UserPromptLabel.Text = "Measurement completed.";
            this.RunningStateLabel.Text = "Done";
            Application.DoEvents();

        }
        catch ( Exception ex )
        {
            this.RunningStateLabel.Text = "Measurement failed";
            this.UserPromptLabel.Text = $"Measurement failed: {ex.Message}";
        }
        finally
        {
            this.MeasureButton.Enabled = true;
            this.ConfigureButton.Enabled = true;
            this.ToggleConnectionButton.Enabled = true;
            Application.DoEvents();
        }
    }

    /// <summary>   Event handler. Called by MeasureButton for click events. </summary>
    /// <remarks>   2024-02-06. </remarks>
    /// <param name="sender">   Source of the event. </param>
    /// <param name="e">        Event information. </param>
	private void MeasureButton_Click( object? sender, EventArgs e )
    {
        this.Measure();
    }
}
