﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GSalvi.EventSourcing
{
    /// <summary>
    /// Defines a repository of snapshot
    /// representing by <see cref="T"/> type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISnapshotRepository<T> where T : Snapshot
    {
        /// <summary>
        /// Adds a <see cref="T"/> object to the database.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task AddAsync(T obj);

        /// <summary>
        /// Returns a <see cref="T"/> object by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(Guid id);

        /// <summary>
        /// Returns a collection <see cref="T"/> object by its aggregate id.
        /// </summary>
        /// <param name="aggregateId"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetByAggregateIdAsync(Guid aggregateId);

        /// <summary>
        /// Returns a collection <see cref="T"/> object by its event type.
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetByEventTypeAsync(string eventType);

        /// <summary>
        /// Provides a query generation functionality for <see cref="T"/> type.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();
    }
}