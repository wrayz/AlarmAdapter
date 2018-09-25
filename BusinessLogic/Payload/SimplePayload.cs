using ModelLibrary;
using ModelLibrary.Enumerate;
using System.Collections.Generic;

namespace BusinessLogic
{
    /// <summary>
    /// 簡易設備通知物件
    /// </summary>
    public class SimplePayload : Payload
    {
        private SimpleLog _simpleLog;

        public SimplePayload(SimpleLog simpleLog)
        {
            _simpleLog = simpleLog;
            //資料設置
            SetData(EventType.Error);
        }

        /// <summary>
        /// 資料設置
        /// <paramref name="type"/>
        /// </summary>
        public override void SetData(EventType type)
        {
            LOG_SN = _simpleLog.LOG_SN;
            LOG_TYPE = "S";
            DEVICE_SN = _simpleLog.DEVICE_SN;
            BUTTON_STATUS = "N";
            COLOR = "danger";
            TITLE = "設備異常資訊";
            GROUP_LIST = _simpleLog.GROUP_LIST;

            FIELD_LIST = new List<Field>
            {
                new Field("設備名稱", _simpleLog.DEVICE_INFO.DEVICE_NAME, true),
                new Field("設備位址", _simpleLog.DEVICE_INFO.DEVICE_ID, true),
                new Field("異常資訊", _simpleLog.ERROR_INFO, false),
                new Field("異常時間", _simpleLog.ERROR_TIME.Value.ToString(@"MM\/dd\/yyyy HH:mm"), true)
            };
        }
    }
}