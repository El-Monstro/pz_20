using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PZ_18.Data;
using PZ_18.Models;
using System.Windows.Input;

namespace PZ_18.ViewModels
{

	public class MainViewModel
	{
		public ObservableCollection<User> Users { get; set; }
		public ObservableCollection<Request> Requests { get; set; }

		public ICommand SearchRequestsCommand { get; }

		public string SearchText { get; set; }

		public MainViewModel()
		{
			using (var context = new CoreContext())
			{
				Users = new ObservableCollection<User>(context.Users.Include(u => u.UserType).ToList());
				Requests = new ObservableCollection<Request>(
					context.Requests.Include(r => r.HomeTechType).ToList()
				);
			}

			SearchRequestsCommand = new RelayCommand(SearchRequests);
		}


		public void SearchRequests(object parameter)
		{
			using (var context = new CoreContext())
			{
				var query = context.Requests.Include(r => r.HomeTechType).AsQueryable();
				if (!string.IsNullOrEmpty(SearchText))
				{
					query = query.Where(r => r.ClientFIO.Contains(SearchText));
				}
				var list = query.ToList();
				Requests.Clear();
				foreach (var r in list)
					Requests.Add(r);
			}
		}
	}
}
