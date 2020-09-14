using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventPublisher
{
    class Program
    {
        private const string topicEndPoint = "https://xyx.eastus-1.eventgrid.azure.net/api/events";
        private const string topicKey = "yourtopickey=";
        public static async Task Main(string[] args)
        {
            TopicCredentials credentials = new TopicCredentials(topicKey);

            EventGridClient client = new EventGridClient(credentials);

            List<EventGridEvent> events = new List<EventGridEvent>();

            var firstperson = new
            {
                FullName = "Amit Sharma",
                Address = "123 Pine Acen, San Francisco, CA,USA"
            };

            EventGridEvent firstEvent = new EventGridEvent
            {
                Id = Guid.NewGuid().ToString(),
                EventType = "Employees.Registration.New",
                EventTime = DateTime.Now,
                Subject = $"New Employee: {firstperson.FullName}",
                Data = firstperson.ToString(),
                DataVersion = "1.0.0"
            };
            events.Add(firstEvent);

            var secondPerson = new
            {
                FullName = "Alex Devix",
                Address = "95 Cottege Lane, lbvd, CA 95107"
            };

            EventGridEvent secondEvent = new EventGridEvent
            {
                Id = Guid.NewGuid().ToString(),
                EventType = "Employees.Registration.New",
                EventTime = DateTime.Now,
                Subject = $"New Employee: {secondPerson.FullName}",
                Data = secondPerson.ToString(),
                DataVersion = "1.0.0"
            };
            events.Add(secondEvent);

            string topicHostName = new Uri(topicEndPoint).Host;

            await client.PublishEventsAsync(topicHostName, events);

            Console.WriteLine("Events published to web  ");

        }


    }
}
