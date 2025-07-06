namespace DutyManager.Data.Repositories
{
    internal class SQLiteConnection
    {
        private string dbPath;

        public SQLiteConnection(string dbPath)
        {
            this.dbPath = dbPath;
        }
    }
}