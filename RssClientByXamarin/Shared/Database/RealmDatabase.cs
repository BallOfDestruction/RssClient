﻿using System;
using System.Threading.Tasks;
using Realms;

namespace Shared.Database
{
	public class RealmDatabase
	{
		private const string DatabaseFilename = "librarydb.realm";

		public Realm MainThreadRealm { get; }

		public RealmDatabase()
		{
            var config = new RealmConfiguration(DatabaseFilename);
            try
            {
                MainThreadRealm = Realm.GetInstance(config);
            }
            catch (Realms.Exceptions.RealmMigrationNeededException)
            {
                Realm.DeleteRealm(config);
                MainThreadRealm = Realm.GetInstance(config);
            }
        }

        public static Realm OpenDatabase => Realm.GetInstance(DatabaseFilename);

        public void Dispose()
		{
			MainThreadRealm.Dispose();
		}

        public static Task DoInBackground(Action<Realm> action)
        {
            return Task.Run(() =>
            {
                using (var realm = OpenDatabase)
                {
                    using (var transaction = realm.BeginWrite())
                    {
                        action?.Invoke(realm);

                        transaction.Commit();
                    }
                }
            });
        }

        public static Task UpdateInBackground<TModel>(string id, Action<TModel, Realm> action)
        where TModel : RealmObject, IHaveId
        {
            return DoInBackground(realm =>
            {
                var currentItem = realm.Find<TModel>(id);

                action?.Invoke(currentItem, realm);
            });
        }

        public static async Task<string> InsertInBackground<TModel>(TModel model)
            where TModel : RealmObject, IHaveId
        {
            return await Task.Run(() =>
            {
                using (var realm = OpenDatabase)
                {
                    using (var transaction = realm.BeginWrite())
                    {
                        var item = realm.Add(model);

                        transaction.Commit();

                        return item.Id;
                    }
                }
            });
        }
    }
}