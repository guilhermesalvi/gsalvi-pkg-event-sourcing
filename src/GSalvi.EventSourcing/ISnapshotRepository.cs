using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GSalvi.EventSourcing
{
    /// <summary>
    /// Defines a repository of <see cref="Snapshot"/>.
    /// </summary>
    public interface ISnapshotRepository : ISnapshotRepository<Snapshot>
    {
    }

    /// <summary>
    /// Defines a repository of <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISnapshotRepository<T> where T : Snapshot
    {
        /// <summary>
        /// Adds a <typeparamref name="T"/> to the database.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task AddAsync(T obj);

        /// <summary>
        /// Returns a <typeparamref name="T"/> by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(Guid id);

        /// <summary>
        /// Returns a collection of <typeparamref name="T"/> by its aggregate id.
        /// </summary>
        /// <param name="aggregateId"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetByAggregateIdAsync(Guid aggregateId);

        /// <summary>
        /// Returns a collection of <typeparamref name="T"/> by its event type.
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetByEventTypeAsync(string eventType);

        /// <summary>
        /// Provides a query generation functionality for <typeparamref name="T"/>.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();
    }
}