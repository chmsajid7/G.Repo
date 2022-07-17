using Domain.Seedwork;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contexts
{
    public abstract class MongoDbContext<TEntity> : IMongoDbContext<TEntity> where TEntity : class
    {
        public IClientSessionHandle? session { get; set; }
        private IMongoDatabase? Database { get; set; }
        public MongoClient? mongoClient { get; set; }
        protected IMongoCollection<TEntity>? DbSet;
        private readonly List<Func<Task>> commands;

        public MongoDbContext()
        {
            // Every command will be stored and it'll be processed at SaveChanges
            this.commands = new List<Func<Task>>();
        }

        public virtual void Add(TEntity obj)
        {
            this.commands.Add(() => DbSet.InsertOneAsync(obj));
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("_id", id));
            return data.SingleOrDefault();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            var all = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);
            return all.ToList();
        }

        public virtual void Update(TEntity obj)
        {
            this.commands.Add(() => DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", obj.GetId()), obj));
        }

        public virtual void Remove(Guid id)
        {
            this.commands.Add(() => DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id)));
        }

        public void Dispose()
        {
            this.session?.Dispose();
            GC.SuppressFinalize(this);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            ConfigureMongo();

            return Database.GetCollection<T>(name);
        }
    }
}
