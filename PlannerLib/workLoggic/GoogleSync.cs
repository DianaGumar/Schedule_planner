using System;
using System.Collections.Generic;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using System.IO;

namespace PlannerLib.workLoggic
{
    public static class GoogleSync
    {

        public static IEnumerable<PlannerLib.Model.Task> GetTasksFromGoogleCalendar(string calendarId)
        {
            List<PlannerLib.Model.Task> tasks = new List<PlannerLib.Model.Task>();

            Calendar calendar;
            Events events = GetEventsFromGoogleCalendar(calendarId, out calendar);

            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    tasks.Add(new Model.Task
                    {
                        Name = eventItem.Summary,
                        Description = eventItem.Description,
                        Label = calendar.Id,
                        Deadline = eventItem.Start.DateTime

                    }) ;

                }
            }

            return tasks;

        }

        public static Events GetEventsFromGoogleCalendar(string calendarId, out Calendar calendar)
        {
            
            string jsonFile = "taskplanner-289113-369f31041766.json";
            //string calendarId = @"sdephph71sfdsnhtrpvh85irag@group.calendar.google.com";
            //string calendarId = @"lantan.mp4@gmail.com";

            string[] Scopes = { CalendarService.Scope.Calendar };

            ServiceAccountCredential credential;

            using (var stream =
                new FileStream(jsonFile, FileMode.Open, FileAccess.Read))
            {
                var confg = Google.Apis.Json.NewtonsoftJsonSerializer.Instance.Deserialize<JsonCredentialParameters>(stream);
                credential = new ServiceAccountCredential(
                   new ServiceAccountCredential.Initializer(confg.ClientEmail)
                   {
                       Scopes = Scopes
                   }.FromPrivateKey(confg.PrivateKey));
            }

            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Calendar API Sample",
            });

            calendar = service.Calendars.Get(calendarId).Execute();

            // Define parameters of request.
            EventsResource.ListRequest listRequest = service.Events.List(calendarId);
            listRequest.TimeMin = DateTime.Now;
            listRequest.ShowDeleted = false;
            listRequest.SingleEvents = true;
            listRequest.MaxResults = 10;
            listRequest.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            return listRequest.Execute();

            
        }



    }
}
