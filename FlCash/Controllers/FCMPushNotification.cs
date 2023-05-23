using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace FlCash.Controllers
{
    public class FCMPushNotification
    {
        public FCMPushNotification()
        {
            // TODO: Add constructor logic here
        }

        public bool Successful
        {
            get;
            set;
        }

        public string Response
        {
            get;
            set;
        }
        public Exception Error
        {
            get;
            set;
        }



        public FCMPushNotification SendNotification(string _title, string _message, string _topic)
        {
            FCMPushNotification result = new FCMPushNotification();
            try
            {
                result.Successful = true;
                result.Error = null;
                // var value = message;
                var requestUri = "https://fcm.googleapis.com/fcm/send";

                WebRequest webRequest = WebRequest.Create(requestUri);
                webRequest.Method = "POST";
                webRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAAG4LhNnc:APA91bF2JFqUAufVvqm35GFotc6Q4KSX9DrsV7URdnNN16Kb2-JhpeMyMySkzBtWjdeornWJu9nd-_F04A51tyOLiKmntE7_EwugLe5R9RbJyDUy4UzAehxYGenys8pq2PtkD5nROEvn"));
                webRequest.Headers.Add(string.Format("Sender: id={0}", "118159914615"));
                webRequest.ContentType = "application/json";

                var data = new
                {
                    // to = YOUR_FCM_DEVICE_ID, // Uncoment this if you want to test for single device
                    to = "/topics/" + _topic, // this is for topic 
                    notification = new
                    {
                        title = _title,
                        body = _message,
                        //icon="myicon"
                    }
                };
            
                var json = JsonConvert.SerializeObject(data);

                Byte[] byteArray = Encoding.UTF8.GetBytes(json);

                webRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = webRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse webResponse = webRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = webResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                result.Response = sResponseFromServer;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.Response = null;
                result.Error = ex;
            }
            return result;
        }
    }
}
