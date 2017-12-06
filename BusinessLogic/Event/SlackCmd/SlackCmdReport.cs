using DataAccess;
using FileHelper;
using ImageMagick;
using ModelLibrary;
using ModelLibrary.Generic;
using Newtonsoft.Json;
using PDFHelper.Core;
using SlackAPIHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BusinessLogic.Event
{
    /// <summary>
    /// Slack cmd - 異常設備報表
    /// </summary>
    public class SlackCmdReport : ISlackCmd
    {
        /// <summary>
        /// Slack cmd 處理動作
        /// </summary>
        /// <param name="id">使用者 Slack ID</param>
        /// <param name="channel">頻道ID</param>
        /// <param name="text">輸入參數字串</param>
        /// <returns></returns>
        public CmdResponse Process(string id, string channel, string text)
        {
            var dao = GenericDataAccessFactory.CreateInstance<DeviceReport>();
            var option = new QueryOption { Plan = new QueryPlan { Join = "Report" } };

            var data = dao.GetList(option, new DeviceReport { CHANNEL_ID = channel });

            var response = new CmdResponse();

            if (data.Count() > 0)
            {
                //app path
                var path = AppDomain.CurrentDomain.BaseDirectory;
                //報表匯出
                string report = PDFContext.ExportList(string.Format("{0}/Profile/PDF/DeviceReport.xml", path), data);
                //檔案搬移
                FileManager.MoveToFormal(report, "DeviceReport");
                //檔案正式路徑
                string reportPath = string.Format("{0}/{1}", FileManager.GetUrl("DeviceReport"), report);

                ////PDF to image
                //string image = ConvertToImage(report);
                ////檔案搬移
                //FileManager.MoveToFormal(image, "DeviceReport");
                ////檔案正式路徑
                //string imagePath = string.Format("{0}/{1}", FileManager.GetUrl("DeviceReport"), image);

                //Cmd response內容
                //var attachment = new Attachment { COLOR_TYPE = "#764FA5", IMAGE_URL = imagePath };
                response.RESPONSE_TYPE = "ephemeral";
                response.TEXT_CONTENT = string.Format("異常設備即時報表: {0}", reportPath);
            }
            else
            {
                //Cmd response內容
                response.RESPONSE_TYPE = "ephemeral";
                response.TEXT_CONTENT = "目前無異常設備";
            }

            return response;
        }

        /// <summary>
        /// PDF 轉為圖片
        /// </summary>
        /// <param name="file">PDF 檔案名稱</param>
        /// <returns></returns>
        private string ConvertToImage(string file)
        {
            using (MagickImageCollection image = new MagickImageCollection())
            {
                //app path
                var path = AppDomain.CurrentDomain.BaseDirectory;

                MagickNET.SetGhostscriptDirectory(string.Format("{0}/GhostScript/", path));
                //PDF路徑
                string reportPath = string.Format("{0}/{1}", FileManager.GetTempPath(), file);
                //圖片名稱
                string imageName = string.Format("{0}.png", Path.GetFileNameWithoutExtension(file));

                //PDF轉換設置
                var settings = new MagickReadSettings
                {
                    //Settings the density to 300 dpi
                    Density = new Density(300, 300)
                };

                //檔案讀取
                image.Read(reportPath, settings);

                //圖片建立
                using (var vertical = image.AppendVertically())
                {
                    vertical.Write(string.Format("{0}/{1}", FileManager.GetTempPath(), imageName));
                }

                return imageName;
            }
        }
    }
}