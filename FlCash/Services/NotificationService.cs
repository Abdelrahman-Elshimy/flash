//using CorePush.Google;
//using FlCash.Models;
//using FlCash.Services;
//using Microsoft.Extensions.Options;
//using System;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Threading.Tasks;
//using static FlCash.Models.GoogleNotification;

//namespace FlCash.Services
//{
//    public class NotificationService : INotificationService
//    {
//        private readonly FCMNotificationSetting _fCMNotificationSetting;
//        public NotificationService(IOptions<FCMNotificationSetting> settings)
//        {
//            _fCMNotificationSetting = settings.Value;
//        }
//        public async Task<NotiResponseModel> SendNotification(Notification notificationM)
//        {
//            NotiResponseModel notiResponseModel = new NotiResponseModel();

//            try
//            {
//                FcmSettings settings = new FcmSettings()
//                {
//                    SenderId = _fCMNotificationSetting.SenderId,
//                    ServerKey = _fCMNotificationSetting.ServerKey
//                };
//                HttpClient httpClient = new HttpClient();

//             string authorizationKey = string.Format("keyy={0}", settings.ServerKey);
//                string deviceToken = notificationM.DeviceId;
//                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
//                httpClient.DefaultRequestHeaders.Accept
//                        .Add(new MediaTypeWithQualityHeaderValue("application/json"));
//                DataPayload dataPayload = new DataPayload();
//                dataPayload.Title = notificationM.Title;
//                dataPayload.Body = notificationM.Body;
//                GoogleNotification notification = new GoogleNotification();
//                notification.Data = dataPayload;
//                notification.Notification = dataPayload;
//                FcmSender fcm = new FcmSender(settings, httpClient);
//                var fcmSendResponse = await fcm.SendAsync(deviceToken, notification);
//                if (fcmSendResponse.IsSuccess())
//                {
//                    notiResponseModel.IsSuccess = true;
//                    notiResponseModel.Message = "Notification sent successfully";
//                    return notiResponseModel;
//                }
//                else
//                {
//                    notiResponseModel.IsSuccess = false;
//                    notiResponseModel.Message = fcmSendResponse.Results[0].Error;
//                    return notiResponseModel;
//                }
//            }
//            catch (Exception ex)
//            {
//                notiResponseModel.IsSuccess = false;
//                notiResponseModel.Message = "Something went wrong";
//                return notiResponseModel;
//            }
//        }
//    }
//}
