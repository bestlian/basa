using BasaProject.Models;
using BasaProject.Outputs;
using Microsoft.EntityFrameworkCore;

namespace BasaProject.Helpers
{
    public class WordlistHelper
    {
        // GET BY WORD
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

        // SEARCH BY KEYWORD
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

        // PAIRING BASA LEMES
        public static Message PairingBasaLemes(PairingRequest f, DataContext _db)
        {
            var wTrim = f.BadWord.ToLower().Trim();
            var check = _db.MsBasaLemes.FirstOrDefault(a => a.SecondWord == f.BadWord.ToLower().Trim() && a.IsDeleted == false);
            if (check != null) return new Message() { Statuscode = 400, Msg = "Word is already exist!" };

            try
            {
                var basaLemes = new MsBasaLemes()
                {
                    FirstWord = f.WordID,
                    SecondWord = wTrim
                };

                return new Message() { Statuscode = 200, Msg = "Word has been save" };
            }
            catch
            {
                return new Message() { Statuscode = 500, Msg = "Unexpected error occured!" };
            }
        }

        // GET LIST WORd BY TYPE
        public static IList<WordlistResponse> GetByType(string type, int page, int size, DataContext _db)
        {
            var res = _db.MsWordLists.Where(x => x.Type == type && x.IsDeleted == false)
                .Skip((page - 1) * size)
                .Select(a => new WordlistResponse
                {
                    WordID = a.WordID,
                    Word = a.Word,
                    Type = a.Type,
                    Description = a.Desc,
                    Indonesian = a.Indonesian,
                    English = a.English
                })
                .Take(size)
                .ToList();

            return res;
        }
    }
}