﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notify.Dal.Models;
using Notify.Dal.Repositories;
using Notify.Enums;

namespace Notify.Dal.Mysql
{
	public class NotificationInstanceRepository : BaseRepository, INotificatorRepository
	{
		protected NotificationInstanceRepository(NotifyDbContext context) : base(context)
		{
		}

		protected NotificationInstanceRepository(string connectionString) : base(connectionString)
		{
		}

		public Task<NotificatorDal[]> GetAll()
		{
			return InContext(c => c.Notificators.ToArrayAsync());
		}

		public Task<NotificatorDal[]> GetList(string nameFilter)
		{
			return InContext(c => c.Notificators.Where(x => x.Name.Contains(nameFilter)).ToArrayAsync());
		}

		public Task<NotificatorDal[]> GetForType(NotificationTypeEnum typeId)
		{
			return InContext(c => c.Notificators.Where(x => x.TypeId == typeId).ToArrayAsync());
		}

		public Task<NotificatorDal> Get(int id)
		{
			return InContext(c => c.Notificators.FirstOrDefaultAsync(x => x.Id == id));
		}

		public Task<NotificatorDal> GetBySlug(string slug)
		{
			return InContext(c => c.Notificators.FirstOrDefaultAsync(x => x.Slug == slug));
		}

		public Task<NotificatorDal> GetByName(string name)
		{
			return InContext(c => c.Notificators.FirstOrDefaultAsync(x => x.Name == name));
		}
	}
}