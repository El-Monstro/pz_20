using System;
using System.Collections.Generic;
using System.Linq;
using PZ_18.Data;
using PZ_18.Models;
using Microsoft.EntityFrameworkCore;

public class RequestRepository
{
    private readonly CoreContext _context;

    // инициализации контекста базы данных.
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

    // Метод для поиска заявок по фильтрам: ФИО клиента, статусу и типу техники.
    public List<Request> SearchRequests(string clientFIO = null, string status = null, int? techTypeId = null)
    {
        var query = _context.Requests.AsQueryable();  // Начинаем с общего запроса ко всем заявкам.

        if (!string.IsNullOrEmpty(clientFIO))
            query = query.Where(r => r.ClientFIO.Contains(clientFIO));

        // Добавляем фильтрацию по статусу заявки, если он указан.
        if (!string.IsNullOrEmpty(status))
            query = query.Where(r => r.RequestStatus == status);


        if (techTypeId.HasValue)
            query = query.Where(r => r.TechTypeID == techTypeId);

  
        return query.Include(r => r.HomeTechType)
                    .Include(r => r.Master)
                    .ToList();
    }



}
