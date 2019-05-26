/**************************************************************************
 *                                                                        *
 *  File:        Transcriber.cs                                           *
 *  Copyright:   (c) 2019, Cristian Pădureac                              *
 *  Description: Implementation of ITranscriber interface                 *
 *               It performs transcribing of audio file by using          *
 *               Google Cloud API                                         *
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
using System.Text;
using Google.Cloud.Speech.V1;
using System.IO;
using Google.Protobuf.Collections;
using Grpc.Core;
using Google.Apis.Auth.OAuth2;
using Grpc.Auth;

namespace Speech_Transcriber
{
    public class Transcriber : ITranscriber
    {
        #region Private members
        Channel _channel;
        Languages _languages = null;
        SpeechClient _speechClient = null;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="jsonPath">Path to json file that contains information for OAuth module</param>
        public Transcriber(string jsonPath)
        {
            _languages = new Languages();

            var credential = GoogleCredential.FromFile(jsonPath).CreateScoped(SpeechClient.DefaultScopes);
            
            _channel = new Channel(SpeechClient.DefaultEndpoint.ToString(), credential.ToChannelCredentials());
            _speechClient = SpeechClient.Create(_channel);
        }
        #endregion

        #region Public class methods
        /// <summary>
        /// Recognizes the text from audio file
        /// </summary>
        /// <param name="cloudFilePath">Path to audio file that should be recognized</param>
        /// <param name="timeout">Timeout for waiting the response</param>
        /// <param name="deleteAfter">Bool that indicate if file should be deleted from cloud after recognizing</param>
        /// <param name="hints">Array of strings that contain special words that are not in language's dictionary</param>
        /// <param name="language">Language of spoken words in audio file</param>
        /// <param name="frameRate">Frame rate of audio file</param>
        /// <param name="numberOfSpeakers">Number of speakers from recorded audio file</param>
        /// <returns>
        /// Recognized text from audio file
        /// </returns>
        public string TranscribeAudioFile(string cloudFilePath, int timeout, bool deleteAfter, string[] hints, string language, int frameRate, int numberOfSpeakers = 1)
        {
            string languageCode = "";
            ErrorCode error = _languages.GetLanguageCode(language, out languageCode);

            if (error == ErrorCode.SUCCESS)
            {
                try
                {
                    var longOperation = _speechClient.LongRunningRecognize(new RecognitionConfig()
                    {
                        Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                        SampleRateHertz = frameRate,
                        LanguageCode = languageCode,
                        EnableAutomaticPunctuation = true,
                        SpeechContexts = { new SpeechContext() { Phrases = { hints } } }
                    }, RecognitionAudio.FromStorageUri("gs://" + cloudFilePath));
                    longOperation = longOperation.PollUntilCompleted();
                    var results = longOperation.Result.Results;

                    _channel.ShutdownAsync().Wait();

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
        /// <summary>
        /// Transforms array of results in readable recognized text
        /// </summary>
        /// <param name="results">Array of possible recognition results</param>
        /// <returns>
        /// Recognized text from results array
        /// </returns>
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
