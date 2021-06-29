﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSalvi.EventSourcing
{
    /// <summary>
    /// Defines a manager of <see cref="T"/> objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEventStoreManager<T> where T : Snapshot
    {
        /// <summary>
        /// Stores a new event of <see cref="T"/> type.
        /// </summary>
        /// <param name="event"></param>
        /// <param name="aggregateId"></param>
        /// <typeparam name="TR"></typeparam>
        /// <returns></returns>
        Task StoreAsync<TR>(TR @event, Guid aggregateId) where TR : class;
        
        /// <summary>
        /// Returns a <see cref="T"/> by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(Guid id);
        
        /// <summary>
        /// Returns a collection <see cref="T"/> by its aggregate id.
        /// </summary>
        /// <param name="aggregateId"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetByAggregateIdAsync(Guid aggregateId);
        
        /// <summary>
        /// Returns a collection <see cref="T"/> by its event type.
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetByEventTypeAsync(string eventType);
    }
}