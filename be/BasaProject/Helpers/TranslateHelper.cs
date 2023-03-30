using BasaProject.Models;
using BasaProject.Outputs;
using Microsoft.EntityFrameworkCore;

namespace BasaProject.Helpers
{
    public class TranslateHelper
    {
        // TRANSLATE TO SUNDA LEMES
        public static TranslateResponse SundaLemes(string words, DataContext _db)
        {
            var listWord = words.Split(" ");
            var res = "";

            for (int i = 0; i < listWord.Count(); i++)
            {
                var item = listWord[i];

                var check = _db.MsWordLists.Where(a => (a.Word == item.ToLower().Trim() || a.Indonesian == item.ToLower().Trim() || a.English.ToLower() == item.ToLower().Trim()) && a.IsDeleted == false).ToList();

                if (check.Count == 0)
                {
                    var query = _db.MsBasaLemes.Where(x => x.SecondWord == item.ToLower().Trim() && x.IsDeleted == false)
                        .Include(a => a.Word)
                        .Select(a => new
                        {
                            Word = a.Word.Word,
                        })
                        .FirstOrDefault();

                    if (query == null) res += (i == 0) ? item : " " + item;
                    else res += (i == 0) ? query.Word : " " + query.Word;
                }
                else
                {
                    var getType = check.Where(a => a.Type == "lemes").FirstOrDefault();
                    if (getType != null)
                    {
                        res += (i == 0) ? getType.Word : " " + getType.Word;
                    }
                    else
                    {
                        var badWords = check.Select(a => a.Word).ToList();
                        var query = _db.MsBasaLemes.Where(x => badWords.Contains(x.SecondWord) && x.IsDeleted == false)
                            .Include(a => a.Word)
                            .Select(a => new
                            {
                                Word = a.Word.Word,
                            })
                            .FirstOrDefault();

                        res += (i == 0) ? query.Word : " " + query.Word;
                    }
                }
            }

            var translated = new TranslateResponse()
            {
                Translated = res
            };

            return translated;
        }
    }
}