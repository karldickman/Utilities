using System;
using NHibernate.Driver;

namespace Ngol.Utilities.NHibernate.Driver
{
    /// <summary>
    /// Wrapper around the SQLite library for consumption by NHibernate.
    /// </summary>
    public class MonoSqliteDriver : ReflectionBasedDriver
    {
        /// <inheritdoc />
        public override string NamedPrefix
        {
            get { return "@"; }
        }

        /// <inheritdoc />
        public override bool SupportsMultipleOpenReaders
        {
            get { return false; }
        }

        /// <inheritdoc />
        public override bool UseNamedPrefixInParameter
        {
            get { return true; }
        }

        /// <inheritdoc />
        public override bool UseNamedPrefixInSql
        {
            get { return true; }
        }

        /// <summary>
        /// Construct a new SQLite driver.
        /// </summary>
        public MonoSqliteDriver()
            : base("Mono.DatalSqlite", "Mono.Data.Sqlite.SqliteConnection", "Mono.Data.Sqlite.SqliteCommand")
        {
        }
    }
}

