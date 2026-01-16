namespace ERP_BACKEND.interfaces;


    public interface IPasswordHasher
    {
        public const int DefaultIterations = 100_000;
        string Hash(string password, int iterations = DefaultIterations);

        bool LooksLikeNew(string s);

        bool Verify(string password, string hash);
    }

