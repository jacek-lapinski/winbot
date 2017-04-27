using System;
using System.Linq;
using LiteDB;
using Winbot.Entities;
using Winbot.Settings;

namespace Winbot.Repositories
{
    internal class LocalRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly DatabaseSettings _settings;

        public LocalRepository(DatabaseSettings settings)
        {
            _settings = settings;
        }

        public TEntity[] GetAll()
        {
            using (var db = new LiteDatabase(_settings.DbFilePath))
            {
                return db.GetCollection<TEntity>().FindAll().ToArray();
            }
        }

        public void Add(TEntity entity)
        {
            using (var db = new LiteDatabase(_settings.DbFilePath))
            {
                db.GetCollection<TEntity>().Insert(entity);
            }
        }

        public void Update(TEntity entity)
        {
            using (var db = new LiteDatabase(_settings.DbFilePath))
            {
                var updateResult = db.GetCollection<TEntity>().Update(entity);
                if (!updateResult)
                {
                    throw new Exception("Entity not found");
                }
            }
        }

        public void Delete(TEntity entity)
        {
            using (var db = new LiteDatabase(_settings.DbFilePath))
            {
                var deleteResult = db.GetCollection<TEntity>().Delete(entity.Id);
                if (!deleteResult)
                {
                    throw new Exception("Entity not found");
                }
            }
        }
    }
}
