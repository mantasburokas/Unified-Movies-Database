using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Movies.Helpers;
using Movies.Models.Entities;
using Movies.Services.Interfaces;

namespace Movies.Services
{
    public class MoviesRequestsCache : IMoviesRequestsCache
    {
        private readonly TimeSpan _cachedPeriod = TimeSpan.FromDays(1);

        private readonly IDictionary<MovieRequest, MoviesRequestsStates> _cache;

        public MoviesRequestsCache()
        {
            _cache = new ConcurrentDictionary<MovieRequest, MoviesRequestsStates>();
        }

        public bool AddRequest(string requestName)
        {
            var request = _cache.Keys.FirstOrDefault(r => r.Name == requestName);

            var isAddable = true;

            if (request != null)
            {
                isAddable = IsUpdatable(request);
            }
            
            var added = false;

            if (isAddable)
            {
                _cache.Add(new MovieRequest(requestName), MoviesRequestsStates.Started);

                added = true;
            }

            return added;
        }

        public bool FinishRequest(string requestName)
        {
            var request = _cache.Keys.FirstOrDefault(r => r.Name == requestName);

            var finishedRequest = false;

            if (request != null)
            {
                _cache[request] = MoviesRequestsStates.Finished;

                finishedRequest = true;
            }

            return finishedRequest;
        }

        public bool IsRequestFinished(string requestName)
        {
            var request = _cache.Keys.FirstOrDefault(r => r.Name == requestName);

            var finished = false;

            if (request != null)
            {
                if (_cache[request] == MoviesRequestsStates.Finished)
                {
                    finished = true;
                }
            }

            return finished;
        }

        protected bool IsUpdatable(MovieRequest request)
        {
            var containsRequest = _cache.Keys.Any(r => r.Name == request.Name);

            var isUpdatable = true;

            if (containsRequest)
            {
                isUpdatable = ClearRequest(request);
            }

            return isUpdatable;
        }

        protected bool ClearRequest(MovieRequest request)
        {
            var cleared = false;

            if (request.Created + _cachedPeriod < DateTime.UtcNow)
            {
                cleared = _cache.Remove(request);
            }

            return cleared;
        }
    }
}