﻿using EventAPI.Controllers;
using EventAPI.DomainModel;
using EventAPI.Exceptions;
using EventAPI.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace EventAPI.Service
{
    public class EventService : IEventService
    {
        private readonly IGenericDbRepository<Event> _eventRepository;
        private readonly IMemoryCache _cache;

        public EventService(IGenericDbRepository<Event> eventRepository, IMemoryCache cache)
        {
            _eventRepository = eventRepository;
            _cache = cache;
        }

        public async Task<ResultSet> GetEventsWithPaginationAsync(int page, int pageSize)
        {
            var events = _cache.Get<IEnumerable<Event>>("events");
            if (events == null)
            {
                events = await _eventRepository.GetAllAsync();
                var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                _cache.Set("events", events, cacheOptions);
            }

            var totalCount = events.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var results = events.Skip((page - 1) * pageSize).Take(pageSize);

            var response = new ResultSet
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                Page = page,
                PageSize = pageSize,
                Results = results
            };
            return response;
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            var result = await _eventRepository.GetByPrimaryKeyAsync(id);
            return result;
        }

        [HttpPost]
        public async Task CreateEventAsync(Event newevent)
        {
            await _eventRepository.AddModelAsync(newevent);
            _cache.Remove("events");
        }

        [HttpPut("{id}")]
        public async Task UpdateEventAsync(int id, Event updateevent)
        {
            var existingEvent = await _eventRepository.GetByPrimaryKeyAsync(id);
            if (existingEvent == null)
                throw new NotFoundException<Event>("Event with id " + id + " is not found");
            existingEvent.Title = updateevent.Title;
            existingEvent.Description = updateevent.Description;
            existingEvent.EndDateTime = updateevent.EndDateTime;
            existingEvent.StartDateTime = updateevent.StartDateTime;
            existingEvent.TimeZone = updateevent.TimeZone;
            existingEvent.Location = updateevent.Location;
            await _eventRepository.UpdateModelAsync(existingEvent);

            _cache.Remove("events");

        }

        public async Task DeleteEventAsync(int id)
        {
            var existingEvent = await _eventRepository.GetByPrimaryKeyAsync(id);
            if (existingEvent == null)
                throw new NotFoundException<Event>("Event with id " + id + " is not found");
            await _eventRepository.DeleteModelAsync(existingEvent);
            _cache.Remove("events");
        }

        public async Task AddParticipantToEventAsync(AddParticipantParams @params)
        {
            var existingEvent = await _eventRepository.GetByPrimaryKeyAsync(@params.eventid);
            if (existingEvent == null)
                throw new NotFoundException<Event>("Event with id " + @params.eventid + " is not found");

            var user = GetUser(@params.userid);
            if (user == null)
                throw new NotFoundException<User>("User with id " + @params.userid + " is not found");

            var participant = new Participant { UserId = @params.userid, Event = existingEvent, EventId = @params.eventid };
            existingEvent.Participants.Add(participant);
            await _eventRepository.UpdateModelAsync(existingEvent);
            _cache.Remove("events");
         }

        public async Task AddInvitationToEventAsync(AddParticipantParams @params)
        {
            var existingEvent = await _eventRepository.GetByPrimaryKeyAsync(@params.eventid);
            if (existingEvent == null)
                throw new NotFoundException<Event>("Event with id " + @params.eventid + " is not found");

            var user = GetUser(@params.userid);
            if (user == null)
                throw new NotFoundException<User>("User with id " + @params.userid + " is not found");

            var invite = new Invitation { UserId = @params.userid, Event = existingEvent, EventId = @params.eventid, Accepted = false };
            existingEvent.Invitations.Add(invite);
            await _eventRepository.UpdateModelAsync(existingEvent);
            _cache.Remove("events");
        }

        private async Task<User?> GetUser(int userid)
        {
            var httpclient = new HttpClient();
            var response = await httpclient.GetAsync("https://jsonplaceholder.typicode.com/users/" + userid);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;
            var result = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(result);
            return user;
        }

        
    }
}
