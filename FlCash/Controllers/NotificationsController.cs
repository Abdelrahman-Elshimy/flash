using FirebaseAdmin.Messaging;
using FlCash.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace FlCash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private IHostingEnvironment _env;
        public string result;
        public NotificationsController(IHostingEnvironment env)
        {
            this._env = env;
        }

        [Route("SendNewNotify")]
        [HttpPost]
        public ActionResult SendNewNotify()
        {
            FCMPushNotification fCMPush = new FCMPushNotification();
            var x = fCMPush.SendNotification("Title", "Message", "flash");

            return Ok(x);
        }
        [Route("SendNoti")]
        [HttpPost]
        public async Task<string> NotifyAsync(string title, string body)
        {
            try
            {
                // Get the server key from FCM console
                var serverKey = string.Format("key=0{0}","AAAAG4LhNnc:APA91bF2JFqUAufVvqm35GFotc6Q4KSX9DrsV7URdnNN16Kb2-JhpeMyMySkzBtWjdeornWJu9nd-_F04A51tyOLiKmntE7_EwugLe5R9RbJyDUy4UzAehxYGenys8pq2PtkD5nROEvn");

                // Get the sender id from FCM console
                var senderId = string.Format("id={0}", "118159914615");

                var data = new
                {
                    notification = new { title, body }
                };

                // Using Newtonsoft.Json
                var jsonBody = JsonConvert.SerializeObject(data);

                using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send/"))
                {
                    httpRequest.Headers.TryAddWithoutValidation("Authorization", serverKey);
                    httpRequest.Headers.TryAddWithoutValidation("Sender", senderId);
                    httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    using (var httpClient = new HttpClient())
                    {
                        var result = await httpClient.SendAsync(httpRequest);

                        if (result.IsSuccessStatusCode)
                        {
                            return "Sent";
                        }
                        else
                        {
                            // Use result.StatusCode to handle failure
                            // Your custom error handler here
                            return result.ToString();
                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return "Problem 2";
        }
        [Route("sendnotification")]
        [HttpPost]
        public async Task<IActionResult> SendNotificationAsync(Notifications notificationModel)
        {
            //var result = await _notificationService.SendNotification(notificationModel);
            //return Ok(result);
            var path = _env.ContentRootPath;
            path = path + "\\auth.json";
            FirebaseApp app = null;

            try
            {
                app = FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(path)
                }, "FlashCashGame");
            }catch (Exception e)
            {
                app = FirebaseApp.GetInstance("FlashCashGame");
            }

            var fcm = FirebaseMessaging.GetMessaging(app);
            Message message = new Message()
            {
                Notification = new Notification
                {
                    Title = "My push notification title",
                    Body = "Content for this push notification"
                },
                Data = new Dictionary<string, string>()
                 {
                     { "AdditionalData1", "data 1" },
                     { "AdditionalData2", "data 2" },
                     { "AdditionalData3", "data 3" },
                 },

                Topic = "WebsiteUpdates"
            };

            this.result = await fcm.SendAsync(message);

            return Ok(result.ToString());
        }
    }
}
