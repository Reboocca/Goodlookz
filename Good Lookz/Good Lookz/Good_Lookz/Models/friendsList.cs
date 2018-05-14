using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Good_Lookz.Models
{
    //Deze class is gemaakt om de vriendelijst te sorteren op gebruikersnaam
    //Code komt van: https://stackoverflow.com/questions/9803817/alphabetically-sort-a-generic-list-of-objects-using-a-specified-property
    public class friendsList : IComparer<FriendsCredentials>
    {
        private readonly bool _sortAscending;
        private readonly string _columnToSortOn;

        public friendsList(bool sortAscending, string columnToSortOn)
        {
            _sortAscending  = sortAscending;
            _columnToSortOn = columnToSortOn;
        }

        public int Compare(FriendsCredentials x, FriendsCredentials y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return ApplySortDirection(-1);
            if (y == null) return ApplySortDirection(1);

            switch (_columnToSortOn)
            {
                case "Username":
                    return ApplySortDirection(SortByUsername(x, y));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(
                        string.Format("Can't sort on column {0}",
                        _columnToSortOn));
            }
        }

        private int SortByUsername(FriendsCredentials x, FriendsCredentials y)
        {
            return x.username.CompareTo(y.username);
        }

       
        private int ApplySortDirection(int result)
        {
            return _sortAscending ? result : (result * -1);
        }
    }
}
