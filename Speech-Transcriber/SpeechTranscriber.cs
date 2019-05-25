using HelperStructures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Speech_Transcriber
{
    public partial class SpeechTranscriber : Form
    {
        string currentCloudFilePath;
        string currentCloudFile;
        public SpeechTranscriber()
        {
            InitializeComponent();
            Languages languageObject = new Languages();
            comboBox1.Items.AddRange(languageObject.GetLanguageList().ToArray());
            comboBox1.SelectedIndex = 0;
            currentCloudFilePath = "interviewstorage1";
        }

        private void uploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentCloudFile = "interviewstorage1/"+ Path.GetFileNameWithoutExtension(label3.Text) + ".wav";
            FileManager.LocalFileManager fmgr = new FileManager.LocalFileManager();
            var localFilePath = label3.Text;// @"C:\Users\cristian\Source\Repos\Speech-Transcriber2\Speech-Transcriber\obj\Debug\recognizer.mp3";
            var wavFilePath = Path.GetDirectoryName(label3.Text) + Path.GetFileNameWithoutExtension(label3.Text) + ".wav";//@"C:\Users\cristian\Source\Repos\Speech-Transcriber2\Speech-Transcriber\obj\Debug\recognizer.wav";
            //richTextBox1.Text = fmgr.CheckExists(localFilePath).ToString();
            AFC.AudioFileConverter conv = new AFC.AudioFileConverter();
            conv.ConvertToFormat(localFilePath, wavFilePath);
            richTextBox1.Text = "Audio converted to wav" + System.Environment.NewLine;

            conv.StereoToMono(wavFilePath, wavFilePath);
            richTextBox1.Text += "Audio converted to mono" + System.Environment.NewLine;

            FileManager.CloudFileManager clfmgr = new FileManager.CloudFileManager("C:/Users/cristian/Downloads/TextToSpeech-d9a5f0e6b87b.json");
            clfmgr.StoreFile(wavFilePath, currentCloudFile);

            richTextBox1.Text += "Audio stored into cloud" + System.Environment.NewLine;
        }

        private void chooseToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "./";
            openFileDialog1.Filter = "Audio files (*.mp3, *.wav)|*.mp3;*.wav";
            openFileDialog1.FilterIndex = 0;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                label3.Text = openFileDialog1.FileName;
            }
        }

        private void recognizeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Transcriber.Transcriber trs = new Transcriber.Transcriber();
            
            AFC.AudioFileConverter conv = new AFC.AudioFileConverter();
            var wavFilePath = Path.GetDirectoryName(label3.Text) + Path.GetFileNameWithoutExtension(label3.Text) + ".wav";//@"C:\Users\cristian\Source\Repos\Speech-Transcriber2\Speech-Transcriber\obj\Debug\recognizer.wav";


            string textResult = trs.TranscribeAudioFile(currentCloudFile, 999999, false, new string[0], comboBox1.SelectedItem.ToString(), conv.Rate(wavFilePath));
            richTextBox1.Text = textResult;
        }

        private void syncCloudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            FileManager.CloudFileManager clfmgr = new FileManager.CloudFileManager("C:/Users/cristian/Downloads/TextToSpeech-d9a5f0e6b87b.json");
            listBox1.Items.AddRange(clfmgr.ListFilesFromPath(currentCloudFilePath));

        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            var selectedItem = listBox1.SelectedItem.ToString();
            currentCloudFile = "interviewstorage1/" + selectedItem;
        }
    }
}
