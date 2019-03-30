using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileManager;

namespace Speech_Transcriber
{
    public partial class SpeechTranscriber : Form
    {
        public SpeechTranscriber()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var mgt = new CloudFileManager("C:/Users/cristian/Downloads/TextToSpeech-d9a5f0e6b87b.json");
            richTextBox1.Text =  mgt.CheckExists("recofile", richTextBox1.Text).ToString();
            //mgt.CheckExists();
        }
    }
}
