//  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
//  PURPOSE.
//
//  This material may not be duplicated in whole or in part, except for 
//  personal use, without the express written consent of the author. 
//
//  Email:  ianier@hotmail.com
//
//  Copyright (C) 1999-2003 Ianier Munoz. All Rights Reserved.

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using ZedGraph;
using Toub.Sound.Midi;
using System.Data;

namespace cswavrec
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
    {
        private IContainer components;
		private System.Windows.Forms.Button StopButton;
		private System.Windows.Forms.Button StartButton;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Panel panel1;
        private Panel panel2;
        private System.Windows.Forms.Label lblFreq;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.Label lblCents;
        private GroupBox groupBox3;
        private Panel pnlScope;
        private TrackBar trackBar1;
        private System.Windows.Forms.Label label1;
        private PianoKeyDemo.PKControl pkControl1;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private Button btnRunTest;
        private Timer timer1;
        private System.Windows.Forms.Label lblmNote;
        private Button button1;
        private Freq m_freqProcessor;
        private DataSet m_DataSet = new DataSet();

        // Control variables
        private int step = 0;
        private byte midinote = 24;
        private double[] freq_array = new double[128];
        private double[] freq_array2 = new double[128];
        private bool captured_freq = false;
        private bool start_capture = false;
        private bool bypass_capture = false;
        private CheckBox checkBox1;
        private System.Windows.Forms.Label lblNumber;
        private bool bothosc = false;
        private int LOMIDI = 24;
        private int HIMIDI = 80;
		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            m_freqProcessor = new Freq();
            MidiPlayer.OpenMidi();

            zedGraphControl1.GraphPane.CurveList.Clear();
            GraphPane myPane = zedGraphControl1.GraphPane;

            // Set the title and axis labels
            myPane.Title.Text = "MIDI Note number versus Frequency";
            myPane.XAxis.Title.Text = "Midi Note Number";
            myPane.YAxis.Title.Text = "Frequency";
            myPane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45.0f);
            myPane.Fill = new Fill(Color.White, Color.SlateGray, 45.0f);
            
            //double dFreq;
            //PointPairList list1 = new PointPairList();

            //for (int n = LOMIDI; n <= HIMIDI; n++)
            //{
            //    dFreq = 440.0 * Math.Pow(2.0, ((n - 69.0) / 12.0));

            //    if (this.checkBox1.Checked)
            //    {
            //        list1.Add(n, Math.Log(dFreq));
            //    }
            //    else
            //    {
            //        list1.Add(n, dFreq);
            //    }

            //}
            pkControl1.NoteEvent += new PianoKeyDemo.PKControl.NoteEventHandler(pkControl1_NoteEvent);

		}

        void pkControl1_NoteEvent(object sender, PianoKeyDemo.KeyArgs ca)
        {
            if (ca.NoteOn)
            {
                NoteOn _nOn= new NoteOn(0, 0, (byte)ca.Note, 127);
                MidiPlayer.Play(_nOn);
            }
            else
            {
                NoteOff _nOff = new NoteOff(0, 0, (byte)ca.Note, 0);
                MidiPlayer.Play(_nOff);
            }           
        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.StartButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblFreq = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblCents = new System.Windows.Forms.Label();
            this.lblNote = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pnlScope = new System.Windows.Forms.Panel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.pkControl1 = new PianoKeyDemo.PKControl();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.btnRunTest = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblmNote = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.lblNumber = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(292, 119);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(72, 24);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Start";
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(372, 119);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(72, 24);
            this.StopButton.TabIndex = 1;
            this.StopButton.Text = "Stop";
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(215, 100);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Frequency";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.lblFreq);
            this.panel1.Location = new System.Drawing.Point(7, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(202, 74);
            this.panel1.TabIndex = 0;
            // 
            // lblFreq
            // 
            this.lblFreq.AutoSize = true;
            this.lblFreq.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFreq.ForeColor = System.Drawing.Color.Lime;
            this.lblFreq.Location = new System.Drawing.Point(17, 15);
            this.lblFreq.Name = "lblFreq";
            this.lblFreq.Size = new System.Drawing.Size(163, 39);
            this.lblFreq.TabIndex = 0;
            this.lblFreq.Text = "440.0 Hz";
            this.lblFreq.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Location = new System.Drawing.Point(234, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(216, 100);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Note Name";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.lblNumber);
            this.panel2.Controls.Add(this.lblCents);
            this.panel2.Controls.Add(this.lblNote);
            this.panel2.Location = new System.Drawing.Point(6, 19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(204, 74);
            this.panel2.TabIndex = 1;
            // 
            // lblCents
            // 
            this.lblCents.AutoSize = true;
            this.lblCents.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCents.ForeColor = System.Drawing.Color.White;
            this.lblCents.Location = new System.Drawing.Point(132, 35);
            this.lblCents.Name = "lblCents";
            this.lblCents.Size = new System.Drawing.Size(49, 24);
            this.lblCents.TabIndex = 2;
            this.lblCents.Text = "+0%";
            this.lblCents.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 33F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNote.ForeColor = System.Drawing.Color.Cyan;
            this.lblNote.Location = new System.Drawing.Point(55, 7);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(53, 52);
            this.lblNote.TabIndex = 1;
            this.lblNote.Text = "A";
            this.lblNote.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pnlScope);
            this.groupBox3.Location = new System.Drawing.Point(456, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(214, 182);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Oscilloscope";
            // 
            // pnlScope
            // 
            this.pnlScope.BackColor = System.Drawing.Color.White;
            this.pnlScope.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlScope.Location = new System.Drawing.Point(7, 20);
            this.pnlScope.Name = "pnlScope";
            this.pnlScope.Size = new System.Drawing.Size(200, 150);
            this.pnlScope.TabIndex = 0;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(13, 119);
            this.trackBar1.Maximum = 50;
            this.trackBar1.Minimum = -50;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(215, 45);
            this.trackBar1.TabIndex = 4;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(115, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "^";
            // 
            // pkControl1
            // 
            this.pkControl1.Location = new System.Drawing.Point(2, 149);
            this.pkControl1.Name = "pkControl1";
            this.pkControl1.Size = new System.Drawing.Size(429, 94);
            this.pkControl1.TabIndex = 6;
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(13, 246);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0;
            this.zedGraphControl1.ScrollMaxX = 0;
            this.zedGraphControl1.ScrollMaxY = 0;
            this.zedGraphControl1.ScrollMaxY2 = 0;
            this.zedGraphControl1.ScrollMinX = 0;
            this.zedGraphControl1.ScrollMinY = 0;
            this.zedGraphControl1.ScrollMinY2 = 0;
            this.zedGraphControl1.Size = new System.Drawing.Size(663, 397);
            this.zedGraphControl1.TabIndex = 7;
            // 
            // btnRunTest
            // 
            this.btnRunTest.Location = new System.Drawing.Point(456, 213);
            this.btnRunTest.Name = "btnRunTest";
            this.btnRunTest.Size = new System.Drawing.Size(114, 24);
            this.btnRunTest.TabIndex = 8;
            this.btnRunTest.Text = "Run Test";
            this.btnRunTest.Click += new System.EventHandler(this.btnRunTest_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 250;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblmNote
            // 
            this.lblmNote.AutoSize = true;
            this.lblmNote.Location = new System.Drawing.Point(592, 224);
            this.lblmNote.Name = "lblmNote";
            this.lblmNote.Size = new System.Drawing.Size(13, 13);
            this.lblmNote.TabIndex = 9;
            this.lblmNote.Text = "0";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(292, 149);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(152, 24);
            this.button1.TabIndex = 10;
            this.button1.Text = "Send Low Note Off";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(611, 223);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(65, 17);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "Plot Log";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = true;
            this.lblNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumber.ForeColor = System.Drawing.Color.Silver;
            this.lblNumber.Location = new System.Drawing.Point(144, 5);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(21, 24);
            this.lblNumber.TabIndex = 3;
            this.lblNumber.Text = "0";
            this.lblNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(688, 655);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblmNote);
            this.Controls.Add(this.btnRunTest);
            this.Controls.Add(this.zedGraphControl1);
            this.Controls.Add(this.pkControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.StartButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "9700s Tuner";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

        private WaveLib.WaveInRecorder m_Recorder;
        private byte[] m_RecBuffer;
        //private WaveLib.WaveOutPlayer m_Player;
		//private WaveLib.FifoStream m_Fifo = new WaveLib.FifoStream();

        //private byte[] m_PlayBuffer;

		/// <summary>
		/// Filler
		/// </summary>
		/// <param name="data"></param>
		/// <param name="size"></param>
        //private void Filler(IntPtr data, int size)
        //{
        //    if (m_PlayBuffer == null || m_PlayBuffer.Length < size)
        //        m_PlayBuffer = new byte[size];
        //    if (m_Fifo.Length >= size)
        //        m_Fifo.Read(m_PlayBuffer, 0, size);
        //    else
        //        for (int i = 0; i < m_PlayBuffer.Length; i++)
        //            m_PlayBuffer[i] = 0;
        //    System.Runtime.InteropServices.Marshal.Copy(m_PlayBuffer, 0, data, size);
        //}
        
        int buffcount = 0;

        /// <summary>
        /// DataArrived
        /// </summary>
        /// <param name="data"></param>
        /// <param name="size"></param>
		private void DataArrived(IntPtr data, int size)
		{
			if (m_RecBuffer == null || m_RecBuffer.Length < size)
				m_RecBuffer = new byte[size];
			
            System.Runtime.InteropServices.Marshal.Copy(data, m_RecBuffer, 0, size);
            
            // Process the wavebuffer here
            if (start_capture || bypass_capture)
            {
                short[] sBuff = new short[size / 2];
                int icount = 0;

                for (int n = 0; n < sBuff.Length; n++)
                {
                    sBuff[n] = (short)(m_RecBuffer[icount] | (m_RecBuffer[icount + 1] << 8));
                    icount += 2;
                }

                m_freqProcessor.ProcessBuffer(sBuff, size / 2);

                if (!bypass_capture)
                {
                    if (bothosc)
                        freq_array2[midinote] = m_freqProcessor.Frequency;
                    else
                        freq_array[midinote] = m_freqProcessor.Frequency;
                }

                // screen updates
                UpdateScreen();
                
                //if (!m_freqProcessor.BadSignal)
                //    buffcount = 0;

                if (buffcount == 3)
                {
                    start_capture = false;
                    captured_freq = true;
                }
                else
                {
                    buffcount++;
                }
            }
			//m_Fifo.Write(m_RecBuffer, 0, m_RecBuffer.Length);
		}
        
        delegate void Invoker();

        private void UpdateScreen()
        {
            // Handle invoke stuff to set the NoteName, Frequency and CentOffset
            if (this.InvokeRequired)
            {
                // Execute the same method, but this time on the GUI thread
                this.BeginInvoke(new Invoker(UpdateScreen));

                // we return immediately
                return;
            }

            // From here on it is safe to access methods and properties on the GUI
            if (!m_freqProcessor.BadSignal)
                lblFreq.ForeColor = Color.Red;
            else
                lblFreq.ForeColor = Color.Green;

            lblFreq.Text = m_freqProcessor.Frequency.ToString("0.0") + "Hz";
            lblNote.Text = m_freqProcessor.NoteName;
            lblCents.Text = m_freqProcessor.CentOffset.ToString() + "%";
            trackBar1.Value = m_freqProcessor.CentOffset;
            if (m_freqProcessor.Frequency == 0.0)
                lblNumber.Text = "-";
            else
                lblNumber.Text = (69 + 12 * Math.Log(m_freqProcessor.Frequency / 440) / Math.Log(2)).ToString("0");

            // Scope Drawing Stuff ////////////////////////////////////////////

            Graphics graphics = pnlScope.CreateGraphics();

            graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, 200, 150));

            graphics.DrawLine(Pens.Black, 0, 72-32, 800, 72-32);
            graphics.DrawLine(Pens.Black, 0, 72 + 32, 800, 72 + 32);

            int tempVol = m_freqProcessor.volSRC;

            if (tempVol == 0)
                tempVol = 1;   // added 20/07/2005
            
            Point previous;
            Point current;
            previous = new Point(0, 72 - (32 * m_freqProcessor.buffer2[0]) / tempVol);
 
            for (int i = 1; i < (1600)/*bufferSize*/; i+=8)
            {
                current = new Point(i / 8, 72 - (32 * m_freqProcessor.buffer2[i]) / tempVol);
                graphics.DrawLine(Pens.Black, previous, current);
                previous = current;
            }

            /*
             for (int i=0; i<bufferSize; i+=4)
             {
              if (i==0)
               Image1->Canvas->MoveTo(i/4, 72-(32*bufs[b][i])/volume);
              else
               Image1->Canvas->LineTo(i/4, 72-(32*bufs[b][i])/volume);
             }
            */

            graphics.Dispose();


        }

		private void Stop()
		{
            //if (m_Player != null)
            //    try
            //    {
            //        m_Player.Dispose();
            //    }
            //    finally
            //    {
            //        m_Player = null;
            //    }
			if (m_Recorder != null)
				try
				{
					m_Recorder.Dispose();
				}
				finally
				{
					m_Recorder = null;
				}
			//m_Fifo.Flush(); // clear all pending data
		}

		private void Start()
		{
			Stop();
			try
			{
				WaveLib.WaveFormat fmt = new WaveLib.WaveFormat(44100, 16, 2);
				//m_Player = new WaveLib.WaveOutPlayer(-1, fmt, 16384, 3, new WaveLib.BufferFillEventHandler(Filler));
                m_Recorder = new WaveLib.WaveInRecorder(-1, fmt, 16384, 3, new WaveLib.BufferDoneEventHandler(DataArrived));
			}
			catch
			{
				Stop();
				throw;
			}
		}

		private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Stop();
            MidiPlayer.CloseMidi();
		}

		private void StartButton_Click(object sender, System.EventArgs e)
		{
            bypass_capture = true;
            Start();
		}

		private void StopButton_Click(object sender, System.EventArgs e)
		{
            bypass_capture = false;
            Stop();
		}

        private void btnRunTest_Click(object sender, EventArgs e)
        {
            // StartTimer
            bothosc = true;
            bypass_capture = false;
            timer1.Start();

        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (step)
            {
                case 0:
                    
                    // Start
                    Start();

                    NoteOn _nOn = new NoteOn(0, 0, midinote, 127);
                    MidiPlayer.Play(_nOn);
                    
                    step = 3; // Move On
                    

                    break;
                case 1:
                    if (captured_freq)
                    {
                        // Silence the note
                        NoteOff _nOff = new NoteOff(0, 0, midinote, 0);
                        MidiPlayer.Play(_nOff);

                        if (midinote == HIMIDI)
                        {
                            // Done
                            step = 2;
                        }
                        else
                        {
                            midinote++;
                            step = 0;
                        }
                        buffcount = 0;
                    }

                    break;
                case 2:

                    step = 0;
                    captured_freq = false;
                    midinote = (byte)LOMIDI;

                    timer1.Stop();
                    if (bothosc)
                    {
                        Stop();
                        bothosc = false;
                        PlotGraph();
                    }
                    else
                    {
                        NoteOn _nOn_local = new NoteOn(0, 0, 0, 127);
                        MidiPlayer.Play(_nOn_local);

                        MessageBox.Show("Now connect the second oscillator");
                        
                        NoteOff _nOff_local = new NoteOff(0, 0, 0, 127);
                        MidiPlayer.Play(_nOff_local);

                        bothosc = true;
                        timer1.Start();
                    }
                    
                    break;
                case 3:
                    
                    // This gives the oscillator time to stabilize at the new frequency
                    step = 1;
                    start_capture = true;
                    captured_freq = false;

                    lblmNote.Text = midinote.ToString();
                    break;
            }
        }

        private void PlotGraph()
        {
            zedGraphControl1.GraphPane.CurveList.Clear();
            GraphPane myPane = zedGraphControl1.GraphPane;

            // Make up some data arrays based on the Sine function
            PointPairList list1 = new PointPairList();
            PointPairList list2 = new PointPairList();

            // Generate the ideal curve
            PointPairList list3 = new PointPairList();
            double dFreq;

            for (int n = LOMIDI; n <= HIMIDI; n++)
            {
                //n = 69 + 12*log(freq/440)/log(2)
                dFreq = 440.0 * Math.Pow(2.0, ((n - 69.0) / 12.0));
                
                if (this.checkBox1.Checked)
                {
                    list3.Add(n, Math.Log(dFreq));
                    list1.Add(n, Math.Log(freq_array[n]));
                    list2.Add(n, Math.Log(freq_array2[n]));
                }
                else
                {
                    list3.Add(n, dFreq);
                    list1.Add(n, freq_array[n]);
                    list2.Add(n, freq_array2[n]);
                }
            }
            // Generate a red curve
            LineItem myCurve = myPane.AddCurve("Osc 1",
               list1, Color.Red, SymbolType.None);
            myCurve.Line.Width = 2.0F;
            myCurve.Line.IsAntiAlias = true;

            // Generate a blue curve
            LineItem myCurve2 = myPane.AddCurve("Osc 2",
               list2, Color.Blue, SymbolType.None);
            myCurve2.Line.Width = 2.0F;
            myCurve2.Line.IsAntiAlias = true;

            // Generate a green curve
            LineItem myCurve3 = myPane.AddCurve("Ideal",
               list3, Color.Green, SymbolType.None);
            myCurve3.Line.Width = 2.0F;
            myCurve3.Line.IsAntiAlias = true;

            // Calculate the Axis Scale Ranges
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NoteOff _nOff = new NoteOff(0, 0, 0, 0);
            MidiPlayer.Play(_nOff);

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            PlotGraph();
        }

	}
}
