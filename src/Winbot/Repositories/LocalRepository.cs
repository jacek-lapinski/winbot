using System;
using System.Linq;
using LiteDB;
using Winbot.Entities;
using Winbot.Settings;

namespace Winbot.Repositories
{
    internal class LocalRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly AppSettings _settings;

        public LocalRepository(AppSettings settings)
        {
            _settings = settings;
        }

        public TEntity[] GetAll()
        {
            using (var db = new LiteDatabase(_settings.DatabaseFilePath))
            {
                return db.GetCollection<TEntity>().FindAll().ToArray();
            }
        }

        public void Add(TEntity entity)
        {
            using (var db = new LiteDatabase(_settings.DatabaseFilePath))
            {
                db.GetCollection<TEntity>().Insert(entity);
            }
        }

        public void Update(TEntity entity)
        {
            using (var db = new LiteDatabase(_settings.DatabaseFilePath))
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
            using (var db = new LiteDatabase(_settings.DatabaseFilePath))
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
