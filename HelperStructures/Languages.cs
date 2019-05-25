/**************************************************************************
 *                                                                        *
 *  File:        Languages.cs                                             *
 *  Copyright:   (c) 2019, Cristian Pădureac                              *
 *  Description: Languages object is responsible with providing           *
 *               easy managing of possible languages for performing       *
 *               audio file recognition.                                  *
 *               Class provides following functions:                      *
 *               * Returns language abbreviation by providing             *
 *                 full language name                                     *
 *               * Returns list of all supported languages                *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using System.Collections.Generic;

namespace Speech_Transcriber
{
    public class Languages
    {
        #region Private members
        private Dictionary<string, string> _language = null;
        #endregion

        #region Constructor
        public Languages()
        {
            _language = new Dictionary<string, string>();
            _language.Add("Afrikaans (South Africa)", "af-ZA");
            _language.Add("Amharic (Ethiopia)", "am-ET");
            _language.Add("Armenian (Armenia)", "hy-AM");
            _language.Add("Azerbaijani (Azerbaijan)", "az-AZ");
            _language.Add("Indonesian (Indonesia)", "id-ID");
            _language.Add("Malay (Malaysia)", "ms-MY");
            _language.Add("Bengali (Bangladesh)", "bn-BD");
            _language.Add("Bengali (India)", "bn-IN");
            _language.Add("Catalan (Spain)", "ca-ES");
            _language.Add("Czech (Czech Republic)", "cs-CZ");
            _language.Add("Danish (Denmark)", "da-DK");
            _language.Add("German (Germany)", "de-DE");
            _language.Add("English (Australia)", "en-AU");
            _language.Add("English (Canada)", "en-CA");
            _language.Add("English (Ghana)", "en-GH");
            _language.Add("English (United Kingdom)", "en-GB");
            _language.Add("English (India)", "en-IN");
            _language.Add("English (Ireland)", "en-IE");
            _language.Add("English (Kenya)", "en-KE");
            _language.Add("English (New Zealand)", "en-NZ");
            _language.Add("English (Nigeria)", "en-NG");
            _language.Add("English (Philippines)", "en-PH");
            _language.Add("English (Singapore)", "en-SG");
            _language.Add("English (South Africa)", "en-ZA");
            _language.Add("English (Tanzania)", "en-TZ");
            _language.Add("English (United States)", "en-US");
            _language.Add("Spanish (Argentina)", "es-AR");
            _language.Add("Spanish (Bolivia)", "es-BO");
            _language.Add("Spanish (Chile)", "es-CL");
            _language.Add("Spanish (Colombia)", "es-CO");
            _language.Add("Spanish (Costa Rica)", "es-CR");
            _language.Add("Spanish (Ecuador)", "es-EC");
            _language.Add("Spanish (El Salvador)", "es-SV");
            _language.Add("Spanish (Spain)", "es-ES");
            _language.Add("Spanish (United States)", "es-US");
            _language.Add("Spanish (Guatemala)", "es-GT");
            _language.Add("Spanish (Honduras)", "es-HN");
            _language.Add("Spanish (Mexico)", "es-MX");
            _language.Add("Spanish (Nicaragua)", "es-NI");
            _language.Add("Spanish (Panama)", "es-PA");
            _language.Add("Spanish (Paraguay)", "es-PY");
            _language.Add("Spanish (Peru)", "es-PE");
            _language.Add("Spanish (Puerto Rico)", "es-PR");
            _language.Add("Spanish (Dominican Republic)", "es-DO");
            _language.Add("Spanish (Uruguay)", "es-UY");
            _language.Add("Spanish (Venezuela)", "es-VE");
            _language.Add("Basque (Spain)", "eu-ES");
            _language.Add("Filipino (Philippines)", "fil-PH");
            _language.Add("French (Canada)", "fr-CA");
            _language.Add("French (France)", "fr-FR");
            _language.Add("Galician (Spain)", "gl-ES");
            _language.Add("Georgian (Georgia)", "ka-GE");
            _language.Add("Gujarati (India)", "gu-IN");
            _language.Add("Croatian (Croatia)", "hr-HR");
            _language.Add("Zulu (South Africa)", "zu-ZA");
            _language.Add("Icelandic (Iceland)", "is-IS");
            _language.Add("Italian (Italy)", "it-IT");
            _language.Add("Javanese (Indonesia)", "jv-ID");
            _language.Add("Kannada (India)", "kn-IN");
            _language.Add("Khmer (Cambodia)", "km-KH");
            _language.Add("Lao (Laos)", "lo-LA");
            _language.Add("Latvian (Latvia)", "lv-LV");
            _language.Add("Lithuanian (Lithuania)", "lt-LT");
            _language.Add("Hungarian (Hungary)", "hu-HU");
            _language.Add("Malayalam (India)", "ml-IN");
            _language.Add("Marathi (India)", "mr-IN");
            _language.Add("Dutch (Netherlands)", "nl-NL");
            _language.Add("Nepali (Nepal)", "ne-NP");
            _language.Add("Norwegian Bokmål (Norway)", "nb-NO");
            _language.Add("Polish (Poland)", "pl-PL");
            _language.Add("Portuguese (Brazil)", "pt-BR");
            _language.Add("Portuguese (Portugal)", "pt-PT");
            _language.Add("Romanian (Romania)", "ro-RO");
            _language.Add("Sinhala (Sri Lanka)", "si-LK");
            _language.Add("Slovak (Slovakia)", "sk-SK");
            _language.Add("Slovenian (Slovenia)", "sl-SI");
            _language.Add("Sundanese (Indonesia)", "su-ID");
            _language.Add("Swahili (Tanzania)", "sw-TZ");
            _language.Add("Swahili (Kenya)", "sw-KE");
            _language.Add("Finnish (Finland)", "fi-FI");
            _language.Add("Swedish (Sweden)", "sv-SE");
            _language.Add("Tamil (India)", "ta-IN");
            _language.Add("Tamil (Singapore)", "ta-SG");
            _language.Add("Tamil (Sri Lanka)", "ta-LK");
            _language.Add("Tamil (Malaysia)", "ta-MY");
            _language.Add("Telugu (India)", "te-IN");
            _language.Add("Vietnamese (Vietnam)", "vi-VN");
            _language.Add("Turkish (Turkey)", "tr-TR");
            _language.Add("Urdu (Pakistan)", "ur-PK");
            _language.Add("Urdu (India)", "ur-IN");
            _language.Add("Greek (Greece)", "el-GR");
            _language.Add("Bulgarian (Bulgaria)", "bg-BG");
            _language.Add("Russian (Russia)", "ru-RU");
            _language.Add("Serbian (Serbia)", "sr-RS");
            _language.Add("Ukrainian (Ukraine)", "uk-UA");
            _language.Add("Hebrew (Israel)", "he-IL");
            _language.Add("Arabic (Israel)", "ar-IL");
            _language.Add("Arabic (Jordan)", "ar-JO");
            _language.Add("Arabic (United Arab Emirates)", "ar-AE");
            _language.Add("Arabic (Bahrain)", "ar-BH");
            _language.Add("Arabic (Algeria)", "ar-DZ");
            _language.Add("Arabic (Saudi Arabia)", "ar-SA");
            _language.Add("Arabic (Iraq)", "ar-IQ");
            _language.Add("Arabic (Kuwait)", "ar-KW");
            _language.Add("Arabic (Morocco)", "ar-MA");
            _language.Add("Arabic (Tunisia)", "ar-TN");
            _language.Add("Arabic (Oman)", "ar-OM");
            _language.Add("Arabic (State of Palestine)", "ar-PS");
            _language.Add("Arabic (Qatar)", "ar-QA");
            _language.Add("Arabic (Lebanon)", "ar-LB");
            _language.Add("Arabic (Egypt)", "ar-EG");
            _language.Add("Persian (Iran)", "fa-IR");
            _language.Add("Hindi (India)", "hi-IN");
            _language.Add("Thai (Thailand)", "th-TH");
            _language.Add("Korean (South Korea)", "ko-KR");
            _language.Add("Chinese, Mandarin (Traditional,Taiwan)", "zh-TW");
            _language.Add("Chinese, Cantonese (Traditional, Hong Kong)", "yue-Hant-HK");
            _language.Add("Japanese (Japan)", "ja-JP");
            _language.Add("Chinese, Mandarin (Simplified, Hong Kong)", "zh-HK");
            _language.Add("Chinese, Mandarin (Simplified, China)", "zh");
        }
        #endregion

        #region Public class methods
        public ErrorCode GetLanguageCode(string languageName, out string languageCode)
        {
            if (_language.ContainsKey(languageName))
            {
                _language.TryGetValue(languageName, out languageCode);
                return ErrorCode.SUCCESS;
            }
            else
            {
                languageCode = null;
                return ErrorCode.INVALID_ARGUMENT;
            }
        }
        public List<string> GetLanguageList()
        {
            return new List<string>(_language.Keys);
        }
        #endregion
    }
}
