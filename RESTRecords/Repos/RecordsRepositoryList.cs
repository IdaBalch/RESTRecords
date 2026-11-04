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

        public IEnumerable<Records> GetAllRecords(
    string? title = null,
    string? artist = null,
    int? maxDuration = null,
    int? publicationYear = null)
        {
            IEnumerable<Records> result = _records.ToList();

            if (title != null)
            {
                result = result.Where(record =>
                    (record.Title ?? "").Contains(
                        title,
                        StringComparison.OrdinalIgnoreCase));
            }

            if (artist != null)
            {
                result = result.Where(record =>
                    (record.Artist ?? "").Contains(
                        artist,
                        StringComparison.OrdinalIgnoreCase));
            }

            if (maxDuration != null)
            {
                result = result.Where(record =>
                    record.Duration <= maxDuration);
            }

            if (publicationYear != null)
            {
                result = result.Where(record =>
                    record.PublicationYear == publicationYear);
            }

            return result;
        }

        public Records AddRecords(Records Record)
        {
            Record.Id = _nextId++;
            _records.Add(Record);
            return Record;
        }
    }
}
