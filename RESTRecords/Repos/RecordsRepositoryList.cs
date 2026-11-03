using RESTRecords.Models;
using System.Numerics;

namespace RESTRecords.Repos
{
    public class RecordsRepositoryList
    {
        private int _nextId = 1;
        private List<Records> _records = new List<Records>();       
        

        public RecordsRepositoryList(bool includesTestData = false)
        {
            if (includesTestData)
            {

                AddRecords(new Records { Title = "Song 1", Artist = "Artist A", Duration = 210, PublicationYear = 2020 });
                AddRecords(new Records { Title = "Song 2", Artist = "Artist B", Duration = 180, PublicationYear = 2019 });
                AddRecords(new Records { Title = "Song 3", Artist = "Artist C", Duration = 240, PublicationYear = 2021 });
            }
        }

        public IEnumerable<Records> GetAllRecords()
        {
            return _records;
        }        

        public Records AddRecords(Records Record)
        {
            Record.Id = _nextId++;
            _records.Add(Record);
            return Record;
        }
    }
}
