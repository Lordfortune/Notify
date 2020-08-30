using System;
using System.Threading.Tasks;

namespace Notify.Dal.Mysql
{
	public class BaseRepository
	{
		protected BaseRepository(NotifyDbContext context)
		{
			_context = context;
		}

		protected BaseRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

		private readonly NotifyDbContext _context;
		private readonly string _connectionString;

		private NotifyDbContext Context => _context ?? new NotifyDbContext(_connectionString);

		protected async Task InContext(Func<NotifyDbContext, Task> func)
		{
			if (_context is null)
			{
				using var context = Context;
				await func(context);
				return;
			}

			await func(_context);
		}

		protected async Task<T> InContext<T>(Func<NotifyDbContext, Task<T>> func)
		{
			if (_context is null)
			{
				using var context = Context;
				return await func(context);
			}

			return await func(_context);
		}
	}
}