using System.Threading.Tasks;

namespace SpellChecker
{
    public interface IBingSpellCheckService
    {
        Task<SpellCheckResult> SpellCheckTextAsync(string text);
    }
}

