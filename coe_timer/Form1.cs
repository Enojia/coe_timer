using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using coe_timer.Properties;
using System.Speech.Recognition;

namespace coe_timer
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine SREngine = new SpeechRecognitionEngine();
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "16";
        }

        SoundPlayer mySound = new SoundPlayer(Resources.Metal_Gong);

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < Convert.ToInt32(textBox1.Text))
            {
                progressBar1.Value++;
            }
            else
            {
                mySound.Play();
                progressBar1.Value = 1;
            }
        }

        private void Reset(Timer MyTimer)
        {
            MyTimer.Stop();
            MyTimer.Interval = 1000;
            progressBar1.Maximum = Convert.ToInt32(textBox1.Text);
            progressBar1.Value = 0;
            MyTimer.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Reset(timer1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Choices command = new Choices();
            command.Add("timer");
            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.Append(command);
            Grammar grammar = new Grammar(gBuilder);

            SREngine.LoadGrammar(grammar);
            SREngine.SetInputToDefaultAudioDevice();
            SREngine.SpeechRecognized += SREngine_SpeechRecognized;
        }

        private void SREngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if(e.Result.Text == "timer")
                Reset(timer1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SREngine.RecognizeAsync(RecognizeMode.Multiple);
            button4.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SREngine.RecognizeAsyncStop();
        }
    }
}
