using Microsoft.Speech.Synthesis;
using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace APIService.Controllers
{
    /// <summary>
    /// 語音清單API
    /// </summary>
    public class LanguagesController : ApiController
    {
        /// <summary>
        /// 語音清單取得
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetLanguages()
        {
            try
            {
                var voices = await GetInstalledVoice();

                return Ok(voices);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// 已安裝語言取得
        /// </summary>
        /// <returns></returns>
        private Task<IEnumerable<Voice>> GetInstalledVoice()
        {
            return Task.Run<IEnumerable<Voice>>(() =>
            {
                List<Voice> voices = new List<Voice>();

                using (SpeechSynthesizer synth = new SpeechSynthesizer())
                {
                    foreach (var voice in synth.GetInstalledVoices())
                    {
                        VoiceInfo info = voice.VoiceInfo;
                        var voiceInfo = new Voice
                        {
                            Name = info.Culture.EnglishName,
                            Culture = info.Culture.Name
                        };

                        voices.Add(voiceInfo);
                    }
                }

                return voices;
            });
        }
    }
}