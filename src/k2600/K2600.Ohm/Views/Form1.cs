using System.Diagnostics;

using Keithley.Dmm7510.Device;

namespace Keithley.Dmm7510.Digitizer.Views;

public partial class Form1 : Form
{
	
	public Form1()
	{
        this.InitializeComponent();
	}

	private void DurationNumeric_ValueChanged(object sender, EventArgs e)
	{
		if( this.durationNumeric.Value < 1)
		{
            _ = MessageBox.Show( "INVALID SELECTION: The minimum number of minutes to digitize is 1.", "Value Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
		}
		if ( this.durationNumeric.Value >= 1440)
		{
            _ = MessageBox.Show( "INVALID SELECTION: The maximum number of minutes to digitize is 1440 (1 day).", "Value Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
			if( this.durationNumeric.Value > 1440)
			{
                this.durationNumeric.Value = 1440;
			}
		}
	}

	private void DigitizeVoltageOption_CheckedChanged(object sender, EventArgs e)
	{
		string[] rangeItems = { "0.1", "1", "10", "100", "1000" };
        this.rangeComboBox.Items.Clear();
        this.rangeComboBox.Items.AddRange(rangeItems);
        this.rangeComboBox.SelectedIndex = 2;
        this.unitsLabel.Text = "V";
	}

	private void DigitizeCurrentOption_CheckedChanged(object sender, EventArgs e)
	{
		string[] rangeItems = { "10e-6", "100e-6", "1e-3", "10e-3", "100e-3", "1"};
        this.rangeComboBox.Items.Clear();
        this.rangeComboBox.Items.AddRange(rangeItems);
        this.rangeComboBox.SelectedIndex = 4;
        this.unitsLabel.Text = "A";
	}

	private void Form1_Load(object sender, EventArgs e)
	{
        this.digitizeVoltageOption.Checked = true;
        this.runningStateLabel.Text = "";
	}

	private void SelectFolderButton_Click(object sender, EventArgs e)
	{
        _ = this.folderBrowserDialog1.ShowDialog();
        this.folderNameTextBox.Text = this.folderBrowserDialog1.SelectedPath;
	}

	private void StartSingleMultimeter()
	{
		// was 30 per minutes. this is per second./ 
		int bufferSize = Convert.ToInt32( this.sampleRateNumeric.Value) / 2 ; 
		Stopwatch stopWatch = new ();
		// int bytesRcvCnt = 8192;      // Size the receive buffer so that data is not lost/clipped
		int chunkSize = 249;         // presently the value for max allowable packet size - do we bother making this scalable?
		int savedReadingsCount = 0;


        this.identityLabel.Text = "";
        this.runtimeLabel.Text = "";
        this.readingsRateLabel.Text = "";

        this.runningStateLabel.Text = "Running...";

        this.startButton.Enabled = false;
        this.startButton.Text = "-----";
		Application.DoEvents();

        int unitFunc1 = this.unitsLabel.Text.Contains( 'V' ) ? 0 : 1;

        // Instantiate the DMM7510 objects...
        DMM7510 unit1 = new ( this.ipAddressTextBox.Text, Convert.ToInt32( this.sampleRateNumeric.Value), unitFunc1,
                                     Convert.ToSingle( this.rangeComboBox.Text), bufferSize);

		// Establish network connection to the instruments...
		string rcvBuffer = "";
        unit1.Connect( true, ref rcvBuffer );
        this.identityLabel.Text = rcvBuffer;

		unit1.Setup_Buffers();
		unit1.Setup_DMM();
		unit1.Setup_Digitizing(Convert.ToInt32( this.durationNumeric.Value));
		unit1.Trigger_DMM();
		stopWatch.Start();          // Start the timers here and stop them inside the thread(s)

		Thread t1 = new (() =>
		{
			unit1.ExtractBufferData( this.folderNameTextBox.Text, "UNIT1", "defbuffer1", bufferSize, 
                                     chunkSize, ref stopWatch, Convert.ToInt32( this.durationNumeric.Value), ref savedReadingsCount);
		});

		t1.Start();
		Application.DoEvents();
		t1.Join();

		System.Media.SystemSounds.Beep.Play();

        this.runningStateLabel.Text = "Stopped...";
        this.startButton.Enabled = true;
        this.startButton.Text = "START";

		// Get the elapsed time as a TimeSpan value.
		TimeSpan ts1 = stopWatch.Elapsed;

		// Format and display the TimeSpan value.
		string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
			ts1.Hours, ts1.Minutes, ts1.Seconds,
			ts1.Milliseconds / 10);
        this.runtimeLabel.Text = elapsedTime;

        this.readingsRateLabel.Text = $"{(savedReadingsCount / ts1.TotalSeconds):0.00} Readings/Sec";

		// Disconnect from the instruments...
		unit1.Disconnect();
	}

	private void StartButton_Click(object sender, EventArgs e)
	{
		this.StartSingleMultimeter();
	}
}
