using HelperStructures;
using System;
using System.Text;
using Google.Cloud.Speech.V1;
using Google.LongRunning;
using System.IO;
using Google.Protobuf.Collections;

namespace Speech_Transcriber
{
    public class Transcriber : ITranscriber
    {
        #region Private members
        Languages _languages = null;
        #endregion

        #region Constructor
        public Transcriber()
        {
            _languages = new Languages();
        }
        #endregion

        #region Public class methods
        public string TranscribeAudioFile(string cloudFilePath, int timeout, bool deleteAfter, string[] hints, string language, int frameRate, int numberOfSpeakers = 1)
        {
            try
            {
                string cloudFileName = Path.GetFileName(cloudFilePath);
                string cloudPath = Path.GetDirectoryName(cloudFilePath);
            }
            catch (ArgumentException exc)
            {
                Console.WriteLine(exc.Message);
                return "";
            }
            catch (PathTooLongException exc)
            {
                Console.WriteLine(exc.Message);
                return "";
            }

            string languageCode = "";
            ErrorCode error = _languages.GetLanguageCode(language, out languageCode);

            if (error == ErrorCode.SUCCESS)
            {
                try
                {
                    var speechClient = SpeechClient.Create();
                    var longOperation = speechClient.LongRunningRecognize(new RecognitionConfig()
                    {
                        Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                        SampleRateHertz = frameRate,
                        LanguageCode = languageCode,
                        EnableAutomaticPunctuation = true,
                        SpeechContexts = { new SpeechContext() { Phrases = { hints } } }
                    }, RecognitionAudio.FromStorageUri("gs://" + cloudFilePath));
                    longOperation = longOperation.PollUntilCompleted();
                    var results = longOperation.Result.Results;

                    return ParseResults(results);
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                    return "";
                }
            }
            else
            {
                Console.WriteLine("Invalid language");
                return "";
            }
        }
        #endregion

        #region Private class methods
        private string ParseResults(RepeatedField<SpeechRecognitionResult> results)
        {
            if (results != null)
            {
                StringBuilder stringBuilder = new StringBuilder();

                foreach (var result in results)
                {
                    foreach (var alternative in result.Alternatives)
                    {
                        Console.WriteLine($"Transcript: { alternative.Transcript}");
                        stringBuilder.AppendLine(alternative.Transcript);
                    }
                }

                return stringBuilder.ToString();
            }
            else
            {
                return "";
            }
        }
        #endregion
    }
}
