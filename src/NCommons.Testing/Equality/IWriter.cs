namespace NCommons.Testing.Equality
{
    public interface IWriter
    {
        void Write(EqualityResult content);
        string GetFormattedResults();
    }
}