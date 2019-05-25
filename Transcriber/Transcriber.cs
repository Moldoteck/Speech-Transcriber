using HelperStructures;
using System;
using System.Text;
using Google.Cloud.Speech.V1;
using System.IO;

namespace Transcriber
{
    public class Transcriber : ITranscriber
    {
        public string TranscribeAudioFile(string cloudFilePath, int timeout, bool deleteAfter, string[] hints, string language, int frameRate, int numberOfSpeakers = 1)
        {
            string cloudFileName = Path.GetFileName(cloudFilePath);
            string cloudPath = Path.GetDirectoryName(cloudFilePath);

            //client = speech.SpeechClient.from_service_account_json('C:/Users/cristian/Downloads/TextToSpeech-d9a5f0e6b87b.json');

            //var framerate = 44100;//call to get the framerate
            Languages ls = new Languages();
            string langCode = "";
            ls.GetLanguageCode(language, out langCode);

            var speech = SpeechClient.Create();
            var longOperation = speech.LongRunningRecognize(new RecognitionConfig()
            {
                Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                SampleRateHertz = frameRate,
                LanguageCode = langCode,
                EnableAutomaticPunctuation = true,
                SpeechContexts={ new SpeechContext() { Phrases = { hints } } }
            }, RecognitionAudio.FromStorageUri("gs://"+cloudFilePath));
            longOperation = longOperation.PollUntilCompleted();
            var response = longOperation.Result;

            StringBuilder tw = new StringBuilder();

            foreach (var result in response.Results)
            {
                foreach (var alternative in result.Alternatives)
                {
                    Console.WriteLine($"Transcript: { alternative.Transcript}");
                    tw.AppendLine(alternative.Transcript);
                }
            }

            return tw.ToString();
        }

    }
}
