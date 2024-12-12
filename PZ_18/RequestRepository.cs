using System;
using System.Collections.Generic;
using System.Linq;
using PZ_18.Data;
using PZ_18.Models;
using Microsoft.EntityFrameworkCore;

public class RequestRepository
{
	private readonly CoreContext _context;

	public RequestRepository(CoreContext context)
	{
		_context = context;
	}

	public Request FindByNumber(int requestId)
	{
		return _context.Requests.Include(r => r.HomeTechType)
								.Include(r => r.Master)
								.FirstOrDefault(r => r.RequestID == requestId);
	}

	public List<Request> SearchRequests(string clientFIO = null, string status = null, int? techTypeId = null)
	{
		var query = _context.Requests.AsQueryable();

		if (!string.IsNullOrEmpty(clientFIO))
			query = query.Where(r => r.ClientFIO.Contains(clientFIO));

		if (!string.IsNullOrEmpty(status))
			query = query.Where(r => r.RequestStatus == status);

		if (techTypeId.HasValue)
			query = query.Where(r => r.TechTypeID == techTypeId);

		return query.Include(r => r.HomeTechType)
					.Include(r => r.Master)
					.ToList();
	}

	public int GetCompletedRequestsCount()
	{
		return _context.Requests.Count(r => r.RequestStatus == "Готова к выдаче");
	}

	public double GetAverageCompletionTime()
	{
		var completed = _context.Requests.Where(r => r.CompletionDate.HasValue && r.StartDate < r.CompletionDate.Value).ToList();
		if (completed.Count == 0) return 0;
		var avgTicks = completed.Average(r => (r.CompletionDate.Value - r.StartDate).Ticks);
		return new TimeSpan((long)avgTicks).TotalDays;
	}
}
