using BasaProject.Models;
using BasaProject.Outputs;
using Microsoft.EntityFrameworkCore;

namespace BasaProject.Helpers
{
    public class WordlistHelper
    {
        // get by word lower case and trim
        public static WordlistResponse Find(string word, DataContext _db)
        {
            var res = _db.MsWordLists.Where(x => x.Word.ToLower().Trim() == word && x.IsDeleted == false)
                .Select(a => new WordlistResponse
                {
                    WordID = a.WordID,
                    Word = a.Word,
                    Type = a.Type,
                    Description = a.Desc,
                    Indonesian = a.Indonesian,
                    English = a.English
                })
                .FirstOrDefault();

            return res;
        }

        // search
        public static IList<WordlistResponse> Search(string word, DataContext _db)
        {
            var res = _db.MsWordLists.Where(x => x.Word.Contains(word) && x.IsDeleted == false)
                .Select(a => new WordlistResponse
                {
                    WordID = a.WordID,
                    Word = a.Word,
                    Type = a.Type,
                    Description = a.Desc,
                    Indonesian = a.Indonesian,
                    English = a.English
                })
                .ToList();

            return res;
        }
    }
}