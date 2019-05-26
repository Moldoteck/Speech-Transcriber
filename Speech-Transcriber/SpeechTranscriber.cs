/**************************************************************************
 *                                                                        *
 *  File:        SpeechTranscriber.cs                                     *
 *  Copyright:   (c) 2019, Cristian Pădureac                              *
 *  Description: Class responsable for interaction with                   *
 *               User Interface and performing requested operations       *
 *               by using provided objects.                               *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using System;
using System.IO;
using System.Windows.Forms;

namespace Speech_Transcriber
{
    public partial class SpeechTranscriber : Form
    {
        private string currentCloudFilePath;
        private string currentCloudFile;
        private IFileManagerFactory fmf = new FileManagerFactory();
        private string username = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\')[1];

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
            currentCloudFile = "interviewstorage1/" + Path.GetFileNameWithoutExtension(label3.Text) + ".wav";
            IFileManager fmgr = fmf.GetFileManager();  // localFileManager
            var localFilePath = label3.Text;// @"C:\Users\cristian\Source\Repos\Speech-Transcriber2\Speech-Transcriber\obj\Debug\recognizer.mp3";
            var wavFilePath = Path.GetDirectoryName(label3.Text) + Path.GetFileNameWithoutExtension(label3.Text) + ".wav";//@"C:\Users\cristian\Source\Repos\Speech-Transcriber2\Speech-Transcriber\obj\Debug\recognizer.wav";
            //richTextBox1.Text = fmgr.CheckExists(localFilePath).ToString();
            AudioFileConverter conv = new AudioFileConverter();
            conv.ConvertToFormat(localFilePath, wavFilePath);
            richTextBox1.Text = "Audio converted to wav" + System.Environment.NewLine;

            conv.StereoToMono(wavFilePath, wavFilePath);
            richTextBox1.Text += "Audio converted to mono" + System.Environment.NewLine;

            IFileManager clfmgr = fmf.GetFileManager("C:/Users/" + username + "/Downloads/TextToSpeech-d9a5f0e6b87b.json");
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
            Transcriber trs = new Transcriber("C:/Users/" + username + "/Downloads/TextToSpeech-d9a5f0e6b87b.json");

            AudioFileConverter conv = new AudioFileConverter();
            var wavFilePath = Path.GetDirectoryName(label3.Text) + Path.GetFileNameWithoutExtension(label3.Text) + ".wav";//@"C:\Users\cristian\Source\Repos\Speech-Transcriber2\Speech-Transcriber\obj\Debug\recognizer.wav";
            string textResult = trs.TranscribeAudioFile(currentCloudFile, 999999, false, new string[0], comboBox1.SelectedItem.ToString(), conv.Rate(wavFilePath));
            richTextBox1.Text = textResult;
        }

        private void syncCloudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            IFileManager clfmgr = fmf.GetFileManager("C:/Users/" + username + "/Downloads/TextToSpeech-d9a5f0e6b87b.json");
            listBox1.Items.AddRange(clfmgr.ListFilesFromPath(currentCloudFilePath));

        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            var selectedItem = listBox1.SelectedItem.ToString();
            currentCloudFile = "interviewstorage1/" + selectedItem;
        }

        private void SpeechTranscriber_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Pentru a recunoaște cuvintele dintr-un fișier audio aveți nevoie de conexiune la internet" + System.Environment.NewLine +
                "1) Alegeți un fișier pentru recunoaștere" + System.Environment.NewLine +
                "2) Apăsați butonul Upload pentru a încărca fișierul pe cloud" + System.Environment.NewLine +
                "3) Apăsați butonul Recognize pentru a efectua operația de recunoaștere" + System.Environment.NewLine +
                "* Recunoașterea este realizată blocant, astfel interacțiunea cu aplicația va fi imposibilă atât timp cât nu este primit rezultatul");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Program implementat pentru proiectul la disciplina Ingineria programării" + System.Environment.NewLine +
                "Autori: " + System.Environment.NewLine +
                "    Octavian Oprișan" + System.Environment.NewLine +
                "    Cristian Pădureac");
        }
    }
}
